using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

//Handles game logic
public class GameController : SceneObject
{
    public float sizeOfWorld = 250f;
    
    //Prefab for direction arrows
    public GameObject arrowPrefabDropLocation;
    public GameObject arrowPrefabJunk;
    
    public GameObject arrowParent;
    
    //Camera brain
    public CinemachineVirtualCamera cinemachineCamera;
    public GameObject cameraObject;
    
    public Transform centerOfWorld;
    
    //List of all spawned junk objects
    public List<GameObject> junkSpawnedList = new List<GameObject>();
    
    //List of all objects to drop stuff off
    public List<GameObject> dropOffLocations = new List<GameObject>();
    
    Dictionary<GameObject, GameObject> objectToArrow = new Dictionary<GameObject, GameObject>();
    
    //Current player score
    public int points;
    
    //Current number of lives left for player
    public int lives;   
    
    public bool gameOver = false;
    
    public float gameTimeScale = 1;
    
    void Start ()
    {
        foreach (GameObject dropLocation in dropOffLocations)
        {
            GenerateArrow(dropLocation, arrowPrefabDropLocation);
        }
    }
    
    void Update ()
    {   
        
       
    }
    
    public GameObject SpawnJunk(GameObject prefab, float x, float y)
    {
        return SpawnJunk(prefab, ToGamePosition(x, y));
    }
    
    public GameObject SpawnJunk(GameObject prefab, Vector3 position)
    {
        //Create
        GameObject junkObject = (GameObject)Instantiate(prefab);
        
        //Set position
        junkObject.transform.position = position;
        
        //Track
        junkSpawnedList.Add(junkObject);
        
        //Generator arrow
        GenerateArrow(junkObject, arrowPrefabJunk);    
        
        //Set max speed limit
        SpeedLimiter speedScript = gameObject.GetComponent<SpeedLimiter>();
        if(speedScript != null)
        {
            speedScript.SetSpeed(GetPlayerOptions().currentSettings.maxJunkSpeed);
        }
        
        return junkObject;
    }
    
    public Vector3 ToGamePosition(float x, float y)
    {
        return centerOfWorld.position + new Vector3(x, y, 0);
    }
    
    public bool IsInView(float x, float y)
    {
        return IsInView(ToGamePosition(x, y));
    }
    
    public bool IsInView(Vector3 position)
    {
        //Convert target position to camera space
        Vector3 pos = Camera.main.WorldToViewportPoint(position);
        
        //Check if visible
        return pos.x >= 0.0f && pos.x <= 1.0f && pos.y >= 0.0f && pos.y <= 1.0f;
    }
    
    public void GenerateArrow(GameObject gameObject, GameObject arrowPrefab)
    {
        //Create arrow
        GameObject arrowObject = (GameObject)Instantiate(arrowPrefab);
        arrowObject.transform.parent = arrowParent.transform;
        arrowObject.transform.localPosition = Vector3.zero;
        
        //Assign target
        ArrowObjective arrowObjective = arrowObject.GetComponent<ArrowObjective>();
        arrowObjective.aimTarget = gameObject.transform;
        
        //Add to dictionary
        objectToArrow.Add(gameObject, arrowObject);
    }
    
    public void DestroyJunk(GameObject gameObject)
    {
        if (objectToArrow.ContainsKey(gameObject))
        {
            objectToArrow.Remove(gameObject);
        }
        junkSpawnedList.Remove(gameObject);
        Destroy(gameObject);
    }
    
    public void AddPoints(int p)
    {
        points += p;
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene ().name);
    }
}
