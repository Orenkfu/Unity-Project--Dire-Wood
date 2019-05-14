using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/New Weapon", order = 0)]
    public class Weapon : ScriptableObject {
        [SerializeField] public AnimatorOverrideController animationOverride;
        [SerializeField] private float minDamage = 20;
        [SerializeField] private float maxDamage = 40;
        [SerializeField] GameObject weaponPrefab = null;
        [Range(0.5f, 10)]
        [SerializeField] public float attackSpeed = 2f;
        [Range(0.1f, 100)]
        [SerializeField] public float range;

        public float Damage { get { return Random.Range(minDamage, maxDamage); } }
        public void Equip(Transform hand, Animator animator) {
            if (weaponPrefab) {
                Instantiate(weaponPrefab, hand);
            }
            if (animationOverride) {
                animator.runtimeAnimatorController = animationOverride;
            }

        }


        void Update() {

        }
    }
}
