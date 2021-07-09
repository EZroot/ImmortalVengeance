using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    //Disables game object
    public void KillMeSoftly()
    {
        //If this is in another container will it fuck it up?
        //Probably...
        Debug.LogWarning("This may disable the wrong object if the bullet is parented to something else.");
        transform.parent.gameObject.SetActive(false);
    }
}
