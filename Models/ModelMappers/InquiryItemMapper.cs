using System;

namespace MPC.Models.ModelMappers
{
    public static class InquiryItemMapper
    {
        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this DomainModels.InquiryItem source, DomainModels.InquiryItem target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.InquiryItemId = source.InquiryItemId;
            target.Title = source.Title;
            target.Notes = source.Notes;
            target.DeliveryDate = source.DeliveryDate;
            target.InquiryId = source.InquiryId;
            target.ProductId = source.ProductId;
        }
    }
}
