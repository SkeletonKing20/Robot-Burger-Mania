﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;

	void Start () {
		player = GetComponent<Player> ();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetKeyDown (KeyCode.Space)) {
			player.OnJumpInputDown ();
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			player.OnJumpInputUp ();
		}
        if (Input.GetKeyDown (KeyCode.S))
        {
			player.OnSInputDown ();
        }
		if (Input.GetKeyUp(KeyCode.S))
		{
			player.OnSInputUp();
		}
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			player.OnMouseLeftDown();
		}
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			player.OnMouseRightDown();
		}
	}
}
