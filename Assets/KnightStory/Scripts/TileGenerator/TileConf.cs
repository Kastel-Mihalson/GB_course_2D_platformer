using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class TileConf
{
    [SerializeField]
    private TileType _tileType;

    [SerializeField]
    private Tile _tile;

    public TileType TileType => _tileType;
    public Tile Tile => _tile;
}
