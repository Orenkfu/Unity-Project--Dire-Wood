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

        void Awake () {
            savingSystem = GetComponent<SavingSystem>();
        }
        IEnumerator Start() {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return savingSystem.LoadLastScene(DEFAULT_SAVE_FILE);
            yield return fader.FadeIn(fadeTime);
        }
        void Update() {
            if (Input.GetKeyDown(KeyCode.S))
                Save();
            if (Input.GetKeyDown(KeyCode.L))
                Load();
        }
        public void Save () {
            savingSystem.Save(DEFAULT_SAVE_FILE);

        }
        public void Load () {
            savingSystem.Load(DEFAULT_SAVE_FILE);

        }

    }
}