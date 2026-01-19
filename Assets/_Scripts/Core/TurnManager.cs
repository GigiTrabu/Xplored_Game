using System.Collections.Generic;
using UnityEngine;
using TMPro; // Serve per la UI

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _turnText;

    // --- NUOVO: Contatore Round per le Analytics ---
    public int CurrentRound { get; private set; } = 1;

    private List<Character> _turnOrder;
    private int _currentIndex = 0;

    public Character ActiveCharacter 
    { 
        get 
        {
            if (_turnOrder == null || _turnOrder.Count == 0) return null;
            return _turnOrder[_currentIndex];
        }
    }

    void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        var allCharacters = FindObjectsOfType<Character>();
        _turnOrder = new List<Character>();

        // Filtra: niente Boss nella lista turni giocatori
        foreach (var c in allCharacters)
        {
            if (!c.name.ToLower().Contains("boss"))
            {
                _turnOrder.Add(c);
            }
        }
        
        // Ordina per nome
        _turnOrder.Sort((p1, p2) => p1.name.CompareTo(p2.name));

        Debug.Log($"TurnManager Inizializzato. Giocatori: {_turnOrder.Count}");

        StartFirstTurn();
    }

    void StartFirstTurn()
    {
        if (ActiveCharacter == null) return;

        _currentIndex = 0;
        ActiveCharacter.StartTurn();
        UpdateUI();

        // LOG INIZIALE ANALYTICS
        if (AnalyticsManager.Instance != null)
        {
            AnalyticsManager.Instance.LogEvent(ActiveCharacter.name, "Inizio Turno", "Primo turno della partita");
        }
    }

    public void NextTurn()
    {
        if (ActiveCharacter == null) return;

        // 1. Logga la fine del turno precedente
        if (AnalyticsManager.Instance != null)
        {
            AnalyticsManager.Instance.LogEvent(ActiveCharacter.name, "Fine Turno", $"Passi usati: {ActiveCharacter.StepsTaken}");
        }

        ActiveCharacter.EndTurn();

        _currentIndex++;
        if (_currentIndex >= _turnOrder.Count)
        {
            _currentIndex = 0;
            CurrentRound++; // Aumenta il round
            Debug.Log($"--- NUOVO ROUND {CurrentRound} ---");
        }

        ActiveCharacter.StartTurn();
        UpdateUI();

        // 2. Logga l'inizio del nuovo turno
        if (AnalyticsManager.Instance != null)
        {
            AnalyticsManager.Instance.LogEvent(ActiveCharacter.name, "Inizio Turno", $"HP: {ActiveCharacter.CurrentHP}");
        }
    }

    void UpdateUI()
    {
        if (_turnText != null && ActiveCharacter != null)
        {
            string cleanName = ActiveCharacter.name.Replace("(Clone)", "");
            _turnText.text = $"Turno di: {cleanName}";

            SpriteRenderer sr = ActiveCharacter.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                _turnText.color = sr.color;
            }
        }
    }
}