using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core {
    public class FollowCamera : MonoBehaviour {
        [SerializeField] Transform target;
        [SerializeField] float yawSpeed = 60f;
        [SerializeField] bool rotationEnabled = true;
        public float currentYawInput = 0f;
        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            currentYawInput = Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;

        }
        void LateUpdate() {

            transform.position = target.position;
            if (rotationEnabled)
                RotateCamera();
        }
        void RotateCamera() {
            transform.RotateAround(target.position, Vector3.up, currentYawInput);
        }
    }
}
