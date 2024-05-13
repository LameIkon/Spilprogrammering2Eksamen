#pragma warning disable IDE1006 // Naming Styles
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Mirror;

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
        _dataPath = Application.dataPath;
        _serializer = new JsonSerializer();
        _playerData = new PlayerData();
    }

    private void Start() 
    {
        _fileName = _playerMovement._Name;

        if (File.Exists(GetFilePath(_fileName)))
        {
            Load(_fileName);
        }
        else 
        {
            Save(_fileName);
        }
    }

    private void Update() 
    {
        _playerData._Position = transform.position;
        _playerData._Rotation = transform.rotation;
        _playerData._Velocity = _playerMovement._rb.velocity;
        Save(_fileName );
    }

    private string GetFilePath(string fileName) 
    {
        return Path.Combine(_dataPath, string.Concat(fileName, _fileExtention));
    }

    public void Save(string fileName) 
    {
        string fileLocation = GetFilePath(fileName);

        File.WriteAllText(fileLocation, _serializer.Serialize(_playerData));
    }

    public void Load(string fileName) 
    {
        string fileLocation = GetFilePath(fileName); // We get the file location

        _playerData = _serializer.Deserialize<PlayerData>(File.ReadAllText(fileLocation)); // we pass the file to the serializer

        transform.position = _playerData._Position;
        transform.rotation = _playerData._Rotation;
        _playerMovement._rb.velocity = _playerData._Velocity;
    }

}
