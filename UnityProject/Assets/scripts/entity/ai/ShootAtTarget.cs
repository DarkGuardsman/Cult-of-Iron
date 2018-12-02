using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtTarget : MonoBehaviour 
{
    public LayerMask rayTraceLayerMask;
    
    public Color gizmosColor = new Color(1, 0, 0, 0.5f);
    public bool enableRangeDebug = true;
 
    public float angleLimit = 1f;
    public float firingDistance = 5f;
    
    public EntityMob host;    
    public Weapon[] weapons;
    public float fireDelay = 0.1f;
    
    private int weaponIndex = 0;
    private float fireTimer = 0;    
    
	// Update is called once per frame
	void Update () 
    {
        if(fireTimer <= 0)
        {
            //Fire if we are inside angle limits
            if(CanHitTarget())
            {     
                //Fire next weapon
                if(weapons[weaponIndex].fire(host.currentTarget))
                {
                    //Reset timer
                    fireTimer = fireDelay;
                    
                    //Index next weapon
                    if(++weaponIndex >= weapons.Length)
                    {
                        weaponIndex = 0;
                    } 
                }                
            }
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }    
	}
    
    public bool CanHitTarget()
    {
        return host.currentTarget != null && IsTargetInsideFiringArk() && ShouldFireAtTarget();
    }
    
    public bool IsTargetInsideFiringArk()
    {        
        Vector2 direction = (Vector2)host.currentTarget.gameObject.transform.position - (Vector2)transform.position;
            
        float distanceSq = direction.y * direction.y + direction.x * direction.x;
                        
        //Convert vector to angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        if(angle < 0)
        {
            angle += 360;
        }
            
        //Get difference in angle
        float delta = Mathf.Abs(angle - gameObject.transform.rotation.eulerAngles.z);
            
        return delta <= angleLimit && distanceSq <= (firingDistance * firingDistance);
    }
    
    public bool ShouldFireAtTarget()
    {       
        //Get direction
        Vector2 direction = (Vector2)host.currentTarget.gameObject.transform.position - (Vector2)transform.position;
        direction.Normalize();
         
        //Cast ray looking for a hit //TODO run every few cycles to reduce CPU load
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, firingDistance * 1.5f, rayTraceLayerMask);
        
        Debug.DrawRay(transform.position, direction * firingDistance * 1.5f, Color.white);
        
        //If we have a hit, check if is the target
        if (hit != null && hit.collider != null)
        {
            Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow);
            bool isTarget = GameObject.ReferenceEquals(hit.collider.gameObject, host.currentTarget.gameObject);
            //TODO may let the AI shoot if hit is still an enemy
            return isTarget;
        }        
        return false;
    }
    
    void OnDrawGizmosSelected() 
    {
        if(enableRangeDebug)
        {
            Gizmos.color = gizmosColor;
            Gizmos.DrawCube(transform.position, new Vector3(firingDistance, firingDistance, 0));
        }
    }
}
