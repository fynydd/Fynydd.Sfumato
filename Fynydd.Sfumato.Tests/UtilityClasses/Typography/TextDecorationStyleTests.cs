namespace Fynydd.Sfumato.Tests.UtilityClasses.Typography;

public class TextDecorationStyleTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void TextDecorationStyle()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "decoration-solid",
                EscapedClassName = ".decoration-solid",
                Styles =
                    """
                    text-decoration-style: solid;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "decoration-wavy",
                EscapedClassName = ".decoration-wavy",
                Styles =
                    """
                    text-decoration-style: wavy;
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
