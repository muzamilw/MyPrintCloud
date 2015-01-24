using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class Utility
    {
        public static void DeleteFile(string completePath)
        {
            try
            {
                if (System.IO.File.Exists(completePath))
                {
                    System.IO.File.Delete(completePath);
                }
            }
            catch (Exception)
            { }
        }
    }
}
