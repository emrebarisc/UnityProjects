using System;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.player = GameObject.FindGameObjectWithTag("Player").gameObject;
		this.updateChopSpeedText(this.player.GetComponent<Player>().chopSpeed);
		this.updateMovementSpeedText(this.player.GetComponent<Player>().movementSpeed);
		this.updateAttackSpeedText(this.player.GetComponent<Player>().attackSpeed);
		this.updateRangeText(this.player.GetComponent<Player>().attackRange);
		this.updateArmorText(this.player.GetComponent<Player>().playerArmor);
		this.updateFoodText(this.player.GetComponent<Player>().playerFoodPoints);
		this.updateGoldText(this.player.GetComponent<Player>().gold);
	}

	public void updateChopSpeedText(float chopSpeed)
	{
		this.chopSpeedText.text = ((int)(chopSpeed * 100f / 5f)).ToString();
	}

	public void updateMovementSpeedText(float movementSpeed)
	{
		this.movementSpeedText.text = ((int)(movementSpeed * 100f / 8f)).ToString();
	}

	public void updateAttackSpeedText(float attackSpeed)
	{
		this.attackSpeedText.text = ((int)(attackSpeed * 100f / 5f)).ToString();
	}

	public void updateRangeText(float attackRange)
	{
		this.rangeText.text = ((int)attackRange * 100 / 8).ToString();
	}

	public void updateArmorText(float playerArmor)
	{
		this.armorText.text = ((int)(playerArmor * 100f / 50f)).ToString();
	}

	public void updateFoodText(int food)
	{
		this.foodText.text = food.ToString();
	}

	public void updateGoldText(int gold)
	{
		this.goldText.text = gold.ToString();
	}

	public Text chopSpeedText;

	public Text movementSpeedText;

	public Text attackSpeedText;

	public Text rangeText;

	public Text armorText;

	public Text foodText;

	public Text goldText;

	private GameObject player;
}
