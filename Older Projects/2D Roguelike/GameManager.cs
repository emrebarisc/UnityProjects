using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private void Awake()
	{
		if (GameManager.instance == null)
		{
			GameManager.instance = this;
		}
		else if (GameManager.instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.boardScript = base.GetComponent<BoardManager>();
		this.InitGame();
	}

	private void OnLevelWasLoaded(int index)
	{
		this.level++;
		this.InitGame();
	}

	private void InitGame()
	{
		this.boardScript.SetupScene(this.level);
	}

	public void GameOver()
	{
		base.enabled = false;
	}

	public float turnDelay = 0.1f;

	public static GameManager instance;

	public BoardManager boardScript;

	private int level = 1;

	private bool enemiesMoving;
}
