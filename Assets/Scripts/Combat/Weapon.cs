using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/New Weapon", order = 0)]
    [Serializable]
    public class Weapon : ScriptableObject {
        [SerializeField] public AnimatorOverrideController animationOverride;
        [SerializeField] private float minDamage = 20;
        [SerializeField] private float maxDamage = 40;
        [SerializeField] GameObject weaponPrefab = null;
        [Range(0.5f, 10)]
        [SerializeField] public float attackSpeed = 2f;
        [Range(0.1f, 100)]
        [SerializeField] public float range;
        [SerializeField] public bool isRightHanded = true;

        GameObject spawnedWeapon;

        public float Damage { get { return UnityEngine.Random.Range(minDamage, maxDamage); } }

        /**
         * returns the weapon equipped
         */ 
        public GameObject Equip(Transform rightHand, Transform leftHand, Animator animator) {
            if (weaponPrefab) {
                Transform handToUse = isRightHanded ? rightHand : leftHand;
                spawnedWeapon = Instantiate(weaponPrefab, handToUse);
            }
            if (animationOverride) {
                animator.runtimeAnimatorController = animationOverride;
            }
            return spawnedWeapon;

        }


        void Update() {

        }
    }
}
