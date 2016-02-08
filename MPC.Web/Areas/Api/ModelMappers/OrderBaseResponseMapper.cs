using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.ResponseModels;

    /// <summary>
    /// Order Base Response WebApi Mapper
    /// </summary>
    public static class OrderBaseResponseMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static OrderBaseResponse CreateFrom(this DomainModels.OrderBaseResponse source)
        {
            return new OrderBaseResponse
            {
                SectionFlags = source.SectionFlags != null ? source.SectionFlags.Select(cc => cc.CreateFromDropDown()) :
                new List<SectionFlagDropDown>(),
                SystemUsers = source.SystemUsers != null ? source.SystemUsers.Select(cc => cc.CreateFrom()) :
                new List<SystemUserDropDown>(),
                PipeLineSources = source.PipeLineSources != null ? source.PipeLineSources.Select(cc => cc.CreateFrom()) :
                new List<PipeLineSource>(),
                PaymentMethods = source.PaymentMethods != null ? source.PaymentMethods.Select(cc => cc.CreateFrom()) :
                new List<PaymentMethod>(),
                CurrencySymbol = (source.Organisation != null && source.Organisation.Currency != null) ? source.Organisation.Currency.CurrencySymbol :
                string.Empty,
                ChartOfAccounts = source.ChartOfAccounts != null ? source.ChartOfAccounts.Select(s => s.CreateFrom()).ToList() : new List<ChartOfAccount>(),
                CostCenters = source.CostCenters != null ? source.CostCenters.Select(x=>x.CreateFrom()).ToList(): new List<CostCentre>(),
                PipeLineProducts = source.PipeLineProducts != null ? source.PipeLineProducts.Select(x=>x.CreateFrom()).ToList(): new List<PipeLineProduct>(),
                LoggedInUser = source.LoggedInUser,
                HeadNotes = source.HeadNotes,
                FootNotes = source.FootNotes,
                DeliveryCarriers = source.DeliveryCarriers != null ? source.DeliveryCarriers.Select(c => c.CreateFrom()) : new List<DeliveryCarrier>()
            };
        }

        public static ItemDetailBaseResponse CreateFrom(this DomainModels.ItemDetailBaseResponse source)
        {
            return new ItemDetailBaseResponse
            {
                Markups = source.Markups != null ? source.Markups.Select(cc => cc.CreateFrom()) : new List<Markup>(),
                PaperSizes = source.PaperSizes != null ? source.PaperSizes.Select(s => s.CreateFromDropDown()).ToList() : new List<PaperSizeDropDown>(),
                InkPlateSides = source.InkPlateSides != null ? source.InkPlateSides.Select(s => s.CreateFromDropDown()).ToList() : new List<InkPlateSide>(),
                Inks = source.Inks != null ? source.Inks.Select(x => x.CreateFromDropDown()).ToList() : new List<StockItemForDropDown>(),
                InkCoverageGroup = source.InkCoverageGroups != null ? source.InkCoverageGroups.Select(x => x.CreateFrom()).ToList() : new List<InkCoverageGroup>(),
                CurrencySymbol = source.CurrencySymbol,
                SystemUsers = source.SystemUsers != null ? source.SystemUsers.Select(cc => cc.CreateFrom()) :
                new List<SystemUserDropDown>(),
                LengthUnit = source.LengthUnit,
                WeightUnit = source.WeightUnit,
                LoggedInUser = source.LoggedInUser,
                DefaultMarkUpId = source.DefaultMarkUpId,
                Machines = source.Machines != null ? source.Machines.Select(cc => cc.CreateFromForOrder()) : new List<Machine>()
            };
        }

    }
}