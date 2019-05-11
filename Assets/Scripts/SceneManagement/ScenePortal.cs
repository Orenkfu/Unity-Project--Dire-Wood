using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Saving;
namespace RPG.SceneManagement {
    public class ScenePortal : MonoBehaviour {
        public enum ScenePortalIdentifier {
            A, B, C, D, E, F, G, H
        }
        private int currentSceneIndex;
        [SerializeField] private int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] public ScenePortalIdentifier identifier;
        [SerializeField] ScenePortalIdentifier destination;
        [SerializeField] float fadeOutTime = 2f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 1f;
        void Start() {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        void OnTriggerEnter(Collider other) {
            if (other.tag != "Player")
                return;

            StartCoroutine(Transition());

        }
        private int GetNextScene() {
            return currentSceneIndex + 1 > SceneManager.sceneCount - 1 ? 0 : currentSceneIndex + 1;
        }

        private IEnumerator Transition() {
            if (sceneToLoad == -1) {
                sceneToLoad = GetNextScene();
            }
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            yield return fader.FadeOut(fadeOutTime);
            savingWrapper.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            savingWrapper.Load();
            UpdatePlayer(GetOtherPortal());
            savingWrapper.Save();
            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject);


        }

        private ScenePortal GetOtherPortal() {
            foreach (ScenePortal portal in FindObjectsOfType<ScenePortal>()) {
                if (portal == this) continue;
                if (portal.identifier == destination) {
                    return portal;
                }
            }
            return null;
        }

        private void UpdatePlayer(ScenePortal portal) {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.SetPositionAndRotation(portal.spawnPoint.position, portal.spawnPoint.rotation);
          
        }
        void Update() {

        }
    }
}
