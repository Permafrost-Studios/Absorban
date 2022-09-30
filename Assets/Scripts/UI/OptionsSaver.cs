using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class OptionsSaver : ScriptableObject {
    public bool CheckOptionsExist() {
        return File.Exists(Application.persistentDataPath + "/options.json");
    }

    public void WriteDefaultOpts() {
        string serialdat = JsonConvert.SerializeObject(defaultsettings);
        File.WriteAllText(Application.persistentDataPath + "/options.json", serialdat);
        
        Debug.Log("DEFAULT Options written to: " + Application.persistentDataPath + "/options.json");
    }

    public Hashtable ReadOpts() {
        if (CheckOptionsExist()) {
            string serialdat = File.ReadAllText(Application.persistentDataPath + "/options.json");
            return JsonConvert.DeserializeObject<Hashtable>(serialdat);
        } else {
            WriteDefaultOpts();
            return defaultsettings;
        }
    }

    public void UpdateOptions(string key, object val) {
        var table = ReadOpts();
        if (table.ContainsKey(key) && 
            table[key].GetType() == val.GetType()) 
        {
            table[key] = val;
            string serialdat = JsonConvert.SerializeObject(table);
            File.WriteAllText(Application.persistentDataPath + "/options.json", serialdat);
        }            
    }

    // This specialisation must exist because the JSONSerializer reads/writes floats as doubles (write float reads double)
    public void UpdateOptions(string key, float val) {
        var table = ReadOpts();
        if (table.ContainsKey(key) && 
            table[key] is double) 
        {
            table[key] = val;
            string serialdat = JsonConvert.SerializeObject(table);
            File.WriteAllText(Application.persistentDataPath + "/options.json", serialdat);
        }            
    }

    
    public static Hashtable defaultsettings = new Hashtable{
        {"main_vol",50d},
        {"music_vol",50d},
        {"sfx_vol",50d}
    };


}
