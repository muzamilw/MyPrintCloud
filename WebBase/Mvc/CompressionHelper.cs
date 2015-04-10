using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.WebBase.Mvc
{
    /// <summary>
    /// Helper class that does compression
    /// </summary>
    public class CompressionHelper
    {
        /// <summary>
        /// Deflate Byte
        /// </summary>
        public static byte[] DeflateByte(byte[] str)
        {
            if (str == null)
            {
                return null;
            }

            using (var output = new MemoryStream())
            {
                using (var compressor = new DeflateStream(output, CompressionMode.Compress))
                {
                    compressor.Write(str, 0, str.Length);
                }

                return output.ToArray();
            }
        }

        /// <summary>
        /// Gzip Byte
        /// </summary>
        public static byte[] GZipByte(byte[] str)
        {
            if (str == null)
            {
                return null;
            }

            using (var output = new MemoryStream())
            {
                using (var compressor = new GZipStream(output, CompressionMode.Compress))
                {
                    compressor.Write(str, 0, str.Length);
                }

                return output.ToArray();
            }
        }
    }
}
