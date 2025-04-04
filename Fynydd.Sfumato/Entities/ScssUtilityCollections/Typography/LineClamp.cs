namespace Fynydd.Sfumato.Entities.ScssUtilityCollections.Typography;

public class LineClamp : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "line-clamp";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.LineClampStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.LineClampStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("integer", cssSelector,
            """
            -webkit-line-clamp: {value};
            overflow: hidden;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            """, AppState, out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}