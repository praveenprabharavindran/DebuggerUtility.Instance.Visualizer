/*************************************************************************
*
*   Class/Module Name: DebuggerSideVisualizer
*
*   Date Created:  06/June/2012, Praveen P R
*
*   Purpose: Defines the base class for DebuggerSideVisualizers.
*
*   Notes:
*
*   Date            Author          Description
*   ----            ------          -----------
*   06/June/2012    Praveen P R     Created the class file.
*************************************************************************/

using Microsoft.VisualStudio.DebuggerVisualizers;
using System.IO;
using DebuggerUtility.Visualizers.Serialization;

namespace DebuggerUtility.Visualizers
{
    public abstract class DebuggerSideVisualizer : DialogDebuggerVisualizer
    {
        /// <summary>
        /// Constructor for the 
        /// </summary>
        protected DebuggerSideVisualizer()
        {
            TargetObject = null;
            IsUpdateRequired = false;
            IsEditable = false;
        }

        /// <summary>
        /// Indicates whether the visualizer supports user editing.
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        /// Holds the target object to be visualized.
        /// </summary>
        public object TargetObject { get; set; }

        /// <summary>
        /// Returns a serialized and formatted text version of the target object.
        /// </summary>
        /// <returns></returns>
        public abstract string FormattedString { get; }

        /// <summary>
        /// Stores the name of the visualizer.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The serializer used by this instance.
        /// </summary>
        public SerializationBase Serializer { get; set; }

        /// <summary>
        /// Indicates whether the debuggee side object needs to be updated.
        /// </summary>
        public bool IsUpdateRequired { get; set; }

        /// <summary>
        /// Saves the target object to a file
        /// </summary>
        public abstract void SaveToFile(string argFilePath);

        /// <summary>
        /// Loads the target object from a file.
        /// </summary>
        /// <param name="argFilePath"></param>
        public abstract void LoadFromFile(string argFilePath);

        public abstract void UpdateTargetObject(string argSerializedObject);
    }
}
