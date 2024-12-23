namespace Fynydd.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class ContentEvenly : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content-evenly";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ContentEvenlyStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.ContentEvenlyStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}