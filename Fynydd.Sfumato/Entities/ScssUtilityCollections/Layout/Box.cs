namespace Fynydd.Sfumato.Entities.ScssUtilityCollections.Layout;

public class Box : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "box";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.BoxStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.BoxStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        return string.Empty;
    }
}