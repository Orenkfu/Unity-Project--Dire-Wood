using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Interaction {
    public interface Interactable {
        Transform transform { get; }
        void Interact(Interacter interacter);
        [SerializeField] Texture2D CursorIcon { get; set; }

    }
}