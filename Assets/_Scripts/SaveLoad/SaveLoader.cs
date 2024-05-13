#pragma warning disable IDE1006 // Naming Styles
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoader : MonoBehaviour
{
    private string _dataPath;
    public string _fileName;
    private const string _fileExtention = ".json";
    public PlayerData _playerData;
    private PlayerMovement _playerMovement;

    ISerializer _serializer;

    private void Awake() 
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _dataPath = Application.persistentDataPath;     // This will be saved in the AppData Folder
        _serializer = new JsonSerializer();
        _playerData = new PlayerData();
    }

    private void Start() 
    {
        _fileName = _playerMovement._Name;

        if (File.Exists(GetFilePath(_fileName)))    // Loading the file if it exists.
        {
            Load(_fileName);
        }
        else 
        {
            Save(_fileName);    // Making a save, such that we know that a PlayerData exists, such that we can save data into it.
        }
    }

    private void Update() 
    {
        _playerData._Position = transform.position;     // Setting the transform in the PlayerData
        _playerData._Rotation = transform.rotation;
        _playerData._Velocity = _playerMovement._Rb.velocity;   // Setting the velocity in the PlayerData
        Save(_fileName);
    }

    private string GetFilePath(string fileName) 
    {
        return Path.Combine(_dataPath, string.Concat(fileName, _fileExtention)); // Here we get the file path, so we can get the files.
    }

    public void Save(string fileName) 
    {
        string fileLocation = GetFilePath(fileName);    // Get the fileLocation

        File.WriteAllText(fileLocation, _serializer.Serialize(_playerData));    // Serialize the data and write the text.
    }

    public void Load(string fileName) 
    {
        string fileLocation = GetFilePath(fileName); // We get the file location

        _playerData = _serializer.Deserialize<PlayerData>(File.ReadAllText(fileLocation)); // we pass the file to the serializer

        transform.position = _playerData._Position;
        transform.rotation = _playerData._Rotation;
        _playerMovement._Rb.velocity = _playerData._Velocity;
    }

}
