using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtTarget : AimAt 
{    
	public EntityMob host;
    
    public override Vector2 getTarget()
    {
        if(host.currentTarget != null)
        {
            return (Vector2)host.currentTarget.gameObject.transform.position;
        }
        return transform.forward;
    }   
}
