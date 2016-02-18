using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class Contact
    {

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }


        /// <summary>
        /// Lead_score
        /// </summary>
        public string Lead_score { get; set; }


        /// <summary>
        /// Tags
        /// </summary>
        public List<string> Tags { get; set; }
    }
}
