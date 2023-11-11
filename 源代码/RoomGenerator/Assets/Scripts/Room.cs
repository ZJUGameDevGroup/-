using UnityEngine;

public class Room : MonoBehaviour
{
    void Start()
    {
        Hexagon cell = GetComponent<Hexagon>();
        cell.Initialize();
        if (cell.ring == 0)
        {
            cell.InstantiateRoom(cell.maxRing);
        }
    }
}
