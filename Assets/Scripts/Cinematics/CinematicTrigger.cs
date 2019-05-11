using UnityEngine;
using UnityEngine.Playables;
namespace RPG.Cinematics {
    public class CinematicTrigger : MonoBehaviour {
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
    }
}
