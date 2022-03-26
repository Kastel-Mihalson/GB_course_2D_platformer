using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateLevelController
{
    private Tilemap _tileMap;
    private TileConf[] _tiles;
    private int _mapWidth;
    private int _mapHeight;
    private int _smoothFactor;
    private int _randomFillPercent;

    private int[,] _map;

    private const int WALL_COUNT = 4;

    public GenerateLevelController(GenerateLevelView view)
    {
        _tileMap = view.TileMap;
        _tiles = view.Tiles;
        _mapWidth = view.MapWidth;
        _mapHeight = view.MaxHeight;
        _smoothFactor = view.SmoothFactor;
        _randomFillPercent = view.RandomFillPrecent;

        _map = new int[_mapWidth, _mapHeight];
    }

    public void GenerateLevel()
    {
        RandomFillLevel();

        for (int i = 0; i < _smoothFactor; i++) SmoothMap();

        DrawTileOnMap();
    }

    public void ClearMap()
    {
        if (_tileMap != null) _tileMap.ClearAllTiles();
    }

    private void DrawTileOnMap()
    {
        if (_map == null) return;

        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                var tilePosition = new Vector3Int(-_mapWidth / 2 + x, -_mapHeight / 2 + y, 0);

                if (_map[x, y] == 1)
                {
                    _tileMap.SetTile(tilePosition, _tiles[0].Tile);
                }
            }
        }
    }

    private void SmoothMap()
    {
        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                var count = GetWallNeighborCount(x, y);

                if (count > WALL_COUNT)
                    _map[x, y] = 1;
                else if (count < WALL_COUNT)
                    _map[x, y] = 0;
            }
        }
    }

    private int GetWallNeighborCount(int x, int y)
    {
        var wallCount = 0;

        for (int neighborX = x - 1; neighborX <= x + 1; neighborX++)
        {
            for (int neighborY = y - 1; neighborY <= y + 1; neighborY++)
            {
                if (neighborX >= 0 && neighborX < _mapWidth &&
                    neighborY >= 0 && neighborY < _mapHeight)
                {
                    if (neighborX != x || neighborY != y)
                        wallCount += _map[neighborX, neighborY];
                }
                else
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }

    private void RandomFillLevel()
    {
        var rand = new System.Random();

        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                if (x == 0 || x == _mapWidth - 1 || y == 0 || y == _mapHeight - 1)
                    _map[x, y] = 1; 
                else
                    _map[x, y] = (rand.Next(0, 100) < _randomFillPercent) ? 1 : 0;
            }
        }
    }
}
