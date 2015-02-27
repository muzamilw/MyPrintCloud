using System.IO;
using System.Web;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Media Library Mapper
    /// </summary>
    public static class MediaLibraryMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static MediaLibrary CreateFrom(this DomainModels.MediaLibrary source)
        {
            byte[] mediaFileBytes = null;
            if (!string.IsNullOrEmpty(source.FilePath))
            {
                string path =
                    HttpContext.Current.Server.MapPath("~/" + source.FilePath);
                if (File.Exists(path))
                {
                    mediaFileBytes = File.ReadAllBytes(path);
                }
            }
            return new MediaLibrary
            {
                MediaId = source.MediaId,
                FileName = source.FileName,
                FilePath = source.FilePath,
                CompanyId = source.CompanyId,
                FileType = source.FileType,
                Image = mediaFileBytes
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static DomainModels.MediaLibrary CreateFrom(this MediaLibrary source)
        {
            return new DomainModels.MediaLibrary
            {
                MediaId = source.MediaId,
                FileName = source.FileName,
                FilePath = source.FilePath ?? string.Empty,
                CompanyId = source.CompanyId,
                FileType = source.FileType,
                FileSource = source.FileSource,
                FakeId = source.FakeId
            };
        }
        #endregion
    }
}