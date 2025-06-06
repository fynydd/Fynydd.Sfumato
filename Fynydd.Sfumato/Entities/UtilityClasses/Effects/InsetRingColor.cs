// ReSharper disable RawStringCanBeSimplified

namespace Fynydd.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class InsetRingColor : ClassDictionaryBase
{
    public InsetRingColor()
    {
        Group = "box-shadow";
        Description = "Utilities for setting the color of inset rings.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "inset-ring-", new ClassDefinition
                {
                    SelectorSort = 1,
                    InColorCollection = true,
                    Template = 
                        """
                        --sf-inset-ring-color: {0};
                        """,
                }
            },
            {
                "inset-ring-inherit", new ClassDefinition
                {
                    SelectorSort = 1,
                    InSimpleUtilityCollection = true,
                    Template = 
                        """
                        --sf-inset-ring-color: inherit;
                        """,
                }
            },
            {
                "inset-ring-current", new ClassDefinition
                {
                    SelectorSort = 1,
                    InSimpleUtilityCollection = true,
                    Template = 
                        """
                        --sf-inset-ring-color: currentColor;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
