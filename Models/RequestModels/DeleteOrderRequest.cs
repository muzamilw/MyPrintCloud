using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.RequestModels
{
    public class DeleteOrderRequest
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Comment to delete
        /// </summary>
        public string Comment { get; set; }

    }
}
