using System;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
public class Hexagon : MonoBehaviour
{
    public GameObject prefab;
    [Header("多边形信息")]
    public float centerDistance; //相连两个六边形的中心距离:1
    public float edge; // 边长:0.8660254
    [Header("坐标信息")]
    public int leftUp = 0;
    public int rightUp = 0;
    [Header("层信息")]
    public int maxRing = 0;
    [ReadOnly]
    public int ring;
    [ReadOnly]
    public AreaType area;
    [ReadOnly]
    public Vector3 moveLeftUp, moveRightUp, moveUp, moveDown, moveLeftDown, moveRightDown;
    public enum AreaType
    {
        CENTER, AXIS_UP, RIGHT_UP, AXIS_RIGHT_UP, RIGHT, AXIS_RIGHT_DOWN, RIGHT_DOWN,
        AXIS_DOWN, LEFT_DOWN, AXIS_LEFT_DOWN, LEFT, AXIS_LEFT_UP, LEFT_UP, ERROR = 1000
    }
    void Start()
    {
        Initialize();
    }
    public void Renew()
    {
        ring = Ring();
        area = Area();
    }
    public void Initialize()
    {
        moveLeftUp = new Vector3(-edge, 0, centerDistance / 2);
        moveRightUp = new Vector3(edge, 0, centerDistance / 2);

        moveLeftDown = -moveRightUp;
        moveRightDown = -moveLeftUp;

        moveUp = moveLeftUp + moveRightUp;
        moveDown = moveLeftDown + moveRightDown;
    }
    public void MoveUp(int step = 1)
    {
        leftUp += step;
        rightUp += step;
        Renew();
    }
    public void MoveDown(int step = 1)
    {
        leftUp -= step;
        rightUp -= step;
        Renew();
    }
    public void MoveLeftUp(int step = 1)
    {
        leftUp += step;
        Renew();
    }
    public void MoveRightUp(int step = 1)
    {
        rightUp += step;
        Renew();
    }
    public void MoveLeftDown(int step = 1)
    {
        rightUp -= step;
        Renew();
    }
    public void MoveRightDown(int step = 1)
    {
        leftUp -= step;
        Renew();
    }
    public int Ring()
    {
        if (leftUp == 0 && rightUp == 0) return 0;
        if (leftUp * rightUp < 0) return Math.Abs(leftUp) + Math.Abs(rightUp);
        else return Math.Max(Math.Abs(leftUp), Math.Abs(rightUp));
    }
    public AreaType Area()
    {
        if (leftUp == 0 && rightUp == 0) return AreaType.CENTER; //中心点
        if (leftUp == rightUp && leftUp > 0) return AreaType.AXIS_UP;//上轴
        if (leftUp < rightUp && leftUp > 0) return AreaType.RIGHT_UP;//右上区域
        if (leftUp == 0 && rightUp > 0) return AreaType.AXIS_RIGHT_UP;//右上轴
        if (leftUp < 0 && rightUp > 0) return AreaType.RIGHT;//右边区域
        if (leftUp < 0 && rightUp == 0) return AreaType.AXIS_RIGHT_DOWN;//右下轴
        if (leftUp < rightUp && rightUp < 0) return AreaType.RIGHT_DOWN;//右下区域
        if (leftUp == rightUp && rightUp < 0) return AreaType.AXIS_DOWN;//下轴
        if (rightUp < leftUp && leftUp < 0) return AreaType.LEFT_DOWN;//左下区域
        if (rightUp < 0 && leftUp == 0) return AreaType.AXIS_LEFT_DOWN;//左下轴
        if (rightUp < 0 && leftUp > 0) return AreaType.LEFT; //左边区域
        if (rightUp == 0 && leftUp > 0) return AreaType.AXIS_LEFT_UP;//左上轴
        if (rightUp < leftUp && rightUp > 0) return AreaType.LEFT_UP; //左上区域
        else return AreaType.ERROR; //错误代码
    }
    //12个区域生成
    public void InstantiateAxisUp(int step = 1)
    {
        Instantiate(prefab, transform.position + step * moveUp, transform.rotation).GetComponent<Hexagon>().MoveUp(step);

    }
    public void InstantiateRightUp(int step = 1)
    {
        Vector3 pos;
        for (int i = 1; i < step; i++)
        {
            pos = transform.position + step * moveRightUp + i * moveLeftUp;
            Hexagon newHexagon = Instantiate(prefab, pos, transform.rotation).GetComponent<Hexagon>();
            newHexagon.MoveRightUp(step);
            newHexagon.MoveLeftUp(i);
        }
    }
    public void InstantiateAxisRightUp(int step = 1)
    {
        Instantiate(prefab, transform.position + step * moveRightUp, transform.rotation).GetComponent<Hexagon>().MoveRightUp(step);
    }
    public void InstantiateRight(int step = 1)
    {
        Vector3 pos;
        for (int i = 1; i < step; i++)
        {
            pos = transform.position + step * moveRightUp + i * moveDown;
            Hexagon newHexagon = Instantiate(prefab, pos, transform.rotation).GetComponent<Hexagon>();
            newHexagon.MoveRightUp(step);
            newHexagon.MoveDown(i);
        }
    }
    public void InstantiateAxisRightDown(int step = 1)
    {
        Instantiate(prefab, transform.position + step * moveRightDown, transform.rotation).GetComponent<Hexagon>().MoveRightDown(step);
    }
    public void InstantiateRightDown(int step = 1)
    {
        Vector3 pos;
        for (int i = 1; i < step; i++)
        {
            pos = transform.position + step * moveRightDown + i * moveLeftDown;
            Hexagon newHexagon = Instantiate(prefab, pos, transform.rotation).GetComponent<Hexagon>();
            newHexagon.MoveRightDown(step);
            newHexagon.MoveLeftDown(i);
        }
    }
    public void InstantiateAxisDown(int step = 1)
    {
        Instantiate(prefab, transform.position + step * moveDown, transform.rotation).GetComponent<Hexagon>().MoveDown(step);
    }
    public void InstantiateLeftDown(int step = 1)
    {
        Vector3 pos;
        for (int i = 1; i < step; i++)
        {
            pos = transform.position + step * moveLeftDown + i * moveRightDown;
            Hexagon newHexagon = Instantiate(prefab, pos, transform.rotation).GetComponent<Hexagon>();
            newHexagon.MoveLeftDown(step);
            newHexagon.MoveRightDown(i);
        }
    }
    public void InstantiateAxisLeftDown(int step = 1)
    {
        Instantiate(prefab, transform.position + step * moveLeftDown, transform.rotation).GetComponent<Hexagon>().MoveLeftDown(step);
    }
    public void InstantiateLeft(int step = 1)
    {
        Vector3 pos;
        for (int i = 1; i < step; i++)
        {
            pos = transform.position + step * moveLeftUp + i * moveDown;
            Hexagon newHexagon = Instantiate(prefab, pos, transform.rotation).GetComponent<Hexagon>();
            newHexagon.MoveLeftUp(step);
            newHexagon.MoveDown(i);
        }
    }
    public void InstantiateAxisLeftUp(int step = 1)
    {
        Instantiate(prefab, transform.position + step * moveLeftUp, transform.rotation).GetComponent<Hexagon>().MoveLeftUp(step);
    }
    public void InstantiateLeftUp(int step = 1)
    {
        Vector3 pos;
        for (int i = 1; i < step; i++)
        {
            pos = transform.position + step * moveLeftUp + i * moveRightUp;
            Hexagon newHexagon = Instantiate(prefab, pos, transform.rotation).GetComponent<Hexagon>();
            newHexagon.MoveLeftUp(step);
            newHexagon.MoveRightUp(i);
        }
    }
    //向顶点生成
    public void InstantiateLU(int step = 1)
    {
        Vector3 pos = transform.position + step * moveLeftUp + step * moveUp;
        Hexagon newHexagon = Instantiate(prefab, pos, transform.rotation).GetComponent<Hexagon>();
        newHexagon.MoveLeftUp(step);
        newHexagon.MoveUp(step);
    }
    public void InstantiateRU(int step = 1)
    {
        Vector3 pos = transform.position + step * moveRightUp + step * moveUp;
        Hexagon newHexagon = Instantiate(prefab, pos, transform.rotation).GetComponent<Hexagon>();
        newHexagon.MoveRightUp(step);
        newHexagon.MoveUp(step);
    }
    public void InstantiateL(int step = 1)
    {
        Vector3 pos = transform.position + step * moveLeftUp + step * moveLeftDown;
        Hexagon newHexagon = Instantiate(prefab, pos, transform.rotation).GetComponent<Hexagon>();
        newHexagon.MoveLeftUp(step);
        newHexagon.MoveLeftDown(step);
    }
    public void InstantiateR(int step = 1)
    {
        Vector3 pos = transform.position + step * moveRightUp + step * moveRightDown;
        Hexagon newHexagon = Instantiate(prefab, pos, transform.rotation).GetComponent<Hexagon>();
        newHexagon.MoveRightUp(step);
        newHexagon.MoveRightDown(step);
    }
    public void InstantiateLD(int step = 1)
    {
        Vector3 pos = transform.position + step * moveLeftDown + step * moveDown;
        Hexagon newHexagon = Instantiate(prefab, pos, transform.rotation).GetComponent<Hexagon>();
        newHexagon.MoveLeftDown(step);
        newHexagon.MoveDown(step);
    }
    public void InstantiateRD(int step = 1)
    {
        Vector3 pos = transform.position + step * moveRightDown + step * moveDown;
        Hexagon newHexagon = Instantiate(prefab, pos, transform.rotation).GetComponent<Hexagon>();
        newHexagon.MoveRightDown(step);
        newHexagon.MoveDown(step);
    }
    public void InstantiateRing(int step = 1)
    {
        if (step <= 0)
        {
            return;
        }
        InstantiateAxisUp(step);
        InstantiateRightUp(step);
        InstantiateAxisRightUp(step);
        InstantiateRight(step);
        InstantiateAxisRightDown(step);
        InstantiateRightDown(step);
        InstantiateAxisDown(step);
        InstantiateLeftDown(step);
        InstantiateAxisLeftDown(step);
        InstantiateLeft(step);
        InstantiateAxisLeftUp(step);
        InstantiateLeftUp(step);
    }
    public void InstantiateArea(AreaType area, int step = 1)
    {
        switch (area)
        {
            case AreaType.AXIS_UP:
                InstantiateAxisUp(step);
                break;
            case AreaType.RIGHT_UP:
                InstantiateRightUp(step);
                break;
            case AreaType.AXIS_RIGHT_UP:
                InstantiateAxisRightUp(step);
                break;
            case AreaType.RIGHT:
                InstantiateRight(step);
                break;
            case AreaType.AXIS_RIGHT_DOWN:
                InstantiateAxisRightDown(step);
                break;
            case AreaType.RIGHT_DOWN:
                InstantiateRightDown(step);
                break;
            case AreaType.AXIS_DOWN:
                InstantiateAxisDown(step);
                break;
            case AreaType.LEFT_DOWN:
                InstantiateLeftDown(step);
                break;
            case AreaType.AXIS_LEFT_DOWN:
                InstantiateAxisLeftDown(step);
                break;
            case AreaType.LEFT:
                InstantiateLeft(step);
                break;
            case AreaType.AXIS_LEFT_UP:
                InstantiateAxisLeftUp(step);
                break;
            case AreaType.LEFT_UP:
                InstantiateLeftUp(step);
                break;
            default: break;
        }
    }
    public void InstantiateCircle(int maxring = 1)
    {
        for (int i = 1; i <= maxring; i++)
        {
            InstantiateRing(i);
        }
    }
    public void InstantiateSector(AreaType beginType, AreaType endType, int maxring = 1)
    {
        if (beginType > endType || beginType == AreaType.CENTER || endType == AreaType.ERROR)
        {
            return;
        }
        for (AreaType type = beginType; type <= endType; type++)
        {
            for (int i = 1; i <= maxring; i++)
            {
                InstantiateArea(type, i);
            }
        }
    }
    public void InstantiateRoom(int maxring = 1)
    {
        if (maxring <= 0)
        {
            return;
        }
        if (maxring % 2 == 1)
        {
            maxring++;
        }
        int half = maxring / 2;
        //生成前面一半房间
        for (AreaType type = AreaType.AXIS_RIGHT_UP; type <= AreaType.AXIS_RIGHT_DOWN; type++)
        {
            for (int i = 1; i <= half; i++)
            {
                InstantiateArea(type, i);
            }
        }
        //生成boss房
        Vector3 pos = transform.position + half * moveRightUp + half * moveRightDown;
        Hexagon newHexagon = Instantiate(prefab, pos, transform.rotation).GetComponent<Hexagon>();
        newHexagon.MoveRightUp(half);
        newHexagon.MoveRightDown(half);
        int reserved = maxring - half - 1;
        //生成前面一半房间
        for (AreaType type = AreaType.AXIS_LEFT_DOWN; type <= AreaType.AXIS_LEFT_UP; type++)
        {
            for (int i = 1; i <= reserved; i++)
            {
                newHexagon.InstantiateArea(type, i);
            }
        }
    }
}
