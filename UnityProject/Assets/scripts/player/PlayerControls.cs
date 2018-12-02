using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    protected PlayerInputManager inputManager;
    protected GameController gameController;
    
	// Use this for initialization
	public virtual void Start () 
    {
        gameController = FindObjectOfType<GameController>();
		inputManager = FindObjectOfType<PlayerInputManager>();
	}
}
