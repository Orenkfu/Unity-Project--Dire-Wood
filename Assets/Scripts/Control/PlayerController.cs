using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control {
    public class PlayerController : MonoBehaviour {

        private Mover movementController;
        private Fighter fighter;
        private bool controlEnabled = true;
        private MouseController mouse;

        void Start() {
            mouse = Camera.main.GetComponent<MouseController>();
            mouse.notifyEnemyObservers += OnTargetHover;
            mouse.notifyWalkableObservers += OnWalkableHover;
            GetComponent<Health>().notifyDeathObservers += OnDeath;
            movementController = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();


        }
        void OnDeath () {
            Destroy(this);
        }
        void Update() {
            if (!controlEnabled)
                return;

            //MouseRaycast();
        }

        void OnTargetHover(CombatTarget target) {
            if (!fighter.CanAttack(target.gameObject)) return;
            if (Input.GetMouseButton(0)) {
                fighter.Attack(target.gameObject);
            }
        }
        void OnWalkableHover(Vector3 destination) {
            if (Input.GetMouseButton(0)) {
                fighter.setTarget(null);
                movementController.StartMove(destination, 1f);
            }
        }
        private void MouseRaycast() {
            RaycastHit[] rayHits = Physics.RaycastAll(getMouseRay());
            var interactionSuccessful = TryInteractWithCombat(rayHits);
            if (interactionSuccessful) return;
            interactionSuccessful = TryInteractWithMovement(rayHits);
            if (interactionSuccessful) return;
           
            //out of game bounds
            //print("Found nothing...");
        }
    
        private bool TryInteractWithCombat(RaycastHit[] hits) {
            foreach (RaycastHit hit in hits) {
                var target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!fighter.CanAttack(target.gameObject)) continue;
                if (Input.GetMouseButton(0)) {
                    fighter.Attack(target.gameObject);
                }
                    return true;
            }
            return false;
        }
        private bool TryInteractWithMovement(RaycastHit[] rayHits) {
            if (rayHits.Length > 0) {
                if (Input.GetMouseButton(0)) {
                    fighter.setTarget(null);
                    movementController.StartMove(rayHits[rayHits.Length - 1].point, 1f);
                }
                return true;
            }
            return false;
        }
        private static Ray getMouseRay() {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
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
