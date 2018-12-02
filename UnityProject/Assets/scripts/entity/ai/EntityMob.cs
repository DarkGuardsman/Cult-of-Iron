using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMob : Entity {
    
    //Entity to attack
    public Entity currentTarget;
    
    //Location to move towards if no target
    public Transform currentObjective;
    
    public bool hasTarget()
    {
        return currentTarget != null;
    }
    
    public bool hasObjective()
    {
        return currentObjective != null;
    }
}
