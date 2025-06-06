// ReSharper disable RawStringCanBeSimplified

namespace Fynydd.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class GridAutoFlow : ClassDictionaryBase
{
    public GridAutoFlow()
    {
        Group = "grid-auto-flow";
        Description = "Utilities for controlling grid auto-placement flow.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "grid-flow-row", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-auto-flow: row;
                        """,
                }
            },
            {
                "grid-flow-col", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-auto-flow: column;
                        """,
                }
            },
            {
                "grid-flow-dense", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-auto-flow: dense;
                        """,
                }
            },
            {
                "grid-flow-row-dense", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-auto-flow: row dense;
                        """,
                }
            },
            {
                "grid-flow-col-dense", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-auto-flow: column dense;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}