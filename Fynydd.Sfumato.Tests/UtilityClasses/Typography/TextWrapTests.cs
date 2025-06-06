namespace Fynydd.Sfumato.Tests.UtilityClasses.Typography;

public class TextWrapTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void TextWrap()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "text-wrap",
                EscapedClassName = ".text-wrap",
                Styles =
                    """
                    text-wrap: wrap;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "text-pretty",
                EscapedClassName = ".text-pretty",
                Styles =
                    """
                    text-wrap: pretty;
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
