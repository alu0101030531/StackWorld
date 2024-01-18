using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelpSystem : MonoBehaviour
{
    public Canvas canvas;
    public GameObject prefabHelpTextUI;
    private GameObject currentTextUI;

    void Start()
    {
        GridManager.OnShowHelp += ShowHelp;
        currentTextUI = Instantiate(prefabHelpTextUI) as GameObject;
    }

    IEnumerator VanishHelp()
    {
        yield return new WaitForSeconds(3f);
        currentTextUI.GetComponent<TMP_Text>().text = ""; 
    }

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
