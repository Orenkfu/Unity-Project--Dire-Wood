using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics {
    public class CinematicControlHandler : MonoBehaviour {
        GameObject player;
        void Awake () {

        }
        void Start() {
            var director = GetComponent<PlayableDirector>();
            director.played += DisableControl;
            director.stopped += EnableControl;
            player = GameObject.FindWithTag("Player");
        }

        void Update() {

        }
        void DisableControl(PlayableDirector pd) {
            player.GetComponent<PlayerController>().DisableControl();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        void EnableControl(PlayableDirector pd) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player.GetComponent<PlayerController>().EnableControl();
        }
    }
}
