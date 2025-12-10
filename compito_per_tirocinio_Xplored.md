# To Be Killed
Gioco realizzato in unity. 2D in UI.
Su una griglia 8x5 Si muovono dei personaggi, controllati dal giocatore. Il movimento è solo Manhattan.
Il gioco è a turni. Ogni turno un personaggio può muovere e poi agire.
Il movimento che un personaggio può effettuare in un turno è di due passi.
I personaggi sono 4, ognuno con un set di azioni differenti. Tutti i personaggi hanno un'azione comune chiamata "Focus" che da un +2 al prossimo tiro di dado.
Le azioni hanno un cooldown in numero di turni. Cooldown 0 vuol dire che si può usare sempre. Cooldown 1 vuol dire che si può usare un turno sì e uno no.

 - A.
   1. Attacco a distanza di massimo 2 di 2 danni più 1d6 `[aggro danni - 2][cooldown 1]`
   2. Attacco melee da 3 danni più 1d6 `[aggro danni][cooldown 0]`

 - B.
   1. Cura sé di 10HP `[aggro 1][cooldown 3]`
   2. Attacco 4 danni più 1d6 `[aggro danni + 2][cooldown 1]`

 - C.
   1. Attacco di 1 danno più 1d6 più un danno per ogni alleato adiacente `[aggro danni -1][cooldown 1]`
   2. Cura tutti gli alleati adiacenti di 2 + 1d6 `[aggro pari al numero di alleati curati * 2][cooldown 3]`

 - D.
   1. Sposta un nemico di 1d6 / 2 (per difetto) `[aggro 6][cooldown 2]`
   2. Sposta un alleato di 1d6 `[aggro distanza mossa * 3][cooldown 2]`

Ogni personaggio ha tot HP:
 - A. 20
 - B. 30
 - C. 15
 - D. 10

L'unico nemico è un boss.
Il boss ha 50HP.
Il boss ha un livello di aggro per ogni personaggio che determina quale sia il suo target.
Ogni azione dei personaggi aggiunge un valore di aggro al loro stack per il boss.
Ad ogni inizio turno del boss il valore di aggro di ogni personaggio diminuisce di uno. Ad ogni inizio turno di ogni giocatore il proprio valore di aggro diminuisce di uno.
Il bersaglio del boss è il personaggio con più aggro.
Ad ogni suo turno il boss guarda se può usare delle azioni contro il suo bersaglio senza muoversi. Se sì ne usa una disponibile random.
Se no si muove verso il suo bersaglio, e usa una delle azioni disponibili dopo essersi mosso. Se non ha nessuna azione disponibile contro il suo bersaglio controlla se ha delle azioni disponibili contro il bersaglio più vicino, spareggiando per valore di aggro prima e a caso in caso di altro pareggio, e la usa. Potrebbe non poter usare alcuna azione, nel caso non ne usa.

Il boss può passare attraverso i personaggi.
I personaggi non possono passare attraverso il boss, ma possono passare attraverso i personaggi.

Il boss può muovere di 3.

Le azioni del boss sono:

 1. Attacco melee a tutti attorno a sé 5 + 1d6 `[cooldown 3]`
 2. Attacco a distanza da minimo 4 di distanza per 3d3 `[cooldown 1]`
 3. Attacco melee 2+1d6 `[cooldown 1]`
 4. Attacco melee 2 danni push target a distanza 2 `[cooldown 1]`
 5. [Se non ha nessun aggro] Cura sé di 15HP `[Cooldown 1]`


# Analytics

Ad ogni movimento e azione viene loggata la cosa nelle analytics. Quale personaggio o il boss muove da dove a dove e quale azione compie, con che valore di dado tirato e con che aggro risultante.
Viene loggato anche l'evento di diminuzione di aggro.
Ogni log di analytics ha un timestamp e salva alcuni dati relativi alla macchina (pc o device mobile).
Questi dati vengono mandati ad un servizio web tramite richieste https, autenticandosi con una chiave che identifica anche il gioco.
I dati vengono salvati in un database mongodb.
Il servizio web può essere implementato in go o in nextjs.

Analytics ha una pagina di visualizzazione che mostra un aggregato dei dati e permette di isolare le singole partite e vederne i log.
La visualizzazione mostra sia un grafico di HP per turno per ogni personaggio e boss, che la loro posizione su griglia.
L'aggregato mostra la durata media dei turni, quanti game sono finiti in vittoria e quanti in sconfitta e la durata media e mediana delle sconfitte e delle vittorie. Mostra un grafico per turno con gli hp medi e mediani di ogni personaggio e del boss. Scegliendo un turno mostra una heatmap delle posizioni del boss e di ogni personaggio.

I servizi di analytics devono essere deployati tramite docker e tramite portainer, che possono essere installati in locale.





Ciao Luigi,

ti scrivo per darti il compito a casa di cui abbiamo parlato. Ti allego
le specifiche. È una miniatura di tutto il nostro stack, escluso l'hardware.
Il lavoro prendilo più a scopo didattico che non per ottenere il
prodotto finale figo. Usa pure LLM ma sempre a scopo didattico. Non
copiaincollare codice ma guarda che librerie usa, che strutture
suggerisce, ecc. Poi scriviti il tuo codice.

Mettiti su un repo git per ogni componente (gioco, e analytics. Vedi te
se separare analytics in due repo, backend e frontend).

Il gioco è probabilmente tanto da implementare. La parte di analytics un
po' meno.

Prima di cominciare a implementare ti chiedo di provare a dividere il
lavoro in task e sottotask e di stimarle. Come linea di principio la
durata massima di un task operativo dovrebbe essere di un giorno. Visto
che dovrai studiarti le varie librerie e tool che non conosci le stime
iniziali saranno approssimative e dovranno essere aggiornate dopo che
hai imparato qualcosa di nuovo che ti dà informazioni che le modificano.
Dopo che hai fatto la prima divisione in task prova a prioritizzarli per
avere prima in MVP e poi un minimo set di feature, con altre feature
considerate meno prioritarie per avere un prodotto funzionante. Visto
che il lavoro totale sarà tanto potremo andare a fare tagli e modifiche
se queste ci fanno risparmiare tanto tempo e danno poco valore. La parte
di analytics ha un po' meno possibilità di essere tagliata, visto che è
meno complessa.

Una volta che hai questo li vediamo assieme, prima che inizi a sviluppare.

Metto in cc Alessandro Carlotto, Head of Development. Quindi quando
rispondi alle email fai "rispondi a tutti".

Fammi sapere se hai dubbi o domande,
Gianluca Alloisio