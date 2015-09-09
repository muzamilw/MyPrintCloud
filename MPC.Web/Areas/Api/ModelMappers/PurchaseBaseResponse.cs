﻿using System;
using System.Collections.Generic;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Purchase Base Response
    /// </summary>
    public class PurchaseBaseResponse
    {
        /// <summary>
        /// Section Flags
        /// </summary>
        public IEnumerable<SectionFlagDropDown> SectionFlags { get; set; }

        /// <summary>
        /// System Users
        /// </summary>
        public IEnumerable<SystemUserDropDown> SystemUsers { get; set; }

        /// <summary>
        /// Delivery Carriers
        /// </summary>
        public IEnumerable<DeliveryCarrier> DeliveryCarriers { get; set; }

        /// <summary>
        /// Currency Symbol
        /// </summary>
        public string CurrencySymbol { get; set; }


        /// <summary>
        /// Logged In User
        /// </summary>
        public Guid LoggedInUser { get; set; }
    }
}