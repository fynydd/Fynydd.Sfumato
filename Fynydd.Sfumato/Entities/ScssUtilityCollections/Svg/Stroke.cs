namespace Fynydd.Sfumato.Entities.ScssUtilityCollections.Svg;

public class Stroke : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "stroke";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.StrokeStaticUtilities);
        await AddToIndexAsync(appState.ColorOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.StrokeStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "stroke: {value};", AppState, out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "stroke: {value};", AppState, out Result))
            return Result;

        if (ProcessArbitraryValues("integer,length,percentage,number", cssSelector, "stroke-width: {value};", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}