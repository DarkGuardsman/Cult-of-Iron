using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All player settings
[System.Serializable]
public class PlayerOptionData
{
    public float arrowMinScale = 0.3f;
    public float arrowMaxScale = 3f;
    
    public float cameraZoom = 8f;
    public float zoomSpeed = 0.1f;
    
    public int maxJunkSpawn = 1000;
    public float maxJunkSpeed = 4f;  //TODO remove
    
    public float cameraMovementHorizontal = 5f; //TODO save
    public float cameraMovementVertical = 5f; //TODO save
    
    public bool enableEffects = true;
    public bool enableShipTrail = true; //TODO remove
    public bool enableBulletTrail = true;
    public bool enableShipBasedMovement = true;	 //TODO remove
    public bool enableMouseAim = true;	 //TODO remove
    
    public void CopyInto(PlayerOptionData data)
    {
        data.arrowMinScale = arrowMinScale;
        data.arrowMaxScale = arrowMaxScale;
        
        data.cameraZoom = cameraZoom;
        data.zoomSpeed = zoomSpeed;
        
        data.maxJunkSpawn = maxJunkSpawn;
        
        data.cameraMovementHorizontal = cameraMovementHorizontal;
        data.cameraMovementVertical = cameraMovementVertical;
        
        data.enableEffects = enableEffects;
        data.enableShipTrail = enableShipTrail;
        data.enableBulletTrail = enableBulletTrail;
        data.enableShipBasedMovement = enableShipBasedMovement;
        data.enableMouseAim = enableMouseAim;
    }
}
