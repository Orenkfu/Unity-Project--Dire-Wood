using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using RPG.Control.AIState;

namespace RPG.Control {
    public class AIController : MonoBehaviour {
        [SerializeField] float chaseDistance = 6f;
        [SerializeField] float suspicionTimer = 3f;
        private  PatrolState patrolBehaviour;

        private Vector3 startPosition;
        GameObject player;
        Fighter fighter;
        Mover moveController;

        private float timeSincePlayerLastSeen = Mathf.Infinity;

        void Start() {
            startPosition = transform.position;
            player = GameObject.FindGameObjectWithTag("Player");
            GetComponent<Health>().notifyDeathObservers += OnDeath;
            fighter = GetComponent<Fighter>();
            moveController = GetComponent<Mover>();
            patrolBehaviour = GetComponent<PatrolState>();
        }
        void OnDeath() {
            Destroy(this);
        }
        void Update() {
            //TODO set up state enum
            if (InRange(player.transform, chaseDistance) && fighter.CanAttack(player)) {
                AttackBehaviour();
            } else if (timeSincePlayerLastSeen < suspicionTimer) {
                SuspicionBehaviour();
            } else {
                patrolBehaviour.Patrol();
            }
                timeSincePlayerLastSeen += Time.deltaTime;
        }

        private void SuspicionBehaviour () {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        private void AttackBehaviour () {
            timeSincePlayerLastSeen = 0f;
            fighter.Attack(player);
        }

        private bool InRange(Transform target, float range) {
            return Vector3.Distance(transform.position, target.position) <= range;
        }

        private bool InRange(Vector3 targetPosition, float range) {
            return Vector3.Distance(transform.position, targetPosition) <= range;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
