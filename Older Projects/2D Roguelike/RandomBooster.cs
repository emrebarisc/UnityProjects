using System;
using UnityEngine;

public class RandomBooster : MonoBehaviour
{
	private void Start()
	{
		base.gameObject.GetComponent<BoxCollider2D>().enabled = false;
		this.timeControl = Time.time;
		this.boosterSR = base.GetComponent<SpriteRenderer>();
		int num = UnityEngine.Random.Range(0, 100);
		if (num >= 0 && num < 5)
		{
			this.boosterSR.sprite = this.chopSpeed;
			this.boosterType = "chopSpeed";
			this.boostValue = UnityEngine.Random.Range(0.5f, 1f);
		}
		else if (num >= 5 && num < 10)
		{
			this.boosterSR.sprite = this.questionMark;
			switch (UnityEngine.Random.Range(1, 6))
			{
			case 1:
				this.boosterType = "chopSpeed";
				this.boostValue = UnityEngine.Random.Range(-0.5f, 0.5f);
				break;
			case 2:
				this.boosterType = "movementSpeed";
				this.boostValue = UnityEngine.Random.Range(-1f, 1f);
				break;
			case 3:
				this.boosterType = "attackSpeed";
				this.boostValue = UnityEngine.Random.Range(-0.5f, 0.5f);
				break;
			case 4:
				this.boosterType = "range";
				this.boostValue = UnityEngine.Random.Range(-1f, 1f);
				break;
			case 5:
				this.boosterType = "armor";
				this.boostValue = UnityEngine.Random.Range(-5f, 10f);
				break;
			}
		}
		else if (num >= 10 && num < 15)
		{
			this.boosterSR.sprite = this.movementSpeed;
			this.boosterType = "movementSpeed";
			this.boostValue = UnityEngine.Random.Range(0.1f, 1f);
		}
		else if (num >= 15 && num < 20)
		{
			this.boosterSR.sprite = this.attackSpeed;
			this.boosterType = "attackSpeed";
			this.boostValue = UnityEngine.Random.Range(0.1f, 0.5f);
		}
		else if (num >= 20 && num < 25)
		{
			this.boosterSR.sprite = this.range;
			this.boosterType = "range";
			this.boostValue = UnityEngine.Random.Range(0.5f, 1f);
		}
		else if (num >= 25 && num < 30)
		{
			this.boosterSR.sprite = this.armor;
			this.boosterType = "armor";
			this.boostValue = UnityEngine.Random.Range(0.5f, 10f);
		}
		else if (num >= 30 && num < 35)
		{
			this.boosterSR.sprite = this.health;
			this.boosterType = "health";
			this.boostValue = (float)UnityEngine.Random.Range(10, 30);
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		if (Time.time > this.timeControl + 1f)
		{
			base.gameObject.GetComponent<BoxCollider2D>().enabled = true;
		}
	}

	public Sprite chopSpeed;

	public Sprite questionMark;

	public Sprite movementSpeed;

	public Sprite attackSpeed;

	public Sprite range;

	public Sprite armor;

	public Sprite health;

	public string boosterType;

	public float boostValue;

	private SpriteRenderer boosterSR;

	private float timeControl;
}
