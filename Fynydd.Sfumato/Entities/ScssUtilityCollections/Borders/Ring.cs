namespace Fynydd.Sfumato.Entities.ScssUtilityCollections.Borders;

public class Ring : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "ring";
    public override string Category => "ring";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ColorOptions);
        await AddToIndexAsync(appState.BorderWidthOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Modifier Utilities
        
        if (ProcessColorModifierOptions(cssSelector, "--sf-ring-color: {value};", out Result))
            return Result;

        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "--sf-ring-color: {value};", AppState, out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.BorderWidthOptions, cssSelector, "box-shadow: var(--sf-ring-inset) 0 0 0 calc({value} + var(--sf-ring-offset-width)) var(--sf-ring-color);", AppState, out Result))
            return Result;
        
        #endregion

        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "--sf-ring-color: {value};", AppState, out Result))
            return Result;

        if (ProcessArbitraryValues("length,percentage", cssSelector, "box-shadow: var(--sf-ring-inset) 0 0 0 calc({value} + var(--sf-ring-offset-width)) var(--sf-ring-color);", AppState, out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}