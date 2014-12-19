using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class TemplateVariable
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public TemplateVariable(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
