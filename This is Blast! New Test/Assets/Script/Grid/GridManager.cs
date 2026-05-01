using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width;
    public int height;
    public float cellSize = 1f;

    public CubeActive[,] grid;

    public List<CubeActive> GetNeighbors(CubeActive cube)
    {
        List<CubeActive> neighbors = new List<CubeActive>();

        Vector2Int[] directions = new Vector2Int[]
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        foreach (var dir in directions)
        {
            Vector2Int newPos = cube.gridPosition + dir;

            if (IsInsideGrid(newPos))
            {
                CubeActive neighbor = grid[newPos.x, newPos.y];
                if (neighbor != null)
                    neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    bool IsInsideGrid(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width &&
               pos.y >= 0 && pos.y < height;
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        Vector3 offset = new Vector3(width, 0, height) * 0.5f;
        return new Vector3(gridPos.x * cellSize, 0, gridPos.y * cellSize) - offset;
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        Vector3 offset = new Vector3(width, 0, height) * 0.5f;

        int x = Mathf.RoundToInt((worldPos.x + offset.x) / cellSize);
        int y = Mathf.RoundToInt((worldPos.z + offset.z) / cellSize);

        return new Vector2Int(x, y);
    }

    void OnDrawGizmos()
    {
        if (width <= 0 || height <= 0) return;

        float size = cellSize;
        Vector3 offset = new Vector3(width, 0, height) * 0.5f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x * size, 0, y * size) - offset;

                // Warna beda kalau ada cube
                if (grid != null && x < width && y < height && grid[x, y] != null)
                    Gizmos.color = Color.green;
                else
                    Gizmos.color = Color.red;

                Gizmos.DrawWireCube(pos, new Vector3(size, 0.1f, size));
            }
        }
    }
}
