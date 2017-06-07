using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AdamOneilSoftware
{
    public static class XmlSerializerHelper
    {
        public static T Load<T>(string fileName)
        {
            T result = default(T);
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamReader reader = File.OpenText(fileName))
            {
                result = (T)xs.Deserialize(reader);
            }
            return result;
        }

        public static bool TryLoad<T>(string fileName, out T result)
        {
            Exception exc;
            return TryLoad<T>(fileName, out result, out exc);
        }

        public static bool TryLoad<T>(string fileName, out T result, out Exception exception)
        {
            result = default(T);
            exception = null;

            try
            {
                result = Load<T>(fileName);
                return true;
            }
            catch (Exception exc)
            {
                exception = exc;
                return false;
            }
        }

        public static void Save(object @object, string fileName, bool createFolder = true)
        {
            if (createFolder)
            {
                string folder = Path.GetDirectoryName(fileName);
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            }

            XmlSerializer xs = new XmlSerializer(@object.GetType());
            using (StreamWriter writer = File.CreateText(fileName))
            {
                xs.Serialize(writer, @object);
            }
        }

        public static string ToXml<T>(T @object)
        {
            // thanks to https://stackoverflow.com/questions/4123590/serialize-an-object-to-xml

            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (var sw = new StringWriter())
            {
                using (var xw = XmlWriter.Create(sw))
                {
                    xs.Serialize(xw, @object);
                    return sw.ToString();
                }
            }
        }
    }
}
