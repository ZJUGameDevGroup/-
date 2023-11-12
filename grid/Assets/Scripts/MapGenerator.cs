using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject hexPrefab;
    public float edge = 1;
    public float centerDistance;

    Vector3 moveLeftUp;
    Vector3 moveRightUp;

    int col = 7;
    int row = 7;

    public GameObject[,] Hexs;
    public int[,] HexState;
    Vector3 oringinalPos;
    float offset = Mathf.Sqrt(3);
    // Start is called before the first frame update
    void Awake()
    {
        centerDistance = edge * Mathf.Sqrt(3) / 2;
        moveLeftUp = new Vector3(-centerDistance, 0, edge * 3 / 2);
        moveRightUp = new Vector3(centerDistance * 2, 0, 0);
        Hexs = new GameObject[col, row];
        HexState = new int[col, row];
        oringinalPos = Vector3.zero + (moveLeftUp + moveRightUp) * -3;
        for(int i = 0;i < col; i++)
        {
            for(int j = 0;j < row; j++)
            {
                GameObject newGrid = Instantiate(hexPrefab, oringinalPos + moveLeftUp * j + moveRightUp * i, Quaternion.identity, transform);
                Hexs[i,j] = newGrid;
                HexState[i, j] = 0;
                MyGrid grid = newGrid.GetComponent<MyGrid>();
                grid.GridState = 0;
            }
        }
        for(int i = 0;i < col; i++)
        {
            for(int j = 0;j < row; j++)
            {
                if((i >= 4 || j >= 4) && Mathf.Abs(i - j) >= 4)
                {
                    HexState[i, j] = -1;
                    Hexs[i, j].SetActive(false);
                    
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
