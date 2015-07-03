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
            target.Width = source.Width;
            target.Height = source.Height;
            target.PageName = source.PageName;
            target.PageNo = source.PageNo;
            target.Orientation = source.Orientation;
        }

        #endregion
    }
}
