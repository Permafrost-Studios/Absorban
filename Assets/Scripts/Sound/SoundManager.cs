using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SoundManager : GenericSingleton<SoundManager> {

    private AudioMixer MasterMixer;

    private void LoadMixer() {
        AsyncOperationHandle<AudioMixer> MixerHandler = Addressables.LoadAssetAsync<AudioMixer>("Assets/Master.mixer");
        Debug.Log(MixerHandler);
        MixerHandler.WaitForCompletion();
        MasterMixer = MixerHandler.Result;
    }

    public void UpdateSoundSettings() {
        if (!MasterMixer) {
            LoadMixer();
        }

        var volumetable = OptionsSaver.ReadOpts();
        Debug.Log(volumetable["main_vol"]);

        Debug.Log(Mathf.Log( (float)(double)(volumetable["main_vol"] ), 10) * 40 * -1);

		MasterMixer.SetFloat("MasterVol", (-80 + Mathf.Log((float)(double)volumetable["main_vol"] , 10 ) * 40));
		MasterMixer.SetFloat("SFXVol", (-80 + Mathf.Log((float)(double)volumetable["sfx_vol"] , 10 ) * 40));
        MasterMixer.SetFloat("MusicVol", (-80 + Mathf.Log((float)(double)volumetable["music_vol"] , 10 ) * 40));
    }
}
