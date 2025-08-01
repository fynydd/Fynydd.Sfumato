namespace Fynydd.Sfumato.Tests.UtilityClasses.Filters;

public class HueRotateTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void HueRotate()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "hue-rotate-37",
                EscapedClassName = ".hue-rotate-37",
                Styles =
                    """
                    --sf-hue-rotate: hue-rotate(37deg);
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "hue-rotate-[37.5rad]",
                EscapedClassName = @".hue-rotate-\[37\.5rad\]",
                Styles =
                    """
                    --sf-hue-rotate: hue-rotate(37.5rad);
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "hue-rotate-(--my-hue)",
                EscapedClassName = @".hue-rotate-\(--my-hue\)",
                Styles =
                    """
                    --sf-hue-rotate: hue-rotate(var(--my-hue));
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "hue-rotate-[var(--my-hue)]",
                EscapedClassName = @".hue-rotate-\[var\(--my-hue\)\]",
                Styles =
                    """
                    --sf-hue-rotate: hue-rotate(var(--my-hue));
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
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
