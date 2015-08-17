using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Common
{
    public class XeroXmlRefData
    {
        #region Product

        public string createdBy { get; set; }
        public string productPurchasePrice { get; set; }
        public string productSellPrice { get; set; }
        public string productHeight { get; set; }
        public string productWidth { get; set; }
        public string notes { get; set; }
        public string packSize { get; set; }
        public string reOrderPoint { get; set; }
        #endregion

        #region order
        public string subTotal { get; set; }
        public string taxTotal { get; set; }
        public string Total { get; set; }
        #endregion
    }
}
