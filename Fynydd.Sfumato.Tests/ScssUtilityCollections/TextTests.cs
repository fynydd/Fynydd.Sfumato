using Fynydd.Sfumato.Entities;
using Fynydd.Sfumato.Extensions;

namespace Fynydd.Sfumato.Tests.ScssUtilityCollections;

public class TextTests
{
    [Fact]
    public async Task StaticValues()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-left");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("text-align: left;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "text-center");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("text-align: center;".CompactCss(), selector.GetStyles().CompactCss());
    }

    [Fact]
    public async Task CalculatedValues()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-rose-100");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("color: rgba(255,228,230,1);".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "text-lg");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal($"font-size: {appState.TextSizeOptions["lg"]}; line-height: {appState.TextSizeLeadingOptions["lg"]};".CompactCss(), selector.GetStyles().CompactCss());
    }

    [Fact]
    public async Task ModifierValues()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-rose-100/50");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("color: rgba(255,228,230,0.5);".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "text-rose-100/[50]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("color: rgba(255,228,230,0.5);".CompactCss(), selector.GetStyles().CompactCss());        

        selector = new CssSelector(appState, "text-rose-100/[0.75]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("color: rgba(255,228,230,0.75);".CompactCss(), selector.GetStyles().CompactCss());        
        
        selector = new CssSelector(appState, "text-lg/[0.5]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal($"font-size: {appState.TextSizeOptions["lg"]}; line-height: 0.5;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "text-lg/[3rem]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal($"font-size: {appState.TextSizeOptions["lg"]}; line-height: 3rem;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "text-lg/[110%]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal($"font-size: {appState.TextSizeOptions["lg"]}; line-height: 110%;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "text-lg/loose");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal($"font-size: {appState.TextSizeOptions["lg"]}; line-height: 2;".CompactCss(), selector.GetStyles().CompactCss());
    }
    
    [Fact]
    public async Task ArbitraryValues()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "text-[#aabbcc]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("color: #aabbcc;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "text-[18px]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("font-size: 18px;".CompactCss(), selector.GetStyles().CompactCss());

        selector = new CssSelector(appState, "text-[110%]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("font-size: 110%;".CompactCss(), selector.GetStyles().CompactCss());
    }
}
