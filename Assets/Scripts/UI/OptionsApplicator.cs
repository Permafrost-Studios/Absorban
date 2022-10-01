using System.Collections.Generic;
using UnityEngine;
using System;

// Will require a different class for applying settings that need to be reapplied every scene

// Will get callbacks registered to them in the settingsMenu
public static class OptionsApplicator {
    public static Dictionary<string,Action<System.Object>> settingsApplicatorTable = new Dictionary<string,Action<System.Object>>{
        {"main_vol",MasterVol},
        {"music_vol",MusicVol},
        {"sfx_vol",SFXVol}
    };

    public static void MasterVol(System.Object obj) {}
    public static void MusicVol(System.Object obj) {}
    public static void SFXVol(System.Object obj) {}
}
