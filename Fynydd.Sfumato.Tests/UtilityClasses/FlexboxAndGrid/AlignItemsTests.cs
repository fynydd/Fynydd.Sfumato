namespace Fynydd.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class AlignItemsTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void AlignItems()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "items-start",
                EscapedClassName = ".items-start",
                Styles =
                    """
                    align-items: flex-start;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "items-end",
                EscapedClassName = ".items-end",
                Styles =
                    """
                    align-items: flex-end;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "items-baseline-last",
                EscapedClassName = ".items-baseline-last",
                Styles =
                    """
                    align-items: last baseline;
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
