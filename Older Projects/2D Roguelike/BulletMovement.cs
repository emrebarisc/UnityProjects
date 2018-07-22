using System;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
	private void Start()
	{
		this.bulletLineRenderer = base.GetComponent<LineRenderer>();
		this.bulletLineRenderer.sortingLayerName = "Units";
		this.target = GameObject.FindGameObjectWithTag("Player");
		this.initialPoint = this.bulletLineRenderer.transform.position;
	}

	private void FixedUpdate()
	{
		base.transform.Translate(Vector3.right * this.bulletSpeed * Time.deltaTime);
		if (Vector3.Distance(this.initialPoint, base.transform.position) >= this.target.gameObject.GetComponent<Player>().attackRange)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (Physics2D.Linecast(base.transform.position, base.transform.position, this.bulletProof).transform != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public LayerMask bulletProof;

	private float bulletSpeed = 30f;

	private LineRenderer bulletLineRenderer;

	private Vector2 initialPoint;

	private GameObject target;
}
