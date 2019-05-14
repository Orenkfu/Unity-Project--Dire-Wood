using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {

    public class AICanvas : MonoBehaviour {
        [SerializeField] Image healthBar;
        Health health;
        Transform camTranform;

        void Start() {
            health = transform.parent.GetComponent<Health>();
            camTranform = Camera.main.transform;
        }

        void Update() {
            transform.LookAt(transform.position + camTranform.rotation * Vector3.forward,
             camTranform.rotation * Vector3.up);
            healthBar.fillAmount = health.CurrentHealth / health.MaxHealth;
        }
    }
}
