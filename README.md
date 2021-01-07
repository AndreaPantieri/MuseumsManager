# MuseumsManager
Museums Manager è un applicativo realizzato in C# WPF che utilizza il DBMS "Microsoft SQL Server Management Studio 18" (TSql, SQL Server), adottando un pattern di programmazione MVC semplificato.

Esso è stato realizzato per l'esame di "Basi di dati" nel corso di laurea di "Ingegneria e Scienze Informatiche" di Cesena (Università di Bologna) in soli 2 mesi di tempo da 2 persone, ed include un intera documentazione sul suo ciclo di vita (analisi dei requisiti, progettazione concettuale e logica, analisi approfondita dei dati e dei costi, ottimizzazione ecc..), oltre che a schemi e mockup di ogni sezione del database.
Degne di nota le Stored Procedures ed ogni ottimizzazione realizzata gestendo tabelle unarie (ricorsive) ed N-N, oltre che ottimizzazioni di codice, metodi ricorsivi e pattern + commenti per il buon mantenimento (ed estendibilità) dello stesso.

L'applicativo permette il completo controllo di ogni ambito che può essere presente nella gestione quotidiana di uno o più museo/i:
- creazione di musei raggruppabili anche in famiglie, per le quali è possibile calcolarne statistiche combinate
- gestione delle sezioni e sottosezioni, orari di apertura/chiusura
- gestione dei contenuti e sottocontenuti e ricerca per sezione, creatore, provenienza, periodo storico e tipo
- gestione dei calendari per le giornate di apertura speciale e di chiusura
- riepilogo dei biglietti acquistati e gestione dei tipi di biglietto, validità e prezzi
- gestione del personale del museo e dei ruoli
- gestione dei registri (manutenzione, spese e presenze dei dipendenti)
- calcolo di diverse tipologie di statistiche ed indici mensili ed annuali, resoconti archiviabili 


#Tutorial all'avvio:
1) Prima di tutto bisognerà disporre di "Visual Studio 2019" con pacchetto C# WPF e "Microsoft SQL Server Management Studio 18" (o superiori), e creare il Database seguendo questa procedura:
- eseguire lo script SQL "PROJECT-MUSEI.sql" che si trova nella cartella "/sql/DB"
- eseguire lo script SQL "MuseumsManager.sql" che si trova nella cartella "/sql/Data"

2) Prima di compilare o eseguire il progetto assicurarsi di modificare il parametro "HostName" della ConnectionString all'interno di "Entities/DBConnection.cs", avendo così la propria QueryString corretta.
3) Compilare il progetto ed avviarlo.