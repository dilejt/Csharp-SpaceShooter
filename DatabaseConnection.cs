using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SpaceShip
{
    public class DatabaseConnection
    {
        private string connectionString;
        private string tableName;
        public DatabaseConnection(string connectionString)
        {
            this.connectionString = connectionString;
            this.tableName = "leaderboard";
        }
        public void InsertData(int seconds, int score, string playerName)
        {
            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    dbConnection.Open();
                    TimeSpan time = TimeSpan.FromMilliseconds(seconds);
                    String timeString = time.ToString(@"mm\:ss\:fff");
                    string query = String.Format("INSERT INTO {0}(name,score,time) VALUES ('{1}',{2},'{3}')", tableName, playerName, score, timeString);
                    MySqlCommand command = new MySqlCommand(query, dbConnection);
                    command.ExecuteNonQuery();
                    dbConnection.Close();
                }
                catch (Exception) { }
            }

        }
        public List<string> GetData()
        {
            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
            {
                List<string> tableData = new List<string>();
                try
                {
                    dbConnection.Open();

                    string query = String.Format("SELECT name,score,time,date FROM {0}", tableName);
                    MySqlCommand command = new MySqlCommand(query, dbConnection);
                    MySqlDataReader reader = command.ExecuteReader();

                    for (int i = 0; i < reader.FieldCount; i++)
                        tableData.Add(reader.GetName(i).ToString());

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                            tableData.Add(reader.GetString(i));
                    }
                    reader.Close();
                    dbConnection.Close();
                }
                catch (Exception) { }
                return tableData;
            }
        }
    }
}
