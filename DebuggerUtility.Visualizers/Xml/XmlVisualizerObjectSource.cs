/*************************************************************************
*
*   Class/Module Name: XmlVisualizerObjectSource
*
*   Date Created:  01/Aug/2011, Praveen P R
*
*   Purpose: Extends the VisualizerObjectSource class to support xml serialization to transport a 
             an object between Debuggee and Debugger processes.
*
*   Notes:
*
*   Date            Author          Description
*   ----            ------          -----------
*   01/Aug/2011    Praveen P R      Created the class file.
*************************************************************************/


namespace DebuggerUtility.Visualizers.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.DebuggerVisualizers;
    using DebuggerUtility.Visualizers.Serialization;
    using System.IO;

    /// <summary>
    /// Extends the VisualizerObjectSource class to support xml serialization to transport a 
    /// an object between Debuggee and Debugger processes.
    /// </summary>
    public class XmlVisualizerObjectSource : VisualizerObjectSource
    {
        public XmlVisualizerObjectSource()
        {
            serializer = new XmlSerialization();
        }

        /// <summary>
        /// The serializer used by this class.
        /// </summary>
        private SerializationBase serializer;

        /// <summary>
        /// Gets data from the specified object and serializes it into the outgoing data stream.
        /// </summary>
        /// <param name="target">Object being visualized.</param>
        /// <param name="outgoingData">Outgoing data stream.</param>
        public override void GetData(object target, Stream outgoingData)
        {
            if (target == null)
                return;

            var writer = new StreamWriter(outgoingData);
            writer.WriteLine(target.GetType().AssemblyQualifiedName);
            writer.WriteLine(serializer.Serialize(target));
            writer.Flush();
        }

        /// <summary>
        /// Reads an incoming data stream from the debugger side and uses the data to construct a replacement object for the target object. 
        /// This method is called when ReplaceData or ReplaceObject is called on the debugger side.
        /// </summary>
        /// <param name="target">Object being visualized.</param>
        /// <param name="incomingData">Incoming data stream.</param>
        /// <returns>An object, with contents constructed from the incoming data stream, that can replace the target object. 
        /// This method does not actually replace target but rather provides a replacement object for the debugger 
        /// to do the actual replacement.</returns>
        public override object CreateReplacementObject(object target, Stream incomingData)
        {
            StreamReader streamReader = new StreamReader(incomingData);
            string targetObjectType = streamReader.ReadLine();

            return (serializer.Deserialize(Type.GetType(targetObjectType), streamReader.ReadToEnd()));
        }
    }
}
