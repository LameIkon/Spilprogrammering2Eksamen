using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using TMPro;
using Mirror;

public class DataBaseDataStore : MonoBehaviour 
{
    //[SerializeField] private TextMeshProUGUI _leaderboarText;

    public GameObject test;

    private string _dataBase = "URI=file:database.db";
    //private string _dataBase = "Assets/Database/database.db";

    public string _Name;
    public int _Score = 0;

    
    public static DataBaseDataStore _Instance;

    private void Awake() 
    {
        if (_Instance == null)  // We make this a Singleton
        {
            _Instance = this;
            transform.SetParent(null);      // We set the parent of the this the gameObject that has this component to be its own parent, this will ensure that it will not be destroyed prematurely.
            DontDestroyOnLoad(gameObject);  // This will persist through scenes, but is not really needed in this instance but it is nice to have

        }
        else
        {
            if (_Instance != this)
            {
                Destroy(gameObject);
            }
        }

        InitDB();
    }



    public void InitDB() 
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(_dataBase)) 
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


    public void Save(string name, int score)
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(_dataBase))
        {
            sqlConnection.Open(); // Open connection to the database

            using (SqliteCommand insertCommand = sqlConnection.CreateCommand())
            {
                // This inspiration from Marco can cause SQL injection -03 karakter til Marco. We need to make it different
                //string text = "INSERT INTO leaderboard (playerName, score) VALUES ('{0}', '{1}')";
                //insertCommand.CommandText = string.Format(text, _Name, _Score);
                //insertCommand.ExecuteNonQuery();

                // Now this is what i consider a 12
                insertCommand.CommandText = "INSERT INTO Leaderboard (playerName, score) VALUES (@PlayerName, @Score)";
                insertCommand.Parameters.AddWithValue("@PlayerName", name);
                insertCommand.Parameters.AddWithValue("@Score", score);
                insertCommand.ExecuteNonQuery(); // Run the SQL code
            }
            sqlConnection.Close();

        }
    }

    public void Load()
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(_dataBase))
        {
            sqlConnection.Open();

            using (SqliteCommand command = sqlConnection.CreateCommand())
            {
                // Use the select command to find the player on the leaderboard
                command.CommandText = "SELECT playerName, score FROM Leaderboard WHERE playerName = @PlayerName";
                command.Parameters.AddWithValue("@PlayerName", _Name); // PlayerName from the scriptable object should be enough to find the wanted player

                using (IDataReader reader = command.ExecuteReader()) // Run the SELECT command 
                {
                    // Update the provided GameData object with the loaded data
                    _Name = reader.GetString(0); // Get the first value = name
                    _Score = reader.GetInt32(1); // Get the second value = score                     
                }
                sqlConnection.Close();

                // We need to set the data back to the scriptable object....
            }
        }
    }

    public void UpdateScore(string name, int score)
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(_dataBase))
        {
            sqlConnection.Open();

            using (SqliteCommand command = sqlConnection.CreateCommand())
            {
                // Use the select command to find the player on the leaderboard
                command.CommandText = "UPDATE Leaderboard SET score = @Score WHERE playerName = @PlayerName";
                command.Parameters.AddWithValue("@Score", score);
                command.Parameters.AddWithValue("@PlayerName", name); // PlayerName from the scriptable object should be enough to find the wanted player
                command.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }
    }

    public void DeleteSave(string fileName)
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(_dataBase))
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
        using (SqliteConnection sqlConnection = new SqliteConnection(_dataBase))
        {
            sqlConnection.Open();

            using (SqliteCommand command = sqlConnection.CreateCommand())
            {
                // Call the command to drop the table
                command.CommandText = "DELETE FROM Leaderboard"; // Remove all values
                command.ExecuteNonQuery(); // Run SQL code
            }
            sqlConnection.Close();
        }
    }

    public void ShowLeaderboard()
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(_dataBase))
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
                    //_leaderboarText.text = leaderboardTextContent;
                }
            }
            sqlConnection.Close();
        }
    }
}
