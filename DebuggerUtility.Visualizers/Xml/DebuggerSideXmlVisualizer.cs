/*************************************************************************
*
*   Class/Module Name: DebuggerSideXmlVisualizer
*
*   Date Created:  06/June/2012, Praveen P R
*
*   Purpose: Defines the debugger side class for XmlVisualizer.
*
*   Notes:
*   Date            Author          Description
*   ----            ------          -----------
*   06/June/2012    Praveen P R     Created the class file.
*************************************************************************/

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DebuggerUtility.Visualizers.Properties;
using DebuggerUtility.Visualizers.Serialization;
using DebuggerUtility.Visualizers.Xml;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly:
    DebuggerVisualizer(typeof(DebuggerSideXmlVisualizer), typeof(XmlVisualizerObjectSource),
        TargetTypeName =
            "DebuggerUtility.Visualizers.Tests.MyClass, VisualizerTests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
        Description = "Xml Visualizer(DebuggerSide)")]
[assembly:
    DebuggerVisualizer(typeof(DebuggerSideXmlVisualizer), typeof(XmlVisualizerObjectSource),
        TargetTypeName =
            "System.Collections.Generic.Dictionary`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Collections.Generic.List`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
        Description = "Xml Visualizer(DebuggerSide)")]

namespace DebuggerUtility.Visualizers.Xml
{
    /// <summary>
    ///     An updatable xml visualizer for any serializable .net class.
    /// </summary>
    public class DebuggerSideXmlVisualizer : DebuggerSideVisualizer
    {
        #region Constants

        /// <summary>
        ///     Friendly name of the visualizer.
        /// </summary>
        private const string VISUALIZER_NAME = "Xml Visualizer";

        #endregion Constants

        #region Constructors

        public DebuggerSideXmlVisualizer()
        {
            IsEditable = true;
            Name = VISUALIZER_NAME;
            Serializer = new XmlSerialization();
        }

        #endregion Constructors

        #region Fields

        /// <summary>
        ///     Holds the type of the target object.
        /// </summary>
        private Type _targetObjectType { get; set; }

        #endregion Fields

        /// <summary>
        ///     Serializes the target object and returns a formatted xml string.
        /// </summary>
        /// <returns></returns>
        public override string FormattedString => Serializer.Serialize(TargetObject, true);

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
            // Get the object to display a visualizer for.
            try
            {
                //TargetObject = (object)objectProvider.GetObject();
                //_targetObjectType = TargetObject.GetType();
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

            //Display the view of the object.
            using (var displayForm = new frmVisualizerDialog(this))
            {
                windowService.ShowDialog(displayForm);

                if (IsUpdateRequired)
                {
                    if (objectProvider.IsObjectReplaceable)
                    {
                        //objectProvider.ReplaceObject(TargetObject);
                        //If the object is replaceable and it has been updated then
                        //replace the target object with the updated version.
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
        ///     Updates the target object with the passed in xml serialized string.
        /// </summary>
        /// <param name="argSerializedObject"></param>
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
    }
}