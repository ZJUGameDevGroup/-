using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public GameObject map;
    MapGenerator mapGenerator;
    public Vector2Int position;
    public GameObject target;
    public Vector2Int nextStep;
    class character
    {
        public int selfPosX;
        public int selfPosY;
        public int targetPosX;
        public int targetPosY;
    }
    class mapInfo
    {
        public int[,] obstacleType;
    }
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

    List<Vector2Int> FindNextMove(List<character> Characters, mapInfo MapInfo)
    {
        int i, j;
        List<Vector2Int> ans = new List<Vector2Int>();
        List<int[]> nextMoveCost = new List<int[]>();//角色走完下一步后剩余路径的列表，0为原地，1为右，23456顺时针绕一圈
        List<int[]> nextMoveCostPoint = new List<int[]>();
        int[] tempMoveCost = new int[7];
        int[] tempRank = new int[7];
        int[] tempPoint = new int[7];
        int[,] tempCostMap = new int[9, 9];
        Vector2Int[] pointToDeltaVector = new Vector2Int[7] { 
            new Vector2Int(0, 0), 
            new Vector2Int(1, 0), 
            new Vector2Int(0, -1), 
            new Vector2Int(-1, -1), 
            new Vector2Int(-1, 0), 
            new Vector2Int(0, 1), 
            new Vector2Int(1, 1) 
        };
        foreach (var item in Characters) //为每个角色的目标点遍历地图，记录周围一圈的下一步后剩余路径并排序
        {
            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    tempCostMap[i, j] = 9999;
                }
            }
            explore(item.targetPosX, item.targetPosY, 0);
            tempMoveCost[0] = tempCostMap[item.selfPosX, item.selfPosY];
            tempMoveCost[1] = tempCostMap[item.selfPosX+1, item.selfPosY];
            tempMoveCost[2] = tempCostMap[item.selfPosX, item.selfPosY-1];
            tempMoveCost[3] = tempCostMap[item.selfPosX-1, item.selfPosY-1];
            tempMoveCost[4] = tempCostMap[item.selfPosX-1, item.selfPosY];
            tempMoveCost[5] = tempCostMap[item.selfPosX, item.selfPosY+1];
            tempMoveCost[6] = tempCostMap[item.selfPosX+1, item.selfPosY+1];
            nextMoveCost.Add(tempMoveCost);
            //排序,获得各自的排名
            for (i = 0; i <= 6; i++)
            {
                tempRank[i] = 1;
            }
            for (i = 0; i <= 6; i++)
            {
                for (j = i + 1; j <= 6; j++)
                {
                    if (tempMoveCost[i] > tempMoveCost[j])
                        tempRank[i]++;
                    else
                        tempRank[j]++;
                }
            }
            //排名转换为指针，point[0]存着最小消耗的下标。
            for (i = 0; i <= 6; i++)
            {
                tempPoint[tempRank[i]-1] = i;
            }
            nextMoveCostPoint.Add(tempPoint);
        }
        for (i = 0; i < nextMoveCost.Count; i++)
        {
            j = 0;
            while (ans.Contains(new Vector2Int(Characters[i].selfPosX, Characters[i].selfPosY) + pointToDeltaVector[nextMoveCostPoint[i][j]]) && j < 6)
            {
                j++;
            }
            ans.Add(new Vector2Int(Characters[i].selfPosX, Characters[i].selfPosY) + pointToDeltaVector[nextMoveCostPoint[i][j]]);
        }
        return ans;
        void explore(int x, int y, int cost)
        {
            if (MapInfo.obstacleType[x,y] != 1)
                {
                if (cost < tempCostMap[x, y])
                {
                    tempCostMap[x, y] = cost;
                    explore(x + 1, y, cost + 1);
                    explore(x - 1, y, cost + 1);
                    explore(x, y + 1, cost + 1);
                    explore(x, y - 1, cost + 1);
                    explore(x + 1, y + 1, cost + 1);
                    explore(x - 1, y - 1, cost + 1);
                }
            }
        }
    }
}
