namespace Fynydd.Sfumato.Tests.UtilityClasses.Backgrounds;

public class BackgroundLinearTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void BackgroundLinear()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "bg-linear-65",
                EscapedClassName = ".bg-linear-65",
                Styles =
                    """
                    @supports (background-image:linear-gradient(in lab, red, red)) {
                        --sf-gradient-position: 65deg in oklab;
                    }
                    
                    --sf-gradient-position: 65deg;
                    background-image: linear-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-linear-65/srgb",
                EscapedClassName = @".bg-linear-65\/srgb",
                Styles =
                    """
                    @supports (background-image:linear-gradient(in lab, red, red)) {
                        --sf-gradient-position: 65deg in srgb;
                    }

                    --sf-gradient-position: 65deg;
                    background-image: linear-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-linear-65/shorter",
                EscapedClassName = @".bg-linear-65\/shorter",
                Styles =
                    """
                    @supports (background-image:linear-gradient(in lab, red, red)) {
                        --sf-gradient-position: 65deg in oklch shorter hue;
                    }

                    --sf-gradient-position: 65deg;
                    background-image: linear-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-bg-linear-65",
                EscapedClassName = ".-bg-linear-65",
                Styles =
                    """
                    @supports (background-image:linear-gradient(in lab, red, red)) {
                        --sf-gradient-position: -65deg in oklab;
                    }

                    --sf-gradient-position: -65deg;
                    background-image: linear-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-linear-[25deg,red_5%,yellow_60%,lime_90%,teal]",
                EscapedClassName = @".bg-linear-\[25deg\,red_5\%\,yellow_60\%\,lime_90\%\,teal\]",
                Styles =
                    """
                    background-image: linear-gradient(var(--sf-gradient-stops, 25deg,red 5%,yellow 60%,lime 90%,teal))
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-linear-(--my-gradient)",
                EscapedClassName = @".bg-linear-\(--my-gradient\)",
                Styles =
                    """
                    background-image: linear-gradient(var(--sf-gradient-stops, var(--my-gradient)))
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-linear-to-t",
                EscapedClassName = ".bg-linear-to-t",
                Styles =
                    """
                    @supports (background-image:linear-gradient(in lab, red, red)) {
                        --sf-gradient-position: to top in oklab;
                    }
                    
                    --sf-gradient-position: to top;
                    background-image: linear-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-linear-to-t/srgb",
                EscapedClassName = @".bg-linear-to-t\/srgb",
                Styles =
                    """
                    @supports (background-image:linear-gradient(in lab, red, red)) {
                        --sf-gradient-position: to top in srgb;
                    }
                    
                    --sf-gradient-position: to top;
                    background-image: linear-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-linear-to-t/longer",
                EscapedClassName = @".bg-linear-to-t\/longer",
                Styles =
                    """
                    @supports (background-image:linear-gradient(in lab, red, red)) {
                        --sf-gradient-position: to top in oklch longer hue;
                    }
                    
                    --sf-gradient-position: to top;
                    background-image: linear-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(appRunner, test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
