// ReSharper disable RawStringCanBeSimplified

namespace Fynydd.Sfumato.Entities.UtilityClasses.Effects;

public sealed class Mask : ClassDictionaryBase
{
    public Mask()
    {
        Group = "mask-image";
        Description = "Utilities for applying masks to elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-linear-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-mask-linear-position: {0}deg;
                        --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops, var(--sf-mask-linear-position)));
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        
                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear-position: {0};
                        --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops, var(--sf-mask-linear-position)));

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                }
            },
            {
                "-mask-linear-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-mask-linear-position: -{0}deg;
                        --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops, var(--sf-mask-linear-position)));
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        
                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear-position: calc({0} * -1);
                        --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops, var(--sf-mask-linear-position)));

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                }
            },
            {
                "mask-radial-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    InLengthCollection = true,
                    Template =
                        """
                        --sf-mask-radial: radial-gradient(var(--sf-mask-radial-stops, var(--sf-mask-radial-size)));
                        --sf-mask-radial-size: {0};

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
            {
                "mask-conic-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-mask-conic: conic-gradient(from {0}deg, black var(--sf-mask-conic-from), transparent var(--sf-mask-conic-to));

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-conic: conic-gradient(from {0}, black var(--sf-mask-conic-from), transparent var(--sf-mask-conic-to));
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        
                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
            {
                "-mask-conic-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-mask-conic: conic-gradient(from -{0}deg, black var(--sf-mask-conic-from), transparent var(--sf-mask-conic-to));

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-conic: conic-gradient(from -{0}, black var(--sf-mask-conic-from), transparent var(--sf-mask-conic-to));

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
            {
                "mask-radial-circle", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-shape: circle;
                        """,
                }
            },
            {
                "mask-radial-ellipse", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-shape: ellipse;
                        """,
                }
            },
            {
                "mask-radial-closest-corner", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-size: closest-corner;
                        """,
                }
            },
            {
                "mask-radial-closest-side", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-size: closest-side;
                        """,
                }
            },
            {
                "mask-radial-farthest-corner", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-size: farthest-corner;
                        """,
                }
            },
            {
                "mask-radial-farthest-side", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-size: farthest-side;
                        """,
                }
            },
            {
                "mask-radial-at-top-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-position: top left;
                        """,
                }
            },
            {
                "mask-radial-at-top", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-position: top;
                        """,
                }
            },
            {
                "mask-radial-at-top-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-position: top right;
                        """,
                }
            },
            {
                "mask-radial-at-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-position: left;
                        """,
                }
            },
            {
                "mask-radial-at-center", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-position: center;
                        """,
                }
            },
            {
                "mask-radial-at-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-position: right;
                        """,
                }
            },
            {
                "mask-radial-at-bottom-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-position: bottom left;
                        """,
                }
            },
            {
                "mask-radial-at-bottom", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-position: bottom;
                        """,
                }
            },
            {
                "mask-radial-at-bottom-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-mask-radial-position: bottom right;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}