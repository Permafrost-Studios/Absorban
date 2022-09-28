using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //Use Transform.GetChild(id)
    //Possibly use Transform.childCount;

    public List<GameObject> WeaponArchive;
    public List<GameObject> discoveredWeapons;

    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
    };

    private int currentActive;

    // Start is called before the first frame update

    void Start() 
    {
        currentActive = 1;
        discoveredWeapons =  new List<GameObject>();

        // Stub();
    }

    // Update is called once per frame
    void Update()
    {
        for (int key = 0 ; key < keyCodes.Length; key++)
        {
            if(Input.GetKeyDown(keyCodes[key]))
            {
                gameObject.transform.GetChild(key).gameObject.SetActive(true); //Child id starts at 0;
                gameObject.transform.GetChild(currentActive).gameObject.SetActive(false);

                currentActive = key;
            }
        }
    }

    public void AddWeapon(int ID) 
    {
        discoveredWeapons.Add(WeaponArchive[ID]);

        var weaponInstance = Instantiate(WeaponArchive[ID], gameObject.transform);
        weaponInstance.SetActive(false);
    }

    // private void Stub() 
    // {
    //     AddWeapon(0);
    //     AddWeapon(1);
    // }
}
