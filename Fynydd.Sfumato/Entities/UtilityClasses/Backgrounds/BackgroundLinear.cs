// ReSharper disable RawStringCanBeSimplified

namespace Fynydd.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundLinear : ClassDictionaryBase
{
    public BackgroundLinear()
    {
        Group = "background-image";
        Description = "Utilities for applying linear gradient backgrounds.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-linear-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    InAbstractValueCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: {0}deg in oklab;
                        }

                        --sf-gradient-position: {0}deg;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: {0}deg in {1};
                        }

                        --sf-gradient-position: {0}deg;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        background-image: linear-gradient(var(--sf-gradient-stops, {0}))
                        """, 
                }
            },
            {
                "-bg-linear-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: -{0}deg in oklab;
                        }

                        --sf-gradient-position: -{0}deg;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: -{0}deg in {1};
                        }

                        --sf-gradient-position: -{0}deg;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                }
            },
            {
                "bg-linear-to-t", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top in oklab;
                        }
                        
                        --sf-gradient-position: to top;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top in {1};
                        }
                        
                        --sf-gradient-position: to top;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                }
            },
            {
                "bg-linear-to-tr", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top right in oklab;
                        }
                        
                        --sf-gradient-position: to top right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top right in {1};
                        }

                        --sf-gradient-position: to top right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                }
            },
            {
                "bg-linear-to-r", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to right in oklab;
                        }
                        
                        --sf-gradient-position: to right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to right in {1};
                        }

                        --sf-gradient-position: to right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                }
            },
            {
                "bg-linear-to-br", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom right in oklab;
                        }
                        
                        --sf-gradient-position: to bottom right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom right in {1};
                        }

                        --sf-gradient-position: to bottom right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                }
            },
            {
                "bg-linear-to-b", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom in oklab;
                        }
                        
                        --sf-gradient-position: to bottom;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom in {1};
                        }

                        --sf-gradient-position: to bottom;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                }
            },
            {
                "bg-linear-to-bl", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom left in oklab;
                        }
                        
                        --sf-gradient-position: to bottom left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom left in {1};
                        }

                        --sf-gradient-position: to bottom left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                }
            },
            {
                "bg-linear-to-l", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to left in oklab;
                        }
                        
                        --sf-gradient-position: to left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to left in {1};
                        }

                        --sf-gradient-position: to left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                }
            },
            {
                "bg-linear-to-tl", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top left in oklab;
                        }
                        
                        --sf-gradient-position: to top left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top left in {1};
                        }

                        --sf-gradient-position: to top left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}