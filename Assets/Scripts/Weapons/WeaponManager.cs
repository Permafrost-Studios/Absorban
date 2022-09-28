using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //Uses Transform.GetChild(id)

    public List<GameObject> WeaponArchive;
    public List<GameObject> discoveredWeapons;

    // Start is called before the first frame update

    void Start() {
        discoveredWeapons =  new List<GameObject>();

        foreach (GameObject weapon in WeaponArchive)
        {
            var weaponInstance = Instantiate(weapon, gameObject.transform);
            weaponInstance.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWeapon(int ID) 
    {
        discoveredWeapons.Add(WeaponArchive[ID]);
    }
}
