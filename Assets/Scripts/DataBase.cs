using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;

static class MyDataBase
    {
        private const string fileName = "BDGame.bytes";
        private static string DBPath;
        public static SqliteConnection connection;
        private static SqliteCommand command;

        static MyDataBase()
        {
            DBPath = GetDatabasePath();
        }

        private static string GetDatabasePath()
        {
            return Path.Combine(Application.streamingAssetsPath, fileName);
        }

        [System.Obsolete]
        private static void UnpackDatabase(string toPath)
        {
            string fromPath = Path.Combine(Application.streamingAssetsPath, fileName);

            WWW reader = new WWW(fromPath);
            while (!reader.isDone) { }

            File.WriteAllBytes(toPath, reader.bytes);
        }

        public static void OpenConnection()
        {
            connection = new SqliteConnection("Data Source=" + DBPath);
            command = new SqliteCommand(connection);
            connection.Open();
        }

        public static void CloseConnection()
        {
            connection.Close();
            command.Dispose();
        }

        /// <summary> Этот метод выполняет запрос query. </summary>
        /// <param name="query"> Собственно запрос. </param>
        public static void ExecuteQueryWithoutAnswer(string query)
        {
            OpenConnection();
            command.CommandText = query;
            command.ExecuteNonQuery();
            CloseConnection();
        }

        public static string ExecuteQueryWithAnswer(string query)
        {
            OpenConnection();
            command.CommandText = query;
            var answer = command.ExecuteScalar();
            CloseConnection();

            if (answer != null) return answer.ToString();
            else return null;
        }

        

        public static DataTable GetTable(string query)
        {
            OpenConnection();

            SqliteDataAdapter adapter = new SqliteDataAdapter(query, connection);

            DataSet DS = new DataSet();
            adapter.Fill(DS);
            adapter.Dispose();

            CloseConnection();

            return DS.Tables[0];
        }
    }

