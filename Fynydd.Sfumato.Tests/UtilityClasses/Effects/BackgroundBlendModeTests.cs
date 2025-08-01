namespace Fynydd.Sfumato.Tests.UtilityClasses.Effects;

public class BackgroundBlendModeTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BackgroundBlendMode()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "bg-blend-normal",
                EscapedClassName = ".bg-blend-normal",
                Styles =
                    """
                    background-blend-mode: normal;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-blend-difference",
                EscapedClassName = ".bg-blend-difference",
                Styles =
                    """
                    background-blend-mode: difference;
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
