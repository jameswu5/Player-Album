using System;
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

    public static void DisplayTable(string table, int limit = 5) {
        SqliteCommand command = CreateCommand($"SELECT * FROM {table} LIMIT {limit}");
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read()) {
            for (int i = 0; i < reader.FieldCount; i++) {
                Console.Write(reader[i] + "\t");
            }
            Console.WriteLine();
        }
    }
}