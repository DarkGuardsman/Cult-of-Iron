using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCultMember : EntityMob {

	public ScrapItem heldScrapItem;
    
    public EntityMachineGod theMachine;
    
    void Start()
    {
        theMachine = FindObjectOfType<EntityMachineGod>();
    }
}
