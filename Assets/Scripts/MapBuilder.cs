using System;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private MapIndexProvider _mapIndexProvider;
    
    private Tile _currentTile;

    /// <summary>
    /// Данный метод вызывается автоматически при клике на кнопки с изображениями тайлов.
    /// В качестве параметра передается префаб тайла, изображенный на кнопке.
    /// Вы можете использовать префаб tilePrefab внутри данного метода.
    /// </summary>
    public void StartPlacingTile(GameObject tilePrefab)
    {
        var tileObject = Instantiate(tilePrefab);
        _currentTile = tileObject.GetComponent<Tile>();
        _currentTile.transform.SetParent(_map.transform);
    }

    private void Update()
    {
        var mousePosition = Input.mousePosition;
        var ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out var hitInfo) && _currentTile != null) 
        {
            var tileIndex = _mapIndexProvider.GetIndex(hitInfo.point);
            var tilePosition = _mapIndexProvider.GetTilePosition(tileIndex);
            _currentTile.transform.localPosition = tilePosition;

            if (Input.GetMouseButtonDown(0))
            {
                _map.SetTile(tileIndex, _currentTile);
                _currentTile = null;
            }
        }
        
    }
}