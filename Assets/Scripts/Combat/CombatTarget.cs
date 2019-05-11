using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
//consider replacing this altogether with health comp..
namespace RPG.Combat {

    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour {
        Health health;
        void Awake () {
            health = GetComponent<Health>();
            health.notifyDeathObservers += OnDeath;
        }
        void Start() {
            
        }
        void OnDeath () {
            Destroy(this);
        }
        void Update() {

        }
    }
}
