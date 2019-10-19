using System;
using UnityEngine;

[Serializable]
public class TilePosition
{
    public Vector3 TileSpawnPosition;
    public TilePrefab TypeTile;
}

public enum TilePrefab
{
    Default,
    Stairs,
    Portal
}