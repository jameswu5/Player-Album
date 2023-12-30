using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace PlayerAlbum;

public static class Database {

    private static readonly SqliteConnection connection;

    static Database() {
        connection = new SqliteConnection($"data source = {Constants.DatabasePath}");
        connection.Open();
        Console.WriteLine("Connection successfully opened");
    }

    private static SqliteCommand CreateCommand(string query) {
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = query;
        return command;
    }

    public static void DisplayQueryResults(string query) {
        SqliteCommand command = CreateCommand(query);
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read()) {
            for (int i = 0; i < reader.FieldCount; i++) {
                Console.Write(reader[i] + "\t");
            }
            Console.WriteLine();
        }
    }

    public static List<string> GetDistinctColumn(string query) {
        List<string> res = new();
        SqliteCommand command = CreateCommand(query);
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read()) {
            res.Add(reader[0].ToString());
        }
        return res;
    }

    public static List<object[]> GetColumns(string query) {
        List<object[]> res = new();
        SqliteCommand command = CreateCommand(query);
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read()) {
            object[] values = new object[reader.FieldCount];
            reader.GetValues(values);
            res.Add(values);
        }
        return res;
    }

    public static void DisplayTable(string table, int limit = 5) {
        DisplayQueryResults($"SELECT * FROM {table} LIMIT {limit}");
    }

    public static List<Player> GetPlayers(string query, bool initialise = false) {
        List<Player> players = new();
        List<object[]> valuesList = GetColumns(query);

        if (initialise)
        {
            foreach (object[] values in valuesList) {
                Player player = new Player(values);
                players.Add(player);
            }
        }
        else
        {
            foreach (object[] values in valuesList) {
                int id = (int)(long)values[0];
                players.Add(Setup.PlayerMap[id]);
            }
        }

        return players;
    }
}