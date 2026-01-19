using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance; //per chiamare gridmaster da altri script

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

    
    private Dictionary<Vector2Int, Tile> _tiles;  //caselle x,y gstite in un dizionario per accesso rapido

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
                //creazione casella fisica
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                
                //Inizializzo la casella con dati logici (x,y)
                spawnedTile.Init(x, y);

                //save dictionary
                _tiles[new Vector2Int(x, y)] = spawnedTile;
            }
        }

        CenterCamera();
    }

    void CenterCamera()
    {
       //center camera sulla griglia
        float xCenter = (_width / 2.0f) - 0.5f;
        float yCenter = (_height / 2.0f) - 0.5f;

        _cam.transform.position = new Vector3(xCenter, yCenter, -10);
    }

    void SpawnTestUnits()
    {
        //Spawn dei personaggi
        SpawnCharacter(_heroPrefabA, 0, 0); 
        SpawnCharacter(_heroPrefabB, 0, 1);
        SpawnCharacter(_heroPrefabC, 0, 3); 
        SpawnCharacter(_heroPrefabD, 0, 4); 

        //spawn boss
        SpawnCharacter(_bossPrefab, 7, 2); 

        TurnManager.Instance.Init(); 
    }

    // Funzione helper per creare un personaggio e "snapparlo" alla casella
    void SpawnCharacter(Character prefab, int x, int y)
    {
        var tile = GetTileAtPosition(new Vector2Int(x, y));
        
        //la casella esiste?
        if (tile != null)
        {
            var character = Instantiate(prefab);
            character.SnapToTile(tile);
            
            character.name = prefab.name;
        }
    }


    //Restituisce la casella a una certa coordinata
    public Tile GetTileAtPosition(Vector2Int pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }
}