using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapItem : MonoBehaviour {

    //Reward of the item
    public int value = 1;
    
    //Set when picked up to prevent two entities fighting over the item
    public EntityCultMember currentHolder;
    
    public EntityCultMember currentEntityPathingTowards; //TODO have priority overides
    
    //Set to true by the machine when nearby
    public bool placedNearMachine = false;
    
    private Collider2D collider;
    
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    
    public void pickup(EntityCultMember entity)
    {
        currentEntityPathingTowards = null;
        
        if(currentHolder != null)
        {
            drop(currentHolder);
        }
        currentHolder = entity;
        
        if(currentHolder != null)
        {
            gameObject.transform.SetParent(entity.gameObject.transform, false);
            gameObject.transform.localPosition = new Vector3(0, 0.1f, 0);
            entity.heldScrapItem = this;
            
            collider.enabled = false;
        }
    }
    
    public void drop(EntityCultMember entity)
    {
        if(currentHolder != null)
        {
           entity.heldScrapItem = null;
        }
        
        collider.enabled = true;
        currentHolder = null;
        gameObject.transform.SetParent(null, true);
    }
}
