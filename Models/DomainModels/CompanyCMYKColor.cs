using System;

namespace MPC.Models.DomainModels
{
    public class CompanyCMYKColor
    {
        public long ColorId { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public string ColorName { get; set; }
        public string ColorC { get; set; }
        public string ColorM { get; set; }
        public string ColorY { get; set; }
        public string ColorK { get; set; }
        public long? TerritoryId { get; set; }
        public virtual Company Company{ get; set; }



        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(CompanyCMYKColor target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }


            target.ColorName = ColorName;
            target.ColorC = ColorC;
            target.ColorM = ColorM;
            target.ColorY = ColorY;
            target.ColorK = ColorK;
           

        }

        #endregion
    }
}
