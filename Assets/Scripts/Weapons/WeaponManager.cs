using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    //Use Transform.GetChild(id)
    //Possibly use Transform.childCount;
    public List<GameObject> WeaponArchive;
    public List<GameObject> discoveredWeapons;

    // WARNING: The 9th element will be Alphanumeric 0
    [SerializeField] private InputAction[] m_actions = new InputAction[] {
        new InputAction(type: InputActionType.Button, binding: "<Keyboard>/1"),
        new InputAction(type: InputActionType.Button, binding: "<Keyboard>/2"),
        new InputAction(type: InputActionType.Button,binding: "<Keyboard>/3"),
        new InputAction(type: InputActionType.Button,binding: "<Keyboard>/4"),
        new InputAction(type: InputActionType.Button,binding: "<Keyboard>/5"),
        new InputAction(type: InputActionType.Button,binding: "<Keyboard>/6"),
        new InputAction(type: InputActionType.Button,binding: "<Keyboard>/7"),
        new InputAction(type: InputActionType.Button,binding: "<Keyboard>/8"),
        new InputAction(type: InputActionType.Button,binding: "<Keyboard>/9"),
        new InputAction(type: InputActionType.Button,binding: "<Keyboard>/0")
    };

    private int currentActive;

    // Start is called before the first frame update

    void Start() {
        // All in child-indexes, so 0-9
        currentActive = 0;
        discoveredWeapons =  new List<GameObject>();
        RegisterCallbacks();

        // Stub();
    }

    void RegisterCallbacks() {
        for (int i = 0; i < m_actions.Length; i++) {
            // Otherwise i is passed by reference which makes all of the OnSwitchWeaponID calls contain the value 10 lmao
            var copy = i;
            m_actions[i].performed += _ => OnSwitchWeaponID(copy);       
        }
    }

    void OnEnable() {
        foreach (var item in m_actions) {
            item.Enable();
        }
    }

    void OnDisable() {
        foreach (var item in m_actions) {
            item.Disable();
        }
    }

    private void OnSwitchWeaponID(int keyval) {   
        //Child id starts at 0;
        gameObject.transform.GetChild(currentActive)
            .gameObject.SetActive(false);
        gameObject.transform.GetChild(keyval).gameObject
            .SetActive(true);
        currentActive = keyval;
    }

    private void OnSwitchWeapon(InputValue value) {
        var switchreq = value.Get<Vector2>();
        switch (switchreq.y) {
            case > 0f:
                // mWheel moved forwards
                // When highest index loop around & Modulo over highest index
                OnSwitchWeaponID((currentActive+1)%m_actions.Length);
                break;

            case < 0f:
                // mWheel moved backwards (into palm)
                OnSwitchWeaponID(((currentActive+(m_actions.Length-1))%m_actions.Length));
                break;            
        }
    }

    // When picked up 
    public void AddWeapon(int ID) 
    {
        discoveredWeapons.Add(WeaponArchive[ID]);

        var weaponInstance = Instantiate(WeaponArchive[ID], this.gameObject.transform);
        weaponInstance.SetActive(false);
    }

    // private void Stub() 
    // {
    //     AddWeapon(0);
    //     AddWeapon(1);
    // }
}
