using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class DataBaseDataStore : IDataStore
{

    private readonly ISerialize _serializer;
    private readonly string _filePath;
    private readonly string _fileExtention;

    public DataBaseDataStore(ISerialize serializer) 
    {
        _filePath = Application.persistentDataPath;
        _fileExtention = ".db";
        _serializer = serializer;
    }


    public void InitDB(GameData data) 
    {
        using (SqliteConnection sqliteConnection = new SqliteConnection(Application.persistentDataPath)) 
        {
            
        
        }
    }


    public void Save(GameData data)
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(Application.persistentDataPath)) 
        {
            
        }


    }


    public GameData Load(string name)
    {
        throw new System.NotImplementedException();
    }

    public void DeleteSave(string fileName)
    {
        throw new System.NotImplementedException();
    }

    public void DeleteAllSaves()
    {
        throw new System.NotImplementedException();
    }
}
