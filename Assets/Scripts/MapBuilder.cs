using System;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private Map _map; //в поле _map сохраняем ссылку на карту
    [SerializeField] private MapIndexProvider _mapIndexProvider; // в поле _mapIndexProvider сохраняем индекс точки (относительно карты) где находится мышка

    private Camera _camera; //поле для хранения ссылки на главную камеру
    private Tile _currentTile; //сохраняем текущий тайл в переменную

    private void Awake()
    {
        _camera = Camera.main; //присваиваем в поле главную камеру
    }

    /// <summary>
    /// Данный метод вызывается автоматически при клике на кнопки с изображениями тайлов.
    /// В качестве параметра передается префаб тайла, изображенный на кнопке.
    /// Вы можете использовать префаб tilePrefab внутри данного метода.
    /// </summary>
    public void StartPlacingTile(GameObject tilePrefab) // метод ,с помощью которого начинаем установку тайла
    {
        var tileObject = Instantiate(tilePrefab); //инициируем объект из префаба и сохраняем его в переменную
        _currentTile = tileObject.GetComponent<Tile>(); // в поле _currentTile сохраняем инициированный объект и берем у этого объекта компоненту Tile
        _currentTile.transform.SetParent(_map.transform); // Текущему тайлу присваиваем родителя с координатами _map.transform
    }

    private void Update()
    {
        var mousePosition = Input.mousePosition; // сохраняем в переменную позицию мыщи
        Ray ray = _camera.ScreenPointToRay(mousePosition); // сохраняем в переменную луч который стреляет из камеры в позицию мыши

        if (Physics.Raycast(ray, out var hitInfo) && _currentTile != null) // если текущий тайл не равен null и луч проходит 
        {
            // Получаем индекс и позицию тайла по позиции курсора 
            var tileIndex = _mapIndexProvider.GetIndex(hitInfo.point);
            var tilePosition = _mapIndexProvider.GetTilePosition(tileIndex);
            _currentTile.transform.localPosition = tilePosition;

            // Проверяем, доступно ли место для постройки тайла
            var isAvailable = _map.IsCellAvailable(tileIndex);
            // Задаем тайлу соответствующий цвет
            _currentTile.SetColor(isAvailable);
            
            // Если место недоступно для постройки - выходим из метода
            if (!isAvailable)
            {
                return;
            }
            
            // Если нажата ЛКМ - устанавливаем тайл 
            if (Input.GetMouseButtonDown(0))
            {
                _map.SetTile(tileIndex, _currentTile);
                _currentTile.ResetColor(); // сбрасываем подсветку у установленного тайла
                _currentTile = null; //в текущий тайл устанавливаем null
            }
        }
    }
}