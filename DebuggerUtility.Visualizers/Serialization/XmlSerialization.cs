using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace DebuggerUtility.Visualizers.Serialization
{
    public class XmlSerialization: SerializationBase
    {
        /// <summary>
        /// Serialize the target object to an Xml string using XmlSerializer
        /// <param name="target">object to be serialized.</param>
        /// <param name="formatted">indicates whether the serialized output needs to be formatted or not.</param>
        /// <returns>Serialized string.</returns>
        public override string Serialize(object target, bool formatted = false)
        {
            if(target == null)
                return string.Empty;
            
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Unicode;
            settings.OmitXmlDeclaration = false;

            if(formatted)
            {
                settings.Indent = true;
            }
            else
            {
                settings.Indent = false;
            }

            using(StringWriter stringWriter = new StringWriter())
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(target.GetType());
                xmlWriter.WriteStartDocument();
                xmlSerializer.Serialize(xmlWriter, target);
                stringWriter.Flush();

                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// This method deserializes the passed in xml string to a object.
        /// </summary>
        /// <param name="serializedString"></param>
        /// <returns></returns>
        public override object Deserialize(Type targetType, string serializedString)
        {
            if (string.IsNullOrEmpty(serializedString))
                return default(object);

            XmlReaderSettings settings = new XmlReaderSettings();

            using (StringReader textReader = new StringReader(serializedString))
            {
                using (XmlReader xmlReader = XmlReader.Create(textReader, settings))
                {
                    XmlSerializer serializer = new XmlSerializer(targetType);
                    return serializer.Deserialize(xmlReader);
                }
            }
        }
    }
}
