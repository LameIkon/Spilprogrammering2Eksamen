using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using TMPro;

public class DataBaseDataStore : IDataStore
{

    private readonly ISerialize _serializer;
    private readonly string _filePath;
    private readonly string _fileExtention;
    [SerializeField] private TextMeshProUGUI _leaderboarText;

    //private string _dataBase = "URI=file:databaseTest.db";

    public DataBaseDataStore(ISerialize serializer) 
    {
        _filePath = Application.persistentDataPath;
        _fileExtention = ".db";
        _serializer = serializer;
    }

    public void InitDB(GameData data) 
    {
        // This should be called when the the player get loaded
        using (SqliteConnection sqlConnection = new SqliteConnection(Application.persistentDataPath)) 
        {
            sqlConnection.Open(); // Open connection to the database

            // Create table
            using (SqliteCommand createCommand = sqlConnection.CreateCommand())
            {
                createCommand.CommandText = "CREATE TABLE IF NOT EXISTS Leaderboard (playerName VARCHAR(30), score INT)";
                createCommand.ExecuteNonQuery(); // Run the SQL code
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
                insertCommand.ExecuteNonQuery(); // Run the SQL code
            }
            sqlConnection.Close();

        }
    }

    public void Load(GameData data)
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(Application.persistentDataPath))
        {
            // Retrieve all values from the scripable object here -- needs to be changed
            string playerName = null; //data.playernameData;
            int score = 0;//data.score;

            sqlConnection.Open();

            using (SqliteCommand command = sqlConnection.CreateCommand())
            {
                // Use the select command to find the player on the leaderboard
                command.CommandText = "SELECT playerName, score FROM Leaderboard WHERE playerName = @PlayerName";
                command.Parameters.AddWithValue("@PlayerName", playerName); // PlayerName from the scriptable object should be enough to find the wanted player

                using (IDataReader reader = command.ExecuteReader()) // Run the SELECT command 
                {
                    // Update the provided GameData object with the loaded data
                    playerName = reader.GetString(0); // Get the first value = name
                    score = reader.GetInt32(1); // Get the second value = score                     
                }
                sqlConnection.Close();

                // We need to set the data back to the scriptable object....
            }
        }
    }

    public GameData Load(string name)
    {
        throw new System.NotImplementedException();       
    }

    public void DeleteSave(string fileName)
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(Application.persistentDataPath))
        {
            sqlConnection.Open();

            using (SqliteCommand command = sqlConnection.CreateCommand())
            {
                // Call the command to delete records of that player
                command.CommandText = "DELETE FROM Leaderboard WHERE playerName = @FileName";
                command.Parameters.AddWithValue("@FileName", fileName); // by getting the name of the player from the file we should be able to find the currect player and delete it
                command.ExecuteNonQuery(); // Run SQL code
            }
            sqlConnection.Close();
        }
    }

    public void DeleteAllSaves()
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(Application.persistentDataPath))
        {
            sqlConnection.Open();

            using (SqliteCommand command = sqlConnection.CreateCommand())
            {
                // Call the command to drop the table
                command.CommandText = "DROP TABLE IF EXISTS Leaderboard"; // Nuke the table if it exist
                command.ExecuteNonQuery(); // Run SQL code
            }
            sqlConnection.Close();
        }
    }

    public void ShowLeaderboard()
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
                    string leaderboardTextContent = ""; // ensure the string is empty

                    while (reader.Read()) // reads from row to row in the table
                    {
                        // Get the player name and score for the text and put to next line
                        leaderboardTextContent += "Name: " + reader["playerName"] + " Score: " + reader["Score"] + "\n"; // Makes a readable leaderboard to be seen ingame
                    }

                    // the table assigns all text to the text component
                    _leaderboarText.text = leaderboardTextContent;
                }
            }
            sqlConnection.Close();
        }
    }
}
