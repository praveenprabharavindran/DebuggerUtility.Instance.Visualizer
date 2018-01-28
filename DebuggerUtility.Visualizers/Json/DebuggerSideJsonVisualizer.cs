/*************************************************************************
*
*   Class/Module Name: DebuggerSideJsonVisualizer
*
*   Date Created:  01/Aug/2011, Praveen P R
*
*   Purpose: Defines the debugger side class for JsonVisualizer.
*
*   Notes:
*
*   Date            Author          Description
*   ----            ------          -----------
*   01/Aug/2011    Praveen P R      Created the class file.
*************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DebuggerUtility.Visualizers.Json;
using DebuggerUtility.Visualizers.Properties;
using DebuggerUtility.Visualizers.Serialization;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(DebuggerSideJsonVisualizer),
    typeof(JsonVisualizerObjectSource),
    Target = typeof(List<>),
    Description = "Json List Visualizer")]
[assembly: DebuggerVisualizer(
    typeof(DebuggerSideJsonVisualizer),
    typeof(JsonVisualizerObjectSource),
    Target = typeof(Dictionary<,>),
    Description = "Json Dictionary Visualizer")]
namespace DebuggerUtility.Visualizers.Json
{
    /// <summary>
    ///     The debugger side class for Json visualizer .
    /// </summary>
    public class DebuggerSideJsonVisualizer : DebuggerSideVisualizer
    {
        public static void TestShowVisualizer(object objectToVisualize)
        {
            VisualizerDevelopmentHost myHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(DebuggerSideJsonVisualizer));
            myHost.ShowVisualizer();
        }
        #region Constants

        /// <summary>
        ///     Friendly name of the visualizer.
        /// </summary>
        private const string VISUALIZER_NAME = "Json Visualizer";

        #endregion Constants

        #region Fields

        /// <summary>
        ///     Holds the type of the target object.
        /// </summary>
        private Type _targetObjectType;

        #endregion Fields

        #region Constructors

        public DebuggerSideJsonVisualizer()
        {
            IsEditable = true;
            Name = VISUALIZER_NAME;
            Serializer = new JsonSerialization();
        }

        #endregion Constructors

        #region override methods

        /// <summary>
        ///     Displays the user interface for the visualizer.
        /// </summary>
        /// <param name="windowService">
        ///     An object of type Microsoft.VisualStudio.DebuggerVisualizers.IDialogVisualizerService,
        ///     which provides methods that a visualizer can use to display Windows forms, controls, and dialogs.
        /// </param>
        /// <param name="objectProvider">
        ///     An object of type Microsoft.VisualStudio.DebuggerVisualizers.IVisualizerObjectProvider.
        ///     This object provides communication from the debugger side of the visualizer
        ///     to the object source (Microsoft.VisualStudio.DebuggerVisualizers.VisualizerObjectSource)
        ///     on the debuggee side.
        /// </param>
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            try
            {
                // Get the object to display a visualizer for.
                using (var streamReader = new StreamReader(objectProvider.GetData()))
                {
                    var targetObjectType = streamReader.ReadLine();
                    _targetObjectType = Type.GetType(targetObjectType);

                    TargetObject = Serializer.Deserialize(_targetObjectType, streamReader.ReadToEnd());
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format(Resources.DeserializationOfXmlFailed, exception.Message));
                return;
            }

            //Display the object in a UI.
            using (var displayForm = new frmVisualizerDialog(this))
            {
                windowService.ShowDialog(displayForm);

                if (IsUpdateRequired)
                {
                    if (objectProvider.IsObjectReplaceable)
                    {
                        //If the debuggee side object is replaceable and it needs to be updated then
                        //replace it with the target object(debugger side) .
                        using (var outgoingData = new MemoryStream())
                        {
                            using (var writer = new StreamWriter(outgoingData))
                            {
                                writer.WriteLine(TargetObject.GetType().AssemblyQualifiedName);
                                writer.WriteLine(Serializer.Serialize(TargetObject));
                                writer.Flush();
                                objectProvider.ReplaceData(outgoingData);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Serializes the target object and returns a formatted xml string.
        /// </summary>
        /// <returns>a formatted xml string</returns>
        public override string FormattedString => Serializer.Serialize(TargetObject, true);

        /// <summary>
        ///     Updates the target object with the passed in xml serialized string.
        /// </summary>
        /// <param name="argSerializedObject">the serialized string version of the object being visualized.</param>
        public override void UpdateTargetObject(string argSerializedObject)
        {
            TargetObject = Serializer.Deserialize(_targetObjectType, argSerializedObject);
        }

        /// <summary>
        ///     Loads the target object from the specified file.
        /// </summary>
        /// <param name="argFilePath">Path to the file from where the object should be loaded.</param>
        public override void LoadFromFile(string argFilePath)
        {
            using (var streamReader = new StreamReader(argFilePath))
            {
                TargetObject = Serializer.Deserialize(_targetObjectType, streamReader.ReadToEnd());
            }
        }

        /// <summary>
        ///     Saves the target object into the specified file.
        /// </summary>
        /// <param name="argFilePath">Path to the file into where the object should be saved.</param>
        public override void SaveToFile(string argFilePath)
        {
            using (var streamWriter = new StreamWriter(argFilePath))
            {
                streamWriter.Write(Serializer.Serialize(TargetObject));
            }
        }

        #endregion override methods
    }
}