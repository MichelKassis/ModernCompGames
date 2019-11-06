using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gt_Maze : MonoBehaviour {

	public IntVector2 size;

	public MazeCell cell_setup;

	public float genDelay;

	public MazePassage passage_prefab;
	public MazeWall wall_prefab;

	private MazeCell[,] cells;

	public IntVector2 RandomCoordinates
    {
		get
        {
			return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}


	public bool ContainsCoordinates (IntVector2 coord)
    {
		return coord.x >= 0 && coord.x < size.x && coord.z >= 0 && coord.z < size.z;
	}

	public MazeCell Get_Cell (IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.z];
	}

	public IEnumerator Generate ()
    {
		WaitForSeconds delay = new WaitForSeconds(genDelay);
		cells = new MazeCell[size.x, size.z];
		List<MazeCell> activeCells = new List<MazeCell>();
		GenerateFirstStep(activeCells);
		while (activeCells.Count > 0)
        {
			yield return delay;
			GenerateNextStep(activeCells);
		}
	}

	private void GenerateFirstStep (List<MazeCell> activeCells) {
		activeCells.Add(Create_Cell(RandomCoordinates));
	}

	private void GenerateNextStep (List<MazeCell> activeCells) {
		int currentIndex = activeCells.Count - 1;
		MazeCell current_cell = activeCells[currentIndex];
		if (current_cell.IsFullInit) {
			activeCells.RemoveAt(currentIndex);
			return;
		}
		MazeDirection direc = current_cell.RandomInit;
		IntVector2 coordinates = current_cell.coordinates + direc.ToIntVector2();
		if (ContainsCoordinates(coordinates)) {
			MazeCell neighbor = Get_Cell(coordinates);
			if (neighbor == null) {
				neighbor = Create_Cell(coordinates);
				Create_Passage(current_cell, neighbor, direc);
				activeCells.Add(neighbor);
			}
			else {
				Create_Wall(current_cell, neighbor, direc);
			}
		}
		else {
			Create_Wall(current_cell, null, direc);
		}
	}

	private MazeCell Create_Cell (IntVector2 coordinates) {
		MazeCell new_cell = Instantiate(cell_setup) as MazeCell;
		cells[coordinates.x, coordinates.z] = new_cell;
		new_cell.coordinates = coordinates;
		new_cell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		new_cell.transform.parent = transform;
		new_cell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
		return new_cell;
	}

	private void Create_Passage (MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
		MazePassage passage = Instantiate(passage_prefab) as MazePassage;

		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passage_prefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

	private void Create_Wall (MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
		MazeWall wall = Instantiate(wall_prefab) as MazeWall;
		wall.Initialize(cell, otherCell, direction);
		if (otherCell != null) {
			wall = Instantiate(wall_prefab) as MazeWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}
	}
}