using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GameObject/Tiles")]
public class WorldTile : AnimatedTile
{
    public Direction[] connections;
    public Tile[] tiles;
    public WorldTile[] negative_affinity_tiles; 
    public string HelpMessage;
}

public enum Direction 
{
    Top,
    Right,
    Bottom,
    Left,
    None
}
