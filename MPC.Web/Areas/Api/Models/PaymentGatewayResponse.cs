using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class PaymentGatewayResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Payment Gateways
        /// </summary>
        public IEnumerable<PaymentGateway> PaymentGateways { get; set; }
    }
}