using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace DebuggerUtility.Visualizers.Tests
{
    //[DebuggerVisualizer(
    //@"DebuggerUtility.Visualizers.DataContract.DebuggerSideJsonVisualizer, "
    //+ @"DebuggerUtility.Visualizers, "
    //+ @"Version=1.0.0.0, Culture=neutral, "
    //+ @"PublicKeyToken=e8c91feafdcfb6e2",
    //@"DebuggerUtility.Visualizers.DataContract.JsonVisualizerObjectSource, "
    //+ @"DebuggerUtility.Visualizers, "
    //+ @"Version=1.0.0.0, Culture=neutral, "
    //+ @"PublicKeyToken=e8c91feafdcfb6e2",
    //Description = "DataContractVisualizer ")]
    //[DataContract]

    //[DebuggerVisualizer(
    //@"DebuggerUtility.Visualizers.Xml.DebuggerSideXmlVisualizer, "
    //+ @"DebuggerUtility.Visualizers, "
    //+ @"Version=1.0.0.0, Culture=neutral, "
    //+ @"PublicKeyToken=e8c91feafdcfb6e2",
    //@"DebuggerUtility.Visualizers.DataContract.JsonVisualizerObjectSource, "
    //+ @"DebuggerUtility.Visualizers, "
    //+ @"Version=1.0.0.0, Culture=neutral, "
    //+ @"PublicKeyToken=e8c91feafdcfb6e2",
    //Description = "XML Visualizer ")]

    [DebuggerVisualizer(
    @"DebuggerUtility.Visualizers.Json.DebuggerSideJsonVisualizer, "
    + @"DebuggerUtility.Visualizers, "
    + @"Version=1.0.0.0, Culture=neutral, "
    + @"PublicKeyToken=e8c91feafdcfb6e2",
    @"DebuggerUtility.Visualizers.Json.JsonVisualizerObjectSource, "
    + @"DebuggerUtility.Visualizers, "
    + @"Version=1.0.0.0, Culture=neutral, "
    + @"PublicKeyToken=e8c91feafdcfb6e2",
    Description = "Json Visualizer ")]
    [Serializable]
    public class TestType
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}