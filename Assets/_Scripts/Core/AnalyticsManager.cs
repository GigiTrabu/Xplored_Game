using UnityEngine;
using System.IO; // Serve per leggere e scrivere file
using System;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;

    private string _filePath;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Il file verrà salvato nella cartella del progetto (Assets)
        _filePath = Application.dataPath + "/Match_Log.csv";

        // Se il file non esiste, creiamo l'intestazione delle colonne per Excel
        if (!File.Exists(_filePath))
        {
            string header = "Timestamp,Turno,Personaggio,Azione,Dettagli\n";
            File.WriteAllText(_filePath, header);
        }
        
        LogEvent("SISTEMA", "Inizio Partita", "Nuova sessione avviata");
    }

    // Funzione generica per registrare qualsiasi cosa
    public void LogEvent(string characterName, string action, string details)
    {
        // Formato: DataOra, NumeroTurno, NomeChi, CosaHaFatto, InfoExtra
        // Usiamo la virgola per separare le colonne del CSV
        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        string turnInfo = "Round " + TurnManager.Instance.CurrentRound; // Aggiungeremo CurrentRound nel TurnManager
        
        string line = $"{timestamp},{turnInfo},{characterName},{action},{details}\n";

        // Scrive la riga nel file (Append significa che aggiunge in fondo senza cancellare il resto)
        File.AppendAllText(_filePath, line);
        
        Debug.Log($"[LOG SALVATO]: {line}"); // Feedback in console
    }
}