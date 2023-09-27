/*
 * Associated Package : Serialized Field Save System
 * Author: Wyatt Murray
 * Version: 1
 * Date: 9/21/23
 * 
 * Description:
 * The SerializationSurrogates class is designed to provide a comprehensive understanding of serialization and serialization surrogates in the context of C#
 * 
 */
using System.Runtime.Serialization;
using UnityEngine;
public class SerializationSurrogates
{
        //KEYWORDS
    //Serialization is the process of converting data structures or object states into a format that can be stored


    //example of C# code vs Json serialized
    /* C# data */
    string[] names = new string[] { "Alice", "Bob", "Carol" };
    /* That same array, serialized in JSON (commented so this class can compile) :  ["Alice", "Bob", "Carol"]  */


        //Setup and explanation of ISerializationSurrogate
    // A serialization surrogate is a class that provides custom serialization and deserialization for a specific type of object. It is used when the default serialization and deserialization mechanisms are not sufficient or when you need to customize the serialization and deserialization process.

    // To create a serialization surrogate, you must implement the `ISerializationSurrogate` interface. This interface has two methods: `GetObjectData()` and `SetObjectData()`.

    // The `GetObjectData()` method is called to serialize the object. It takes the object to be serialized and a `SerializationInfo` object as parameters. The `SerializationInfo` object stores the serialized data.

    // The `SetObjectData()` method is called to deserialize the object. It takes the object to be deserialized and a `SerializationInfo` object as parameters. The `SerializationInfo` object contains the serialized data.

    // Serialization surrogates can be used to customize the serialization and deserialization process in a variety of ways. For example, you can use a serialization surrogate to:

    // * Serialize and deserialize objects in a different format.
    // * Serialize and deserialize only certain properties of an object.
    // * Serialize and deserialize objects in a specific order.
    // * Serialize and deserialize objects to and from a specific data source.

        //description of the parameters for the get and set object data interface methods
    //obj: The object to be deserialized.This is the object that will be populated with the data from the info object.
    //info: A SerializationInfo object that contains the serialized data. The serialized data is a collection of key-value pairs, where the keys are the names of the object's properties and the values are the serialized values of those properties.
    //context: A StreamingContext object that provides information about the context in which the object is being deserialized. This information is typically used to determine the culture to use when deserializing the object's data.
    //selector: An ISurrogateSelector object that is used to select surrogates for deserializing any nested objects in the obj object. A surrogate is a class that provides custom serialization and deserialization for a specific type of object.


        //Filling the data inside the serialization surrogates
    // Reminder: (info) is considered the variable. Extra parentheses are required for usage.

    // Think of SerializationInfo as a dictionary, where you assign a key to a value.
    // The format is: (SerializationInfo).AddValue("(name of key)", (value));

    // To retrieve the info, create an empty object of the type of data you wish to serialize, and use the GetValue method to retrieve it.
    // The format is: ((datatype))(DataObject.Variable).GetValue("(key)", typeof((datatype)))

        //specifics of the methods
    //GetObjectData is add the values
    //SetObjectData retrieves the values and returns it as the data object requested.



        //to use the surrogates we make, add them to the serialization managers binary formatter
    //SurrogateSelector selector = new SurrogateSelector();

    //add and initialize surrogates here
    //(Surrogate) (surrogate Name) = new (Surrogate)();
    //(selector).AddSurrogate(typeof(Surrogate),new StreamingContext(StreamingContextStates), (Surrogate));

        //More in depth on Streaming Context
    // The StreamingContext(StreamingContextStates) constructor creates a new StreamingContext object with the specified StreamingContextStates.
    // The StreamingContext object provides information about the context in which serialization or deserialization is occurring. This information can be used to customize the serialization and deserialization process.
    // The StreamingContextStates enumeration specifies the state of the serialization or deserialization operation. The following are the possible values of the StreamingContextStates enumeration:

    // * All: The serialization or deserialization operation is occurring across process boundaries.
    // * Clone: The serialization or deserialization operation is occurring within the same process.
    // * CrossAppDomain: The serialization or deserialization operation is occurring across application domain boundaries.
    // * CrossMachine: The serialization or deserialization operation is occurring across machine boundaries.
    // * Default: The serialization or deserialization operation is occurring within the same process and application domain.
    // * File: The serialization or deserialization operation is occurring to or from a file.
    // * Other: The serialization or deserialization operation is occurring in an unspecified context.

    // The StreamingContext object can be passed to the Formatter.Serialize()` and `Formatter.Deserialize()` methods to customize the serialization and deserialization process.
    // For example, you can use the StreamingContext object to specify the culture to use when serializing or deserializing objects.

}


//Add Serialization surrogates as needed for non serializable data types

public class Vector3SerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        Vector3 v3 = (Vector3)obj;
        info.AddValue("x", v3.x);
        info.AddValue("y", v3.y);
        info.AddValue("z", v3.z);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        Vector3 v3 = (Vector3)obj;
        v3.x = (float)info.GetValue("x", typeof(float));
        v3.y = (float)info.GetValue("y", typeof(float));
        v3.z = (float)info.GetValue("z", typeof(float));
        obj = v3;
        return obj;
    }
}

public class QuaternionSerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        Quaternion quaternion = (Quaternion)obj;
        info.AddValue("x", quaternion.x);
        info.AddValue("y", quaternion.y);
        info.AddValue("z", quaternion.z);
        info.AddValue("w", quaternion.w);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        Quaternion quaternion = (Quaternion)obj;
        quaternion.x = (float)info.GetValue("x", typeof(float));
        quaternion.y = (float)info.GetValue("y", typeof(float));
        quaternion.z = (float)info.GetValue("z", typeof(float));
        quaternion.w = (float)info.GetValue("w", typeof(float));
        obj = quaternion; 
        return obj;
    }
}

