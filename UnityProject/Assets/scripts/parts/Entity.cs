using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour 
{    
    public int teamID = 0;
    public float pauseMovementOnTeleportDelay = 1f;
    private float pauseMovementOnTeleportTimer = 0f;
    
    [SerializeField]
    private Team team;  
    
    [SerializeField]    
    private TeamManager teamManager;
    
    [SerializeField] 
    private DamageData damageData;
    
    //Value of the unity when killed
    public int basePointValue = 1;  
    
    // Called when component wakes up
    protected virtual void Awake ()
    {           
       
    }
    
	// Use this for initialization
	void Start () 
    {
        //Get our team
		teamManager = (TeamManager)FindObjectOfType(typeof(TeamManager));
        team = teamManager.getTeam(teamID);
        
        //Get our damage data
        damageData = gameObject.GetComponent<DamageData>();
        
        //Init sub classes
        Init();
	}  
    
    // Update is called once per frame
	protected virtual void Update () 
    {
		if(pauseMovementOnTeleportTimer > 0)
        {
            pauseMovementOnTeleportTimer -= Time.deltaTime;
        }
	}
    
    protected virtual void Init()
    {
       
    }
    
    protected virtual void OnEntityDeath(GameObject source)
    {
       
    }
    
    public virtual void MoveEntityTo(Transform teleportTarget)
    {
        //Trigger movement delay
        pauseMovementOnTeleportTimer = pauseMovementOnTeleportDelay;
        
        //Set position and rotation
        transform.position = teleportTarget.position;
        transform.rotation = teleportTarget.rotation;   
    }
    
    public bool isMovementPaused()
    {
        return pauseMovementOnTeleportTimer > 0 || isGamePaused();
    }
    
    public bool isGamePaused()
    {
        return false; //TODO add pause screen check
    }
    
    public Team getTeam()
    {
        return team;
    }
}
