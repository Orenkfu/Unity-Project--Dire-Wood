using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
namespace RPG.Core {
    public class Health : MonoBehaviour, ISaveable {

        [SerializeField] float maxHealth = 100f;
        [SerializeField] private float currentHealth;
        private bool _isDead = false;

        public delegate void OnCharacterDeath();
        public event OnCharacterDeath notifyDeathObservers;
        public delegate void OnDamageTaken(float damage);
        public event OnDamageTaken notifyDamageObservers;

        public float CurrentHealth { get { return currentHealth;  } }
        public float MaxHealth { get { return maxHealth; } }
        public bool isDead { get { return _isDead; } set { _isDead = value; } }
        private bool isRestored = false;
        void Awake () {
            notifyDeathObservers += OnDeath;
            notifyDamageObservers += EvaluateDeath;

        }
        void Start() {
            if (!isRestored) {
                currentHealth = MaxHealth;
            }
        }

        public void TakeDamage(float damage) {
            if (_isDead) return;
            currentHealth = Mathf.Max(currentHealth - damage, 0);
            notifyDamageObservers(damage);
        }

        //mostly exists so event emitted will never be null..
        void EvaluateDeath (float damage) {
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
        private IEnumerator AsyncNotifyDeathObservers () {
            yield return null;
            notifyDeathObservers();
        }
        void OnDeath () {
            GetComponent<Animator>().SetTrigger("death");
            var collider = GetComponent<CapsuleCollider>();
            if (collider) {
                Destroy(collider);
            }
            //TODO: replace this with permanent solution
           // Invoke("Dispose", 7f);
        }
        void Dispose() {
            Destroy(gameObject);
        }
        void Update() {

        }

        public void RestoreState(object state) {
            isRestored = true;
            var healthDict = (Dictionary<string, object>)state;
            if ((bool)healthDict["isDead"]) {
                print("Restoring dead character: " + gameObject);
                Die();
            }
            currentHealth = (float)healthDict["currentHealth"];
            if (currentHealth == 0) {
                currentHealth = MaxHealth;
            }

        }

        public object CaptureState() {
            
            var healthDict = new Dictionary<string, object>();
            healthDict["currentHealth"] = currentHealth;
            healthDict["isDead"] = isDead;
            return healthDict;
            
        }
    }
}
