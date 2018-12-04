using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnScrap : MonoBehaviour {
    
	public EntityCultMember host;
    
	// Update is called once per frame
	void FixedUpdate () {
		if(host.heldScrapItem != null)
        {
            host.currentObjective = host.theMachine.gameObject.transform;
            
            //Check pickup distance
            float distance = Vector3.Distance(gameObject.transform.position, host.theMachine.gameObject.transform.position);
            if(distance < host.theMachine.dropRadius)
            {
                host.theMachine.giveScrap(host);
                host.currentObjective = null;
            }
        }
	}
}
