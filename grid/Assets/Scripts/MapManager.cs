using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    MapGenerator mapGenerator;
    public GameObject[] members;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        members = new GameObject[3];
        enemies = new GameObject[3];
        mapGenerator = GetComponent<MapGenerator>();
        /*pathfinding function*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

