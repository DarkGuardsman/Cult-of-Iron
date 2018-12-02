using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour {

	public float angleLimit = 20f;
    public float stopDistane = 5f;
    public float moveSpeed = 1f;
    
    public EntityMob host;
    public Rigidbody2D rigidbody2D;
    
    void Start ()
    {
        rigidbody2D = host.GetComponent<Rigidbody2D>();
    }
    
	// Update is called once per frame
	void Update () 
    {
		if(host.currentTarget != null)
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
            
            //Fire if we are inside angle limits
            if(delta <= angleLimit && distanceSq > (stopDistane * stopDistane))
            {
                float moveForce = rigidbody2D.mass * moveSpeed;
                rigidbody2D.AddForce(transform.up * moveForce * Time.deltaTime);
            }
        }		
	}
}
