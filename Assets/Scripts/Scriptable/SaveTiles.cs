using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SaveTiles : ScriptableObject
{
    public TileBase tile;
    public Vector3Int tileLocation;

    public void Init(TileBase tile, Vector3Int tileLocation)
    {
        this.tile = tile;
        this.tileLocation = tileLocation;
    }

    public static SaveTiles CreateInstance(TileBase tile, Vector3Int tileLocation)
    {
        SaveTiles data = ScriptableObject.CreateInstance<SaveTiles>();
        data.Init(tile, tileLocation);
        return data;
    }
}
