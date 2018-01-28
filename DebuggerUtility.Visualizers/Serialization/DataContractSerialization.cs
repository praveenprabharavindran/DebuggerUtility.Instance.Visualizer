using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;

namespace DebuggerUtility.Visualizers.Serialization
{
    public class DataContractSerialization : SerializationBase
    {
        /// <summary>
        /// Serialize the target object to an Xml string using DataContractSerializer
        /// <param name="target">object to be serialized.</param>
        /// <param name="formatted">indicates whether the serialized output needs to be formatted or not.</param>
        /// <returns>Serialized string.</returns>
        public override string Serialize(object target, bool formatted = false)
        {

            if (target == null)
                return string.Empty;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = Encoding.Unicode;
                settings.OmitXmlDeclaration = false;

                if (formatted)
                {
                    settings.Indent = true;
                }
                else
                {
                    settings.Indent = false;
                }

                using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, settings))
                using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(xmlWriter))
                using (StreamReader streamReader = new StreamReader(memoryStream))
                {
                    DataContractSerializer dataContractSerializer = new DataContractSerializer(target.GetType());
                    dataContractSerializer.WriteObject(xmlDictionaryWriter, target);
                    xmlDictionaryWriter.Flush();

                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.Flush();
                    return(streamReader.ReadToEnd());
                }
            }
        }

        /// <summary>
        /// This method deserializes the passed in xml string to an object using DataContractSerializer.
        /// </summary>
        /// <param name="targetType">Type of the target object into which the serialized string need to be deserialized.</param>
        /// <param name="serializedString">The serialized string to be deserialized.</param>
        /// <returns>The deserialized object.</returns>
        public override object Deserialize(Type targetType, string serializedString)
        {
            if(string.IsNullOrWhiteSpace(serializedString))
                return default(object);

            using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(serializedString)))
            {
                using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(memoryStream, Encoding.Unicode, new XmlDictionaryReaderQuotas(), null))
                {
                    DataContractSerializer dataContractSerializer = new DataContractSerializer(targetType);

                    return (dataContractSerializer.ReadObject(xmlDictionaryReader, true));
                }
            }
        }
    }
}
