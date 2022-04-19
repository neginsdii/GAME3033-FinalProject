using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{
	public Transform spawnPoint;
	public GameObject player;
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			player = other.gameObject;
			StartCoroutine(TeleportPlayer());
		}


	}

	IEnumerator TeleportPlayer()
	{
		yield return new WaitForSeconds(1.0f);
		player.gameObject.transform.position = spawnPoint.transform.position;

	}
}
