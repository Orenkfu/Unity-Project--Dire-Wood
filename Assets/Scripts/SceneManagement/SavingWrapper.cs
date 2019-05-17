using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using RPG.Saving;
namespace RPG.SceneManagement {
    public class SavingWrapper : MonoBehaviour {

        const string DEFAULT_SAVE_FILE = "default";
        private SavingSystem savingSystem;
        [SerializeField] float fadeTime = 1.5f;
        Fader fader;
        void Awake () {
            savingSystem = GetComponent<SavingSystem>();
        }
        IEnumerator Start() {
            print("Loading Saving Wrapper!");
            print("Fade time.." + fadeTime);
            fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return fader.FadeIn(fadeTime);
        }

        void Update() {
            if (!fader) {
                fader = FindObjectOfType<Fader>();
            }
            if (Input.GetKeyDown(KeyCode.S))
                Save();
            if (Input.GetKeyDown(KeyCode.L))
                 StartCoroutine(Continue());
                    //Load();
        }
        public IEnumerator Continue () {
            print("Continuing..");
            yield return fader.FadeOut(fadeTime);
            yield return savingSystem.LoadLastScene(DEFAULT_SAVE_FILE);
            yield return new WaitForSeconds(fadeTime);
            yield return fader.FadeIn(fadeTime);
        }
        public void Save () {
            print("Saving..");
            savingSystem.Save(DEFAULT_SAVE_FILE);

        }
        public void Load () {
            StartCoroutine(fader.FadeIn(fadeTime));
            savingSystem.Load(DEFAULT_SAVE_FILE);

        }

    }
}