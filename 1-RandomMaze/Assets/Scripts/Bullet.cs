using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public float range = 100;
    public int score = 0;
    private float dist;
    private Vector3 orig_gun_pos;
    private GameManager gm;

    // Use this for initialization
    void Start()
    {
        orig_gun_pos = GameObject.Find("Gun").transform.position;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        dist += Time.deltaTime * speed;
        if (dist >= range)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Collectible")
        {
            other.gameObject.SetActive(false);
            gm.score = gm.score +1;
            gm.setScore();
        }

        if (other.gameObject.tag == "Key")
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            gm.foundKey = true;
        }

        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

}