using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class MachineGuilotinePtv
    {
        public int Id { get; set; }
        public int NoofSections { get; set; }
        public int NoofUps { get; set; }
        public int Noofcutswithoutgutters { get; set; }
        public int Noofcutswithgutters { get; set; }
        public int GuilotineId { get; set; }
    }
}