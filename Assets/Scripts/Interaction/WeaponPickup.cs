using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat; //remove this dependency when inventory system is implemented
//temporary class
namespace RPG.Interaction {

    public class WeaponPickup : MonoBehaviour, Interactable {
        [SerializeField] Texture2D cursorIcon;
        public Texture2D CursorIcon { get { return cursorIcon; } set { cursorIcon = value; } }
        [SerializeField] Weapon weapon;
        public void Interact(Interacter interacter) {
            Fighter fighter = interacter.GetComponent<Fighter>();
            if (!fighter) {
                print("Interacter is not a fighter, cannot interact with weapon pickup (temp)");
                return;
            }
            fighter.EquipWeapon(weapon);
            print("Picking up weapon..");
            Destroy(gameObject);
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}
