namespace Fynydd.Sfumato.Entities.ScssUtilityCollections.Filters;

public class BackdropGrayscale : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-grayscale";
    public override string Category => "backdrop";
    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);
        SelectorSort = 3;

        await AddToIndexAsync(appState.BackdropGrayscaleStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.BackdropGrayscaleStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("percentage", cssSelector, "--sf-backdrop-grayscale: grayscale({value});", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}