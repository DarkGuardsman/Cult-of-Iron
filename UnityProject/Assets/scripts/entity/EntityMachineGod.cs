using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMachineGod : Entity {

    public float dropRadius = 5f;
    public float consumeRadius = 6f;
    
    public int level = 0;
	
	// Update is called once per frame
	void Update () {
		//TODO consume scrap
        //TODO grow
        //TODO randomly generate events
	}
    
    public void giveScrap(EntityCultMember entity)
    {
        ScrapItem scrap = entity.heldScrapItem;
        scrap.drop(entity);
        scrap.placedNearMachine = true;
        //TODO drop scrap
        //TODO mark scrap as collected
        //TODO add to consume list
    }
}
