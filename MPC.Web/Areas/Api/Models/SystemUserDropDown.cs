﻿using System;

namespace MPC.MIS.Areas.Api.Models
{
    public class SystemUserDropDown
    {
        public Guid SystemUserId { get; set; }
        public string UserName { get; set; }
        public bool isSelected { get; set; }
        public string VariableString { get; set; }
        public string FullName { get; set; }
    }
}