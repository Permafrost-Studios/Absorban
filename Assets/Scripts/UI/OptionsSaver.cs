using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class OptionsSaver : MonoBehaviour
{
    void Awake() {
    }

    bool CheckOptionsExist() {
        return File.Exists(Application.persistentDataPath + "/options.json");
    }

    void WriteDefaultOpts() {
        if (!CheckOptionsExist()) {
            OptionData dat = new OptionData();
            dat.mastervolume = 1f;
            dat.sfxvolume = 1f;
            dat.musicvolume = 1f;
            string serialdat = JsonConvert.SerializeObject(dat);

            File.WriteAllText(Application.persistentDataPath + "/options.json", serialdat);
        }
    }

    OptionData ReadOpts() {
        if (!CheckOptionsExist()) {
            string serialdat = File.ReadAllText(Application.persistentDataPath + "/options.json");
            return JsonConvert.DeserializeObject<OptionData>(serialdat);
        } else {
            WriteDefaultOpts();
            OptionData dat = new OptionData();
            dat.mastervolume = 1f;
            dat.sfxvolume = 1f;
            dat.musicvolume = 1f;
            return dat;
        }
    }

    void UpdateOptions(OptionData newdat) {
        string serialdat = JsonConvert.SerializeObject(newdat);
        File.WriteAllText(Application.persistentDataPath + "/options.json", serialdat);
    }

    [System.Serializable]
    public struct OptionData {
        public float mastervolume;
        public float musicvolume;
        public float sfxvolume;
    }

}
