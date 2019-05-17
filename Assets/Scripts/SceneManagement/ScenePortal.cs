using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Saving;

namespace RPG.SceneManagement {
    public enum ScenePortalIdentifier {
        A, B, C, D, E, F, G, H
    }
}

namespace RPG.SceneManagement {
    public class ScenePortal : MonoBehaviour {
        
        private int currentSceneIndex;
        [SerializeField] public Transform spawnPoint;
        [SerializeField] public ScenePortalIdentifier identifier;
        [SerializeField] ScenePortalIdentifier destination;
        private SceneTransition st;
        void Start() {
            st = GetComponent<SceneTransition>();
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        void OnTriggerEnter(Collider other) {
            if (other.tag != "Player")
                return;

            StartCoroutine(st.Transition(destination));

        }
       
    }
}
