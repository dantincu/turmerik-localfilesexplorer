using System.Xml.Serialization;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class XmlH
    {
        public static string ToXml(
            this object obj,
            Type type = null)
        {
            type = type ?? obj.GetType();
            var serializer = new XmlSerializer(type);

            string xmlStr;

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                xmlStr = writer.ToString();
            }

            return xmlStr;
        }

        public static T FromXml<T>(
            this string xmlStr,
            Type type = null)
        {
            type = type ?? typeof(T);
            var serializer = new XmlSerializer(type);

            T obj;

            using (var reader = new StringReader(xmlStr))
            {
                obj = (T)serializer.Deserialize(reader);
            }

            return obj;
        }
    }
}
