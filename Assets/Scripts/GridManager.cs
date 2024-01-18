using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public delegate WorldTile GetTile(Vector3Int mouseOnGrid);
public delegate float CalculateScore(List<WorldTile> adjacentTiles, WorldTile tile);
public delegate void AddCard(int amount);
public delegate void ShowHelp(WorldTile tile);

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tilemap tempTilemap;
    [SerializeField] private Tilemap worldTilemap;
    [SerializeField] private Vector3Int highlightedTilePos;
    [SerializeField] private TileBase highlightTile;
    public static GetTile OnGetTile;
    public static CalculateScore OnCalculateScore;
    public static ShowHelp OnShowHelp;
    public static AddCard OnAddCard;
    [Header("TileManaging")]
    private int minAdjacentTiles;
    [Header("TileSounds")]
    public AudioSource tilePlaced;

    void Start() {
        minAdjacentTiles = -1;
    }


    private Vector3Int GetMousePosOnGrid() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int mouseCellPos = worldTilemap.WorldToCell(mousePos);
        mouseCellPos.z = 0;

        return mouseCellPos;
    }
    
    private void HighlightTile() {
        Vector3Int mouseGridPos = GetMousePosOnGrid();

        if (highlightedTilePos != mouseGridPos) {
            tempTilemap.SetTile(highlightedTilePos, null);
            tempTilemap.SetTile(mouseGridPos, highlightTile);
            highlightedTilePos = mouseGridPos;
        }

    }

    private void SetTile() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Vector3Int mouseGridPos = GetMousePosOnGrid();
            List<WorldTile> adjacentTiles = AdjacentToTiles(mouseGridPos);
            if (adjacentTiles.Count > minAdjacentTiles && OnGetTile != null) {
                minAdjacentTiles = 0;
                WorldTile tile = OnGetTile(mouseGridPos);
                worldTilemap.SetTile(mouseGridPos, tile);
                tilePlaced.Play(0);
                if (OnCalculateScore != null) {
                    float score = OnCalculateScore(adjacentTiles, tile);
                    if (score >= 1f && OnAddCard != null) {
                        OnAddCard(1);
                    } else if (OnShowHelp != null && adjacentTiles.Count > minAdjacentTiles) {
                        OnShowHelp(tile);
                    }
                }
            }

        }
    }

    private List<WorldTile> AdjacentToTiles(Vector3Int mouseGridPos) {
        List<WorldTile> adjacentTiles = new List<WorldTile>();
        for (int x = -1; x <= 1; x += 2) {
            WorldTile adjacentTile = worldTilemap.GetTile(new Vector3Int(mouseGridPos.x + x, mouseGridPos.y, mouseGridPos.z)) as WorldTile;
            if (adjacentTile != null)
                adjacentTiles.Add(adjacentTile);
        }
        for (int y = -1; y <= 1; y += 2) {
            WorldTile adjacentTile = worldTilemap.GetTile(new Vector3Int(mouseGridPos.x, mouseGridPos.y + y, mouseGridPos.z)) as WorldTile;
            if (adjacentTile != null)
                adjacentTiles.Add(adjacentTile);
        }
        return adjacentTiles;
    }

    // Update is called once per frame
    void Update()
    {
        SetTile();
        HighlightTile(); 
    }
}
