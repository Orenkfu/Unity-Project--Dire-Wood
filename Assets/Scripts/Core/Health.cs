using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
namespace RPG.Core {
    public class Health : MonoBehaviour, ISaveable {

        [SerializeField] float maxHealth = 100f;
        private float currentHealth;
        private bool _isDead = false;

        public delegate void OnCharacterDeath();
        public event OnCharacterDeath notifyDeathObservers;

        public bool isDead { get { return _isDead; } set { _isDead = value; } }
        void Awake () {
            notifyDeathObservers += OnDeath;

        }
        void Start() {
            currentHealth = maxHealth;

        }

        public void TakeDamage(float damage) {
            if (_isDead) return;
            currentHealth = Mathf.Max(currentHealth - damage, 0);
            if (currentHealth <= 0)
                Die();
        }

        void Die() {
            _isDead = true;
            if (notifyDeathObservers == null ) {
                print("Notify death observers is null?");
            }
            notifyDeathObservers();
     
        }
        void OnDeath () {
            GetComponent<Animator>().SetTrigger("death");
            //TODO: replace this with permanent solution
            Invoke("Dispose", 7f);
        }
        void Dispose() {
            Destroy(gameObject);
        }
        void Update() {

        }

        public void RestoreState(object state) {
            var healthDict = (Dictionary<string, object>)state;
            currentHealth = healthDict["currentHealth"] == null ? maxHealth : (float)healthDict["currentHealth"];
            if (currentHealth <= 0) {
                Die();
            }

        }

        public object CaptureState() {
            var healthDict = new Dictionary<string, object>();
            healthDict["currentHealth"] = currentHealth;
            return healthDict;

        }
    }
}
