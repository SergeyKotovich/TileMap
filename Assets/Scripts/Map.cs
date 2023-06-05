using UnityEngine;

public class Map : MonoBehaviour
{    
    public Vector2Int Size => _size;

    [SerializeField] 
    private Vector2Int _size;
    
    private Tile[,] _tiles;
    
    private void Awake()
    {
        _tiles = new Tile[Size.x, Size.y];
    }

    public Tile GetTile(Vector2Int index)
    {
        return _tiles[index.x, index.y];
    }

    public void SetTile(Vector2Int index, Tile tile)
    {
        _tiles[index.x, index.y] = tile;
    }
}