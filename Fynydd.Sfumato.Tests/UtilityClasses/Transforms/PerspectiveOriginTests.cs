namespace Fynydd.Sfumato.Tests.UtilityClasses.Transforms;

public class PerspectiveOriginTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void PerspectiveOrigin()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "perspective-origin-center",
                EscapedClassName = ".perspective-origin-center",
                Styles =
                    """
                    perspective-origin: center;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "perspective-origin-top-left",
                EscapedClassName = ".perspective-origin-top-left",
                Styles =
                    """
                    perspective-origin: top left;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "perspective-origin-top",
                EscapedClassName = ".perspective-origin-top",
                Styles =
                    """
                    perspective-origin: top;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "perspective-origin-[200%_150%]",
                EscapedClassName = @".perspective-origin-\[200\%_150\%\]",
                Styles =
                    """
                    perspective-origin: 200% 150%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "perspective-origin-[var(--my-length)]",
                EscapedClassName = @".perspective-origin-\[var\(--my-length\)\]",
                Styles =
                    """
                    perspective-origin: var(--my-length);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "perspective-origin-(--my-length)",
                EscapedClassName = @".perspective-origin-\(--my-length\)",
                Styles =
                    """
                    perspective-origin: var(--my-length);
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
