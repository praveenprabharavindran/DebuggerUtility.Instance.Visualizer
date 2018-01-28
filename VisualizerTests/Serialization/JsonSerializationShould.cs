using System;
using DebuggerUtility.Visualizers.Serialization;
using DebuggerUtility.Visualizers.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VisualizerTests.Serialization
{
    [TestClass]
    public class JsonSerializationShould
    {
        [TestMethod]
        public void SerializeAnType()
        {
            SerializationBase ser = new JsonSerialization();
            var testType = new TestType() {Id = 23, Name = "Test"};
            var serialized = ser.Serialize(testType);
            Assert.IsNotNull(serialized);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(serialized) && serialized.Length > 1);
        }

        [TestMethod]
        public void DeSerializeAType()
        {
            SerializationBase ser = new JsonSerialization();
            var testType = new TestType() { Id = 23, Name = "Test" };
            var input =ser.Serialize(testType);

            var result = ser.Deserialize(typeof(TestType), input);
            Assert.IsNotNull(result);
        }
    }
}
