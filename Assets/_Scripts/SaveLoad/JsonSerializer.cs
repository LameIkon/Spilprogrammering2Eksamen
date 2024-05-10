using UnityEngine;

// This is the concrete implementation of how to serialize .json files.
public class JsonSerializer : ISerializer // It implements the ISerialize interface therefore we know it needs to have a way of serializing and deserializing
{
    public string Serialize<T>(T obj)
    {
        return JsonUtility.ToJson(obj); // The ToJson method returns a string which is want we will use to save data.
    }

    public T Deserialize<T>(string json)
    {
        return JsonUtility.FromJson<T>(json); // Likewise the FromJson returns a generic type which we can use to load data.
    }   

}
