// ReSharper disable RawStringCanBeSimplified

namespace Fynydd.Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextDecorationStyle : ClassDictionaryBase
{
    public TextDecorationStyle()
    {
        Group = "text-decoration-style";
        Description = "Utilities for setting the style of text decorations.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "decoration-solid", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-decoration-style: solid;
                               """
                }
            },
            {
                "decoration-double", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-decoration-style: double;
                               """
                }
            },
            {
                "decoration-dotted", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-decoration-style: dotted;
                               """
                }
            },
            {
                "decoration-dashed", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-decoration-style: dashed;
                               """
                }
            },
            {
                "decoration-wavy", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-decoration-style: wavy;
                               """
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}