/*************************************************************************
*
*   Class/Module Name: JsonVisualizerObjectSource
*
*   Date Created:  01/Aug/2011, Praveen P R
*
*   Purpose: Extends the VisualizerObjectSource class, uses DataContract serialization to transport a 
                DataContract object between Debuggee and Debugger processes.
*
*   Notes:
*
*   Date            Author          Description
*   ----            ------          -----------
*   01/Aug/2011    Praveen P R      Created the class file.
*************************************************************************/
namespace DebuggerUtility.Visualizers.DataContract
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.DebuggerVisualizers;
    using System.IO;
    using DebuggerUtility.Visualizers.Serialization;

    /// <summary>
    /// Extends the VisualizerObjectSource class, uses DataContract serialization to transport a 
    /// DataContract object between Debuggee and Debugger processes.
    /// </summary>
    public class DataContractVisualizerObjectSource : VisualizerObjectSource
    {
    #region Constructors
        public DataContractVisualizerObjectSource()
        {
            serializer = new DataContractSerialization();
        }
    #endregion Constructors

    #region Fields
        /// <summary>
        /// The serializer used by this class.
        /// </summary>
        private SerializationBase serializer;
    #endregion Fields

    #region Override methods
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
    #endregion Override methods
    }
}
