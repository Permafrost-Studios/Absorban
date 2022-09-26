using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustSceneMgr : MonoBehaviour {
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("donotdestroy");
    }

    // Update is called once per frame
    // TODO: Impl
    void Start() {
        SceneManager.LoadScene(2);
    }


}
