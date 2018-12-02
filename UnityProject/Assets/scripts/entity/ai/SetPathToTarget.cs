using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

//Sets path to target or objective
public class SetPathToTarget : MonoBehaviour {
	
	IAstarAI ai;
    public EntityMob host;
    
    void Awake ()
    {
        host = gameObject.GetComponent<EntityMob>();
        ai = gameObject.GetComponent<IAstarAI>();
    }

	void OnEnable () 
    {        
        //Trigger AI to repath
		if (ai != null)
        {            
            ai.onSearchPath += FixedUpdate;
        }
	}

	void OnDisable () 
    {
		if (ai != null) 
        {
            ai.onSearchPath -= FixedUpdate;
        }
	}

	/** Updates the AI's destination every frame */
	void FixedUpdate ()
    {
		if (host != null) 
        {
            if(host.currentTarget != null)
            {
                ai.destination = host.currentTarget.gameObject.transform.position;
            }
            else if(host.currentObjective != null)
            {
                ai.destination = host.currentObjective.transform.position;
            }
        }
	}
}	
