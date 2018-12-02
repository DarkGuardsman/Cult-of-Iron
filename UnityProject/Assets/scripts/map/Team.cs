using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour {
    
    public string name;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	} 
    
    public bool isHostile(Entity entity)
    {
        return entity.getTeam() != this;
    }
}
