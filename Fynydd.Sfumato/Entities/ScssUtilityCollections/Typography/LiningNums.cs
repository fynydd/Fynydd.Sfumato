namespace Fynydd.Sfumato.Entities.ScssUtilityCollections.Typography;

public class LiningNums : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "lining-nums";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.LiningNumsStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.LiningNumsStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}