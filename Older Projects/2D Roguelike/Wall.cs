using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
	private void Awake()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	public void DamageWall(int loss)
	{
		this.spriteRenderer.sprite = this.dmgSprite;
		this.hp -= loss;
		if (this.hp <= 0)
		{
			this.DestroyControl();
		}
	}

	private void DestroyControl()
	{
		if (this.finalSprite == null)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			base.gameObject.layer = 0;
			this.spriteRenderer.sprite = this.finalSprite;
		}
		UnityEngine.Object.Instantiate(this.booster, new Vector3(base.transform.position.x, base.transform.position.y + 0.5f, base.transform.position.y), this.booster.transform.rotation);
	}

	public Sprite dmgSprite;

	public Sprite finalSprite;

	public int hp;

	public GameObject booster;

	private SpriteRenderer spriteRenderer;
}
