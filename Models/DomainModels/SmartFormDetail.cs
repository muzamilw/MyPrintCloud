using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
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

        #region Additional Properties
        [NotMapped]
        public long? FakeVariableId { get; set; }
        #endregion


        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(SmartFormDetail target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }


            target.ObjectType = ObjectType;
            target.IsRequired = IsRequired;
            target.SortOrder = SortOrder;
            target.VariableId = VariableId;
            target.SortOrder = SortOrder;
           // target.CaptionValue = CaptionValue;
            target.FakeVariableId = FakeVariableId;


        }

        #endregion
    }
}
