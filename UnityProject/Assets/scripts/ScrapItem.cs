using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapItem : MonoBehaviour {

    //Reward of the item
    public int value = 1;
    
    //Set when picked up to prevent two entities fighting over the item
    public EntityCultMember currentHolder;
    
    //Set to true by the machine when nearby
    public bool placedNearMachine = false;
    
    public void pickup(EntityCultMember entity)
    {
        if(currentHolder != null)
        {
            drop(currentHolder);
        }
        currentHolder = entity;
        
        if(currentHolder != null)
        {
            gameObject.transform.SetParent(entity.gameObject.transform, false);
            entity.heldScrapItem = this;
        }
    }
    
    public void drop(EntityCultMember entity)
    {
        if(currentHolder != null)
        {
           entity.heldScrapItem = null;
        }
        
        currentHolder = null;
        gameObject.transform.SetParent(null, true);
    }
}
