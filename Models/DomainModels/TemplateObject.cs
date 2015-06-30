namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Template Object Domain Model
    /// </summary>
    public class TemplateObject
    {
        public long ObjectId { get; set; }
        public int? ObjectType { get; set; }
        public string Name { get; set; }
        public bool? IsEditable { get; set; }
        public bool? IsHidden { get; set; }
        public bool? IsMandatory { get; set; }
        public double? PositionX { get; set; }
        public double? PositionY { get; set; }
        public double? MaxHeight { get; set; }
        public double? MaxWidth { get; set; }
        public double? MaxCharacters { get; set; }
        public double? RotationAngle { get; set; }
        public bool? IsFontCustom { get; set; }
        public bool? IsFontNamePrivate { get; set; }
        public string FontName { get; set; }
        public double? FontSize { get; set; }
        public bool? IsBold { get; set; }
        public bool? IsItalic { get; set; }
        public int? Allignment { get; set; }
        public int? VAllignment { get; set; }
        public double? Indent { get; set; }
        public bool? IsUnderlinedText { get; set; }
        public int? ColorType { get; set; }
        public string ColorName { get; set; }
        public int? ColorC { get; set; }
        public int? ColorM { get; set; }
        public int? ColorY { get; set; }
        public int? ColorK { get; set; }
        public int? Tint { get; set; }
        public bool? IsSpotColor { get; set; }
        public string SpotColorName { get; set; }
        public string ContentString { get; set; }
        public int? ContentCaseType { get; set; }
        public long? ProductId { get; set; }
        public int? DisplayOrderPdf { get; set; }
        public int? DisplayOrderTxtControl { get; set; }
        public int? RColor { get; set; }
        public int? GColor { get; set; }
        public int? BColor { get; set; }
        public double? LineSpacing { get; set; }
        public long? ProductPageId { get; set; }
        public long? ParentId { get; set; }
        public double? CircleRadiusX { get; set; }
        public double? Opacity { get; set; }
        public string ExField1 { get; set; }
        public string ExField2 { get; set; }
        public bool? IsPositionLocked { get; set; }
        public string ColorHex { get; set; }
        public double? CircleRadiusY { get; set; }
        public bool? IsTextEditable { get; set; }
        public int? QuickTextOrder { get; set; }
        public bool? IsQuickText { get; set; }
        public double? CharSpacing { get; set; }
        public string watermarkText { get; set; }
        public string textStyles { get; set; }
        public bool? AutoShrinkText { get; set; }
        public bool? IsOverlayObject { get; set; }
        public string ClippedInfo { get; set; }
        public int textCase { get; set; }
        public string originalTextStyles { get; set; }
        public string originalContentString { get; set; }
        public bool? hasInlineFontStyle { get; set; }
        public bool? autoCollapseText { get; set; }
        public bool? hasClippingPath { get; set; }
        public virtual Template Template { get; set; }
    }
}
