using RKSignage.Server.Models;
using Microsoft.Data.Sqlite;

namespace RKSignage.Server
{
    public static class DBManager
    {
        public static List<Slide> slidesList = new List<Slide>();
        public static List<Playlist> playLists = new List<Playlist>();

        public static List<Slide> testSlides = new List<Slide>() // test objects to populate database before front end management built
        {
            new Slide() {ID = 1, Path = "https://localhost:7174/StaticFiles/River1.jpg"},
            new Slide() {ID = 2, Path = "https://localhost:7174/StaticFiles/River2.jpg"},
            new Slide() {ID = 3, Path = "https://localhost:7174/StaticFiles/River3.jpg"},
            new Slide() {ID = 4, Path = "https://localhost:7174/StaticFiles/River4.jpg"}
        };

        private static string connectionString = @"Data Source=Database\Signage.db";


        public static void CreateTable(string tableName)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = $@"CREATE TABLE IF NOT EXISTS {tableName} (id INTEGER PRIMARY KEY AUTOINCREMENT, Path TEXT)";

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqliteException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public static void WriteTable(List<Slide> slideData, string tableName)
        {
            using (var connection = new SqliteConnection(connectionString))
            {

                foreach (var slide in slideData)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"INSERT INTO {tableName}(Path) VALUES('{slide.Path}')";

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqliteException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        public static List<Slide> ViewTable(string tableName)
        {
            List<Slide> tempSlides = new List<Slide>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = $"SELECT * FROM {tableName} ";

                SqliteDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tempSlides.Add(
                            new Slide
                            {
                                ID = reader.GetInt32(0),
                                Path = reader.GetString(1)
                            });
                    }
                }
                else
                {
                    Console.WriteLine($"No Rows found in {tableName}");
                }

                command.Dispose();
                connection.Dispose();
            }
            return tempSlides;
        }

        public static List<Playlist> ViewTables()
        {
            List<Playlist> tempTables = new List<Playlist>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = $"SELECT name FROM sqlite_master WHERE type='table'";

                SqliteDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tempTables.Add(
                            new Playlist
                            {
                                Name = reader.GetString(0)
                            });
                    }
                }
                else
                {
                    Console.WriteLine($"No tables found");
                }

                command.Dispose();
                connection.Dispose();
            }
            return tempTables;
        }

        public static void ClearTable(string tableName)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = $@"DELETE FROM {tableName}; VACUUM; DELETE FROM SQLITE_SEQUENCE WHERE NAME='{tableName}'";

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqliteException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}
