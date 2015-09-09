using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Media Library Domain Model
    /// </summary>
    public class MediaLibrary
    {
        public long MediaId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long CompanyId { get; set; }

        public virtual Company Company { get; set; }

        #region Additional Properties
        [NotMapped]
        public string FileSource { get; set; }
        #endregion

        #region Additional Properties
        [NotMapped]
        public string FakeId { get; set; }
        #endregion


        /// <summary>
        /// Makes a copy of Item
        /// </summary>
        public void Clone(MediaLibrary target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemClone_InvalidItem, "target");
            }


            target.FilePath = FilePath;
            target.FileName = FileName;
            target.FileType = FileType;



           

        }
    }
}
