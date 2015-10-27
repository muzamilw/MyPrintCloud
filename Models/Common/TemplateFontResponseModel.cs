using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class TemplateFontResponseModel
    {
        public string FontName { get; set; }
        public string FontFile { get; set; }
        public string FontPath { get; set; }

        //public TemplateFontResponseModel(string fontName, string fontFile, string fontPath)
        //{
        //    this.FontFile = fontFile;
        //    this.FontPath = fontPath;
        //    this.FontName = fontName;
        //}
    }
}
