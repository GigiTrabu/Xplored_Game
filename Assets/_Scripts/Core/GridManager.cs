using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // --- SINGLETON ---
    // Permette di chiamare GridManager.Instance da qualsiasi altro script
    public static GridManager Instance;

    [Header("Grid Settings")]
    [SerializeField] private int _width = 8;
    [SerializeField] private int _height = 5;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;

    [Header("Characters")]
    [SerializeField] private Character _heroPrefabA;
    [SerializeField] private Character _heroPrefabB;
    [SerializeField] private Character _heroPrefabC;
    [SerializeField] private Character _heroPrefabD;
    [SerializeField] private Character _bossPrefab;

    // Il nostro "database" delle caselle: chiave (x,y) -> valore (Tile script)
    private Dictionary<Vector2Int, Tile> _tiles;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateGrid();
        SpawnTestUnits();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2Int, Tile>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                // 1. Crea la casella fisica
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                
                // 2. Inizializza i dati logici e visivi
                spawnedTile.Init(x, y);

                // 3. Salva nel dizionario
                _tiles[new Vector2Int(x, y)] = spawnedTile;
            }
        }

        CenterCamera();
    }

    void CenterCamera()
    {
        // Centra la camera sulla griglia
        float xCenter = (_width / 2.0f) - 0.5f;
        float yCenter = (_height / 2.0f) - 0.5f;

        // Z a -10 è standard per il 2D
        _cam.transform.position = new Vector3(xCenter, yCenter, -10);
    }

    void SpawnTestUnits()
    {
        // Spawna gli Eroi sulla sinistra
        SpawnCharacter(_heroPrefabA, 0, 0); // Angolo basso sx
        SpawnCharacter(_heroPrefabB, 0, 1);
        SpawnCharacter(_heroPrefabC, 0, 3); // Lascia un buco a y=2
        SpawnCharacter(_heroPrefabD, 0, 4); // Un po' avanti

        // Spawna il Boss sulla destra
        SpawnCharacter(_bossPrefab, 7, 2);  // Centro destra

        TurnManager.Instance.Init(); 
    }

    // Funzione helper per creare un personaggio e "snapparlo" alla casella
    void SpawnCharacter(Character prefab, int x, int y)
    {
        var tile = GetTileAtPosition(new Vector2Int(x, y));
        
        // Controllo di sicurezza: la casella esiste?
        if (tile != null)
        {
            var character = Instantiate(prefab);
            character.SnapToTile(tile);
            
            // Rinomina l'oggetto nella gerarchia per pulizia
            character.name = prefab.name;
        }
    }

    // --- API PUBBLICHE ---

    // Restituisce la casella a una certa coordinata (o null se fuori mappa)
    public Tile GetTileAtPosition(Vector2Int pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }
}