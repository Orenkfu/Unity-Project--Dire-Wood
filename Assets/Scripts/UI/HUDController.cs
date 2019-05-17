using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Core;

public class HUDController : MonoBehaviour {

    [SerializeField] Image healthOrb;
    [SerializeField] Image energyOrb;
    GameObject player;
    //Energy playerEnergy;
    void Start() {
        player = GameObject.FindWithTag("Player");
        
    }

    private float HealthAsPercentage() {
        var playerHealth = player.GetComponent<Health>();
        return playerHealth.CurrentHealth / playerHealth.MaxHealth;
    }
    void Update() {
        if (!player) {
            player = GameObject.FindWithTag("Player");
        }
        var hp = HealthAsPercentage();
        healthOrb.fillAmount = hp;
    }
}
