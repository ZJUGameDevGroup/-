using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public GameObject map;
    MapGenerator mapGenerator;
    public Vector2Int position;
    public GameObject target;
    public Vector2Int nextStep;
    // Start is called before the first frame update
    void Start()
    {
        mapGenerator = map.GetComponent<MapGenerator>();
        moveTo(position); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveTo(Vector2Int targetPos)
    {
        mapGenerator.HexState[position.x, position.y] = 0;
        Vector3 target = GetWorldPosition(targetPos);
        transform.position = new Vector3(target.x,transform.position.y,target.z);
        mapGenerator.HexState[targetPos.x,targetPos.y] = 1; 
    }

    public Vector3 GetWorldPosition(Vector2Int target)
    {
        Vector3 oringinalPos;
        float edge = 1;
        float centerDistance;
        centerDistance = edge * Mathf.Sqrt(3) / 2;
        Vector3 moveLeftUp = new Vector3(-centerDistance, 0, edge * 3 / 2);
        Vector3 moveRightUp = new Vector3(centerDistance * 2, 0, 0);
        oringinalPos = Vector3.zero + (moveLeftUp + moveRightUp) * -3;
        return
        oringinalPos + moveLeftUp * target.y + moveRightUp * target.x; 
    }
}
