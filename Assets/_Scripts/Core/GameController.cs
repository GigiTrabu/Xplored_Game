using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private float _inputCooldown = 0.2f; 
    private float _lastInputTime;

    void Update()
    {
        //fine turno possibile anche con space (TODO change later maybe)
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TurnManager.Instance.NextTurn();
            return;
        }

        //suggerito bho ok
        if (Time.time - _lastInputTime < _inputCooldown) return;

        //gestione WASD/frecce
        Vector2Int moveDir = Vector2Int.zero;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            moveDir.y = 1;
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            moveDir.y = -1;
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            moveDir.x = -1;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            moveDir.x = 1;

        if (moveDir != Vector2Int.zero)
        {
            TryMoveActiveCharacter(moveDir);
            _lastInputTime = Time.time;
        }
    }

    void TryMoveActiveCharacter(Vector2Int dir)
    {
        Character hero = TurnManager.Instance.ActiveCharacter;
        
        if (hero == null) return;

        //check passi
        if (hero.StepsTaken >= hero.MaxStepsPerTurn)
        {
            Debug.Log("Passi esauriti! Premi SPAZIO per passare il turno.");
            return;
        }

        //calcolo destinazione per logo
        int targetX = hero.GridX + dir.x;
        int targetY = hero.GridY + dir.y;

        //check muri
        if (targetX < 0 || targetX >= 8 || targetY < 0 || targetY >= 5)
        {
            Debug.Log("Muro!");
            return;
        }

        //check occupato
        if (IsTileOccupied(targetX, targetY))
        {
            Debug.Log("Occupato!");
            return;
        }

        //sAVE INITAL POS per log
        string oldPos = $"({hero.GridX}, {hero.GridY})";

        //Esegui movimento
        hero.Move(dir.x, dir.y);

        //SAVE FINALE PO per log
        string newPos = $"({hero.GridX}, {hero.GridY})";
        
        //invio movimento ad analytics
        AnalyticsManager.Instance.LogEvent(hero.name, "Movimento", $"{oldPos} -> {newPos}");
        
        Debug.Log($"Passi: {hero.StepsTaken}/2");
    }

    //Funzione che controlla se c'è qualcuno in quella casella
    bool IsTileOccupied(int x, int y)
    {
        
        Character[] allCharacters = FindObjectsOfType<Character>();

        foreach (var c in allCharacters)
        {
            // Se il personaggio è presente
            if (c.gameObject.activeSelf && c.GridX == x && c.GridY == y)
            {
                return true; 
            }
        }
        return false;
    }
}