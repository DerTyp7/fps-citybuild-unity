using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPSCounter : MonoBehaviour
{
    public TMPro.TextMeshProUGUI fpsText;
    public float deltaTime;
    void Start()
    {
        fpsText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString() + "FPS";
    }
}
