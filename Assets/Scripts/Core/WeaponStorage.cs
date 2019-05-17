using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Core {
    public class WeaponStorage : MonoBehaviour {
        [SerializeField] List<ScriptableObject> weaponStorage;
        void Start() {

        }
        public bool Register(ScriptableObject asset) {
            print("Adding " + asset + " to weapon storage..");
            if (weaponStorage.Contains(asset))
                return false;
            weaponStorage.Add(asset);
            return true;
        }
        public ScriptableObject Get (string reference) {
            foreach (var item in weaponStorage) {
                if (item && item.name == reference)
                    return item;
            }
            return null;
        }
        // Update is called once per frame
        void Update() {

        }
    }
}
