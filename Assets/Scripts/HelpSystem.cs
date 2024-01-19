using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelpSystem : MonoBehaviour
{
    public Canvas canvas;
    public GameObject prefabHelpTextUI;
    private GameObject currentTextUI;

    // We instantiate a TMP_text  to show the help messages to the player
    void Start()
    {
        GridManager.OnShowHelp += ShowHelp;
        currentTextUI = Instantiate(prefabHelpTextUI) as GameObject;
    }

    // After showing the message we change the text after 3 seconds
    IEnumerator VanishHelp()
    {
        yield return new WaitForSeconds(3f);
        currentTextUI.GetComponent<TMP_Text>().text = ""; 
    }

    // We get the help message from the tile
    private void ShowHelp(WorldTile tile) {
        StartCoroutine(VanishHelp());
        currentTextUI.transform.SetParent(canvas.transform);
        currentTextUI.GetComponent<TMP_Text>().text = tile.HelpMessage;
        currentTextUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
    }

    void OnDestroy() {
        GridManager.OnShowHelp -= ShowHelp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
