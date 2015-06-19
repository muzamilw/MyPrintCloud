using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Company Contact For Order Web Model
    /// </summary>
    public class CompanyContactForOrder
    {
        public long ContactId { get; set; }
        public long AddressId { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string StoreName { get; set; }
        public long? StoreId { get; set; }
        public short IsCustomer { get; set; }
    }
}