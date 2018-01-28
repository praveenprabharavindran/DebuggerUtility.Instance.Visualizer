/*************************************************************************
*
*   Class/Module Name: SerializationBase
*
*   Date Created:  06/June/2012, Praveen P R
*
*   Purpose: Base class for serialization classes.
*
*   Notes:
*
*   Date            Author          Description
*   ----            ------          -----------
*   06/June/2012    Praveen P R     Created the class file.
*************************************************************************/

namespace DebuggerUtility.Visualizers.Serialization
{
    using System;

    public abstract class SerializationBase
    {
        /// <summary>
        /// Serializes the target object into a string.
        /// </summary>
        /// <param name="target">object to be serialized.</param>
        /// <param name="formatted">indicates whether the serialized output needs to be formatted or not.</param>
        /// <returns>Serialized string.</returns>
        public abstract string Serialize(object target, bool formatted = false);

        /// <summary>
        /// Deserializes a string to an object.
        /// </summary>
        /// <param name="targetType">Type of the target object into which the serialized string will be deserialized.</param>
        /// <param name="serializedString">The serialized string to be deserialized.</param>
        /// <returns>The deserialized object.</returns>
        public abstract object Deserialize(Type targetType, string serializedString);
    }
}
