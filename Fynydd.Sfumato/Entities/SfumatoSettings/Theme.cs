namespace Fynydd.Sfumato.Entities.SfumatoSettings;

public sealed class Theme
{
    public MediaBreakpoints? MediaBreakpoint { get; set; } = new();
    public FontSizeUnits? FontSizeUnit { get; set; } = new();
    public bool UseAdaptiveLayout { get; set; }

    #region Value Options
    
    public Dictionary<string,string>? Color { get; set; } = new();
    public Dictionary<string,string>? BorderWidth { get; set; } = new();
    public Dictionary<string,string>? BorderRadius { get; set; } = new();
    public Dictionary<string,string>? FilterSize { get; set; } = new();
    
    #endregion
    
    #region Background
    
    public Dictionary<string,string>? BackgroundImage { get; set; } = new();
    public Dictionary<string,string>? BackgroundPosition { get; set; } = new();
    public Dictionary<string,string>? BackgroundSize { get; set; } = new();
    public Dictionary<string,string>? FromPosition { get; set; } = new();
    public Dictionary<string,string>? ToPosition { get; set; } = new();
    public Dictionary<string,string>? ViaPosition { get; set; } = new();

    #endregion

    #region Effects

    public Dictionary<string,string>? BoxShadow { get; set; } = new();

    #endregion

    #region Filters

    public Dictionary<string,string>? BackdropGrayscale { get; set; } = new();
    public Dictionary<string,string>? Grayscale { get; set; } = new();
    public Dictionary<string,string>? BackdropHueRotate { get; set; } = new();
    public Dictionary<string,string>? HueRotate { get; set; } = new();
    public Dictionary<string,string>? BackdropInvert { get; set; } = new();
    public Dictionary<string,string>? Invert { get; set; } = new();
    public Dictionary<string,string>? BackdropSepia { get; set; } = new();
    public Dictionary<string,string>? Sepia { get; set; } = new();

    #endregion
    
    #region Grid

    public Dictionary<string,string>? GridAutoCols { get; set; } = new();
    public Dictionary<string,string>? GridAutoRows { get; set; } = new();
    public Dictionary<string,string>? GridCol { get; set; } = new();
    public Dictionary<string,string>? Gap { get; set; } = new();
    public Dictionary<string,string>? GridCols { get; set; } = new();
    public Dictionary<string,string>? GridRows { get; set; } = new();
    public Dictionary<string,string>? Order { get; set; } = new();
    public Dictionary<string,string>? GridRowEnd { get; set; } = new();
    public Dictionary<string,string>? GridRowSpan { get; set; } = new();
    public Dictionary<string,string>? GridRowStart { get; set; } = new();

    #endregion

    #region Flexbox
    
    public Dictionary<string,string>? Flex { get; set; } = new();
    public Dictionary<string,string>? FlexBasis { get; set; } = new();
    public Dictionary<string,string>? FlexGrow { get; set; } = new();
    public Dictionary<string,string>? FlexShrink { get; set; } = new();
    
    #endregion

    #region Interactivity

    public Dictionary<string,string>? Cursor { get; set; } = new();
    public Dictionary<string,string>? PointerEvents { get; set; } = new();
    public Dictionary<string,string>? ScrollMargin { get; set; } = new();
    public Dictionary<string,string>? ScrollPadding { get; set; } = new();
    public Dictionary<string,string>? UserSelect { get; set; } = new();

    #endregion

    #region Layout

    public Dictionary<string,string>? AspectRatio { get; set; } = new();
    public Dictionary<string,string>? Bottom { get; set; } = new();
    public Dictionary<string,string>? Box { get; set; } = new();
    public Dictionary<string,string>? BoxDecoration { get; set; } = new();
    public Dictionary<string,string>? BreakAfter { get; set; } = new();
    public Dictionary<string,string>? BreakBefore { get; set; } = new();
    public Dictionary<string,string>? BreakInside { get; set; } = new();
    public Dictionary<string,string>? Clear { get; set; } = new();
    public Dictionary<string,string>? Columns { get; set; } = new();
    public Dictionary<string,string>? Container { get; set; } = new();
    public Dictionary<string,string>? Elastic { get; set; } = new();
    public Dictionary<string,string>? InsetEnd { get; set; } = new();
    public Dictionary<string,string>? InsetStart { get; set; } = new();
    public Dictionary<string,string>? Inset { get; set; } = new();
    public Dictionary<string,string>? InsetX { get; set; } = new();
    public Dictionary<string,string>? InsetY { get; set; } = new();
    public Dictionary<string,string>? Isolate { get; set; } = new();
    public Dictionary<string,string>? Left { get; set; } = new();
    public Dictionary<string,string>? Right { get; set; } = new();
    public Dictionary<string,string>? Top { get; set; } = new();
    public Dictionary<string,string>? ZIndex { get; set; } = new();

    #endregion

    #region Sizing

    public Dictionary<string,string>? Height { get; set; } = new();
    public Dictionary<string,string>? MinHeight { get; set; } = new();
    public Dictionary<string,string>? MaxHeight { get; set; } = new();
    public Dictionary<string,string>? Width { get; set; } = new();
    public Dictionary<string,string>? MinWidth { get; set; } = new();
    public Dictionary<string,string>? MaxWidth { get; set; } = new();
    public Dictionary<string,string>? Size { get; set; } = new();
    
    #endregion

    #region Spacing

    public Dictionary<string,string>? Margin { get; set; } = new();
    public Dictionary<string,string>? Padding { get; set; } = new();
    public Dictionary<string,string>? SpaceX { get; set; } = new();
    public Dictionary<string,string>? SpaceY { get; set; } = new();
    
    #endregion

    #region SVG

    public Dictionary<string,string>? SvgStroke { get; set; } = new();

    #endregion

    #region Tables

    public Dictionary<string,string>? BorderSpacing { get; set; } = new();
    public Dictionary<string,string>? BorderSpacingX { get; set; } = new();
    public Dictionary<string,string>? BorderSpacingY { get; set; } = new();
    public Dictionary<string,string>? CaptionSide { get; set; } = new();

    #endregion

    #region Transforms

    public Dictionary<string,string>? Animate { get; set; } = new();
    public Dictionary<string,string>? Transition { get; set; } = new();
    public Dictionary<string,string>? TransitionDelay { get; set; } = new();
    public Dictionary<string,string>? TransitionDuration { get; set; } = new();
    public Dictionary<string,string>? TransitionTiming { get; set; } = new();
    
    #endregion
    
    #region Transitions And Animations

    public Dictionary<string,string>? Origin { get; set; } = new();
    public Dictionary<string,string>? Rotate { get; set; } = new();
    public Dictionary<string,string>? Scale { get; set; } = new();
    public Dictionary<string,string>? ScaleX { get; set; } = new();
    public Dictionary<string,string>? ScaleY { get; set; } = new();
    public Dictionary<string,string>? SkewX { get; set; } = new();
    public Dictionary<string,string>? SkewY { get; set; } = new();
    public Dictionary<string,string>? TranslateX { get; set; } = new();
    public Dictionary<string,string>? TranslateY { get; set; } = new();
    
    #endregion
    
    #region Typography

    public Dictionary<string,string>? DecorationThickness { get; set; } = new();
    public Dictionary<string,string>? FontFamily { get; set; } = new();
    public Dictionary<string,string>? FontWeight { get; set; } = new();
    public Dictionary<string,string>? Leading { get; set; } = new();
    public Dictionary<string,string>? LineClamp { get; set; } = new();
    public Dictionary<string,string>? ListStylePosition { get; set; } = new();
    public Dictionary<string,string>? ListStyleType { get; set; } = new();
    public Dictionary<string,string>? ListStyleImage { get; set; } = new();
    public Dictionary<string,string>? TextAlign { get; set; } = new();
    public Dictionary<string,string>? TextSize { get; set; } = new();
    public Dictionary<string,string>? Tracking { get; set; } = new();
    public Dictionary<string,string>? UnderlineOffset { get; set; } = new();
    
    #endregion
}
