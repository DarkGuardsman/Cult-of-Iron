using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : PlayerControls {

    private PlayerOptions playerOptions;
    
    // Use this for initialization
	public override void Start () 
    {
        base.Start();
        playerOptions = FindObjectOfType<PlayerOptions>();
	}
	
	// Update is called once per frame
	void Update () {
		//Get user input
        float moveHorizontal = GetHorizontal();
        float moveVertical = GetVertical();
        
        GameObject camera = gameController.cameraObject;
        camera.transform.Translate(
            Time.deltaTime * playerOptions.currentSettings.cameraMovementHorizontal * moveHorizontal, 
            Time.deltaTime * playerOptions.currentSettings.cameraMovementVertical * moveVertical, 
            0);
            
            //TODO if shift is held increase speed
            //TODO if shift + crol is held go sonic fast
        
	}
    
    float GetHorizontal()
    {
        if(inputManager.getInputActions().left.IsKeyDown())
        {
            return -1;
        }
        else if(inputManager.getInputActions().right.IsKeyDown())
        {
            return 1;
        }
        return Input.GetAxis ("Horizontal");
    }
    
     float GetVertical()
    {
        if(inputManager.getInputActions().down.IsKeyDown())
        {
            return -1;
        }
        else if(inputManager.getInputActions().up.IsKeyDown())
        {
            return 1;
        }
        return Input.GetAxis ("Vertical");
    }
}
