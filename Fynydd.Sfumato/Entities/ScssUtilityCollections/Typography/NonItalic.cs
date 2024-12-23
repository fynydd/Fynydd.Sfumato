namespace Fynydd.Sfumato.Entities.ScssUtilityCollections.Typography;

public class NonItalic : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "not-italic";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.NotItalicStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.NotItalicStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}