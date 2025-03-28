namespace Fynydd.Sfumato.Entities.ScssUtilityCollections.Layout;

public class BreakInside : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "break-inside";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.BreakInsideStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.BreakInsideStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        return string.Empty;
    }
}