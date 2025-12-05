using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
public class SlidingPuzzle : MonoBehaviour
{
    public GameObject tilePrefab;
    public Sprite[] sliceImages;
    public Transform tileParent;
    public int gridSize = 3;
    public GameObject puzzleUI; // reference to your UI panel


    [HideInInspector] public List<PuzzleTile> tiles = new List<PuzzleTile>();
    private PuzzleTile selectedTile = null;

    void OnEnable()
    {
        BuildPuzzle();
        ShuffleTiles();
        UpdateTilePositions();   //
    }

    void OnDisable()
    {
        foreach (Transform child in tileParent)
            Destroy(child.gameObject);

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

        UpdateTilePositions(); //  Apply changes to grid
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

    bool IsNeighbor(PuzzleTile a, PuzzleTile b)
    {
        if (a == b) return false;
        return (Mathf.Abs(a.row - b.row) + Mathf.Abs(a.col - b.col)) == 1;
    }

    void SwapTiles(PuzzleTile a, PuzzleTile b)
    {
        int temp = a.currentIndex;
        a.currentIndex = b.currentIndex;
        b.currentIndex = temp;

        UpdateTilePositions(); // Required after swap
    }

    void HighlightAdjacent(PuzzleTile tile)
    {
        foreach (var t in tiles)
        {
            t.image.color = Color.white;

            if (IsNeighbor(tile, t))
                t.image.color = Color.yellow;
        }

        tile.image.color = Color.green;
    }

    void ResetColors()
    {
        foreach (var t in tiles)
            t.image.color = Color.white;
    }
    public void CheckIfSolved()
    {
        foreach (var tile in tiles)
        {
            if (tile.correctIndex != tile.currentIndex)
                return;
        }

        Debug.Log("Sliding Puzzle Completed!");
        puzzleUI.SetActive(false);
    }

    public void ClosePuzzle()
    {
        bool isOpen = puzzleUI.activeSelf;
        puzzleUI.SetActive(!isOpen);   // hides the puzzle UI
    }
}
