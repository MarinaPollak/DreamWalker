using UnityEngine;
using UnityEngine.UI;
public class PuzzleTile : MonoBehaviour
{
    public int correctIndex;
    public int currentIndex;
    public Image image;

    [HideInInspector] public int row;
    [HideInInspector] public int col;

    private SlidingPuzzle manager;
    private Button button;

    void Start()
    {
        //  Auto-assign image if missing
        if (image == null)
            image = GetComponent<Image>();

        manager = FindFirstObjectByType<SlidingPuzzle>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnTileClicked);
    }

    void OnTileClicked()
    {
        manager.TileClicked(this);
    }
}
