using System;
using UnityEngine;

public class Enemy : MovingObject
{
	protected override void Start()
	{
		this.animator = base.GetComponent<Animator>();
		this.target = GameObject.FindGameObjectWithTag("Player").transform;
		this.enemyAttackTime = Time.time;
		base.Start();
	}

	private void FixedUpdate()
	{
		this.MoveEnemy();
	}

	protected override void AttemptMove<T>(float xDir, float yDir)
	{
		base.AttemptMove<T>(xDir, yDir);
	}

	public void MoveEnemy()
	{
		if (Vector3.Distance(this.target.position, base.transform.position) < 15f)
		{
			float yDir = (float)((this.target.position.y <= base.transform.position.y) ? -1 : 1);
			float xDir = (float)((this.target.position.x <= base.transform.position.x) ? -1 : 1);
			if (base.transform.position.y - 0.1f < this.target.position.y && this.target.position.y < base.transform.position.y + 0.1f)
			{
				yDir = 0f;
			}
			if (base.transform.position.x - 0.1f < this.target.position.x && this.target.position.x < base.transform.position.x + 0.1f)
			{
				xDir = 0f;
			}
			this.AttemptMove<Player>(xDir, yDir);
		}
	}

	protected override void OnCantMove<T>(T component)
	{
		MonoBehaviour.print(component.name);
		if (Time.time > this.enemyAttackTime + 1f / this.enemyAttackSpeed)
		{
			this.enemyAttackTime = Time.time;
			Player player = component as Player;
			this.animator.SetTrigger("enemyAttack");
			player.LoseHealth(this.playerDamage);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Bullet")
		{
			float num = (this.target.position.x >= base.transform.position.x) ? -0.2f : 0.2f;
			float num2 = (this.target.position.y >= base.transform.position.y) ? -0.2f : 0.2f;
			if (base.transform.position.y - 0.1f < this.target.position.y && this.target.position.y < base.transform.position.y + 0.1f)
			{
				num2 = 0f;
			}
			if (base.transform.position.x - 0.1f < this.target.position.x && this.target.position.x < base.transform.position.x + 0.1f)
			{
				num = 0f;
			}
			if (num != 0f && num2 != 0f)
			{
				if (num > 0f)
				{
					num = 0.1414f;
				}
				else
				{
					num = -0.1414f;
				}
				if (num2 > 0f)
				{
					num2 = 0.1414f;
				}
				else
				{
					num2 = -0.1414f;
				}
			}
			Vector3 vector = new Vector3(base.transform.position.x + num, base.transform.position.y + num2, 0f);
			if (Physics2D.Linecast(base.transform.position, vector, this.blockingLayer).transform != null)
			{
				base.transform.position = vector;
			}
			this.enemyHealthPoint -= this.target.GetComponent<Player>().damage;
			this.enemyHpBar.localScale = new Vector2(this.enemyHealthPoint / 100f, 1f);
			UnityEngine.Object.Destroy(other.gameObject);
			this.checkIfEnemyDied();
		}
	}

	private void checkIfEnemyDied()
	{
		if (this.enemyHealthPoint <= 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public int playerDamage = 10;

	public float enemyHealthPoint = 100f;

	public float enemyArmor = 10f;

	public float enemyAttackSpeed = 0.5f;

	public Transform enemyHpBar;

	private Animator animator;

	private Transform target;

	private float enemyAttackTime;
}
