using UnityEngine;

public abstract class MazeCellEdge : MonoBehaviour {

	public MazeCell cell, otherCell;

	public MazeDirection direc;

	public void Initialize (MazeCell cell1, MazeCell cell2, MazeDirection direc) {
		this.cell = cell1;
		this.otherCell = cell2;
		this.direc = direc;
		cell1.SetEdge(direc, this);
		transform.parent = cell1.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = direc.ToRotation();
	}
}