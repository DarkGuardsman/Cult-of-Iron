using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScrap : MonoBehaviour {
    
    public bool debug = false;
    
    public EntityCultMember host;
    
    public float targetDelay = 1;
    public float targetRange = 100;
    public float pickUpDistance = 2;
    
    private float targetTimer = 0;
    
    private ScrapItem currentTarget;
	
	// Update is called once per frame
	void Update () 
    {
        if(shouldCollectScrap())
        {
            if(!host.hasTarget() && !host.hasObjective())
            {
                //Delay search to save on performance
                if(targetTimer > 0)
                {
                    targetTimer -= Time.deltaTime;
                    return;
                }
                
                //Find target if we have no valid target
                if(!isTargetValid(currentTarget))
                {
                    clearTarget();
                    findTarget();
                }
            }
            
            //Clear current target if different object is set
            if(currentTarget != null)
            {
                if(host.hasTarget())
                {
                    clearTarget();
                    log("CollectScrap#update() - clearing scrap reference has host has a target");
                }
                else if(currentTarget.currentEntityPathingTowards != null && currentTarget.currentEntityPathingTowards != host)
                {
                    clearTarget();
                    log("CollectScrap#update() - clearing scrap reference as its being pathed by another entity");
                }
                //else if(host.currentObjective != currentTarget.gameObject.transform)
                //{
                //    currentTarget = null;
                //    log("CollectScrap#update() - clearing scrap reference has host has an objective");
                //}
            }
            
            //Path to target if its valid
            if(isTargetValid(currentTarget))
            {
                //Set entity as path target
                currentTarget.currentEntityPathingTowards = host;
                
                //Set path
                host.currentObjective = currentTarget.gameObject.transform;
                    
                //Check pickup distance
                float distance = Vector3.Distance(gameObject.transform.position, currentTarget.gameObject.transform.position);
                log("Distance: " + distance);
                if(distance <= pickUpDistance)
                {
                    currentTarget.pickup(host);
                }
            }   
        }        
	}
    
    bool shouldCollectScrap()
    {
        return host.heldScrapItem == null; //TODO add role check
    }
    
    protected virtual void clearTarget()
    {
        if(currentTarget != null)
        {
           if(currentTarget.currentEntityPathingTowards == host)
           {
               currentTarget.currentEntityPathingTowards = null;
           }
           if(currentTarget.currentHolder == host)
           {
               currentTarget.drop(host);
           }
        }
        currentTarget = null;
    }
    
    protected virtual bool isTargetValid(ScrapItem target)
    {
        return target != null 
            && target.currentHolder == null 
            && !target.placedNearMachine
            && (target.currentEntityPathingTowards == null || target.currentEntityPathingTowards == host);
    }
    
    protected virtual bool isInTargetArea(ScrapItem target, float distance)
    {
        return distance < targetRange; //TODO check that we can path
    }
    
    void findTarget()
    {
        ScrapItem[] foundObjects = FindObjectsOfType<ScrapItem>() as ScrapItem[];
        
        float range = 1000;
        ScrapItem target = null;
        foreach (ScrapItem entity in foundObjects)
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
            currentTarget = target;
            log("CollectScrap#findTarget() - target found " + target);
        }
    }
    
    void log(string msg)
    {
        if(debug)
        {
            Debug.Log(msg);
        }
    }
}
