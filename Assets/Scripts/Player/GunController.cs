using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunController : MonoBehaviour
{
    public List<GameObject> gunInventory;
    public GunAnimator gunAnimator;

    private string gunPickupLayer = "GunPickup";

    private MouseAngle mouseAngle;
    private GameObject selectedGun;

    private Transform tipOfBarrel;

    private GunStats gunStats;
    //Shoot counter
    private float shootCounter = 0;

    public int selectedGunIndex = 0;

    public UnityEvent OnGunFired;

    // Start is called before the first frame update
    void Start()
    {
        gunInventory = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(selectedGunIndex >=0 && selectedGunIndex <= gunInventory.Count-1)
        {
            selectedGunIndex += (int)Input.mouseScrollDelta.y;
            if(selectedGunIndex>gunInventory.Count-1)
                selectedGunIndex = gunInventory.Count-1;
            if(selectedGunIndex<0)
                selectedGunIndex=0;
            SelectGun(selectedGunIndex);
        }

        if (selectedGun == null)
            return;

        //Gun stats + timer
        if (shootCounter != gunStats.ShootTimer)
        {
            shootCounter += Time.deltaTime;
        }

        //Shooting
        if (Input.GetMouseButton(0) && shootCounter >= gunStats.ShootTimer)
        {
            OnGunFired.Invoke();
            //Change this bullshit from random?
            //Not sure how
            //Also, add hueristics so longer you shooter more unaccurate you get
            float offset = Random.Range(-5f, 5f);

            GameObject tmp = ObjectPooler.Instance.GetPlayerBullet();
            if (tmp != null)
            {
                tmp.transform.position = tipOfBarrel.position;
                tmp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, mouseAngle.Angle + offset));

                tmp.SetActive(true);
                shootCounter = 0;
            }
        }

        //Sprite flipping
        if (mouseAngle.Angle >= -90 && mouseAngle.Angle <= 90)
        {
            gunAnimator.Flip = false;
        }
        else
        {
            gunAnimator.Flip = true;
        }
    }

    void LateUpdate()
    {
        if (selectedGun == null)
            return;
        if (gunAnimator.Flip)
        {
            selectedGun.transform.position = new Vector2(transform.localPosition.x - 0.25f, transform.localPosition.y - 0.2f);
        }
        else
        {
            selectedGun.transform.position = new Vector2(transform.localPosition.x + 0.25f, transform.localPosition.y - 0.2f);
        }
        selectedGun.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, mouseAngle.Angle));
    }

    private void SelectGun(int gunIndex)
    {
        mouseAngle = GetComponent<MouseAngle>();

        if(selectedGun != null)
        {
            selectedGun.SetActive(false);
            selectedGun = null;            
        }
        //Create default gun + Add to animator to flip it
        selectedGun = gunInventory[gunIndex];
        selectedGun.transform.parent = transform;
        selectedGun.SetActive(true);
        gunAnimator.SetSpriteRenderer(selectedGun.transform.GetChild(0).GetComponent<SpriteRenderer>());
        //Get tip to shoot form
        tipOfBarrel = selectedGun.transform.GetChild(1);

        //Setup gun stats
        gunStats = selectedGun.GetComponent<GunStats>();
        gunStats.IsPickedUp = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer(gunPickupLayer))
        {
            //Remove circle trigger from gun
            other.GetComponent<CircleCollider2D>().enabled = false;
            gunInventory.Add(other.gameObject);

            //Select current Gun on pickup
            SelectGun(gunInventory.Count-1);
        }
    }
}
