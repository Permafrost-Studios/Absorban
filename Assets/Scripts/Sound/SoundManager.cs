using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : GenericSingleton<SoundManager> {
    public UnityEngine.UIElements.UIDocument doc;
    public AudioMixer MasterMixer; 
	public AudioMixer SFXMixer; 
	public AudioMixer MusicMixer; 

    // Start is called before the first frame update
    void Start() {
        var volumetable = OptionsSaver.ReadOpts();
		MasterMixer.SetFloat("MasterVol", ( Mathf.Log( (float)(double)(volumetable["main_vol"] ), 10) * 40 * -1) );
		SFXMixer.SetFloat("SFXVol", ( Mathf.Log( (float)(double)volumetable["music_vol"], 10 ) * 40 * -1) );
        MusicMixer.SetFloat("MusicVol", ( Mathf.Log( (float)(double)volumetable["music_vol"] , 10 ) * 40 * -1) );
    }

    // Update is called once per frame
    void Update() {
        
    }
}
