// ReSharper disable RawStringCanBeSimplified

namespace Fynydd.Sfumato.Entities.UtilityClasses.TransitionsAndAnimations;

public sealed class TransitionTimingFunction : ClassDictionaryBase
{
    public TransitionTimingFunction()
    {
        Group = "transition-timing-function";
        Description = "Utilities for setting the timing function for transitions and animations.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "ease-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    InDurationCollection = true,
                    Template =
                        """
                        --sf-ease: {0};
                        transition-timing-function: {0};
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-ease: {0};
                        transition-timing-function: {0};
                        """,
                }
            },
            {
                "ease-linear", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-ease: linear;
                        transition-timing-function: linear;
                        """,
                }
            },
            {
                "ease-initial", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-ease: initial;
                        transition-timing-function: initial;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--ease-")))
        {
            var key = item.Key.Trim('-');
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = 
                    $"""
                     --sf-ease: var({item.Key});
                     transition-timing-function: var({item.Key});
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}