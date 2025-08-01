namespace Fynydd.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class AlignContentTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void AlignContent()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "content-start",
                EscapedClassName = ".content-start",
                Styles =
                    """
                    align-content: flex-start;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "content-end",
                EscapedClassName = ".content-end",
                Styles =
                    """
                    align-content: flex-end;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "content-between",
                EscapedClassName = ".content-between",
                Styles =
                    """
                    align-content: space-between;
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
