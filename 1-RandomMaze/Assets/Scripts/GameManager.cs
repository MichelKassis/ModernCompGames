using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public Gt_Maze mazePrefab;
	private Gt_Maze mazeInstance;
    public Text text;

    public int cubesNumber =3;
    public int score= 0;
    public bool foundKey = false;
    public bool teleported = false;
    public bool levelWon = false;
    public GameObject player;
    public GameObject key;

	private void Start () {
		BeginGame();
        score = 0;
        cubesNumber = 3;
        foundKey = false;
        teleported = false;
        levelWon = false;
    }

    public void setScore() {
        int cubesLeft = cubesNumber - score;
        text.text = ("Cubes Left: " + cubesLeft.ToString());
    }

	private void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			RestartGame();
		}
        if (score == cubesNumber) {
            text.text = ("Get Golden Key!");
            score = 0;
            key.SetActive(true);
        }
        if (foundKey) {
            text.text = ("Shoot red gate to get into maze!");

        }
        if (teleported) {
            text.text = ("Reach  Purple Diamond! Press ESC to reset Maze");
        }
        if (levelWon) {
            text.text = ("You won!!");
        }

	}

	private void BeginGame () {
		mazeInstance = Instantiate(mazePrefab) as Gt_Maze;
		StartCoroutine(mazeInstance.Generate());
	}

	private void RestartGame () {
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}
}