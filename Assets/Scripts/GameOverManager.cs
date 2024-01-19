using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public string mainScene = "MainScene";
    public AudioSource buttonClickAudio;

    // Update is called once per frame
    void Update()
    {
        
    }

    // We load the game scene waiting a delay time to reproduce a sound
    private IEnumerator LoadScene(AudioSource audio, float delay)
    {
        audio.Play(0);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(mainScene);
    }
    public void OnTryAgain() {
        StartCoroutine(LoadScene(buttonClickAudio, 1f));
    }
}
