// ReSharper disable RawStringCanBeSimplified

namespace Fynydd.Sfumato.Entities.UtilityClasses.Effects;

public sealed class MaskImage : ClassDictionaryBase
{
    public MaskImage()
    {
        Group = "mask-image";
        GroupDescription = "Utilities for applying image-based masks.";
        Description = "Utilities for applying image-based masks.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    InUrlCollection = true,
                    Template = """
                               -webkit-mask-image: {0};
                               mask-image: {0};
                               """,
                }
            },
            {
                "mask-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               -webkit-mask-image: none;
                               mask-image: none;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}