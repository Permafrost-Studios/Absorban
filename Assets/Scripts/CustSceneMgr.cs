using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class CustSceneMgr : MonoBehaviour 
{
	
	public AudioMixer MasterMixer; 
	public AudioMixer SFXMixer; 
	public AudioMixer MusicMixer; 
	
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("donotdestroy");
    }

    // Update is called once per frame
    // TODO: Impl
    void Start() {
        
		OptionsSaver volumeoptions = new OptionsSaver();
		var volumetable = volumeoptions.ReadOpts();
		
		MasterMixer.SetFloat("MasterVol", ( Mathf.Log( (float)(double)(volumetable["main_vol"] ), 10) * 40 * -1) );
		SFXMixer.SetFloat("SFXVol", ( Mathf.Log( (float)(double)volumetable["music_vol"], 10 ) * 40 * -1) );
        MusicMixer.SetFloat("MusicVol", ( Mathf.Log( (float)(double)volumetable["music_vol"] , 10 ) * 40 * -1) );
    }


}
