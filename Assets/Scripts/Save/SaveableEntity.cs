using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving {
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour {
        [SerializeField] string uniqueIdentifer = ""; //Guid.NewGuid();
        static Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();

        public object CaptureState() {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (var saveable in GetComponents<ISaveable>()) {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        public void RestoreState(object state) {
            var stateDict = (Dictionary<string, object>)state;
            foreach (var saveable in GetComponents<ISaveable>()) {
                var type = saveable.GetType().ToString();
                if (stateDict.ContainsKey(type)) {
                    print(stateDict[type]);
                    saveable.RestoreState(stateDict[type]);
                }
            }
        }
        public string UniqueIdentifer() {
            return uniqueIdentifer;
        }
        void Start() {
        }

#if UNITY_EDITOR
        void Update() {
            if (Application.IsPlaying(gameObject))
                return;
            if (string.IsNullOrEmpty(gameObject.scene.path))
                return;
            SerializedObject obj = new SerializedObject(this);
            SerializedProperty property = obj.FindProperty("uniqueIdentifer");
            if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue)) {
                property.stringValue = Guid.NewGuid().ToString();
                obj.ApplyModifiedProperties();
            }
            globalLookup[property.stringValue] = this;
        }
#endif

        private bool IsUnique(string candidate) {
            if (!globalLookup.ContainsKey(candidate)) return true;
            if (globalLookup[candidate] == this) return true;

            if (globalLookup[candidate] == null || globalLookup[candidate].UniqueIdentifer() != candidate) {
                globalLookup.Remove(candidate);
                return true;
            }

            return false;

        }
    }
}
