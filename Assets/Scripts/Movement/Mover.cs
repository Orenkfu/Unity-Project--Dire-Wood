using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
namespace RPG.Movement {

    public class Mover : MonoBehaviour, IAction, ISaveable {
        private Vector3 target;
        private NavMeshAgent navMeshAgent;
        private Animator animator;

        [SerializeField] float maxMovespeed = 10f;
        [SerializeField] float movespeed = 6f;

        void Start() {
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = true;
            var health = GetComponent<Health>();
            if (health)
                health.notifyDeathObservers += OnDeath;
        }

        void OnDeath () {
            GetComponent<ActionScheduler>().CancelCurrentAction();

            navMeshAgent.enabled = false;
        }
        void Update() {

            Animate();
        }
   
        private void Animate() {
            // keep animator's forwardspeed updated with the value of the velocity of nav mesh agent
            animator.SetFloat("forwardSpeed", transform.InverseTransformDirection(navMeshAgent.velocity).z);
        }

        public void Cancel() {
            navMeshAgent.isStopped = true;
        }
        public void StartMove(Vector3 destination, float speedFraction) {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void SetMovespeed(float speedFraction) {
            speedFraction = Mathf.Clamp01(speedFraction);
            navMeshAgent.speed = Mathf.Clamp(movespeed * speedFraction, 0, maxMovespeed);
        }
        public void MoveTo(Vector3 destination, float speedFraction) {
            SetMovespeed(speedFraction);
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(destination);
        }

        public void RestoreState(object state) {
            SerializableVector3 position = (SerializableVector3)state;
            //GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector3();
            //GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState() {
            return new SerializableVector3(transform.position);
        }
    }
}
