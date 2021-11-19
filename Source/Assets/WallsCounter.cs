using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsCounter : MonoBehaviour
{

    private static WallsCounter instance;
    public static WallsCounter Instance 
    { 
        get 
        {
            if (!instance)
                instance = FindObjectOfType<WallsCounter>();

            return instance;
                    
                 
        } 
    }

    [SerializeField] List<Wall> _Walls = new List<Wall>();
    public int Count => _Walls.Count;
    public List<Wall> Walls => _Walls;

    public void AddWall(Wall wall)
    {
        _Walls.Add(wall);
    }


}
