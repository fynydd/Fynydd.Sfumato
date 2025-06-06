namespace Fynydd.Sfumato.Tests.UtilityClasses.Typography;

public class OverflowWrapTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void OverflowWrap()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "wrap-break-word",
                EscapedClassName = ".wrap-break-word",
                Styles =
                    """
                    overflow-wrap: break-word;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "wrap-normal",
                EscapedClassName = ".wrap-normal",
                Styles =
                    """
                    overflow-wrap: normal;
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
