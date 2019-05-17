using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Combat {
    [CreateAssetMenu(fileName = "Arrow Stack", menuName = "Weapons/New Arrow Type", order = 0)]
    public class Arrow : ScriptableObject {
        [SerializeField] float baseDamage;

        //TODO: change arrow type to enum
        [SerializeField] string type;
        [SerializeField] int amount;
        [SerializeField] Projectile arrowPrefab;
        public float Damage { get => baseDamage; set => baseDamage = value; }
        public string Type { get => type; set => type = value; }
        private const int MAX_STACK = 80;
        public Projectile Prefab { get => arrowPrefab; }

        public bool CanUse () {
            
            return amount > 0;
        }

        public Projectile Use() {
            if (amount <= 0) {
                return null;
            }
            amount--;
            return Instantiate(arrowPrefab);
        }

        public int Refill (int amount, string type) {
            this.amount += amount;
            if (this.amount > MAX_STACK) {
                int amountToReturn = this.amount - MAX_STACK;
                this.amount = MAX_STACK;
                return amountToReturn;
            }
            return 0;
        }
    }
}
