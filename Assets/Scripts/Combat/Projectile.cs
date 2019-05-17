using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {

    public class Projectile : MonoBehaviour {
        Vector3 destination;
        [SerializeField] float launchSpeed = 30f;
        private float damage;
        private string launcherTag;
        private string[] damageableTags = new string[] { "Enemy", "Player", "NPC", "Critter" };
        void Start() {
        }
        void Update() {
            if (destination == null)
                return;
            var translateVector = Vector3.forward * Time.deltaTime * launchSpeed;
            transform.Translate(translateVector);
        }
        public void Launch(Vector3 targetPosition, float damage, string groupTag) {
            destination = targetPosition;
            transform.LookAt(targetPosition);
            this.damage = damage;
            launcherTag = groupTag;
        }

        void OnTriggerEnter(Collider other) {
            if (other.tag == launcherTag || other.gameObject == gameObject)
                return;
            if (!isTagDamageable(other.tag))
                return;

            Collide(other);
        }

        private void Collide(Collider other) {
            var damageableTarget = other.GetComponent<Health>();
            if (damageableTarget != null) {
                damageableTarget.TakeDamage(damage);
            }
            Destroy(gameObject);
        }

        private bool isTagDamageable(string otherTag) {
            if (otherTag == launcherTag) {
                return false;
            }
            bool isDamageable = false;
            foreach (var tag in damageableTags) {
                if (otherTag == tag)
                    isDamageable = true;
            }


            return isDamageable;
        }
    }
}
