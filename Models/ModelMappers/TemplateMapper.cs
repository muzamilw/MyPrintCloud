using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Template mapper
    /// </summary>
    public static class TemplateMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this Template source, Template target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.ProductId = source.ProductId;
            target.isCreatedManual = source.isCreatedManual;
            target.isSpotTemplate = source.isSpotTemplate;
            target.PDFTemplateHeight = source.PDFTemplateHeight;
            target.PDFTemplateWidth = source.PDFTemplateWidth;
            target.FileSource = source.FileSource;
            target.FileName = source.FileName;
            target.IsAllowCustomSize = source.IsAllowCustomSize;
            target.HideSharedImages = source.HideSharedImages;
            target.CuttingMargin = source.CuttingMargin;
        }

        #endregion
    }
}
