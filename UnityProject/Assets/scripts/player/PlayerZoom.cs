﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerZoom : PlayerControls
{    
    public float minZoom = 5;
    public float maxZoom = 20;
    
    private PlayerOptions playerOptions;
    //private CinemachineVirtualCamera cinemachineCamera;
    
    private Camera camera;
    
    // Use this for initialization
	public override void Start () 
    {
        base.Start();
        playerOptions = FindObjectOfType<PlayerOptions>();
        camera = gameObject.GetComponent<Camera>();
        //cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Get input
        float zoom = GetZoom();
         
        camera.orthographicSize -= playerOptions.currentSettings.zoomSpeed * zoom;
       
        if(camera.orthographicSize > maxZoom)
        {
            camera.orthographicSize = maxZoom;
        }
        else if(camera.orthographicSize < minZoom)
        {
            camera.orthographicSize = minZoom;
        }
        
        /*
        //Do zoom
        cinemachineCamera.m_Lens.OrthographicSize -= playerOptions.currentSettings.zoomSpeed * zoom;
        
        //Clamp
        if(cinemachineCamera.m_Lens.OrthographicSize > maxZoom)
        {
            cinemachineCamera.m_Lens.OrthographicSize = maxZoom;
        }
        else if(cinemachineCamera.m_Lens.OrthographicSize < minZoom)
        {
            cinemachineCamera.m_Lens.OrthographicSize = minZoom;
        }
        */
	}
    
    float GetZoom()
    {
        if(inputManager.getInputActions().zoomIn.IsKeyDown())
        {
            return 1;
        }
        else if(inputManager.getInputActions().zoomOut.IsKeyDown())
        {
            return -1;
        }
        return Input.GetAxis("Zoom");
    }
}
