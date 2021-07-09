using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float delaySeconds = 1.0f;

    private Color color;

    void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        spriteRenderer.color = color;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(delaySeconds);
        float fade = 1f;
        while (fade > 0)
        {
            spriteRenderer.color = new Color(color.r, color.g, color.b, fade);
            fade -= Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
