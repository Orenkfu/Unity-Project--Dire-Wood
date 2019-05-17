using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement {

    public class SceneTransition : MonoBehaviour {
        [SerializeField] private int sceneToLoad = -1;
        [SerializeField] float fadeOutTime = 2f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 1f;
        private int currentSceneIndex;

        void Start() {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        private int GetNextScene() {
            return currentSceneIndex + 1 > SceneManager.sceneCount - 1 ? 0 : currentSceneIndex + 1;
        }

        public IEnumerator Transition(ScenePortalIdentifier destination) {
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
            UpdatePlayer(GetOtherPortal(destination));
            savingWrapper.Save();
            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject);


        }

        private ScenePortal GetOtherPortal(ScenePortalIdentifier destination) {
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
            if (player)
                player.transform.SetPositionAndRotation(portal.spawnPoint.position, portal.spawnPoint.rotation);

        }
        void Update() {

        }
    }
}
