using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlidingPuzzle : MonoBehaviour
{
    public GameObject tilePrefab;
    public Sprite[] sliceImages; // Drag sliced sprites here!
    public Transform tileParent; // The Grid Layout Group area
    public int gridSize = 3;

    [HideInInspector] public List<PuzzleTile> tiles = new List<PuzzleTile>();
    private PuzzleTile selectedTile = null;
    void OnEnable() // Whenever puzzle opens, rebuild
    {
        BuildPuzzle();
        ShuffleTiles();
    }

    void OnDisable() // Whenever puzzle closes, clean up
    {
        foreach (Transform child in tileParent) Destroy(child.gameObject);
        tiles.Clear();
    }

    void BuildPuzzle()
    {
        for (int i = 0; i < sliceImages.Length; i++)
        {
            GameObject newTile = Instantiate(tilePrefab, tileParent);
            PuzzleTile tile = newTile.GetComponent<PuzzleTile>();

            tile.correctIndex = i;
            tile.currentIndex = i;
            tile.image.sprite = sliceImages[i];

            tiles.Add(tile);
        }
    }

    void ShuffleTiles()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            int rand = Random.Range(i, tiles.Count);

            int t = tiles[i].currentIndex;
            tiles[i].currentIndex = tiles[rand].currentIndex;
            tiles[rand].currentIndex = t;
        }
    }

    public void CheckIfSolved()
    {
        foreach (var tile in tiles)
        {
            if (tile.correctIndex != tile.currentIndex) return;
        }

        Debug.Log("Puzzle Solved!");
        gameObject.SetActive(false); // Hide puzzle once solved
    }


    void UpdateTilePositions()
    {
        foreach (var tile in tiles)
        {
            tile.transform.SetSiblingIndex(tile.currentIndex);

            tile.row = tile.currentIndex / gridSize;
            tile.col = tile.currentIndex % gridSize;
        }
    }

    void HighlightAdjacent(PuzzleTile tile)
    {
        foreach (var t in tiles)
        {
            var img = t.image;
            img.color = Color.white; // reset all tiles

            if (IsNeighbor(tile, t))
                img.color = Color.yellow; // highlight neighbors
        }

        tile.image.color = Color.green; // selected tile
    }

    bool IsNeighbor(PuzzleTile a, PuzzleTile b)
    {
        if (a == b) return false;
        return (Mathf.Abs(a.row - b.row) + Mathf.Abs(a.col - b.col)) == 1;
    }

    public void TileClicked(PuzzleTile tile)
    {
        if (selectedTile == null)
        {
            selectedTile = tile;
            HighlightAdjacent(tile);
        }
        else if (selectedTile == tile)
        {
            selectedTile = null;
            ResetColors();
        }
        else if (IsNeighbor(selectedTile, tile))
        {
            SwapTiles(selectedTile, tile);
            selectedTile = null;
            ResetColors();
            CheckIfSolved();
        }
    }

    void SwapTiles(PuzzleTile a, PuzzleTile b)
    {
        int temp = a.currentIndex;
        a.currentIndex = b.currentIndex;
        b.currentIndex = temp;

        UpdateTilePositions();
    }

    void ResetColors()
    {
        foreach (var t in tiles)
            t.image.color = Color.white;
    }
}
