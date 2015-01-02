using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Common
{
    public class DesignerUtils
    {
        public static double MMToPoint(double val)
        {
            return val * 2.834645669;
        }

        public static double PointToMM(double val)
        {
            return val / 2.834645669;
        }


        public static double PointToPixel(double Val)
        {
            return Val * 96 / 72;
        }
        public static double PixelToPoint(double Val)
        {
            return Val / 96 * 72;
        }


    }

}
