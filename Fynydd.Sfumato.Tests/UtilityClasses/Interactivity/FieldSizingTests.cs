namespace Fynydd.Sfumato.Tests.UtilityClasses.Interactivity;

public class FieldSizingTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void FieldSizing()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "field-sizing-fixed",
                EscapedClassName = ".field-sizing-fixed",
                Styles =
                    """
                    field-sizing: fixed;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "field-sizing-content",
                EscapedClassName = ".field-sizing-content",
                Styles =
                    """
                    field-sizing: content;
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
