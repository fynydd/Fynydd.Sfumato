namespace Fynydd.Sfumato.Tests.UtilityClasses.Transforms;

public class TransformStyleTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void TransformStyle()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "transform-3d",
                EscapedClassName = ".transform-3d",
                Styles =
                    """
                    transform-style: preserve-3d;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "transform-flat",
                EscapedClassName = ".transform-flat",
                Styles =
                    """
                    transform-style: flat;
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
