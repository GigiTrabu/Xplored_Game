using UnityEngine;

public class Tile : MonoBehaviour
{
    // x,y per caselle
    public int X { get; private set; }
    public int Y { get; private set; }

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color _baseColor, _offsetColor; //doppio colorep per eefftto scacchiera

    [SerializeField] private GameObject _highlight; 



    //Fnzione chiamata dall'object manager per creazione mappa
    public void Init(int x, int y)
    {
        X = x;
        Y = y;

        //calcolo per scacchiera
        bool isOffset = (x + y) % 2 == 1;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        
        //assegno un nome per ogni mattonella diversa
        name = $"Tile {x},{y}";
    }
}