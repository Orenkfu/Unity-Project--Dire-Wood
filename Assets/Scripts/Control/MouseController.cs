using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Interaction;
namespace RPG.Control {
    public class MouseController : MonoBehaviour {

        [SerializeField] Texture2D walkCursor = null;
        [SerializeField] Texture2D interactCursor = null;
        [SerializeField] Texture2D enemyCursor = null;
        [SerializeField] Texture2D noInteractCursor = null;
        [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);

        public delegate void OnMouseOverTarget(CombatTarget target);
        public event OnMouseOverTarget notifyEnemyObservers;
        public delegate void OnMouseOverWalkable(Vector3 destination);
        public event OnMouseOverWalkable notifyWalkableObservers;
        int walkableLayer = 0;
        public delegate void OnMouseOverInteractable(Interactable interactable);
        public event OnMouseOverInteractable notifyInteractableObservers;
        Camera mainCam;
        void Start() {
            mainCam = Camera.main;
        }

        void Update() {
            if (!MouseWithinScreenBounds()) {
                return;
            }
            RaycastHit[] rayHits = Physics.RaycastAll(getMouseRay());            
            foreach (var hit in rayHits) {

                var target = hit.transform.GetComponent<CombatTarget>();
                if (target) {
                    SetCursor(enemyCursor);
                    notifyEnemyObservers(target);
                    return;
                }
                var interactable = hit.transform.GetComponent<Interactable>();
                if (interactable != null) {
                    SetCursor(interactable.CursorIcon);
                    notifyInteractableObservers(interactable);
                    return;
                }

            }

            if (rayHits.Length > 0) {
                SetCursor(walkCursor);
                notifyWalkableObservers(rayHits[rayHits.Length - 1].point);    
            } else {
                SetCursor(noInteractCursor);
            }
        }
        private bool MouseWithinScreenBounds() {
            return new Rect(0, 0, Screen.width, Screen.height).Contains(Input.mousePosition);
        }

        private void SetCursor(Texture2D texture) {
            Cursor.SetCursor(texture, cursorHotspot, CursorMode.Auto);
        }
        private Ray getMouseRay() {
            return mainCam.ScreenPointToRay(Input.mousePosition);
        }
    }
}
