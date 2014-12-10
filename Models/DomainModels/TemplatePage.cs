﻿namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Template Page
    /// </summary>
    public class TemplatePage
    {
        public int ProductPageId { get; set; }
        public int? ProductId { get; set; }
        public int? PageNo { get; set; }
        public int? PageType { get; set; }
        public int? Orientation { get; set; }
        public int? BackGroundType { get; set; }
        public string BackgroundFileName { get; set; }
        public int? ColorC { get; set; }
        public int? ColorM { get; set; }
        public int? ColorY { get; set; }
        public string PageName { get; set; }
        public int? ColorK { get; set; }
        public bool? IsPrintable { get; set; }
        public bool? hasOverlayObjects { get; set; }

        public virtual Template Template { get; set; }
    }
}
