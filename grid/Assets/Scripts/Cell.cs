using UnityEngine;

public class Cell : MonoBehaviour
{
    void Start()
    {
        Hexagon cell = GetComponent<Hexagon>();
        cell.Initialize();
        if (cell.ring == 0)
        {
            cell.InstantiateCircle(cell.maxRing);
        }
    }
}
