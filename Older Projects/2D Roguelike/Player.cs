using System;
using UnityEngine;

public class Player : MovingObject
{
	protected override void Start()
	{
		this.animator = base.GetComponent<Animator>();
		this.attackTime = Time.time;
		UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
		base.Start();
		this.statCanvas = GameObject.FindGameObjectWithTag("StatUI").gameObject;
		this.uiSetting = this.statCanvas.GetComponent<StatsUI>();
	}

	private void OnLevelWasLoaded()
	{
		base.transform.position = Vector3.zero;
		base.enabled = true;
	}

	private void FixedUpdate()
	{
		float num = (float)((int)Input.GetAxisRaw("Horizontal"));
		float num2 = (float)((int)Input.GetAxisRaw("Vertical"));
		MonoBehaviour.print(string.Concat(new object[]
		{
			this.playerFoodPoints,
			" ",
			num,
			" ",
			num2
		}));
		if (num != 0f || num2 != 0f)
		{
			this.AttemptMove<Wall>(num, num2);
		}
		if (Input.GetKeyDown(KeyCode.Space) && (this.timeToTeleport == 0f || Time.time > this.timeToTeleport + this.teleportDelay))
		{
			this.teleport();
		}
	}

	private void teleport()
	{
		Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 vector2 = base.transform.position;
		float num = Vector2.Distance(vector2, vector);
		if (num > 3f)
		{
			vector = Vector3.MoveTowards(vector2, vector, 3f);
		}
		RaycastHit2D raycastHit2D = Physics2D.Linecast(vector, vector, this.blockingLayer);
		RaycastHit2D raycastHit2D2 = Physics2D.Linecast(vector2, vector, this.teleportBlockingLayer);
		if (raycastHit2D.transform == null && raycastHit2D2.transform == null)
		{
			this.timeToTeleport = Time.time;
			base.transform.position = Vector3.MoveTowards(vector2, vector, 3f);
		}
	}

	private void RestartAfterDeath()
	{
		Time.timeScale = 1f;
	}

	protected override void OnCantMove<T>(T component)
	{
		if (this.attackTime == 0f || Time.time > this.attackTime + 1f / this.chopSpeed)
		{
			this.attackTime = Time.time;
			Wall wall = component as Wall;
			wall.DamageWall(this.wallDamage);
			this.animator.SetTrigger("playerChop");
		}
	}

	private void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void LoseHealth(int loss)
	{
		MonoBehaviour.print("LOSS: " + loss);
		this.animator.SetTrigger("playerHit");
		this.playerHealth -= (float)loss - (float)loss * this.playerArmor / 100f;
		Renderer component = this.playerHpBar.GetComponent<Renderer>();
		this.setHealthBar();
	}

	private void setHealthBar()
	{
		if (this.playerHealth >= 0f)
		{
			this.playerHpBar.localScale = new Vector2(this.playerHealth / 100f, 1f);
		}
		else
		{
			this.playerHpBar.localScale = new Vector2(0f, 1f);
			this.GameOver();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Exit")
		{
			base.Invoke("Restart", this.restartLevelDelay);
			base.enabled = false;
		}
		else if (other.tag == "Food")
		{
			this.playerFoodPoints += this.pointsPerFood;
			other.gameObject.SetActive(false);
			this.uiSetting.updateFoodText(this.playerFoodPoints);
		}
		else if (other.tag == "Soda")
		{
			this.playerFoodPoints += this.pointsPerSoda;
			other.gameObject.SetActive(false);
			this.uiSetting.updateFoodText(this.playerFoodPoints);
		}
		else if (other.tag == "Booster")
		{
			float boostValue = other.GetComponent<RandomBooster>().boostValue;
			string boosterType = other.GetComponent<RandomBooster>().boosterType;
			switch (boosterType)
			{
			case "chopSpeed":
				if (this.chopSpeed + boostValue > 1f && this.chopSpeed + boostValue < 5f)
				{
					this.chopSpeed += boostValue;
				}
				else if (this.chopSpeed + boostValue >= 5f)
				{
					this.chopSpeed = 5f;
				}
				this.uiSetting.updateChopSpeedText(this.chopSpeed);
				goto IL_40D;
			case "movementSpeed":
				if (this.movementSpeed + boostValue > 5f && this.movementSpeed + boostValue < 8f)
				{
					this.movementSpeed += boostValue;
				}
				else if (this.movementSpeed + boostValue >= 8f)
				{
					this.movementSpeed = 8f;
				}
				this.uiSetting.updateMovementSpeedText(this.movementSpeed);
				goto IL_40D;
			case "attackSpeed":
				if (this.attackSpeed + boostValue > 1f && this.attackSpeed + boostValue < 5f)
				{
					this.attackSpeed += boostValue;
				}
				else if (this.attackSpeed + boostValue >= 5f)
				{
					this.attackSpeed = 5f;
				}
				this.uiSetting.updateAttackSpeedText(this.attackSpeed);
				goto IL_40D;
			case "range":
				if (this.attackRange + boostValue > 5f && this.attackRange + boostValue < 8f)
				{
					this.attackRange += boostValue;
				}
				else if (this.attackRange + boostValue >= 8f)
				{
					this.attackRange = 8f;
				}
				this.uiSetting.updateRangeText(this.attackRange);
				goto IL_40D;
			case "armor":
				if (this.playerArmor + boostValue > 0f && this.playerArmor + boostValue < 50f)
				{
					this.playerArmor += boostValue;
				}
				else if (this.playerArmor + boostValue >= 50f)
				{
					this.playerArmor = 50f;
				}
				this.uiSetting.updateArmorText(this.playerArmor);
				goto IL_40D;
			case "health":
				if (this.playerHealth + boostValue > 0f && this.playerHealth + boostValue < 100f)
				{
					this.playerHealth += boostValue;
				}
				else if (this.playerHealth + boostValue >= 100f)
				{
					this.playerHealth = 100f;
				}
				this.setHealthBar();
				goto IL_40D;
			}
			MonoBehaviour.print("An Error Occured!");
			IL_40D:
			UnityEngine.Object.Destroy(other.gameObject);
		}
	}

	protected override void AttemptMove<T>(float xDir, float yDir)
	{
		base.AttemptMove<T>(xDir, yDir);
	}

	private void GameOver()
	{
		Time.timeScale = 0f;
	}

	public int wallDamage = 1;

	public int pointsPerFood = 10;

	public int pointsPerSoda = 20;

	public LayerMask teleportBlockingLayer;

	public Transform playerHpBar;

	[HideInInspector]
	public int gold;

	[HideInInspector]
	public int playerFoodPoints = 100;

	[HideInInspector]
	public float attackSpeed = 1f;

	[HideInInspector]
	public float chopSpeed = 2f;

	[HideInInspector]
	public float damage = 10f;

	[HideInInspector]
	public float attackRange = 5f;

	[HideInInspector]
	public float playerHealth = 100f;

	[HideInInspector]
	public float playerArmor = 5f;

	public float restartLevelDelay = 1f;

	private GameObject statCanvas;

	private StatsUI uiSetting;

	private Animator animator;

	private float attackTime;

	private float timeToTeleport;

	private float teleportDelay = 2f;
}
