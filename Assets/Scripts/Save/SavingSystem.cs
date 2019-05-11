using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace RPG.Saving {
    public class SavingSystem : MonoBehaviour {
        void Start() {

        }
        public IEnumerator LoadLastScene(string filename) {
            var state = LoadFile(filename);
            if (state.ContainsKey("lastSceneBuildIndex")) {
                var buildIndex = state["lastSceneBuildIndex"];
                if (buildIndex != null && (int)buildIndex != SceneManager.GetActiveScene().buildIndex) {
                    yield return SceneManager.LoadSceneAsync((int)buildIndex);
                }
            }
            RestoreState(state);
        }
        public void Save(string filename) {
            Dictionary<string, object> state = LoadFile(filename);
            CaptureState(state);
            SaveFile(filename, state);
        }
        public void Load(string saveFile) {
            RestoreState(LoadFile(saveFile));
        }

        string GetSavePath() {
            return Path.Combine(Application.persistentDataPath);
        }

        private void SaveFile(string filename, Dictionary<string, object> data) {
            var filePath = Path.Combine(GetSavePath(), filename + ".sav");
            using (FileStream stream = File.Open(filePath, FileMode.Create)) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
            }
        }

        private Dictionary<string, object> LoadFile(string filename) {
            var filePath = Path.Combine(GetSavePath(), filename + ".sav");
            if (!File.Exists(filePath)) {
                return new Dictionary<string, object>();
            }
            using (FileStream stream = File.OpenRead(filePath)) {
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void RestoreState(Dictionary<string, object> state) {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()) {
                if (state.ContainsKey(saveable.UniqueIdentifer())) {
                    saveable.RestoreState(state[saveable.UniqueIdentifer()]);
                }
            }
        }
        private void CaptureState(Dictionary<string, object> state) {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()) {
                state[saveable.UniqueIdentifer()] = saveable.CaptureState();
            }
            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }
    }
}
