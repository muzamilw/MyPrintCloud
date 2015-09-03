using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Variable Option Domain Model
    /// </summary>
    public class VariableOption
    {
        public long VariableOptionId { get; set; }
        public long? VariableId { get; set; }
        public string Value { get; set; }
        public int? SortOrder { get; set; }

        public virtual FieldVariable FieldVariable { get; set; }

        #region Additional Properties
        [NotMapped]
        public long? FakeId { get; set; }
        #endregion



        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(VariableOption target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }


            target.Value = Value;
            target.SortOrder = SortOrder;
            target.FakeId = FakeId;
           


        }

        #endregion
    }
}
