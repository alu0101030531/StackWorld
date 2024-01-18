using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("Score manipulation")]
    public float scoreMultiplier = 2;
    public float negativeScoreMultiplier = 3;
    public float maxAdjacentCells = 4;
    [Header("Score UI")]
    private float currentScore = 0;
    public TMP_Text scoreUI;

    void Start()
    {
        GridManager.OnCalculateScore += CalculateScore;
    }

    void OnDestroy() {
        GridManager.OnCalculateScore -= CalculateScore;
    }

    float CalculateAffinity(List<WorldTile> adjacentTiles, WorldTile tile) {
        float negativeTiles = 0;
        foreach(WorldTile adjacentTile in adjacentTiles) {
            foreach(WorldTile negativeTile in adjacentTile.negative_affinity_tiles) {
                if (tile == negativeTile) {
                    negativeTiles++;
                }
            }
        }
        return negativeTiles;
    }

    float CalculateScore(List<WorldTile> adjacentTiles, WorldTile tile) {
        if (tile == null) {
            return 0f;
        }
        float numNegativeAffinityTiles = CalculateAffinity(adjacentTiles, tile);
        float score = (adjacentTiles.Count - numNegativeAffinityTiles) * scoreMultiplier - numNegativeAffinityTiles * negativeScoreMultiplier;
        currentScore += score;
        scoreUI.text = currentScore.ToString();
        return score;
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
