using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Maze mazePrefab;
    private Maze mazeInstance;

    void Start()
    {
        beginGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            restartGame();
        }
            
    }

    private void beginGame() {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        StartCoroutine(mazeInstance.Generate());
    }
    private void restartGame() {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        beginGame();
    }
}
