using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using TMPro;
using System;

public class Database : MonoBehaviour 
{
    private string _dataBase = "URI=file:database.db";

    
    public static Database _Instance;

    private void Awake() 
    {
        if (_Instance == null)  // We make this a Singleton
        {
            _Instance = this;
            transform.SetParent(null);      // We set the parent of the this the gameObject that has this component to be its own parent, this will ensure that it will not be destroyed prematurely.
            DontDestroyOnLoad(gameObject);  // This will persist through scenes

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


    // Inititilize database
    public void InitDB() 
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(_dataBase)) 
        {
            sqlConnection.Open(); // Open connection to the database

            // Create table if it does not exist
            using (SqliteCommand createCommand = sqlConnection.CreateCommand())
            {
                createCommand.CommandText = "CREATE TABLE IF NOT EXISTS Leaderboard (playerName VARCHAR(30), score INT)";
                createCommand.ExecuteNonQuery(); // Run the SQL code
            }            
            sqlConnection.Close();

        }
    }

    // Called from player when getting instantiated
    public void Save(string name, int score, out int savedScore) // Get playername, its current score, and a score to be returned to the player
    {
        savedScore = 0;

        using (SqliteConnection sqlConnection = new SqliteConnection(_dataBase))
        {
            sqlConnection.Open(); // Open connection to the database

            // Check if the name exist in the table
            using (SqliteCommand selectCommand = sqlConnection.CreateCommand())
            {
                // Checks all rows in the table for the searched name. Returns 1 if the name is matched and 0 if not
                selectCommand.CommandText = "SELECT EXISTS (SELECT 1 FROM Leaderboard WHERE playerName = @PlayerName)"; 
                selectCommand.Parameters.AddWithValue("@PlayerName", name);
                bool exists = Convert.ToBoolean(selectCommand.ExecuteScalar()); // 1 its true, 0 its false

                // If the player already exist load that ones files and stop here
                if (exists)
                {
                    savedScore = Load(name); // Retrieve that players score from database
                }
                else // Else create new playerdata
                {
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
                }
            }
            sqlConnection.Close();
        }
    }

    public int Load(string name) // Called when a players name is already stored in the database
    {
        int score = 0; 

        using (SqliteConnection sqlConnection = new SqliteConnection(_dataBase))
        {
            sqlConnection.Open();

            using (SqliteCommand command = sqlConnection.CreateCommand())
            {
                // Use the select command to find the player on the leaderboard
                command.CommandText = "SELECT score FROM Leaderboard WHERE playerName = @PlayerName";
                command.Parameters.AddWithValue("@PlayerName", name); // PlayerName used to find the currect data

                using (IDataReader reader = command.ExecuteReader()) // Run the SELECT command 
                {
                    if (reader.Read())
                    {
                        score = reader.GetInt16(0); // Get the score 
                    }
                }             
            }
            sqlConnection.Close();
        }
        return score; // This score will then be returned back to the player script
    }

    public void UpdateScore(string name, int score) // Called whenever the score gets updated (Coins collected)
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(_dataBase))
        {
            sqlConnection.Open();

            using (SqliteCommand command = sqlConnection.CreateCommand())
            {
                // Use the select command to find the player on the leaderboard
                command.CommandText = "UPDATE Leaderboard SET score = @Score WHERE playerName = @PlayerName"; // Update that players files in the database 
                command.Parameters.AddWithValue("@Score", score);
                command.Parameters.AddWithValue("@PlayerName", name);  // PlayerName used to find the currect data
                command.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }
    }

    public void DeleteAllSaves() // Called by pressing button
    {
        using (SqliteConnection sqlConnection = new SqliteConnection(_dataBase))
        {
            sqlConnection.Open();

            using (SqliteCommand command = sqlConnection.CreateCommand())
            {
                // Call the command to delete everything in the table
                command.CommandText = "DELETE FROM Leaderboard"; // Remove all values
                command.ExecuteNonQuery(); // Run SQL code
            }
            sqlConnection.Close();
        }
    }

    #region unused code
    /// <summary>
    /// Below is the collection of unused code that with more time would perhaps been implemented
    /// </summary>

    //[SerializeField] private TextMeshProUGUI _leaderboarText;
    //private string _dataBase = "Assets/Database/database.db";
    //public string _Name;
    //public int _Score = 0;


    public void DeleteSave(string fileName) // Not being used. If project continued getting developed we would get this method to delete specific players and their scores
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


    public void ShowLeaderboard() // Not being used. If project continued getting developed we would get this method to show all players score in the game
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
    #endregion
}
