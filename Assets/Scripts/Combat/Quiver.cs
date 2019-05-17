using RPG.Core;
using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Combat {

    public class Quiver : MonoBehaviour, ISaveable {
        private const int MAX_STACK = 80;

        [Range(0, MAX_STACK)]
        [SerializeField] int amount;
        [SerializeField] Arrow arrowData;
        [SerializeField] private float damageBonus = 2f;

        public int Amount { get => amount; }
        public float Damage { get => arrowData.Damage + damageBonus; }
        void Start() {

        }

        public bool CanUse() {

            return amount > 0;
        }

        public Projectile Use() {
            if (amount <= 0) {
                return null;
            }
            amount--;
            return Instantiate(arrowData.Prefab);
        }

        public int Refill(int amount) {
            this.amount += amount;
            if (this.amount > MAX_STACK) {
                int amountToReturn = this.amount - MAX_STACK;
                this.amount = MAX_STACK;
                return amountToReturn;
            }
            return 0;
        }

        public void RestoreState(object state) {
            var stateDict = (Dictionary<string, object>)state;
            arrowData = (Arrow) FindObjectOfType<WeaponStorage>().Get((string)stateDict["arrow_type"]);
            amount = (int)stateDict["amount"];
        }

        public object CaptureState() {
            var quiverDictionary = new Dictionary<string, object>();
            quiverDictionary["arrow_type"] = arrowData.name;
            quiverDictionary["amount"] = amount;
            return quiverDictionary;
        }
    }
}
