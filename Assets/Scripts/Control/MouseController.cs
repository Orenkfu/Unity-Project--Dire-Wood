using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

namespace RPG.Control {
    public class MouseController : MonoBehaviour {

        [SerializeField] Texture2D walkCursor = null;
        [SerializeField] Texture2D interactCursor = null;
        [SerializeField] Texture2D enemyCursor = null;
        [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);

        public delegate void OnMouseOverTarget(CombatTarget target);
        public event OnMouseOverTarget notifyEnemyObservers;
        public delegate void OnMouseOverWalkable(Vector3 destination);
        public event OnMouseOverWalkable notifyWalkableObservers;
        // public delegate void OnMouseOverInteractable(Interactable interactable);
        // public event OnMouseOverInteractable notifyInteractableObservers;

        void Start() {

        }

        void Update() {
            RaycastHit[] rayHits = Physics.RaycastAll(getMouseRay());
            foreach (var hit in rayHits) {
                var target = hit.transform.GetComponent<CombatTarget>();
                if (target) {
                    SetCursor(enemyCursor);
                    notifyEnemyObservers(target);
                    return;
                }
            }
            if (rayHits.Length > 0) {
                SetCursor(walkCursor);
                notifyWalkableObservers(rayHits[rayHits.Length - 1].point);
            }
        }

        private void SetCursor(Texture2D texture) {
            Cursor.SetCursor(texture, cursorHotspot, CursorMode.Auto);
        }
        private static Ray getMouseRay() {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
