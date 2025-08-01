namespace Fynydd.Sfumato.Tests.UtilityClasses.Tables;

public class BorderCollapseTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BorderCollapse()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "border-collapse",
                EscapedClassName = ".border-collapse",
                Styles =
                    """
                    border-collapse: collapse;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "border-separate",
                EscapedClassName = ".border-separate",
                Styles =
                    """
                    border-collapse: separate;
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
