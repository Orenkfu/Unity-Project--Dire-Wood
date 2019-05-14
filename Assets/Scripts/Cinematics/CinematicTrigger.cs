using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematics {
    public class CinematicTrigger : MonoBehaviour, ISaveable {
        [SerializeField] string triggerTag = "Player";
        [SerializeField] int timesToTrigger = 1;

        private float cinematicsTriggered = 0;
        private PlayableDirector director;

        void Start() {
            director = GetComponent<PlayableDirector>();
        }

        private void OnTriggerEnter(Collider other) {
            if (cinematicsTriggered >= timesToTrigger || other.tag != triggerTag)
                return;

            cinematicsTriggered++;
            director.Play();

        }

        public void RestoreState(object state) {
            cinematicsTriggered = (float)state;
        }

        public object CaptureState() {
            return cinematicsTriggered;
        }
    }
}
