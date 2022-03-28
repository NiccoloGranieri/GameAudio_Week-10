using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Height2Float : MonoBehaviour
{

	public LibPdInstance pdPatch;

	private Transform player;

	void Start()
	{
		this.player = GameObject.FindWithTag("Player").transform;
	}

	void Update()
	{
		float height = this.player.position.y;
        height /= 16.0f;
		Debug.Log(height);

		pdPatch.SendFloat("height", height);
	}
}