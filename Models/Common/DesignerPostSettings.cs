using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class DesignerPostSettings
    {
        public bool printCropMarks = false;
        public bool printWaterMarks = false;
        public List<TemplateObject> objects = null;
        public string orderCode = null;
        public string CustomerName = null;
        public List<TemplatePage> objPages = null;
        public bool isRoundCornerrs = false;
        public int organisationId = 0;
        public bool isMultiPageProduct = false;
    }
}
