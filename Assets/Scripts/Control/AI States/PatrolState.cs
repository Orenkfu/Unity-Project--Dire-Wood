using RPG.Movement;
using UnityEngine;

namespace RPG.Control.AIState {
    public class PatrolState : MonoBehaviour {
        [Tooltip("Attach a patrol path to enable patrol behaviour.")]
        [SerializeField] PatrolPath patrolPath;
        [Tooltip("How close the character must be to a waypoint to cycle to next one.")]
        [SerializeField] private float waypointTolerance = .5f;
        [Tooltip("How long the character will stay at waypoint before moving to the next one.")]
        [SerializeField] private float waypointDwellTime = 2f;
        [Range(0, 1)]
        [SerializeField] float patrolMovespeedFraction = .3f;

        private float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private Mover moveController;
        private Vector3 startPosition;
        private int currentWaypointIndex = 0;

        void Start() {
            startPosition = transform.position;
            moveController = GetComponent<Mover>();

        }

        public void Patrol() {
            moveController.SetMovespeed(patrolMovespeedFraction);
            Vector3 nextPosition = startPosition;
            if (patrolPath != null) {
                if (AtWayPoint()) {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }

                nextPosition = patrolPath.GetWaypoint(currentWaypointIndex);
            }
            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
                moveController.StartMove(nextPosition, patrolMovespeedFraction);
        }

        private bool AtWayPoint() {
            return Vector3.Distance(transform.position, patrolPath.GetWaypoint(currentWaypointIndex)) <= waypointTolerance;
        }
        private void CycleWaypoint() {
            currentWaypointIndex = patrolPath.GetNextWaypointIndex(currentWaypointIndex);
        }
        void Update() {
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }
    }
}
