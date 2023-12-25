using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace PlayerAlbum;

public static class Database {

    private static readonly SqliteConnection connection;
    private const string path = "database/players.db";

    static Database() {
        connection = new SqliteConnection($"data source = {path}");
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

    public static List<Player> GetPlayers(string query) {
        List<Player> players = new();
        SqliteCommand command = CreateCommand(query);
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read()) {
            object[] values = new object[reader.FieldCount];
            reader.GetValues(values);
            players.Add(new Player(values));
        }
        return players;
    }
}