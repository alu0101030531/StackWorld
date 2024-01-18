using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgParallax : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public List<GameObject> backgrounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speedFactor = scrollSpeed / backgrounds.Count;
        float speed = speedFactor;
        foreach (GameObject background in backgrounds) {
            background.GetComponent<Renderer>().sharedMaterial.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0f);
            speed += speedFactor;
        }
    }
}
