using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DebuggerUtility.Visualizers.Serialization
{
    public class JsonSerialization: SerializationBase
    {
        public override string Serialize(object target, bool formatted = false)
        {
            var json = JsonConvert.SerializeObject(target, Formatting.Indented);
            return json;
        }

        public override object Deserialize(Type targetType, string serializedString)
        {
            var obj = JsonConvert.DeserializeObject(serializedString, targetType);
            return obj;
        }
    }
}
