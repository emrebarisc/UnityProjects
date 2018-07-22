using System;
using UnityEngine;

public class Loader : MonoBehaviour
{
	private void Awake()
	{
		if (GameManager.instance == null)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.gameManager);
		}
	}

	private void Update()
	{
	}

	public GameObject gameManager;
}
