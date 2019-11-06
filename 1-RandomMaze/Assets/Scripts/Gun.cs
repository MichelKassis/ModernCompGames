using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject bulletInstance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !bulletInstance)
        {
            bulletInstance = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
            //source.PlayOneShot(alarm);

        }

    }

}
