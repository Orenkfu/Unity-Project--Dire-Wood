using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.SceneManagement;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

    void Start() {
    }

    public void StartNewGame() {
        StartCoroutine(AsyncStartGame());
    }
    IEnumerator AsyncStartGame() {
        Fader fader = FindObjectOfType<Fader>();
        fader.FadeOutImmediate();
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        yield return fader.FadeIn(8);

    }
    public void Continue() {
        DontDestroyOnLoad(gameObject);
        var saveWrapper = FindObjectOfType<SavingWrapper>();
            StartCoroutine(saveWrapper.Continue());
        
    }
    public IEnumerator LoadSave() {
        yield return FindObjectOfType<SavingWrapper>().Continue();
    }
    public void LoadOptions() {
        print("Loading Options..");
    }
    public void QuitGame() {
        Application.Quit();
    }

    // Update is called once per frame
    void Update() {

    }
}
