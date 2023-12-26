using System;
using System.Collections.Generic;
using System.IO;

namespace PlayerAlbum;

public static class File {

    public static Dictionary<int, int> LoadFile(string path) {
        Dictionary<int, int> res = new();

        using (StreamReader reader = new StreamReader(path)) {
            while (!reader.EndOfStream) {
                string line = reader.ReadLine();
                int id = int.Parse(line);
                Console.WriteLine(id);
                if (res.ContainsKey(id)) {
                    res[id]++;
                } else {
                    res[id] = 1;
                }
            }
        }

        return res;
    }

    public static void WriteFile(string path, Dictionary<int, int> dict) {
        using StreamWriter writer = new StreamWriter(path);

        foreach (int id in dict.Keys)
        {
            for (int k = 0; k < dict[id]; k++)
            {
                writer.WriteLine(id);
            }
        }
    }

    public static void AppendFile(string path, List<int> ids) {
        using StreamWriter writer = new StreamWriter(path, true);
        
        foreach (int id in ids) {
            writer.WriteLine(id);
        }
    }

    public static void AppendFile(string path, int id) {
        using StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(id);
    }

}