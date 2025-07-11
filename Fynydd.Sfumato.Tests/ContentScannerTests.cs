namespace Fynydd.Sfumato.Tests;

public class ContentScannerTests(ITestOutputHelper testOutputHelper)
{
    #region Constants

    private static string Markup => """
                                    <!DOCTYPE html>
                                    <html lang="en" class="@@container bg-[url(/media/ze0liffq/alien-world.jpg?width=1920&quality=90)] [content:'arbitrary_test'] font-sans">
                                    <head>
                                        <meta charset="UTF-8">
                                        <title>Sample Website</title>
                                        <meta name="viewport" content="width=device-width, initial-scale=1">
                                        <link rel="stylesheet" href="css/sfumato.css">
                                    </head>
                                    <body class="@container content-['test'] content-[''] phab:hover:text-xs theme-midnight:text-lime-950 @(true ? "xl:text-base/[3rem]" : "dark:text-base/5") [-webkit-backdrop-filter:blur(1rem)]">
                                        <div id="test-home" class="@max-md:flex-col text-[1rem] lg:text-[1.25rem] xl:text-(length:--my-text-size) bg-fuchsia-500 dark:sm:bg-fuchsia-300 dark:text-[length:1rem] xl:text-[#112233] xl:text-[red] xl:text-[--my-color-var] xl:text-[var(--my-color-var)]">
                                            <p class="[font-weight:900] sm:[font-weight:900]">Placeholder</p>
                                            <p class="[fontweight:400] sm:[fontweight:300] xl:text[#112233] xl:text-slate[#112233] xl:text-slate-50[#112233] xxl:text-slate-50-[#112233]">Invalid Classes</p>
                                        </div>
                                        <div class="[color:#eee;font-weight:500;] content-['Hello!'] [--margin-val6:_1.25rem]! dark:sm:supports-backdrop-blur:motion-safe:block invisible lg:max-xl:top-8 break-after-auto container aspect-screen xxl:aspect-[8/4] xl:aspect-8/4"></div>
                                        <div class="-top-px *:whitespace-pre!"></div>
                                        <div class="top-1/2 antialiased"></div>
                                        <script>
                                            function test() {
                                              let el = document.getElementById('test-element');
                                              if (el) {
                                                    el.classList.add($`
                                                        bg-emerald-900
                                                        [font-weight:700]
                                                        md:[font-weight:700]
                                                    `);
                                                    el.classList.add(`bg-emerald-800`);
                                                    el.classList.add(`[font-weight:600]`);
                                                    el.classList.add(`lg:[font-weight:600]`);
                                              }
                                            }
                                        </script>
                                        @{
                                            var test1 = $""
                                                block bg-slate-400
                                            "";
                                            var array = [ "text-9xl",@"content-[\"test2\"]" ];
                                            
                                            var detailsMask = $"<span class=\"line-clamp-1 -mt-1! text-slate-500 dark:text-dark-foreground-dim line-clamp-2\"><span class=""line-clamp-3"">{description}</span></span>";
                                            
                                            return $@"
                                            <div><div class=""text-[0.65rem] mb-1.5"">{category}</div><div>{GetHeading(content)}</div></div>
                                            ";
                                        }
                                        <p>top-999</p>
                                    </body>
                                    </html>
                                    """;

    private static string Css => """
                                 :root {
                                     --my-prop-var: 1.25rem;
                                     --my-prop-var2: #112233;
                                     --my-prop-var3: var(--my-prop-var4);
                                     --my-prop-var5: 1.25rem;--my-prop-var6: #112233;--my-prop-var7: var(--my-prop-var8);
                                 }

                                 .test-class {
                                     font-weight: var(--font-weight);
                                     font-size: var( --font-size );
                                     color: var (
                                         --font-color
                                         );
                                     background-color: --alpha(var(--color-lime-500) / 15%);
                                     top: --spacing(4);
                                 }
                                 """;
    #endregion

    [Fact]
    public void FileContentParsing()
    {
        var appRunner = new AppRunner(new AppState());
        var utilityClasses = ContentScanner.ScanFileForUtilityClasses(Markup, appRunner, true);

        testOutputHelper.WriteLine("FileContentParsing() => Found:");
        testOutputHelper.WriteLine("");

        foreach (var kvp in utilityClasses)
            testOutputHelper.WriteLine($"{kvp.Value.Selector}");
        
        Assert.Equal(54, utilityClasses.Count);
    }

    [Fact]
    public async Task CssCustomPropertyScanner()
    {
        var appRunner = new AppRunner(new AppState());
        var props = new List<string>();
        var segment = new GenerationSegment
        {
            Content = new StringBuilder(Css)
        };

        appRunner.GatherSegmentCssCustomPropertyRefs(segment);
        
        foreach (var kvp in segment.UsedCssCustomProperties)
        {
            testOutputHelper.WriteLine(kvp.Key);
            props.Add(kvp.Key);
        }
        
        Assert.Single(props);

        var dict = new Dictionary<string, string>();
        
        foreach (var kvp in segment.UsedCssCustomProperties)
        {
            testOutputHelper.WriteLine(kvp.Key + " : " + kvp.Value);
            dict.Add(kvp.Key, kvp.Value);
        }
        
        Assert.Single(dict);

        props.Clear();

        Assert.True(segment.Content.Contains("--alpha(var(--color-lime-500) / 15%)"));
        Assert.True(segment.Content.Contains("--spacing(4)"));

        await appRunner.ProcessSegmentFunctionsAsync(segment);

        Assert.True(segment.Content.Contains("oklch(0.768 0.233 130.85 / 0.15)"));
        Assert.True(segment.Content.Contains("calc(var(--spacing) * 4)"));
    }
}
