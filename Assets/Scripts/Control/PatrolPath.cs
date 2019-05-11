using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control {
    public class PatrolPath : MonoBehaviour {
        [SerializeField] float gizmoDrawRadius = .3f;
        void Start() {

        }

        void Update() {

        }

     

        void OnDrawGizmos() {
            Gizmos.color = Color.green;
            for (int i = 0; i < transform.childCount; i++) {
                Gizmos.DrawSphere(GetWaypoint(i), gizmoDrawRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(GetNextWaypointIndex(i)));
                
            }
        }
        public int GetNextWaypointIndex (int i) {
            return  i >= transform.childCount - 1 ? 0 : i + 1;
        }
        public Vector3 GetWaypoint(int i) {
            return transform.GetChild(i).transform.position;
        }
    }
}
