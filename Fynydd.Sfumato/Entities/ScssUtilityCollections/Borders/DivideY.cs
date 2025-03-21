namespace Fynydd.Sfumato.Entities.ScssUtilityCollections.Borders;

public class DivideY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "divide-y";
    
    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.DivideYStaticUtilities);
        await AddToIndexAsync(appState.DivideWidthOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.DivideYStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.DivideWidthOptions, cssSelector,
                """
                & > * + * {
                    border-top-width: {value};
                    border-bottom-width: 0px;
                }
                """, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
                """
                & > * + * {
                   border-top-width: {value};
                   border-bottom-width: 0px;
                }
                """, AppState, out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}