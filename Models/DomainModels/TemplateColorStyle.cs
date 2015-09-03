using System;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Template Color Style Domain Model
    /// </summary>
    public class TemplateColorStyle
    {
        public long PelleteId { get; set; }
        public long? ProductId { get; set; }
        public string Name { get; set; }
        public int? ColorC { get; set; }
        public int? ColorM { get; set; }
        public int? ColorY { get; set; }
        public int? ColorK { get; set; }
        public string SpotColor { get; set; }
        public bool? IsSpotColor { get; set; }
        public int? Field1 { get; set; }
        public string ColorHex { get; set; }
        public bool? IsColorActive { get; set; }
        public long? CustomerId { get; set; }

        public virtual Template Template { get; set; }
        public virtual Company Company { get; set; }

        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(TemplateColorStyle target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }


            target.Name = Name;
            target.ColorC = ColorC;
            target.ColorM = ColorM;
            target.ColorY = ColorY;
            target.ColorK = ColorK;
            target.SpotColor = SpotColor;
            target.Field1 = Field1;
            target.ColorHex = ColorHex;
            target.IsColorActive = IsColorActive;

          

        }

        #endregion
    }
}
