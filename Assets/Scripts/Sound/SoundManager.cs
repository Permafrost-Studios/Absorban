using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SoundManager : GenericSingleton<SoundManager> {

    private AudioMixer MasterMixer;

    // Start is called before the first frame update
    void Start() {
        MasterMixer = Addressables.LoadAssetAsync<AudioMixer>("Assets/Master.mixer").Result;
    }

    public void UpdateSoundSettings() {
        var volumetable = OptionsSaver.ReadOpts();
		MasterMixer.SetFloat("MasterVol", ( Mathf.Log( (float)(double)(volumetable["main_vol"] ), 10) * 40 * -1) );
		MasterMixer.SetFloat("SFXVol", ( Mathf.Log( (float)(double)volumetable["music_vol"], 10 ) * 40 * -1) );
        MasterMixer.SetFloat("MusicVol", ( Mathf.Log( (float)(double)volumetable["music_vol"] , 10 ) * 40 * -1) );
    }
}
