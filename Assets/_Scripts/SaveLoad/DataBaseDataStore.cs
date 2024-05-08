using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting.Dependencies.Sqlite;
using System.Data;

public class DataBaseDataStore : IDataStore
{

    private readonly ISerialize _serializer;
    private readonly string _filePath;
    private readonly string _fileExtention;

    //private string _dataBase = "URI=file:databaseTest.db";

    public DataBaseDataStore(ISerialize serializer) 
    {
        _filePath = Application.persistentDataPath;
        _fileExtention = ".db";
        _serializer = serializer;
    }

    public void InitDB(GameData data) 
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(Application.persistentDataPath)) 
        {
            sqlConnection.Open(); // Open connection to the database

            // Retrieve all values from the scripable object here
            string playerName = null; //data.playernameData;
            int score = 0;//data.score;
            


            // Create table
            using (SqliteCommand createCommand = sqlConnection.CreateCommand())
            {
                createCommand.CommandText = "CREATE TABLE IF NOT EXISTS Leaderboard (playerName VARCHAR(30), score INT)";
                createCommand.ExecuteNonQuery(); // Send to the DB
            }            
            sqlConnection.Close();

        }
    }


    public void Save(GameData data)
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(Application.persistentDataPath)) 
        {
            // Retrieve all values from the scripable object here -- needs to be changed
            string playerName = null; //data.playernameData;
            int score = 0;//data.score;

            sqlConnection.Open(); // Open connection to the database

            using (SqliteCommand insertCommand = sqlConnection.CreateCommand())
            {
                // This inspiration from Marco can cause SQL injection -03 karakter til Marco. We need to make it different
                //string text = "INSERT INTO leaderboard (playerName, score) VALUES ('{0}', '{1}')";
                //insertCommand.CommandText = string.Format(text, playerName, score);
                //insertCommand.ExecuteNonQuery();

                // Now this is what i consider a 12
                insertCommand.CommandText = "INSERT INTO Leaderboard (playerName, score) VALUES (@PlayerName, @Score)";
                insertCommand.Parameters.AddWithValue("@PlayerName", playerName);
                insertCommand.Parameters.AddWithValue("@Score", score);
                insertCommand.ExecuteNonQuery();
            }

        }
    }

    public void ShowRecords()
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(Application.persistentDataPath))
        {
            sqlConnection.Open();

            using (SqliteCommand command = sqlConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Leaderboard";

                // Read all values from the database
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Print values from database
                        Debug.Log("Name: " + reader["playerName"] + " Score: " + reader["Score"]);
                    }
                }
            }
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
