using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	private void BoardSetup()
	{
		this.coloumns = this.floorLength[UnityEngine.Random.Range(0, this.floorLength.Length)];
		this.rows = this.floorLength[UnityEngine.Random.Range(0, this.floorLength.Length)];
		this.prevNextFloor = -1;
		this.currentX = -1;
		this.currentY = -1;
		for (int i = 0; i < this.floorCount; i++)
		{
			this.boardHolder = new GameObject("Board").transform;
			this.nextColoumns = this.floorLength[UnityEngine.Random.Range(0, this.floorLength.Length)];
			this.nextRows = this.floorLength[UnityEngine.Random.Range(0, this.floorLength.Length)];
			this.nextFloor = this.nextFloorCoordinates[UnityEngine.Random.Range(0, this.nextFloorCoordinates.Length)];
			if (this.nextFloor == 0 && this.nextRows >= this.rows)
			{
				this.middlePoint = this.rows / 2;
			}
			else if (this.nextFloor == 0 && this.nextRows < this.rows)
			{
				this.middlePoint = this.nextRows / 2;
			}
			else if (this.nextFloor == 1 && this.nextColoumns >= this.coloumns)
			{
				this.middlePoint = this.coloumns / 2;
			}
			else if (this.nextFloor == 1 && this.nextColoumns < this.coloumns)
			{
				this.middlePoint = this.nextColoumns / 2;
			}
			for (int j = this.currentX; j < this.currentX + this.coloumns + 1; j++)
			{
				for (int k = this.currentY; k < this.currentY + this.rows + 1; k++)
				{
					GameObject original = this.floorTiles[UnityEngine.Random.Range(0, this.floorTiles.Length)];
					if ((this.prevNextFloor != 0 || ((j != this.currentX || k != this.currentY + this.prevMiddlePoint) && (j != this.currentX || k != this.currentY + this.prevMiddlePoint - 1) && (j != this.currentX || k != this.currentY + this.prevMiddlePoint + 1))) && (this.prevNextFloor != 1 || ((k != this.currentY || j != this.currentX + this.prevMiddlePoint) && (k != this.currentY || j != this.currentX + this.prevMiddlePoint - 1) && (k != this.currentY || j != this.currentX + this.prevMiddlePoint + 1))))
					{
						if (j == this.currentX || (this.nextFloor != 0 && j == this.currentX + this.coloumns) || (i == this.floorCount - 1 && j == this.currentX + this.coloumns) || (this.nextFloor == 0 && j == this.currentX + this.coloumns && (k <= this.currentY + this.middlePoint - 2 || k >= this.currentY + this.middlePoint + 2)) || (k == this.currentY || (this.nextFloor != 1 && k == this.currentY + this.rows)) || (i == this.floorCount - 1 && k == this.currentY + this.rows) || (this.nextFloor == 1 && k == this.currentY + this.rows && (j <= this.currentX + this.middlePoint - 2 || j >= this.currentX + this.middlePoint + 2)))
						{
							original = this.outerWallTiles[UnityEngine.Random.Range(0, this.outerWallTiles.Length)];
						}
						else if (j != 0 && k != 0 && (i != this.floorCount - 1 || j != this.currentX + this.coloumns - 1 || k != this.currentY + this.rows - 1))
						{
							this.gridPosition.Add(new Vector3((float)j, (float)k, 0f));
						}
					}
					GameObject gameObject;
					if (i != this.floorCount - 1 && this.nextFloor == 0 && j == this.currentX + this.coloumns && k >= this.currentY + this.middlePoint - 2 && k <= this.currentY + this.middlePoint + 2)
					{
						gameObject = (UnityEngine.Object.Instantiate(original, new Vector3((float)(j + 1), (float)k, 0f), Quaternion.identity) as GameObject);
						gameObject.transform.SetParent(this.boardHolder);
						gameObject = (UnityEngine.Object.Instantiate(original, new Vector3((float)(j + 2), (float)k, 0f), Quaternion.identity) as GameObject);
						gameObject.transform.SetParent(this.boardHolder);
					}
					else if (i != this.floorCount - 1 && this.nextFloor == 1 && k == this.currentY + this.rows && j >= this.currentX + this.middlePoint - 2 && j <= this.currentX + this.middlePoint + 2)
					{
						gameObject = (UnityEngine.Object.Instantiate(original, new Vector3((float)j, (float)(k + 1), 0f), Quaternion.identity) as GameObject);
						gameObject.transform.SetParent(this.boardHolder);
						gameObject = (UnityEngine.Object.Instantiate(original, new Vector3((float)j, (float)(k + 2), 0f), Quaternion.identity) as GameObject);
						gameObject.transform.SetParent(this.boardHolder);
					}
					gameObject = (UnityEngine.Object.Instantiate(original, new Vector3((float)j, (float)k, 0f), Quaternion.identity) as GameObject);
					gameObject.transform.SetParent(this.boardHolder);
				}
			}
			if (i == this.floorCount - 1)
			{
				UnityEngine.Object.Instantiate(this.exit, new Vector3((float)(this.currentX + this.coloumns - 1), (float)(this.currentY + this.rows - 1), 0f), Quaternion.identity);
			}
			if (this.nextFloor == 0)
			{
				this.currentX += this.coloumns + 3;
			}
			else if (this.nextFloor == 1)
			{
				this.currentY += this.rows + 3;
			}
			this.coloumns = this.nextColoumns;
			this.rows = this.nextRows;
			this.prevNextFloor = this.nextFloor;
			this.prevMiddlePoint = this.middlePoint;
		}
	}

	private Vector3 RandomPosition()
	{
		int index = UnityEngine.Random.Range(0, this.gridPosition.Count);
		Vector3 result = this.gridPosition[index];
		this.gridPosition.RemoveAt(index);
		return result;
	}

	private void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
	{
		int num = UnityEngine.Random.Range(minimum, maximum + 1);
		for (int i = 0; i < num; i++)
		{
			Vector3 position = this.RandomPosition();
			GameObject original = tileArray[UnityEngine.Random.Range(0, tileArray.Length)];
			UnityEngine.Object.Instantiate(original, position, Quaternion.identity);
		}
	}

	public void SetupScene(int level)
	{
		this.floorCount = level + 2;
		this.wallCount = new BoardManager.Count((int)Math.Pow((double)level, 1.0), (int)Mathf.Pow((float)level, 2f));
		this.foodCount = new BoardManager.Count((int)Math.Pow((double)level, 1.0), (int)Mathf.Pow((float)level, 2f));
		this.gridPosition.Clear();
		this.BoardSetup();
		this.LayoutObjectAtRandom(this.wallTiles, this.wallCount.minimum, this.wallCount.maximum);
		this.LayoutObjectAtRandom(this.foodTiles, this.foodCount.minimum, this.foodCount.maximum);
		int num = 1 + level * (int)Mathf.Log((float)level, 2f);
		this.LayoutObjectAtRandom(this.enemyTiles, num, num);
		if (GameObject.FindGameObjectWithTag("StatUI") == null)
		{
			UnityEngine.Object.Instantiate(this.statsUI, this.statsUI.transform.position, this.statsUI.transform.rotation);
		}
		if (GameObject.FindGameObjectWithTag("Player") == null)
		{
			UnityEngine.Object.Instantiate(this.player, Vector3.zero, this.player.transform.rotation);
		}
	}

	public int coloumns = 8;

	public int rows = 8;

	public BoardManager.Count wallCount;

	public BoardManager.Count foodCount;

	public GameObject exit;

	public GameObject[] floorTiles;

	public GameObject[] foodTiles;

	public GameObject[] wallTiles;

	public GameObject[] enemyTiles;

	public GameObject[] outerWallTiles;

	public int[] nextFloorCoordinates = new int[]
	{
		0,
		1
	};

	public GameObject player;

	public GameObject statsUI;

	private int currentX;

	private int currentY;

	private int floorCount;

	private int prevNextFloor;

	private int nextColoumns;

	private int nextRows;

	private int nextFloor;

	private int middlePoint;

	private int prevMiddlePoint;

	private int[] floorLength = new int[]
	{
		4,
		8,
		16,
		24
	};

	private Transform boardHolder;

	private List<Vector3> gridPosition = new List<Vector3>();

	[Serializable]
	public class Count
	{
		public Count(int min, int max)
		{
			this.minimum = min;
			this.maximum = max;
		}

		public int minimum;

		public int maximum;
	}
}
