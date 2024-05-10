using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileWriteDataStore : IDataStore
{
    private readonly ISerializer _serializer; // Making this as generic as possible we pass in a ISerialize, in this inplementation we will do it as a .json file, but we could make different inplementations if we wanted in the future.
    private readonly string _dataPath;  // We need a path to were the data will be saved
    private readonly string _fileExtension; // We need a file extension for the data we will store
    
    public FileWriteDataStore(ISerializer serializer) // As all the fields are readonly they need to be assiged in a consturctor
    {
        _dataPath = Application.persistentDataPath; // the place we want to store the data, this will be in the AppData folder.
        _fileExtension = ".json"; // we are storing it as a .json file, but this could become more generic, then it would be assigned in the constructor
        _serializer = serializer; // The ISerialize we will passing 
    }

    private string GetFilePath(string fileName) 
    {
        return Path.Combine(_dataPath, string.Concat(fileName, _fileExtension)); // Here we find the location of our path to the file we would like to save in.
    }

    public void Save(GameData data)
    {
        string fileLocation = GetFilePath(data._FileName);  // We get the file location
        
        File.WriteAllText(fileLocation, _serializer.Serialize(data)); // we pass the file to the serializer so it can handle that implementation
    }

    public GameData Load(string fileName)
    {
        string fileLocation = GetFilePath(fileName); // We get the file location

        if (!File.Exists(fileLocation)) 
        {
            throw new IOException($"{fileName} does not exist in folder"); // Checking that it exists in the location
        } 

        return _serializer.Deserialize<GameData>(File.ReadAllText(fileLocation)); // we pass the file to the serializer
    }

    public void DeleteSave(string fileName) 
    {
        string fileLocation = GetFilePath(fileName);

        if (File.Exists(fileLocation)) 
        {
            File.Delete(fileLocation);
        }
    }

}
