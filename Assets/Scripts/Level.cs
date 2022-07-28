using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Level : MonoBehaviour
{
    public static Level get;
    public Tilemap road;
    TileBase[] tiles;

    private void Awake()
    {
        get = this;
        tiles = road.GetTilesBlock(road.cellBounds);
    }

    public bool OnRoad(int x, int y)
    {
        BoundsInt bounds = road.cellBounds;
        TileBase t = tiles[x + y * bounds.size.x];
        //Tile tile = (Tile)
        return t != null;
    }


}
