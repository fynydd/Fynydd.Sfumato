namespace Fynydd.Sfumato.Entities.ScssUtilityCollections.Transforms;

public class Transition : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "transition";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.TransitionStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.TransitionStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "transition: {value};", AppState, out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}