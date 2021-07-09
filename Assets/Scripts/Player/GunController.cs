using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject[] gunInventory;
    public GunAnimator gunAnimator;

    private MouseAngle mouseAngle;
    private GameObject selectedGun;

    private Transform tipOfBarrel;

    private GunStats gunStats;
    //Shoot counter
    private float shootCounter = 0;

    public int selectedGunIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        mouseAngle = GetComponent<MouseAngle>();
        //Create default gun + Add to animator to flip it
        selectedGun = gunInventory[selectedGunIndex];
        selectedGun.SetActive(true);
        gunAnimator.SetSpriteRenderer(selectedGun.transform.GetChild(0).GetComponent<SpriteRenderer>());
        //Get tip to shoot form
        tipOfBarrel = selectedGun.transform.GetChild(1);
        
        //Setup gun stats
        gunStats = selectedGun.GetComponent<GunStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootCounter != gunStats.ShootTimer)
        {
            shootCounter += Time.deltaTime;
        }

        if (selectedGun == null)
            return;

        //Shooting
        if (Input.GetMouseButton(0) && shootCounter >= gunStats.ShootTimer)
        {
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
}
