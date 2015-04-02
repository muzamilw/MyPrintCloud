using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class DividerLine
    {
        public int X1;
        public int X2;
        public int Y1;

        public int Y2;
        public DividerLine(int X1, int X2, int Y1, int Y2)
        {
            this.X1 = X1;
            this.X2 = X2;
            this.Y1 = Y1;
            this.Y2 = Y2;
        }
    }
}
