using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIMovementStateHandler : MonoBehaviour 
{
	public EntityMob host;
    public ShootAtTarget[] shootAtTargetArray;
    public AIPath pathMovementScript;
    
    //Should AI stop if can see and attack target
    public bool stopIfCanFire = true;
    
    //Distance to stop from objective
    public float objectiveStopDistance = 1f;
    public float targetStopDistance = 10f;
    public int ticksBetweenUpdates = 5;
    
    private int ticksUpdateTimer = 0;

    void Start ()
    {
        host = gameObject.GetComponent<EntityMob>();
        pathMovementScript = gameObject.GetComponent<AIPath>();
    }    
    
    void FixedUpdate () 
    {
        if(ticksUpdateTimer-- <= 0)
        {
            ticksUpdateTimer = ticksBetweenUpdates;
            
            //Reset
            pathMovementScript.enabled = true;
            
            //Run checks
            if(host.currentTarget != null)
            {
                float dist = Vector3.Distance(host.currentTarget.gameObject.transform.position, transform.position);
                if((targetStopDistance <= 0 || dist <= targetStopDistance) && shootAtTargetArray != null && CanWeaponsTarget())
                {
                    pathMovementScript.enabled = false;
                }
            }
            else if(host.currentObjective != null && objectiveStopDistance > 0)
            {
                float dist = Vector3.Distance(host.currentObjective.position, transform.position);
                if(dist <= objectiveStopDistance)
                {
                    pathMovementScript.enabled = false; 
                }
            }
        }
    }    
    
    bool CanWeaponsTarget ()
    {
        foreach(ShootAtTarget shootAtTarget in shootAtTargetArray)
        {
            if(shootAtTarget != null && !shootAtTarget.ShouldFireAtTarget())
            {
                return false;
            }
        }
        return true;
    }
}
