namespace Fynydd.Sfumato;

public sealed class SfumatoRunner
{
	#region Properties
	
	public SfumatoAppState AppState { get; } = new();

	private static int IndentationSpaces => 4;

	public static int MaxConsoleWidth => GetMaxConsoleWidth();
	
	private static int GetMaxConsoleWidth()
	{
		try
		{
			return Console.WindowWidth - 1;
		}
		catch
		{
			return 78;
		}
	}
	
	#endregion

	public SfumatoRunner()
	{
#if DEBUG
		AppState.DiagnosticMode = true;
#endif		
	}

	#region Entry Points
	
	/// <summary>
	/// Loads settings and app state.
	/// </summary>
	public async Task InitializeAsync(IEnumerable<string>? args = null)
	{
		await AppState.InitializeAsync(args ?? []);
	}
	
	/// <summary>
	/// Build CSS based on watched SCSS files.
	/// </summary>
	/// <param name="timer"></param>
	/// <param name="onlyFilesUsingBaseAndUtilities"></param>
	public async Task PerformCoreBuildAsync(Stopwatch timer, bool onlyFilesUsingBaseAndUtilities = false)
	{
		var fileBytes = new ConcurrentBag<decimal>();
		var tasks = new List<Task>();

		// ReSharper disable once LoopCanBeConvertedToQuery
		foreach (var watchedFile in AppState.WatchedScssFiles.Values)
		{
			var matches = AppState.SfumatoScssRegex.Matches(watchedFile.Scss);

			if (onlyFilesUsingBaseAndUtilities == false || (onlyFilesUsingBaseAndUtilities && matches.Any(m => m.Value.Contains("base") || m.Value.Contains("utilities"))))
			{
				tasks.Add(TranspileAsync(watchedFile, fileBytes));
			}
		}

		await Task.WhenAll(tasks);

		await Console.Out.WriteLineAsync($"Completed build of {fileBytes.Count:N0} CSS file{(fileBytes.Count != 1 ? "s" : string.Empty)} ({fileBytes.Sum().FormatBytes()}) in {timer.FormatTimer()}");
	}

	private async Task TranspileAsync(WatchedScssFile watchedFile, ConcurrentBag<decimal> fileBytes)
	{
        var css = await SfumatoScss.TranspileScssAsync(watchedFile.FilePath, watchedFile.Scss, this);
        
		fileBytes.Add(css.Length);
	}
	
	#endregion
	
	#region Generation Methods

    private static ScssMediaQuery? EnsureMediaQueryPathExists(SfumatoAppState appState, string fullMediaQueryVariantPath, ScssMediaQuery? rootNode, int index = 0, ScssMediaQuery? currentNode = null)
    {
        if (fullMediaQueryVariantPath == string.Empty)
            return rootNode;

        if (fullMediaQueryVariantPath.EndsWith(':') == false)
            fullMediaQueryVariantPath += ':';
        
        if (index == 0 || currentNode is null)
            currentNode = rootNode;
        
        var variantSegments = fullMediaQueryVariantPath.Split(':', StringSplitOptions.RemoveEmptyEntries);

        if (variantSegments.Length == 0 || index > variantSegments.Length - 1)
            return rootNode;
        
        //var variant = variantSegments[index];
        var partialMediaQueryVariantPath = string.Join(':', variantSegments[..(index + 1)]) + ':';
        var existingNode = currentNode?.MediaQueries.FirstOrDefault(n => n.PrefixPath == partialMediaQueryVariantPath);

        if (existingNode is null)
        {
            existingNode = new ScssMediaQuery(appState, partialMediaQueryVariantPath);
            currentNode?.MediaQueries.Add(existingNode);
        }

        if (variantSegments.Length <= index + 1)
            return existingNode;

        index++;

        return EnsureMediaQueryPathExists(appState, fullMediaQueryVariantPath, rootNode, index, existingNode);
    }

    public string GenerateUtilityScss()
    {
        var scss = AppState.StringBuilderPool.Get();
        var globalSelector = AppState.StringBuilderPool.Get();
        var scssRootNode = new ScssMediaQuery(AppState, string.Empty, -1);

        try
        {
            #region Generate SCSS Tree

            var usedCssSelectors = AppState.UsedClasses
                .OrderBy(c => c.Value.Depth)
                .ThenBy(c => c.Value.VariantSortOrder)
                .ThenBy(c => c.Value.SelectorSort)
                .ThenBy(c => c.Value.FixedSelector)
                .ToList();

            foreach (var (_, usedCssSelector) in usedCssSelectors)
            {
                if (usedCssSelector.IsInvalid)
                    continue;

                if (string.IsNullOrEmpty(usedCssSelector.ScssMarkup))
                    usedCssSelector.GetStyles();

                if (usedCssSelector is { IsArbitraryCss: false, ScssMarkup: "" })
                {
                    usedCssSelector.IsInvalid = true;
                    continue;
                }

                var compactScss = usedCssSelector.ScssMarkup.CompactCss();
                var parentNode = EnsureMediaQueryPathExists(AppState,
                    string.Join(':', usedCssSelector.MediaQueryVariants), scssRootNode);
                var existingClass = parentNode?.ScssClasses.FirstOrDefault(c =>
                    c.CompactScssProperties == compactScss && c.PseudoclassSuffix == usedCssSelector.PseudoclassPath);

                if (existingClass is null)
                {
                    parentNode?.ScssClasses.Add(new ScssClass
                    {
                        Depth = parentNode.Depth + 1,
                        Selectors = { $".{usedCssSelector.EscapedSelector}" },
                        PseudoclassSuffix = usedCssSelector.PseudoclassPath,
                        CompactScssProperties = compactScss,
                        ScssProperties = usedCssSelector.ScssMarkup
                    });

                    continue;
                }

                existingClass.Selectors.Add($".{usedCssSelector.EscapedSelector}");
            }

            #endregion

            #region Process global class assignments

            foreach (var (_, usedCssSelector) in AppState.UsedClasses
                         .OrderBy(c => c.Value.Depth)
                         .ThenBy(c => c.Value.VariantSortOrder)
                         .ThenBy(c => c.Value.SelectorSort)
                         .ThenBy(c => c.Value.FixedSelector)
                         .ToList())
            {
                if (usedCssSelector.ScssUtilityClassGroup?.Category == "gradients")
                    globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) +
                                          $".{usedCssSelector.EscapedSelector}");
            }

            if (globalSelector.Length > 0)
            {
                scss.Append(globalSelector + " {" + Environment.NewLine);
                scss.Append(
                    $"{Indent(1)}--sf-gradient-from-position: ; --sf-gradient-via-position: ; --sf-gradient-to-position: ;" +
                    Environment.NewLine);
                scss.Append("}" + Environment.NewLine);
            }

            globalSelector.Clear();

            foreach (var (_, usedCssSelector) in AppState.UsedClasses
                         .OrderBy(c => c.Value.Depth)
                         .ThenBy(c => c.Value.VariantSortOrder)
                         .ThenBy(c => c.Value.SelectorSort)
                         .ThenBy(c => c.Value.FixedSelector)
                         .ToList())
            {
                if (usedCssSelector.ScssUtilityClassGroup?.Category == "ring")
                    globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) +
                                          $".{usedCssSelector.EscapedSelector}");
            }

            if (globalSelector.Length > 0)
            {
                scss.Append(globalSelector + " {" + Environment.NewLine);
                scss.Append(
                    $"{Indent(1)}--sf-ring-inset: ; --sf-ring-offset-width: 0px; --sf-ring-offset-color: #fff; --sf-ring-color: #3b82f680; --sf-ring-offset-shadow: 0 0 #0000; --sf-ring-shadow: 0 0 #0000; --sf-shadow: 0 0 #0000; --sf-shadow-colored: 0 0 #0000;" +
                    Environment.NewLine);
                scss.Append("}" + Environment.NewLine);
            }

            globalSelector.Clear();

            foreach (var (_, usedCssSelector) in AppState.UsedClasses
                         .OrderBy(c => c.Value.Depth)
                         .ThenBy(c => c.Value.VariantSortOrder)
                         .ThenBy(c => c.Value.SelectorSort)
                         .ThenBy(c => c.Value.FixedSelector)
                         .ToList())
            {
                if (usedCssSelector.ScssUtilityClassGroup?.Category == "shadow")
                    globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) +
                                          $".{usedCssSelector.EscapedSelector}");
            }

            if (globalSelector.Length > 0)
            {
                scss.Append(globalSelector + " {" + Environment.NewLine);
                scss.Append(
                    $"{Indent(1)}--sf-ring-offset-shadow: 0 0 #0000; --sf-ring-shadow: 0 0 #0000; --sf-shadow: 0 0 #0000; --sf-shadow-colored: 0 0 #0000;" +
                    Environment.NewLine);
                scss.Append("}" + Environment.NewLine);
            }

            globalSelector.Clear();

            foreach (var (_, usedCssSelector) in AppState.UsedClasses
                         .OrderBy(c => c.Value.Depth)
                         .ThenBy(c => c.Value.VariantSortOrder)
                         .ThenBy(c => c.Value.SelectorSort)
                         .ThenBy(c => c.Value.FixedSelector)
                         .ToList())
            {
                if (usedCssSelector.ScssUtilityClassGroup?.Category == "backdrop")
                    globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) +
                                          $".{usedCssSelector.EscapedSelector}");
            }

            if (globalSelector.Length > 0)
            {
                scss.Append(globalSelector + " {" + Environment.NewLine);
                scss.Append(
                    $"{Indent(1)}--sf-backdrop-blur: ; --sf-backdrop-brightness: ; --sf-backdrop-contrast: ; --sf-backdrop-grayscale: ; --sf-backdrop-hue-rotate: ; --sf-backdrop-invert: ; --sf-backdrop-opacity: ; --sf-backdrop-saturate: ; --sf-backdrop-sepia: ;" +
                    Environment.NewLine +
                    $"{Indent(1)}backdrop-filter: var(--sf-backdrop-blur) var(--sf-backdrop-brightness) var(--sf-backdrop-contrast) var(--sf-backdrop-grayscale) var(--sf-backdrop-hue-rotate) var(--sf-backdrop-invert) var(--sf-backdrop-opacity) var(--sf-backdrop-saturate) var(--sf-backdrop-sepia);" +
                    Environment.NewLine +
                    $"{Indent(1)}-webkit-backdrop-filter: var(--sf-backdrop-blur) var(--sf-backdrop-brightness) var(--sf-backdrop-contrast) var(--sf-backdrop-grayscale) var(--sf-backdrop-hue-rotate) var(--sf-backdrop-invert) var(--sf-backdrop-opacity) var(--sf-backdrop-saturate) var(--sf-backdrop-sepia);" +
                    Environment.NewLine);
                scss.Append("}" + Environment.NewLine);
            }

            globalSelector.Clear();

            foreach (var (_, usedCssSelector) in AppState.UsedClasses
                         .OrderBy(c => c.Value.Depth)
                         .ThenBy(c => c.Value.VariantSortOrder)
                         .ThenBy(c => c.Value.SelectorSort)
                         .ThenBy(c => c.Value.FixedSelector)
                         .ToList())
            {
                if (usedCssSelector.ScssUtilityClassGroup?.Category == "filter")
                    globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) +
                                          $".{usedCssSelector.EscapedSelector}");
            }

            if (globalSelector.Length > 0)
            {
                scss.Append(globalSelector + " {" + Environment.NewLine);
                scss.Append(
                    $"{Indent(1)}--sf-blur: ; --sf-brightness: ; --sf-contrast: ; --sf-drop-shadow: ; --sf-grayscale: ; --sf-hue-rotate: ; --sf-invert: ; --sf-saturate: ; --sf-sepia: ;" +
                    Environment.NewLine +
                    $"{Indent(1)}filter: var(--sf-blur) var(--sf-brightness) var(--sf-contrast) var(--sf-drop-shadow) var(--sf-grayscale) var(--sf-hue-rotate) var(--sf-invert) var(--sf-saturate) var(--sf-sepia);" +
                    Environment.NewLine);
                scss.Append("}" + Environment.NewLine);
            }

            globalSelector.Clear();

            foreach (var (_, usedCssSelector) in AppState.UsedClasses
                         .OrderBy(c => c.Value.Depth)
                         .ThenBy(c => c.Value.VariantSortOrder)
                         .ThenBy(c => c.Value.SelectorSort)
                         .ThenBy(c => c.Value.FixedSelector)
                         .ToList())
            {
                if (usedCssSelector.ScssUtilityClassGroup?.Category == "transform")
                    globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) +
                                          $".{usedCssSelector.EscapedSelector}");
            }

            if (globalSelector.Length > 0)
            {
                scss.Append(globalSelector + " {" + Environment.NewLine);
                scss.Append(
                    $"{Indent(1)}--sf-translate-x: 0; --sf-translate-y: 0; --sf-rotate: 0; --sf-skew-x: 0; --sf-skew-y: 0; --sf-scale-x: 1; --sf-scale-y: 1;" +
                    Environment.NewLine +
                    $"{Indent(1)}transform: translate(var(--sf-translate-x), var(--sf-translate-y)) rotate(var(--sf-rotate)) skewX(var(--sf-skew-x)) skewY(var(--sf-skew-y)) scaleX(var(--sf-scale-x)) scaleY(var(--sf-scale-y));" +
                    Environment.NewLine);
                scss.Append("}" + Environment.NewLine);
            }

            #endregion

            scss.Append(scssRootNode.GetScssMarkup());

            return scss.ToString();
        }

        catch
        {
            return string.Empty;
        }
        
        finally
        {
            AppState.StringBuilderPool.Return(scss);
            AppState.StringBuilderPool.Return(globalSelector);
        }
    }

    #endregion
	
	#region SCSS Watcher Methods
	
	/// <summary>
	/// Remove a watched SCSS file from the watched SCSS files collection.
	/// Remove any associated CSS file from storage.
	/// </summary>
	/// <param name="filePath"></param>
	public async Task DeleteWatchedScssFileAsync(string filePath)
	{
        _ = AppState.WatchedScssFiles.TryRemove(filePath, out _);

        var cssFilePath =
			filePath.TrimEnd(".scss", StringComparison.OrdinalIgnoreCase) +
			".css"; 
							
		if (File.Exists(cssFilePath))
			File.Delete(cssFilePath);

		if (File.Exists(cssFilePath + ".map"))
			File.Delete(cssFilePath + ".map");
		
		await Task.CompletedTask;
	}

	/// <summary>
	/// Add/update a watched SCSS file in the watched SCSS files collection.
	/// Transpile to CSS file on storage and in collection.
	/// </summary>
	/// <param name="filePath"></param>
	/// <param name="cancellationTokenSource"></param>
	public async Task AddUpdateWatchedScssFileAsync(string filePath, CancellationTokenSource cancellationTokenSource)
	{
		await AppState.ExamineWatchedFilesForUsedClassesAsync();

        if (File.Exists(filePath) == false)
        {
            if (AppState.WatchedScssFiles.TryGetValue(filePath, out _))
                AppState.WatchedScssFiles.TryRemove(filePath, out _);
                    
            return;
        }

        var scss = await Storage.ReadAllTextWithRetriesAsync(filePath, SfumatoAppState.FileAccessRetryMs, cancellationTokenSource.Token);
		var css = await SfumatoScss.TranspileScssAsync(filePath, scss, this);

		if (AppState.WatchedScssFiles.TryGetValue(filePath, out var watchedScssFile))
		{
			watchedScssFile.Css = css;
			watchedScssFile.Scss = scss;
		}

		else
		{
			AppState.WatchedScssFiles.TryAdd(filePath, new WatchedScssFile
			{
				FilePath = filePath,
				Css = css,
				Scss = scss
			});
		}
	}
	
	#endregion
	
	#region Watcher Methods
	
	/// <summary>
	/// Remove a watched file from the watched files collection.
	/// </summary>
	/// <param name="filePath"></param>
	public async Task DeleteWatchedFileAsync(string filePath)
	{
		if (filePath.Equals(AppState.SettingsFilePath, StringComparison.OrdinalIgnoreCase))
			return;

		_ = AppState.WatchedFiles.TryRemove(filePath, out _);
								
		await Task.CompletedTask;
	}

	/// <summary>
	/// Add/update a watched file in the watched files collection.
	/// </summary>
	/// <param name="filePath"></param>
	/// <param name="cancellationTokenSource"></param>
	public async Task AddUpdateWatchedFileAsync(string filePath, CancellationTokenSource cancellationTokenSource)
	{
		if (filePath.Equals(AppState.SettingsFilePath, StringComparison.OrdinalIgnoreCase))
			return;

        if (File.Exists(filePath) == false)
        {
            if (AppState.WatchedFiles.TryGetValue(filePath, out _))
                AppState.WatchedFiles.TryRemove(filePath, out _);
                    
            return;
        }
        
        var markup = await Storage.ReadAllTextWithRetriesAsync(filePath, SfumatoAppState.FileAccessRetryMs, cancellationTokenSource.Token);

		if (AppState.WatchedFiles.TryGetValue(filePath, out var watchedFile))
		{
			watchedFile.Markup = markup;

			await AppState.ProcessFileMatchesAsync(watchedFile);        
		}

		else
		{
			var newWatchedFile = new WatchedFile
			{
				FilePath = filePath,
				Markup = markup
			};

			if (AppState.WatchedFiles.TryAdd(filePath, newWatchedFile))
				await AppState.ProcessFileMatchesAsync(newWatchedFile);
		}
	}
	
	#endregion
	
	#region Helper Methods

	/// <summary>
	/// Trim the working path from the start of a file path, and prefix with "./" to make it relative.
	/// </summary>
	/// <param name="path"></param>
	/// <param name="appState"></param>
	/// <returns></returns>
	public static string ShortenPathForOutput(string path, SfumatoAppState appState)
	{
		return $".{Path.DirectorySeparatorChar}{path.TrimStart(appState.WorkingPath, StringComparison.OrdinalIgnoreCase)?.TrimStart(Path.DirectorySeparatorChar)}";
	}
	
	/// <summary>
	/// Create space indentation based on level of depth.
	/// </summary>
	/// <param name="level"></param>
	/// <returns></returns>
	private static string Indent(int level)
	{
		return new string(' ', level * IndentationSpaces);
	}

	/// <summary>
	/// Determine if a prefix is a media query prefix.
	/// </summary>
	/// <param name="prefix"></param>
	/// <param name="appState"></param>
	/// <returns></returns>
	public static bool IsMediaQueryPrefix(string prefix, SfumatoAppState? appState)
	{
		var mediaQueryPrefix = appState?.MediaQueryPrefixes.FirstOrDefault(p => p.Prefix.Equals(prefix, StringComparison.Ordinal));
		return string.IsNullOrEmpty(mediaQueryPrefix?.Prefix) == false;
	}

	/// <summary>
	/// Determine if a prefix is a pseudo class prefix.
	/// </summary>
	/// <param name="prefix"></param>
	/// <param name="appState"></param>
	/// <returns></returns>
	public static bool IsPseudoClassPrefix(string prefix, SfumatoAppState? appState)
	{
		var pseudoClassPrefix = appState?.PseudoclassPrefixes.FirstOrDefault(p => p.Key.Equals(prefix, StringComparison.Ordinal));
		
		return string.IsNullOrEmpty(pseudoClassPrefix?.Key) == false;
	}
	
	#endregion
}
