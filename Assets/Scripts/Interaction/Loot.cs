using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Interaction {

    public class Loot : MonoBehaviour, Interactable {
        public Texture2D CursorIcon { get => cursorIcon; set => cursorIcon = value; }
        [SerializeField] private Texture2D cursorIcon = null;

        public void Interact(Interacter interacter) {
            interacter.GetComponent<Health>().TakeDamage(-10f);
            Destroy(gameObject);
        }

        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}
