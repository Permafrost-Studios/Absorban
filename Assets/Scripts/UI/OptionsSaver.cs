using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public static class OptionsSaver {

    private static Object _lock;
    public static Hashtable defaultsettings = new Hashtable{
        {"main_vol",50d},
        {"music_vol",50d},
        {"sfx_vol",50d}
    };

    public static bool CheckOptionsExist() {
        return File.Exists(Application.persistentDataPath + "/options.json");
    }

    public static void WriteDefaultOpts() {
        string serialdat = JsonConvert.SerializeObject(defaultsettings);
        lock(_lock) {
            File.WriteAllText(Application.persistentDataPath + "/options.json", serialdat);
            Debug.Log("DEFAULT Options written to: " + Application.persistentDataPath + "/options.json");
        }

    }

    public static Hashtable ReadOpts() {
        if (CheckOptionsExist()) {
            lock(_lock) {
                string serialdat = File.ReadAllText(Application.persistentDataPath + "/options.json");
                return JsonConvert.DeserializeObject<Hashtable>(serialdat);
            }
        } else {
            WriteDefaultOpts();
            return defaultsettings;
        }
    }

    public static void UpdateOptions(string key, object val) {
        lock(_lock) {
            var table = ReadOpts();
            if (table.ContainsKey(key) && 
                table[key].GetType() == val.GetType()) 
            {
                table[key] = val;
                string serialdat = JsonConvert.SerializeObject(table);
                File.WriteAllText(Application.persistentDataPath + "/options.json", serialdat);
            }   
        }
    }

    // This specialisation must exist because the JSONSerializer reads/writes floats as doubles (write float reads double)
    public static void UpdateOptions(string key, float val) {
        lock(_lock) {
            var table = ReadOpts();
            if (table.ContainsKey(key) && 
                table[key] is double) 
            {
                table[key] = val;
                string serialdat = JsonConvert.SerializeObject(table);
                File.WriteAllText(Application.persistentDataPath + "/options.json", serialdat);
            }        
        }    
    }
}
