namespace Fynydd.Sfumato.Tests.UtilityClasses.Svg;

public class FillTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Fill()
    {
        AppRunner.Library.ColorsByName.Add("fynydd-hex", "#0088ff");
        AppRunner.Library.ColorsByName.Add("fynydd-rgb", "rgba(0, 136, 255, 1.0)");
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "fill-lime-800",
                EscapedClassName = ".fill-lime-800",
                Styles =
                    """
                    fill: var(--color-lime-800);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "fill-lime-800/37",
                EscapedClassName = @".fill-lime-800\/37",
                Styles =
                    """
                    fill: color-mix(in oklab, var(--color-lime-800) 37%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "fill-fynydd-hex/37",
                EscapedClassName = @".fill-fynydd-hex\/37",
                Styles =
                    """
                    fill: color-mix(in srgb, var(--color-fynydd-hex) 37%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "fill-fynydd-rgb/37",
                EscapedClassName = @".fill-fynydd-rgb\/37",
                Styles =
                    """
                    fill: color-mix(in srgb, var(--color-fynydd-rgb) 37%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "fill-lime-800!",
                EscapedClassName = @".fill-lime-800\!",
                Styles =
                    """
                    fill: var(--color-lime-800) !important;
                    """,
                IsValid = true,
                IsImportant = true,
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(AppRunner, selector: test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            TestOutputHelper?.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
