// ReSharper disable RawStringCanBeSimplified

namespace Fynydd.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class BorderRadius : ClassDictionaryBase
{
    public BorderRadius()
    {
        Group = "border-radius";
        Description = "Utilities for setting border radius for rounded corners.";
    }

    public static readonly Dictionary<string, string> Borders = new ()
    {
        {
            "rounded",
            """
            border-radius: {0};
            """
        },
        {
            "rounded-s",
            """
            border-start-start-radius: {0};
            border-end-start-radius: {0};
            """
        },
        {
            "rounded-e",
            """
            border-start-end-radius: {0};
            border-end-end-radius: {0};
            """
        },
        {
            "rounded-t",
            """
            border-top-left-radius: {0};
            border-top-right-radius: {0};
            """
        },
        {
            "rounded-r",
            """
            border-top-right-radius: {0};
            border-bottom-right-radius: {0};
            """
        },
        {
            "rounded-b",
            """
            border-bottom-right-radius: {0};
            border-bottom-left-radius: {0};
            """
        },
        {
            "rounded-l",
            """
            border-top-left-radius: {0};
            border-bottom-left-radius: {0};
            """
        },
        {
            "rounded-ss",
            """
            border-start-start-radius: {0};
            """
        },
        {
            "rounded-se",
            """
            border-start-end-radius: {0};
            """
        },
        {
            "rounded-ee",
            """
            border-end-end-radius: {0};
            """
        },
        {
            "rounded-es",
            """
            border-end-start-radius: {0};
            """
        },
        {
            "rounded-tl",
            """
            border-top-left-radius: {0};
            """
        },
        {
            "rounded-tr",
            """
            border-top-right-radius: {0};
            """
        },
        {
            "rounded-br",
            """
            border-bottom-right-radius: {0};
            """
        },
        {
            "rounded-bl",
            """
            border-bottom-left-radius: {0};
            """
        },
    };

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var border in Borders)
        {
            foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--radius-")))
            {
                var key = item.Key.Replace("radius", border.Key).Trim('-');
                var value = new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = border.Value.Replace("{0}", $"var({item.Key})"),
                };

                if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                    appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
                else
                    appRunner.Library.SimpleClasses[key] = value;
            }
            
            var aKey = $"{border.Key}-";
            var aValue = new ClassDefinition
            {
                InLengthCollection = true,
                Template = border.Value.Replace("{0}", "calc(var(--spacing) * {0})"),
                ArbitraryCssValueTemplate = border.Value,
            };

            if (appRunner.Library.LengthClasses.TryAdd(aKey, aValue))
                appRunner.Library.ScannerClassNamePrefixes.Insert(aKey, null);
            else
                appRunner.Library.LengthClasses[aKey] = aValue;
            
            aKey = $"{border.Key}-none";
            aValue = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = border.Value.Replace("{0}", "0"),
            };

            if (appRunner.Library.SimpleClasses.TryAdd(aKey, aValue))
                appRunner.Library.ScannerClassNamePrefixes.Insert(aKey, null);
            else
                appRunner.Library.SimpleClasses[aKey] = aValue;
            
            aKey = $"{border.Key}-full";
            aValue = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = border.Value.Replace("{0}", "calc(infinity * 1px)"),
            };

            if (appRunner.Library.SimpleClasses.TryAdd(aKey, aValue))
                appRunner.Library.ScannerClassNamePrefixes.Insert(aKey, null);
            else
                appRunner.Library.SimpleClasses[aKey] = aValue;

            aKey = $"{border.Key}";
            aValue = new ClassDefinition
            {
                SelectorSort = -1,
                InSimpleUtilityCollection = true,
                Template = border.Value.Replace("{0}", "var(--radius-sm)"),
            };

            if (appRunner.Library.SimpleClasses.TryAdd(aKey, aValue))
                appRunner.Library.ScannerClassNamePrefixes.Insert(aKey, null);
            else
                appRunner.Library.SimpleClasses[aKey] = aValue;
        }
    }
}
