namespace Fynydd.Sfumato.Tests.UtilityClasses.Interactivity;

public class PointerEventsTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void PointerEvents()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "pointer-events-auto",
                EscapedClassName = ".pointer-events-auto",
                Styles =
                    """
                    pointer-events: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "pointer-events-none",
                EscapedClassName = ".pointer-events-none",
                Styles =
                    """
                    pointer-events: none;
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
