using System;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
	private void Update()
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - base.transform.position;
		vector.Normalize();
		float z = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		base.transform.rotation = Quaternion.Euler(0f, 0f, z);
	}
}
