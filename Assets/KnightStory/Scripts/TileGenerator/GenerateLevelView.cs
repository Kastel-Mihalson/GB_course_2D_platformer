using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateLevelView : MonoBehaviour
{
    [SerializeField]
    private Tilemap _tileMap;

    [SerializeField]
    private TileConf[] _tiles;

    [SerializeField]
    private int _mapWidth;

    [SerializeField]
    private int _mapHeight;

    [SerializeField]
    private int _smoothFactor;

    [SerializeField][Range(0, 100)]
    private int _randomFillPercent;

    public Tilemap TileMap => _tileMap;
    
    public TileConf[] Tiles => _tiles;
    
    public int MapWidth => _mapWidth;
    
    public int MaxHeight => _mapHeight;
    
    public int SmoothFactor => _smoothFactor;

    public int RandomFillPrecent => _randomFillPercent;

}
