using Fynydd.Sfumato.Entities;
using Fynydd.Sfumato.Extensions;

namespace Fynydd.Sfumato.Tests.ScssUtilityCollections;

public class BorderTests
{
    [Fact]
    public async Task StaticValues()
    {
        var appState = new SfumatoAppState();
        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "border-solid");
        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("border-style:solid;", selector.GetStyles().CompactCss());
    }

    [Fact]
    public async Task CalculatedValues()
    {
        var appState = new SfumatoAppState();
        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "border-rose-100");
        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("border-color:rgba(255,228,230,1);", selector.GetStyles().CompactCss());

        selector = new CssSelector(appState, "border-2");
        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("border-width:0.125rem;", selector.GetStyles().CompactCss());
    }

    [Fact]
    public async Task ModifierValues()
    {
        var appState = new SfumatoAppState();
        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "border-rose-100/50");
        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("border-color:rgba(255,228,230,0.5);", selector.GetStyles().CompactCss());
    }

    [Fact]
    public async Task ArbitraryValues()
    {
        var appState = new SfumatoAppState();
        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "border-[#aabbcc]");
        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("border-color:#aabbcc;", selector.GetStyles().CompactCss());

        selector = new CssSelector(appState, "border-[2px]");
        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("border-width:2px;", selector.GetStyles().CompactCss());

        selector = new CssSelector(appState, "border-[dotted]");
        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("border-style:dotted;", selector.GetStyles().CompactCss());
    }
}
