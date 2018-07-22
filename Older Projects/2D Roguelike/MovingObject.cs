using System;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
	protected virtual void Start()
	{
		this.boxCollider = base.GetComponent<BoxCollider2D>();
	}

	protected bool Move(float xDir, float yDir, out RaycastHit2D hit)
	{
		Vector2 vector = base.transform.position;
		Vector2 end = vector + new Vector2(xDir / 2f, yDir / 2f);
		this.boxCollider.enabled = false;
		hit = Physics2D.Linecast(vector, end, this.blockingLayer);
		this.boxCollider.enabled = true;
		if (hit.transform == null)
		{
			if (xDir != 0f && yDir != 0f)
			{
				if (xDir > 0f)
				{
					xDir = 0.7071f;
				}
				else
				{
					xDir = -0.7071f;
				}
				if (yDir > 0f)
				{
					yDir = 0.7071f;
				}
				else
				{
					yDir = -0.7071f;
				}
			}
			base.transform.position += new Vector3(xDir, yDir, 0f) * this.movementSpeed * Time.deltaTime;
			return true;
		}
		return false;
	}

	protected virtual void AttemptMove<T>(float xDir, float yDir) where T : Component
	{
		float xDir2 = xDir;
		float yDir2 = yDir;
		RaycastHit2D raycastHit2D;
		bool flag = this.Move(xDir2, yDir2, out raycastHit2D);
		if (raycastHit2D.transform == null)
		{
			return;
		}
		T component = raycastHit2D.transform.GetComponent<T>();
		if (!flag && component != null)
		{
			this.OnCantMove<T>(component);
		}
	}

	protected abstract void OnCantMove<T>(T Component) where T : Component;

	public LayerMask blockingLayer;

	[HideInInspector]
	public float movementSpeed = 5f;

	private BoxCollider2D boxCollider;
}
