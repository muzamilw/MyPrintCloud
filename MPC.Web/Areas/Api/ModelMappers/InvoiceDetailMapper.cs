using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class InvoiceDetailMapper
    {
        public static InvoiceDetail CreateFrom(this DomainModels.InvoiceDetail source)
        {
            return new InvoiceDetail
            {
                InvoiceDetailId = source.InvoiceDetailId,
                InvoiceId = source.InvoiceId,
                InvoiceTitle = source.InvoiceTitle,
                ItemId = source.ItemId,
                ItemCharge = source.ItemCharge,
                Quantity = source.Quantity,
                ItemTaxValue = source.ItemTaxValue,
                Description = source.Description,
                ItemType = source.ItemType,
                TaxId = source.TaxId,
                DetailType = source.DetailType,
                FlagId = source.FlagId,
                ItemGrossTotal = source.ItemGrossTotal,
                TaxValue = source.TaxValue

            };
        }

        public static DomainModels.InvoiceDetail CreateFrom(this InvoiceDetail source)
        {
            return new DomainModels.InvoiceDetail
            {
                InvoiceDetailId = source.InvoiceDetailId,
                InvoiceId = source.InvoiceId,
                InvoiceTitle = source.InvoiceTitle,
                FlagId = source.FlagId,
                ItemId = source.ItemId,
                ItemCharge = source.ItemCharge,
                Quantity = source.Quantity,
                ItemTaxValue = source.ItemTaxValue,
                Description = source.Description,
                ItemType = source.ItemType,
                TaxId = source.TaxId, 
                DetailType = source.DetailType,
                ItemGrossTotal = source.ItemGrossTotal,
                TaxValue = source.TaxValue
            };
        }
    }
}