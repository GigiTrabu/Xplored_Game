using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int _maxHP = 20;
    public int CurrentHP { get; private set; }

    // Dati Posizione
    public int GridX { get; private set; }
    public int GridY { get; private set; }

    // Dati Turno
    public int StepsTaken { get; private set; } = 0;
    public int MaxStepsPerTurn = 2;

    void Start()
    {
        CurrentHP = _maxHP;
    }

    public void SnapToTile(Tile tile)
    {
        GridX = tile.X;
        GridY = tile.Y;
        transform.position = tile.transform.position;
    }

    public void Move(int xDir, int yDir)
    {
        GridX += xDir;
        GridY += yDir;

        var tile = GridManager.Instance.GetTileAtPosition(new Vector2Int(GridX, GridY));
        if (tile != null)
        {
            transform.position = tile.transform.position;
            StepsTaken++;
        }
    }

    public void StartTurn()
    {
        StepsTaken = 0;
        // Abbiamo tolto il cambio colore!
    }

    public void EndTurn()
    {
        // Abbiamo tolto il cambio colore!
    }
}