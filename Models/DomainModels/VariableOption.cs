﻿using System.ComponentModel.DataAnnotations.Schema;

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
    }
}