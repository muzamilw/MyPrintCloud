using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// TemplatePage mapper
    /// </summary>
    public static class TemplatePageMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this TemplatePage source, TemplatePage target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.ProductId = source.ProductId;
            // If Dimensions get changed in case of custom
            target.OldWidth = target.Width;
            target.OldHeight = target.Height;
            target.Width = source.Width;
            target.Height = source.Height;
            target.PageName = source.PageName;
            // Keep old page no if changes
            target.OldPageNo = target.PageNo;
            target.PageNo = source.PageNo;
            target.Orientation = source.Orientation;
        }

        #endregion
    }
}
