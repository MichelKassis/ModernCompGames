using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(15, 85, 45) * Time.deltaTime);
    }
}
