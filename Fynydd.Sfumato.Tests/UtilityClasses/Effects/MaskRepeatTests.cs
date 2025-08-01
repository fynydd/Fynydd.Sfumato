namespace Fynydd.Sfumato.Tests.UtilityClasses.Effects;

public class MaskRepeatTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void MaskRepeat()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "mask-repeat",
                EscapedClassName = ".mask-repeat",
                Styles =
                    """
                    -webkit-mask-repeat: repeat;
                    mask-repeat: repeat;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-repeat-round",
                EscapedClassName = ".mask-repeat-round",
                Styles =
                    """
                    -webkit-mask-repeat: round;
                    mask-repeat: round;
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
