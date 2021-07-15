using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

//BLOOD DISCO
//Sound of Sins
public class MusicManager : MonoBehaviour
{
    AudioProcessor audioProcessor;

    public Light2D[] musicLights;
    private Color[] defaultColors;

    void Start()
    {
        defaultColors = new Color[musicLights.Length];
        for (int i = 0; i < musicLights.Length; i++)
        {
            defaultColors[i] = musicLights[i].color;
        }

        //InvokeRepeating("UpdateLights",0,1.0f/15.0f); //update at 15 fps
    }

    public void UpdateLights()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        //Debug.Log(r + "," + g + "," + b);
        Color color = new Color(r, g, b);

        for (int i = 0; i < musicLights.Length; i++)
        {
            musicLights[i].color = color;
        }
        //StartCoroutine(ReturnToOriginalColor());
    }

    IEnumerator ReturnToOriginalColor()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < musicLights.Length; i++)
        {
            while (musicLights[i].color.r != defaultColors[i].r)
            {
                musicLights[i].color = Color.Lerp(musicLights[i].color, defaultColors[i], Time.time);
            }
        }
    }
}
