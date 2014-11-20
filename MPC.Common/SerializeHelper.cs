using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace MPC.Common
{
    /// <summary>
    /// Use this helper to serialize and deserialize to and from a text representation
    /// </summary>
    public static class SerializeHelper
    {
        /// <summary>
        /// Serialize the value
        /// </summary>
        public static string Serialize<T>(T value)
            where T : class
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                using (MemoryStream stream = new MemoryStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(stream, value);
                    stream.Position = 0;
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        public static T Deserialize<T>(string claimValue)
            where T : class
        {
            if (string.IsNullOrEmpty(claimValue))
            {
                return null;
            }

            using (StringReader reader = new StringReader(claimValue))
            using (XmlReader xmlReader = XmlReader.Create(reader))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                return serializer.ReadObject(xmlReader) as T;
            }
        }
    }
}
