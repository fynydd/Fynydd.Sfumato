using Fynydd.Sfumato.Entities;
using Fynydd.Sfumato.Extensions;

namespace Fynydd.Sfumato.Tests.ScssUtilityCollections;

public class BgTests
{
    [Fact]
    public async Task StaticValues()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "bg-no-repeat");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-repeat: no-repeat;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "bg-none");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-image: none;".CompactCss(), selector.GetStyles().CompactCss());
    }

    [Fact]
    public async Task CalculatedValues()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "bg-rose-100");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,1);".CompactCss(), selector.GetStyles().CompactCss());
    }

    [Fact]
    public async Task ModifierValues()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "bg-rose-100/50");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,0.5);".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "bg-rose-100/[0.5]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,0.5);".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "bg-rose-100/[.5]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: rgba(255,228,230,0.5);".CompactCss(), selector.GetStyles().CompactCss());
    }
    
    [Fact]
    public async Task ArbitraryValues()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "bg-[#aabbcc]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: #aabbcc;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "bg-[#fff]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: #fff;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "bg-[color:--mycolor]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: var(--mycolor);".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "bg-[color:var(--mycolor)]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-color: var(--mycolor);".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "bg-[100vw]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-size: 100vw;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "bg-[50%]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-size: 50%;".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "bg-[./images/bg.jpg]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-image: url(./images/bg.jpg);".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "bg-[url(../images/bg.jpg)]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-image: url(../images/bg.jpg);".CompactCss(), selector.GetStyles().CompactCss());
        
        selector = new CssSelector(appState, "bg-[left_top]");

        await selector.ProcessSelectorAsync();

        Assert.NotNull(selector.ScssUtilityClassGroup);
        Assert.Equal("background-position: left top;".CompactCss(), selector.GetStyles().CompactCss());
    }
}
