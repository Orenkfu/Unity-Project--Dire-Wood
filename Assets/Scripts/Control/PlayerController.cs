using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.Interaction;

namespace RPG.Control {
    public class PlayerController : MonoBehaviour {

        private Mover mover;
        private Fighter fighter;
        private bool controlEnabled = true;
        private MouseController mouse;
        private Interacter interacter;


        void Awake() {

            interacter = GetComponent<Interacter>();
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }
        void Start() {
            mouse = Camera.main.GetComponent<MouseController>();
            mouse.notifyEnemyObservers += OnTargetHover;
            mouse.notifyWalkableObservers += OnWalkableHover;
            mouse.notifyInteractableObservers += OnInteractableHover;
            GetComponent<Health>().notifyDeathObservers += OnDeath;
        }

        void OnDeath () {
            Destroy(this);
        }
        void Update() {
        }

        void OnTargetHover(CombatTarget target) {
            if (!controlEnabled)
                return;
            if (!fighter.CanAttack(target.gameObject)) return;
            if (Input.GetMouseButton(0)) {
                interacter.Target = null;
                fighter.Attack(target.gameObject);
            }
        }

        void OnInteractableHover(Interactable interactable) {
            if (!controlEnabled)
                return;
            
            if (Input.GetMouseButton(0)) {
                interacter.Target = interactable;
                //interactable.Interact();
            }
        }
        void OnWalkableHover(Vector3 destination) {
            if (!controlEnabled)
                return;
            if (Input.GetMouseButton(0)) {
                interacter.Target = null;
                fighter.setTarget(null);
                mover.StartMove(destination, 1f);
            }
        }

        public void EnableControl () {
            controlEnabled = true;
        }
        public void DisableControl () {
            GetComponent<ActionScheduler>().CancelCurrentAction();
            controlEnabled = false;
        }

    }
}
