using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{

    public GameObject projectile; 
    public float fireRate; 
    private float nextFire; 
    public Quaternion BarrelAngleNow;
    private Quaternion subtractAngle;

    // Start is called before the first frame update
    void Start()
    {
        BarrelAngleNow= this.transform.parent.rotation;
        nextFire = 0.0f; // initialize
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate; 
            BarrelAngleNow = this.transform.parent.rotation;
            Instantiate(projectile, transform.position, transform.rotation); 
        }
    }
}
