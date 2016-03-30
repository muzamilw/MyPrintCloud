using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Template Mapper
    /// </summary>
    public static class TemplateMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static Template CreateFrom(this DomainModels.Template source)
        {
            return new Template
            {
                ProductId = source.ProductId,
                ProductName = source.ProductName,
                PdfTemplateHeight = source.PDFTemplateHeight,
                PdfTemplateWidth = source.PDFTemplateWidth,
                IsCreatedManual = source.isCreatedManual,
                IsSpotTemplate = source.isSpotTemplate,
                FileOriginalBytes = source.FileOriginalBytes,
                IsAllowCustomSize = source.IsAllowCustomSize,
                HideSharedImages = source.HideSharedImages,
                CuttingMargin = source.CuttingMargin,
                TemplatePages = source.TemplatePages != null ? source.TemplatePages.Select(vdp => vdp.CreateFrom()) : new List<TemplatePage>()
            };
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.Template CreateFrom(this Template source)
        {
            return new DomainModels.Template
            {
                ProductId = source.ProductId,
                ProductName = source.ProductName,
                PDFTemplateHeight = source.PdfTemplateHeight,
                PDFTemplateWidth = source.PdfTemplateWidth,
                isCreatedManual = source.IsCreatedManual,
                isSpotTemplate = source.IsSpotTemplate,
                FileName = source.FileName,
                FileSource = source.FileSource,
                IsAllowCustomSize = source.IsAllowCustomSize,
                HideSharedImages = source.HideSharedImages,
                CuttingMargin = source.CuttingMargin,
                TemplatePages = source.TemplatePages != null ? source.TemplatePages.Select(vdp => vdp.CreateFrom()).ToList() : new List<DomainModels.TemplatePage>()
            };
        }
    }
}