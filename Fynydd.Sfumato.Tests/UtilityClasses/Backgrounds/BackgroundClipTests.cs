namespace Fynydd.Sfumato.Tests.UtilityClasses.Backgrounds;

public class BackgroundClipTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BackgroundClip()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "bg-clip-padding",
                EscapedClassName = ".bg-clip-padding",
                Styles =
                    """
                    background-clip: padding-box;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-clip-text",
                EscapedClassName = ".bg-clip-text",
                Styles =
                    """
                    background-clip: text;
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
