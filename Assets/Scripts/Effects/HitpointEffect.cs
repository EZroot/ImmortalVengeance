using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitpointEffect : MonoBehaviour
{
    TextMeshProUGUI gui;

    void Awake()
    {
        gui = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        StartCoroutine(FadeOut());
    }

    public void SetText(string text)
    {
        gui.text = text;
    }

    IEnumerator FadeOut()
    {
        float fade = 1f;
        while (fade > 0)
        {
            gui.color = new Color(255, 255, 255, fade);
            transform.localPosition = new Vector2(transform.position.x,transform.position.y + Time.deltaTime);
            fade -= Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
