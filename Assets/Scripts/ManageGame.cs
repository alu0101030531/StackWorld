using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    public string GameOverScene = "GameOver";
    // Start is called before the first frame update
    void Start()
    {
        TileGenerator.OnGameOver += OnGameOver;
    }

    // Once the game finish we load the gameOver scene
    private void OnGameOver() {
        SceneManager.LoadScene(GameOverScene);
    }     
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
