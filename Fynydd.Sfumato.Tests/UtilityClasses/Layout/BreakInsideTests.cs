namespace Fynydd.Sfumato.Tests.UtilityClasses.Layout;

public class BreakInsideTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BreakInside()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "break-inside-auto",
                EscapedClassName = ".break-inside-auto",
                Styles =
                    """
                    break-inside: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "break-inside-avoid",
                EscapedClassName = ".break-inside-avoid",
                Styles =
                    """
                    break-inside: avoid;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "break-inside-avoid-page",
                EscapedClassName = ".break-inside-avoid-page",
                Styles =
                    """
                    break-inside: avoid-page;
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
