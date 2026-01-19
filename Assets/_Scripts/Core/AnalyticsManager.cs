using UnityEngine;
using System.IO; //per comporre CSV
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
        //file salvato nella cartella ASSets
        _filePath = Application.dataPath + "/Match_Log.csv";

        //s enon esiste lo creiamo
        if (!File.Exists(_filePath))
        {
            string header = "Timestamp,Turno,Personaggio,Azione,Dettagli\n";
            File.WriteAllText(_filePath, header);
        }
        
        LogEvent("SISTEMA", "Inizio Partita", "Nuova sessione avviata");
    }

    //Funzione generica per registrare qualsiasi cosa
    public void LogEvent(string characterName, string action, string details)
    {
       
        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        string turnInfo = "Round " + TurnManager.Instance.CurrentRound;
        
        string line = $"{timestamp},{turnInfo},{characterName},{action},{details}\n";

        
        File.AppendAllText(_filePath, line);
        
        Debug.Log($"[LOG SALVATO]: {line}"); //feeedback sulla console
    }
}