using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private float _inputCooldown = 0.2f; 
    private float _lastInputTime;

    void Update()
    {
        // 1. Gestione FINE TURNO (Spazio)
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TurnManager.Instance.NextTurn();
            return;
        }

        // 2. Gestione Cooldown per non muoversi troppo veloce
        if (Time.time - _lastInputTime < _inputCooldown) return;

        // 3. Gestione MOVIMENTO (WASD / Frecce)
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

        // A. Controllo Passi
        if (hero.StepsTaken >= hero.MaxStepsPerTurn)
        {
            Debug.Log("Passi esauriti! Premi SPAZIO per passare il turno.");
            return;
        }

        // B. Calcolo destinazione prevista
        int targetX = hero.GridX + dir.x;
        int targetY = hero.GridY + dir.y;

        // C. Controllo Muri
        if (targetX < 0 || targetX >= 8 || targetY < 0 || targetY >= 5)
        {
            Debug.Log("Muro!");
            return;
        }

        // D. Controllo Occupato
        if (IsTileOccupied(targetX, targetY))
        {
            Debug.Log("Occupato!");
            return;
        }

        // --- SALVIAMO LA POSIZIONE DI PARTENZA ---
        string oldPos = $"({hero.GridX}, {hero.GridY})";

        // E. Movimento valido (Qui GridX e GridY vengono aggiornati dentro Character.cs)
        hero.Move(dir.x, dir.y);

        // --- SALVIAMO LA POSIZIONE DI ARRIVO ---
        string newPos = $"({hero.GridX}, {hero.GridY})";
        
        // F. LOG PER ANALYTICS (Formato: Da -> A)
        AnalyticsManager.Instance.LogEvent(hero.name, "Movimento", $"{oldPos} -> {newPos}");
        
        Debug.Log($"Passi: {hero.StepsTaken}/2");
    }

    // Funzione che controlla se c'è qualcuno in quella casella
    bool IsTileOccupied(int x, int y)
    {
        // Trova tutti i personaggi nella scena (Eroi e Boss)
        Character[] allCharacters = FindObjectsOfType<Character>();

        foreach (var c in allCharacters)
        {
            // Se il personaggio è attivo (vivo) E si trova alle coordinate target
            if (c.gameObject.activeSelf && c.GridX == x && c.GridY == y)
            {
                return true; // Sì, è occupato
            }
        }
        return false; // No, è libero
    }
}