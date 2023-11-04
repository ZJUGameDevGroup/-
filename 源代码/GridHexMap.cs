using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHexMap : MonoBehaviour
{
    [SerializeField] private Transform pfHex;

    private GridHexXZ<GridObject> gridHexXZ;
    public int width;
    public int height;

    private class GridObject
    {
        public Transform visualTransform;

        public void Show()
        {
            visualTransform.Find("Selected").gameObject.SetActive(true);
        }

        public void Hide()
        {
            visualTransform.Find("Selected").gameObject.SetActive(false);
        }

    }

    private void Awake()
    {
        
        float cellSize = 1f;
        gridHexXZ =
            new GridHexXZ<GridObject>(width, height, cellSize, Vector3.zero, (GridHexXZ<GridObject> g, int x, int y) => new GridObject());

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Transform visualTransform = Instantiate(pfHex, gridHexXZ.GetWorldPosition(x, z), Quaternion.identity);
                gridHexXZ.GetGridObject(x, z).visualTransform = visualTransform;
                gridHexXZ.GetGridObject(x, z).Hide();
            }
        }
    }
}
