using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapGenerator : MonoBehaviour 
{
    public Tilemap tilemap;
    public TileBase tile;
    public TileBase tileDirt;
    
    public int width = 100;
    public int height = 100;
    public int offsetX = -50;
    public int offsetY = -50;
    
    int[] mapData;
    
    void Start()
    {
        GenerateMap();
    }
    
	public void GenerateMap()
    {
        //Init data
        mapData = new int[width * height];
        
        //Generate
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                int index = getIndex(x, y);
                mapData[index] = index % 2;
            }
        }
        
        //Update display
        UpdateMapTiles();
    }
    
    public void UpdateMapTiles()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                int index = getIndex(x, y);
                int data = mapData[index];
                if(data == 1)
                {
                    tilemap.SetTile(new Vector3Int(x + offsetX, y + offsetY, 0), tile); 
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x + offsetX, y + offsetY, 0), tileDirt); 
                }
            }
        }
    }
    
    int getIndex(int x, int y)
    {
        return y * height + x;
    }
}
