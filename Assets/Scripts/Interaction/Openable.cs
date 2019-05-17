using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
namespace RPG.Interaction {

    public class Openable : MonoBehaviour, Interactable, ISaveable {
        [SerializeField] private Texture2D cursorIcon;
        public Texture2D CursorIcon { get => cursorIcon; set => cursorIcon = value; }
        private bool isOpen;
        public void Interact(Interacter interacter) {
            var animator = GetComponent<Animator>();
            if (animator) {
                animator.SetTrigger("open");
                isOpen = true;
            }
        }

        void Start() {

        }

        void Update() {

        }

        public void RestoreState(object state) {
            isOpen = (bool)state;
            if (isOpen) {
                GetComponent<Animator>().SetTrigger("open");
            }
        }

        public object CaptureState() {
            return isOpen;
        }
    }
}
