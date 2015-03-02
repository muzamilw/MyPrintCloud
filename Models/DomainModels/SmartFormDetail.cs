﻿namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Smart From Detail Domain Model
    /// </summary>
    public class SmartFormDetail
    {
        public long SmartFormDetailId { get; set; }
        public long SmartFormId { get; set; }
        public int? ObjectType { get; set; }
        public bool? IsRequired { get; set; }
        public int? SortOrder { get; set; }
        public long? VariableId { get; set; }
        public string CaptionValue { get; set; }

        public virtual SmartForm SmartForm { get; set; }
        public virtual FieldVariable FieldVariable { get; set; }
    }
}
