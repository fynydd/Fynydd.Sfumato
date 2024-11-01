namespace Fynydd.Sfumato.Entities.ScssUtilityCollections.Accessibility;

public class SrOnly : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "sr-only";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await Task.CompletedTask;
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        return """
               position: absolute;
               width: 1px;
               height: 1px;
               padding: 0;
               margin: -1px;
               overflow: hidden;
               clip: rect(0, 0, 0, 0);
               white-space: nowrap;
               border-width: 0;
               """;
    }
}