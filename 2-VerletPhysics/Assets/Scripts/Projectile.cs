using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public bool projectileHit;
    public int idleCounter;
    private float Barrel_angle;
    private Vector2 Init_velocity;
    public Vector2 Velocity;
    public Vector2 Position;
    private float Gravity;
    private float AirResistance;
    private float[] WindsArray;
    private float CurrentWind;
    private float currentTime;
    public float windChangeInterval;
    public float yAcceleration = 8.5f;
    public float xAcceleration = 9.5f;
    public float bounceCoef = 3.0f;

    public static List<GameObject>[] StoneHengeBlocksList;
    public static List<Vector3>[] pointList;


    GameObject[] StoneHengeList;

    void Start()
    {
        projectileHit = false;
        idleCounter = 0;
        currentTime = 0f;
        windChangeInterval = 1.0f;
        WindsArray = new float[] { -0.025f, -0.01f, 0.05f, 0.15f };
        Gravity = -9.8f;
        AirResistance = -1.5f;

        GameObject BarrelTip = GameObject.Find("BarrelTip");

        CannonShoot CannonShoot = BarrelTip.GetComponent<CannonShoot>();

        Barrel_angle = CannonShoot.BarrelAngleNow.eulerAngles.z;
        Barrel_angle = Barrel_angle / 180 * Mathf.PI;

        Init_velocity = new Vector2(Mathf.Cos(Barrel_angle), Mathf.Sin(Barrel_angle));

        Velocity = Init_velocity;
        Velocity.x *= xAcceleration*2.0f;
        Velocity.y *= yAcceleration * 1.2f;

        StoneHengeList = GameObject.FindGameObjectsWithTag("StoneCreator");

        StoneHengeBlocksList = new List<GameObject>[StoneHengeList.Length];
        pointList = new List<Vector3>[StoneHengeList.Length];

        //call on StoneHengeCreator
        for (int i = 0; i < StoneHengeList.Length; i++)
        {
            StoneHengeList[i].SendMessage("updateStoneList", i);
        }
    }
    // Update is called once per frame
    void Update()
    {
        projectileBoundaries();

        GameObject CloudText = GameObject.Find("CloudText");
        

        //every second wind changes
        if (Time.time > currentTime)
        {
            currentTime += windChangeInterval;
            CurrentWind = WindsArray[Random.Range(0, WindsArray.Length - 1)];
        }

        CloudText.GetComponent<UnityEngine.UI.Text>().text = "Wind Power: " + CurrentWind;


            Velocity.y = Velocity.y + Gravity * Time.deltaTime;
            Velocity.x = Velocity.x + AirResistance * Time.deltaTime + CurrentWind;
            gameObject.transform.Translate(Velocity * Time.deltaTime);
            Position = gameObject.transform.position;

        for (int i = 0; i < StoneHengeBlocksList.Length; i++)
        {
            int j = 0;
            foreach (GameObject StoneHengeLine in StoneHengeBlocksList[i])
            {
                if ((StoneHengeLine.transform.position.x - transform.position.x) > (StoneHengeLine.transform.localScale.x / 4) &&
                    (transform.position.y - StoneHengeLine.transform.position.y) < (transform.localScale.y / 4) &&
                    (StoneHengeLine.transform.position.x - transform.position.x) < (StoneHengeLine.transform.localScale.x * 5))
                {
                    //bounce

                    Velocity = new Vector3(0, 0, 0);
                    //transform.Rotate(Vector3.right, 180.0f);
                    if (pointList[i][j].y < pointList[i][j + 1].y)
                    {
                        transform.position = new Vector3(transform.position.x - 0.07f, StoneHengeLine.transform.position.y + transform.localScale.y / 4, 0);
                        idleCounter = 0;
                    }  
                    else
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                        idleCounter++;
                    }

                    if (!projectileHit)
                    {
                        StoneHengeList[i].SendMessage("updateStoneList", j);
                    }
                                                                             
                    projectileHit = true;
                }
                j++;
            }

        }

    }

    void projectileBoundaries() {

        if (transform.position.y < -4.0f)
        {
            Destroy(gameObject);
        }

        
    }

}
