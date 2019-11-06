using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StoneHengeCreator : MonoBehaviour
{
    public Vector3 begin;
    public Vector3 end;
    List<Vector3> pointList;
    public List<GameObject> StoneHengeParts;

    void Start()
    {
        StoneHengeParts = new List<GameObject>();
        pointList = new List<Vector3>();

        pointList.Add(begin);
        pointList.Add(end);
        spawnMountain(pointList, 5);
    }

    void spawnMountain(List<Vector3> pointsList, int depth)
    {
        //Midpoint
        for (int i = 0; i < depth; i++)
        {
            int prevCount = pointsList.Count;
            for (int x = 0; x < pointsList.Count - 1; x += 2)
            {
                pointsList.Insert(x + 1, getMidPoint(pointsList[x], pointsList[x + 1]));
                if (x + 1 != 0 && x + 1 != pointsList.Count - 1)
                {
                    Vector3 lineVec = pointsList[x + 2] - pointsList[x];
                    Vector3 pLineVec = new Vector3(-lineVec.y, lineVec.x, 0);
                    pointsList[x + 1] = pointsList[x + 1] + (Random.Range(-0.1f, 0.1f) * pLineVec);
                }
            }

        }
        for (int i = 0; i < pointsList.Count - 1; i++)
        {
            GameObject temporaryCube;
            temporaryCube = (GameObject)Instantiate(Resources.Load("StoneCube") as GameObject, getMidPoint(pointsList[i], pointsList[i + 1]), Quaternion.identity);

            temporaryCube.tag = "Stone";

            temporaryCube.transform.rotation = (Quaternion.LookRotation(pointsList[i + 1] - pointsList[i]));
            temporaryCube.transform.localScale = new Vector3(0.05f, 0.05f, Vector3.Distance(pointsList[i], pointsList[i + 1]));
            StoneHengeParts.Add(temporaryCube);
        }
    }
    Vector3 getMidPoint(Vector3 point1, Vector3 point2)
    {
        Vector3 midpointVector = new Vector3((point1.x + point2.x) / 2, (point1.y + point2.y) / 2, 0);
        return midpointVector;
    }

    //updates the bullet lists to the newest mountain
    void updateStoneList(int i)
    {
        Projectile.StoneHengeBlocksList[i] = StoneHengeParts;
        Projectile.pointList[i] = pointList;
    }

}