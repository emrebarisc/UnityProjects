using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	private void Awake()
	{
		this.firePoint = base.transform.FindChild("FirePoint");
	}

	private void Update()
	{
		this.playerAttackSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().attackSpeed;
		if (Input.GetAxisRaw("Fire1") != 0f && (this.timeToFire == 0f || Time.time > this.timeToFire + 1f / this.playerAttackSpeed))
		{
			this.timeToFire = Time.time;
			this.Shoot();
		}
	}

	private void Shoot()
	{
		UnityEngine.Object.Instantiate(this.bulletPrefab, this.firePoint.position, this.firePoint.rotation);
	}

	public Transform bulletPrefab;

	public LayerMask whatToHit;

	private float playerAttackSpeed;

	private float timeToFire;

	private Transform firePoint;
}
