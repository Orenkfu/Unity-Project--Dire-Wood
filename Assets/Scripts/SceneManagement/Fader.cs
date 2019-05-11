using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.SceneManagement {
    public class Fader : MonoBehaviour {
            CanvasGroup canvasGroup;

        void Awake () {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        void Start() {

        }
        void Update() {

        }

        public IEnumerator FadeOutIn(float fadeoutTime, float fadeinTime) {
            yield return FadeOut(fadeoutTime);
            yield return FadeIn(fadeinTime);
        }

        public void FadeOutImmediate () {
            canvasGroup.alpha = 1;
        }
        public IEnumerator FadeOut(float time) {
            while (1 - canvasGroup.alpha > Mathf.Epsilon) {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time) {
            while (canvasGroup.alpha > 0) {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}
