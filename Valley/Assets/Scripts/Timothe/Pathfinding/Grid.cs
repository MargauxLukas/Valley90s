using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public static Grid instance;

	public bool displayGridGizmos;
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;

	public GameObject caseSprite;
	public Transform caseParent;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Awake() {
		instance = this;
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
		CreateGrid();
	}

    public int MaxSize {
		get {
			return gridSizeX * gridSizeY;
		}
	}

	public Vector4 WorldBounds
    {
        get { return new Vector4(-gridWorldSize.x/2, gridWorldSize.x / 2, -gridWorldSize.x / 2, gridWorldSize.y / 2); }
    }

	public void CreateGrid() {
		grid = new Node[gridSizeX,gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;

		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				//bool walkable = (Physics.OverlapSphere(worldPoint, nodeRadius, unwalkableMask)).Length<=0;
				bool walkable = (Physics.OverlapBox(worldPoint, new Vector3(nodeRadius, 50, nodeRadius), Quaternion.identity, unwalkableMask)).Length<=0;
				grid[x,y] = new Node(walkable, worldPoint, x, y);
			}
		}
	}

	public Node GetNode(int x, int y)
    {
		return grid[x, y];
    }

	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}
	
	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y/2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);

		return grid[x,y];
	}

	public Node GetRandomNodePosition()
	{
		return grid[Random.Range(0, gridSizeX), Random.Range(0, gridSizeY)];
	}
	
	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,1, gridWorldSize.y));
		if (grid != null && displayGridGizmos) {
			foreach (Node n in grid) {
				bool redColor = n.walkable;
				Gizmos.color = redColor?Color.white:Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter/2));
			}
		}
	}
}