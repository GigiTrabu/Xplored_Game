# Task 1: Setup e Architettura
 - Task 1.1: Setup Repository e Progetto Unity [2-3ore]
    1. Creare la repository per il gioco
    2. Setup per progetto Unity 2D (placeholder iniziali)

 - Task 1.2: Setup Repository per le Analytics e il Database [3-4ore]
    1. Creo repository per Analytics
    2. Setup per MongoDB in locale
    3. Scrivere lo schema dati iniziale per la raccolta (da modificare in seguito)

# Task 2: MVP del gioco
 - Task 2.1: Creazione griglia e movimento player (singolo) [1giorno]
    1. Creazione griglia 8x5
    2. implementazione del movimento manhattan
    3. Verifica del movimento con controlli di validità di movimento prima dello spostamento

 - Task 2.2: Turni di Gioco [6-8ore]
    1. Implementazione degli stati dei turni (giocatore/Boss/Fineturno...)
    2. Gestione movimento turno (massimo 2 passi)

 - Task 2.3: UI di base [1giorno]
    1. Disegnare la griglia di gioco
    2. Aggiungere bottoni di gioco (Start/Endturn)
    3. Aggiungere feedback con log a schermo delle varie azioni

# Task 3: Gestione Analytics
    [Le stime per questa parte sono indicative essendo argomenti su cui ho meno dimestichezza]
 - Task 3.1 Backend API [1giorno]
 - Task 3.2 Invio asincrono dei dati per scrittura su MOngoDB [1giorno]

# Task 4: non MVP features del gioco
 - Task 4.1: Sistema di azione con dadi [6ore]
    1. Creazione di una classe carattere con HP ed un alista di azioni
    2. Implementazione del lancio del D6
    3. Implementazione dell'abilità Focus

 - Task 4.2: Creazione dei diversi personaggi (A,B,C,D) [6-8ore]
    1. Configurazione stats personali
    2. Configurazione skill specifiche (attacco melee, range, cura, push)
    3. Implementazione e gestione cooldown

 - Task 4.3: Sistema Aggro [6-8ore]
    1. Aggiunta proprietà aggro nei confronti del Boss
    2. Logica di incremento e decremento
    3. Visualizzazione aggro in UI?

# Task 5: logica Boss
 - Task 5.1: Selezione del target (Boss state machine) [8ore]
    1. cerco il PG con aggro maggiore

 - Task 5.2: Movimento Boss e Azione [1giorno]
    1. Se PG con aggro >> è in range allora effettuo attacco
    2. Se PG non è in range ->move
    3. Implementazione logica in caso di mancata presenza di PG scelto nel range

 - Task 5.3: Boss Skill [8ore]
    1. Implementazione skill del boss
    2. Implementazioe logica delle collisioni

# Task 6: Visualizzazione delle Analytics
 - Task 6.1: Aggregazione dati con query su MongooDB [2-3ore]

 - Task 6.2: Dashboard frontend [2-3ore]
    1. list apartite/dettagli partite
    2. grafici statistiche parite per singoli giocatori?

 - task 6.3: Heatmap celle occupate maggiormente[2ore]?

# Task 7: Docker
 - Task 7.1: Creazione Dockerfile per backend/frontend Analytics [2ore]

 - task 7.2: Setup Portainer [4-5ore]

# Task 8: Refactoring e Pulizia
 - task 8.1: refactoring [2-3giorni]

 - task 8.2: pulizia e commenti [1-2giorni]
