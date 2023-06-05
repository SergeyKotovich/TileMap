using UnityEngine;

public class MapIndexProvider : MonoBehaviour
{
    [SerializeField] 
    private Map _map; // ссылка на карту

    public Vector2Int GetIndex(Vector3 worldPosition) // метод для получения индекса позиции
    {
        // Переводим мировую позицию в локальную позицию карты
        var tilePositionInMap = _map.transform.InverseTransformPoint(worldPosition);
        
        // Получаем целочисленные координаты тайла
        var x = Mathf.FloorToInt(tilePositionInMap.x);
        var y = Mathf.FloorToInt(tilePositionInMap.z);
        
        // Получаем индекс тайла в сетке
        var halfMapSize = _map.Size / 2; // смещение на половину карты
        var mapIndex = new Vector2Int(x, y)  + halfMapSize; 

        return mapIndex;
    }

    public Vector3 GetTilePosition(Vector2Int index)
    {        
        var halfMapSize = _map.Size / 2;
        var halfTileSize = Vector2.one / 2;

        var tilePositionXY = index - halfMapSize + halfTileSize;
        return new Vector3(tilePositionXY.x, 0, tilePositionXY.y);
    }
}