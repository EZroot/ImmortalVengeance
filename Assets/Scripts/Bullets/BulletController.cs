using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * 5 * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject tmp = EffectPooler.Instance.GetBulletEffect();
        tmp.transform.position = transform.position;
        tmp.SetActive(true);

        gameObject.SetActive(false);
    }
}
