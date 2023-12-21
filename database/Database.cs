using System;
using System.Collections.Generic;
using System.Data;
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

    public static void DisplayTable(string table, int limit = 5) {
        DisplayQueryResults($"SELECT * FROM {table} LIMIT {limit}");
    }

    public static List<Player> GetPlayers(string league, int count) {
        List<Player> players = new();
        SqliteCommand command = CreateCommand(
            $"SELECT * FROM Player WHERE League = '{league}' ORDER BY RANDOM() LIMIT {count}"
        );
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read()) {
            object[] values = new object[reader.FieldCount];
            reader.GetValues(values);
            players.Add(new Player(values));
        }
        return players;
    }
}