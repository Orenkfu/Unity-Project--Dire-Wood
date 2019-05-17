using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Interaction {
    public class Interacter : MonoBehaviour, IAction {
        private Interactable target;
        private Animator animator;
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
            animator = GetComponent<Animator>();
            moveController = GetComponent<Mover>();
        }

        void Start() {

        }

        void Update() {
            if (!CanInteract(Target)) return;
            if (WithinDistance()) {
                InteractionBehaviour();
                
            } else {
                moveController.StartMove(target.transform.position, 1f);
            }
            
        }

        private void InteractionBehaviour() {
            GetComponent<ActionScheduler>().StartAction(this);
            animator.SetTrigger("interact");
            target.Interact(this);
            target = null;
        }

        private bool WithinDistance() {
            return Vector3.Distance(transform.position, target.transform.position) <= interactDistance;
        }

        bool CanInteract(Interactable target) {
            //TODO: create a check logic for interaction
            return target != null;
        }

        public void Cancel() {
            animator.ResetTrigger("interact");
            animator.SetTrigger("stopInteract");
            target = null;
        }
    }
}
