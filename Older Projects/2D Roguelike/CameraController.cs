using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
		this.cameraTransform = base.GetComponent<Transform>();
	}

	private void FixedUpdate()
	{
		this.playerPos = this.player.position;
		this.cameraPos = this.cameraTransform.position;
		if (base.gameObject.tag == "MainCamera")
		{
			if (Input.mousePosition.x > (float)(Screen.width - this.boundary) && Vector3.Distance(this.playerPos, this.cameraPos - new Vector3(-2f, 0f, 0f)) < 20f)
			{
				base.transform.position += new Vector3((float)this.speedOfTheCamera * Time.deltaTime, 0f, 0f);
			}
			if (Input.mousePosition.x < (float)(0 + this.boundary) && Vector3.Distance(this.playerPos, this.cameraPos - new Vector3(2f, 0f, 0f)) < 20f)
			{
				base.transform.position -= new Vector3((float)this.speedOfTheCamera * Time.deltaTime, 0f, 0f);
			}
			if (Input.mousePosition.y > (float)(Screen.height - this.boundary) && Vector3.Distance(this.playerPos, this.cameraPos - new Vector3(0f, 2f, 0f)) < 20f)
			{
				base.transform.position += new Vector3(0f, (float)this.speedOfTheCamera * Time.deltaTime, 0f);
			}
			if (Input.mousePosition.y < (float)(0 + this.boundary) && Vector3.Distance(this.playerPos, this.cameraPos - new Vector3(0f, -2f, 0f)) < 20f)
			{
				base.transform.position -= new Vector3(0f, (float)this.speedOfTheCamera * Time.deltaTime, 0f);
			}
		}
		else
		{
			if (Mathf.Abs(this.playerPos.x - this.cameraPos.x) >= 5f)
			{
				base.gameObject.transform.position += new Vector3(this.playerPos.x - this.cameraPos.x - (this.playerPos.x - this.cameraPos.x) % 5f, 0f, -10f);
			}
			if (Mathf.Abs(this.playerPos.y - this.cameraPos.y) >= 5f)
			{
				base.gameObject.transform.position += new Vector3(0f, this.playerPos.y - this.cameraPos.y - (this.playerPos.y - this.cameraPos.y) % 5f, -10f);
			}
		}
	}

	private Transform player;

	private Transform cameraTransform;

	private Vector3 playerPos;

	private Vector3 cameraPos;

	private int speedOfTheCamera = 10;

	private int boundary = 20;
}
