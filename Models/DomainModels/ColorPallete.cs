namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Color Pallete Domain Model
    /// </summary>
    public class ColorPallete
    {
        #region Persisted Properties

        /// <summary>
        /// Pallete Id
        /// </summary>
        public long PalleteId { get; set; }

        /// <summary>
        /// Pallete Name
        /// </summary>
        public string PalleteName { get; set; }

        /// <summary>
        /// Color 1
        /// </summary>
        public string Color1 { get; set; }

        /// <summary>
        /// Color 2
        /// </summary>
        public string Color2 { get; set; }

        /// <summary>
        /// Color 3
        /// </summary>
        public string Color3 { get; set; }

        /// <summary>
        /// Color 4
        /// </summary>
        public string Color4 { get; set; }

        /// <summary>
        /// Color 5
        /// </summary>
        public string Color5 { get; set; }

        /// <summary>
        /// Color 6
        /// </summary>
        public string Color6 { get; set; }

        /// <summary>
        /// Color 7
        /// </summary>
        public string Color7 { get; set; }

        /// <summary>
        /// Skin Id
        /// </summary>
        public long? SkinId { get; set; }

        /// <summary>
        /// Is Default
        /// </summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// Company Id
        /// </summary>
        public long? CompanyId { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        public virtual Company Company { get; set; }

        #endregion
    }
}
