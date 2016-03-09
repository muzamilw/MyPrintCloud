using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Protocols.WSFederation;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class TemplateFontMapper
    {
        public static TemplateFont CreateFrom(this MPC.Models.DomainModels.TemplateFont source)
        {
            return new TemplateFont
            {
                ProductFontId = source.ProductFontId,
                ProductId = source.ProductId,
                FontName = source.FontName,
                FontDisplayName = source.FontDisplayName,
                FontFile = source.FontFile,
                DisplayIndex = source.DisplayIndex,
                IsPrivateFont = source.IsPrivateFont,
                IsEnable = source.IsEnable,
                FontPath = source.FontPath,
                CustomerId = source.CustomerId,
                TerritoryId = source.TerritoryId
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static MPC.Models.DomainModels.TemplateFont CreateFrom(this TemplateFont source)
        {
            var templateFont = new MPC.Models.DomainModels.TemplateFont
            {
                ProductFontId = source.ProductFontId,
                ProductId = source.ProductId,
                FontName = source.FontName,
                FontDisplayName = source.FontDisplayName,
                FontFile = source.FontFile,
                DisplayIndex = source.DisplayIndex,
                IsPrivateFont = source.IsPrivateFont,
                IsEnable = source.IsEnable,
                FontPath = source.FontPath,
                CustomerId = source.CustomerId,
                TerritoryId = source.TerritoryId,
                TtFFileBytes = source.TtFFileBytes,
                EotFileBytes = source.EotFileBytes,
                WofFileBytes = source.WofFileBytes
            };

            return templateFont;
        }
    }
}