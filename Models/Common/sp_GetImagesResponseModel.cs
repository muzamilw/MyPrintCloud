using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class sp_GetImagesResponseModel
    {
        public long? RowNum { get; set; }
        public int? ID { get; set; }
        public int? ProductID { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string BackgroundImageRelativePath { get; set; }
        public int? ImageType { get; set; }
        public int? ImageWidth { get; set; }
        public int? ImageHeight { get; set; }
        public string ImageTitle { get; set; }
        public string ImageDescription { get; set; }
        public string ImageKeywords { get; set; }
    }
}
