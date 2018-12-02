using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityMob))]
public class FindTarget : MonoBehaviour 
{	
    public float targetDelay = 1;
    public float targetRange = 100;
    
    [SerializeField]
    protected EntityMob host;
    
    private float targetTimer = 0;
    
    void Start ()
    {
        host = gameObject.GetComponent<EntityMob>();
        targetTimer = targetDelay;
    }
    
	// Update is called once per frame
	void Update () 
    {
		if(!isTargetValid(host.currentTarget))
        {
            //Set target to null in case it was set but not valid
            host.currentTarget = null;
            
            //Delay to target again (reduces lag issues)
            if(targetTimer <= 0)
            {
                targetTimer = targetDelay;
                findTarget();
            }
            else
            {
                targetTimer -= Time.deltaTime;
            }
        }
	}
    
    protected virtual bool isTargetValid(Entity target)
    {
        return target != null && host.getTeam() != null && host.getTeam().isHostile(target);
    }
    
    protected virtual bool isInTargetArea(Entity target, float distance)
    {
        return distance < targetRange; //TODO add line of sight check
    }
    
    void findTarget()
    {
        Entity[] foundObjects = FindObjectsOfType<Entity>() as Entity[];
        
        float range = 1000;
        Entity target = null;
        foreach (Entity entity in foundObjects)
        {
            if(isTargetValid(entity))
            {
                float distance = Vector3.Distance(gameObject.transform.position, entity.gameObject.transform.position);
                if(isInTargetArea(target, distance) && (target == null || distance < range))
                {
                    target = entity;
                    range = distance;
                }
            }
        }
        
        
        //Set target
        if(target != null)
        {
            host.currentTarget = target;
        }
    }
}
