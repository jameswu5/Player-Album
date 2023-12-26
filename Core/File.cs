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

    public static void SaveFile(string path, Dictionary<int, int> dict) {
        using StreamWriter writer = new StreamWriter(path);
        
        foreach (int id in dict.Keys)
        {
            for (int k = 0; k < dict[id]; k++)
            {
                writer.WriteLine(id);
            }
        }
    }

}