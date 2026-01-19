using UnityEngine;

public class Tile : MonoBehaviour
{
    // Queste variabili diranno alla casella dove si trova nella griglia (es. 0,0 o 7,4)
    public int X { get; private set; }
    public int Y { get; private set; }

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color _baseColor, _offsetColor; // Colori per fare l'effetto scacchiera

    [SerializeField] private GameObject _highlight; // Trascineremo qui un oggetto grafico

    // ... (il resto delle variabili Awake e Init rimangono uguali) ...

    public void SetHighlight(bool active)
    {
        if (_highlight != null)
        {
            _highlight.SetActive(active);
        }
    }

    // Questa funzione verrà chiamata dal Manager quando crea la casella
    public void Init(int x, int y)
    {
        X = x;
        Y = y;

        // Calcolo matematico per alternare i colori (come una scacchiera)
        bool isOffset = (x + y) % 2 == 1;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        
        // Cambiamo il nome nell'editor per trovarla facilmente (es. "Tile 3,2")
        name = $"Tile {x},{y}";
    }
}