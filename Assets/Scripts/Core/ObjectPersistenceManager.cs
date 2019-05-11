using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Core {
    public class ObjectPersistenceManager : MonoBehaviour {
        static bool hasSpawned = false;
        static ObjectPersistenceManager instance = null;
        void Awake() {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
           // LoadPersistentObjects();
        }

        private void LoadPersistentObjects() {
          
          
        }

        void Update() {

        }
    }
}
