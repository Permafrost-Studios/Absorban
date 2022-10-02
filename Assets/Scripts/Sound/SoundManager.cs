using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SoundManager : GenericSingleton<SoundManager> {

    // Start is called before the first frame update
    void Start() {
        AsyncOperationHandle<AudioMixer> MasterMixer = Addressables.LoadAssetAsync<AudioMixer>("Assets/Master.mixer");
        
        var volumetable = OptionsSaver.ReadOpts();
		MasterMixer.Result.SetFloat("MasterVol", ( Mathf.Log( (float)(double)(volumetable["main_vol"] ), 10) * 40 * -1) );
		MasterMixer.Result.SetFloat("SFXVol", ( Mathf.Log( (float)(double)volumetable["music_vol"], 10 ) * 40 * -1) );
        MasterMixer.Result.SetFloat("MusicVol", ( Mathf.Log( (float)(double)volumetable["music_vol"] , 10 ) * 40 * -1) );
    }
}
