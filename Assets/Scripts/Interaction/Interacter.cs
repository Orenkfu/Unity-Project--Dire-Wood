using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Interaction {
    public class Interacter : MonoBehaviour {
        private Interactable target;
        public Interactable Target {
            get { return target; }
            set {
                GetComponent<ActionScheduler>().CancelCurrentAction();
                target = value;
            }
        }
        [SerializeField] float interactDistance;
        private Mover moveController;
        void Awake() {
            moveController = GetComponent<Mover>();
        }

        void Start() {

        }

        void Update() {
            if (!CanInteract(Target)) return;
            if (WithinDistance()) {
                InteractionBehaviour();
                
            } else {
                moveController.MoveTo(target.transform.position, 1f);
            }
            
        }

        private void InteractionBehaviour() {
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GetComponent<Animator>().SetTrigger("interact");
            target.Interact(this);
            target = null;
        }

        private bool WithinDistance() {
            print("Target's Transform: " + target.transform);
            return Vector3.Distance(transform.position, target.transform.position) <= interactDistance;
        }

        bool CanInteract(Interactable target) {
            //TODO: create a check logic for interaction
            return target != null;
        }
    }
}
