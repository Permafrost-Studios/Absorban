using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponPersistanceManager : GenericSingleton<WeaponPersistanceManager> {
    public List<int> discoveredWeapons = new List<int>() {0};
}
