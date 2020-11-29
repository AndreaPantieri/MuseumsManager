-- *********************************************
-- * Academic SQL generation                   
-- *--------------------------------------------
-- * DB-MAIN version: 11.0.1              
-- * Generator date: Dec  4 2018              
-- * Generation date: Sun Nov 29 12:45:40 2020 
-- * LUN file: Z:\Database project\MuseumsManager\doc\PROJECT-MUSEI.lun 
-- * Schema: SCHEMA-LOGICO-NO-GER-NOLABEL/1-1-1 
-- ********************************************* 


-- Database Section
-- ________________ 

create database SCHEMA-LOGICO-NO-GER-NOLABEL;


-- DBSpace Section
-- _______________


-- Tables Section
-- _____________ 

create table Biglietto (
     idBiglietto numeric(1) not null,
     DataValidita date not null,
     idMuseo numeric(1) not null,
     idTipoBiglietto numeric(1) not null,
     primary key (idBiglietto),
     foreign key (idMuseo) references Museo,
     foreign key (idTipoBiglietto) references TipoBiglietto);

create table CalendarioApertureSpeciali (
     idCalendarioApertureSpeciali numeric(1) not null,
     Data date not null,
     OrarioApertura date not null,
     OrarioChiusura date not null,
     NumBigliettiMax char(1) not null,
     idMuseo numeric(1) not null,
     primary key (idCalendarioApertureSpeciali),
     foreign key (idMuseo) references Museo);

create table CalendarioChiusure (
     idCalendarioChiusure numeric(1) not null,
     Data date not null,
     idMuseo numeric(1) not null,
     primary key (idCalendarioChiusure),
     foreign key (idMuseo) references Museo);

create table Contenuto (
     idContenuto numeric(1) not null,
     Nome char(1) not null,
     Descrizione varchar(1) not null,
     DataRitrovamento date not null,
     DataArrivoMuseo date not null,
     idContenutoPadre numeric(1) not null,
     idProvenienza numeric(1) not null,
     idPeriodoStorico numeric(1) not null,
     idSezione numeric(1) not null,
     primary key (idContenuto) ,
     check(exists(select * from Contenuto_Tipologia
                  where Contenuto_Tipologia.idContenuto = idContenuto)),
     foreign key (idContenutoPadre) references Contenuto,
     foreign key (idProvenienza) references Provenienza,
     foreign key (idPeriodoStorico) references PeriodoStorico,
     foreign key (idSezione) references Sezione);

create table Contenuto_Tipologia (
     idContenuto numeric(1) not null,
     idTipoContenuto numeric(1) not null,
     primary key (idContenuto, idTipoContenuto),
     foreign key (idTipoContenuto) references TipoContenuto,
     foreign key (idContenuto) references Contenuto);

create table Creato (
     idContenuto numeric(1) not null,
     idCreatore numeric(1) not null,
     primary key (idContenuto, idCreatore),
     foreign key (idCreatore) references Creatore,
     foreign key (idContenuto) references Contenuto);

create table Creatore (
     idCreatore numeric(1) not null,
     Nome varchar(150) not null,
     Cognome varchar(150) not null,
     Descrizione varchar(150) not null,
     AnnoNascita numeric(1) not null,
     primary key (idCreatore) ,
     check(exists(select * from Creato
                  where Creato.idCreatore = idCreatore)) ,
     check(exists(select * from Museo_Creatore
                  where Museo_Creatore.idCreatore = idCreatore)));

create table FamigliaMusei (
     idFamiglia char(1) not null,
     Nome char(1) not null,
     primary key (idFamiglia) ,
     check(exists(select * from Museo
                  where Museo.idFamiglia = idFamiglia)));

create table Museo (
     idMuseo numeric(1) not null,
     Nome char(1) not null,
     Luogo char(1) not null,
     OrarioAperturaGenerale date not null,
     OrarioChiusuraGenerale date not null,
     NumBigliettiMaxGenerale numeric(1) not null,
     idFamiglia char(1),
     primary key (idMuseo) ,
     check(exists(select * from Sezione
                  where Sezione.idMuseo = idMuseo)) ,
     check(exists(select * from Personale
                  where Personale.idMuseo = idMuseo)) ,
     check(exists(select * from Museo_Tipologia
                  where Museo_Tipologia.idMuseo = idMuseo)) ,
     check(exists(select * from TipoBiglietto
                  where TipoBiglietto.idMuseo = idMuseo)),
     foreign key (idFamiglia) references FamigliaMusei);

create table Museo_Creatore (
     idCreatore numeric(1) not null,
     idMuseo numeric(1) not null,
     primary key (idCreatore, idMuseo),
     foreign key (idMuseo) references Museo,
     foreign key (idCreatore) references Creatore);

create table Museo_PeriodoStorico (
     idMuseo numeric(1) not null,
     idPeriodoStorico numeric(1) not null,
     primary key (idPeriodoStorico, idMuseo),
     foreign key (idPeriodoStorico) references PeriodoStorico,
     foreign key (idMuseo) references Museo);

create table Museo_Provenienza (
     idMuseo numeric(1) not null,
     idProvenienza numeric(1) not null,
     primary key (idProvenienza, idMuseo),
     foreign key (idProvenienza) references Provenienza,
     foreign key (idMuseo) references Museo);

create table Museo_Tipologia (
     idMuseo numeric(1) not null,
     idTipoMuseo numeric(1) not null,
     primary key (idTipoMuseo, idMuseo),
     foreign key (idTipoMuseo) references TipoMuseo,
     foreign key (idMuseo) references Museo);

create table PeriodoStorico (
     idPeriodoStorico numeric(1) not null,
     Nome varchar(150) not null,
     Descrizione varchar(150) not null,
     AnnoInizio numeric(1) not null,
     AnnoFine char(1) not null,
     primary key (idPeriodoStorico) ,
     check(exists(select * from Museo_PeriodoStorico
                  where Museo_PeriodoStorico.idPeriodoStorico = idPeriodoStorico)));

create table Personale (
     idPersonale numeric(1) not null,
     Nome char(1) not null,
     Cognome char(1) not null,
     Cellulare char(1) not null,
     Email char(1) not null,
     StipendioOra numeric(1) not null,
     idMuseo numeric(1),
     primary key (idPersonale) ,
     check(exists(select * from Personale_Tipologia
                  where Personale_Tipologia.idPersonale = idPersonale)),
     foreign key (idMuseo) references Museo);

create table Personale_Tipologia (
     idPersonale numeric(1) not null,
     idTipoPersonale numeric(1) not null,
     primary key (idPersonale, idTipoPersonale),
     foreign key (idTipoPersonale) references TipoPersonale,
     foreign key (idPersonale) references Personale);

create table Provenienza (
     idProvenienza numeric(1) not null,
     Nome varchar(150) not null,
     Descrizione varchar(150) not null,
     primary key (idProvenienza) ,
     check(exists(select * from Museo_Provenienza
                  where Museo_Provenienza.idProvenienza = idProvenienza)) ,
     check(exists(select * from Contenuto
                  where Contenuto.idProvenienza = idProvenienza)));

create table RegistroManutenzioni (
     idManutenzione numeric(1) not null,
     idMuseo numeric(1) not null,
     Data date not null,
     Descrizione varchar(1) not null,
     Prezzo numeric(1) not null,
     primary key (idManutenzione),
     unique (idMuseo),
     foreign key (idMuseo) references Museo);

create table RegistroPresenze (
     idRegistro numeric(1) not null,
     idPersonale numeric(1) not null,
     DataEntrata date not null,
     DataUscita date not null,
     primary key (idRegistro),
     unique (idPersonale),
     foreign key (idPersonale) references Personale);

create table Sezione (
     idSezione numeric(1) not null,
     Nome char(1) not null,
     Descrizione char(1) not null,
     idSezionePadre numeric(1) not null,
     idMuseo numeric(1) not null,
     primary key (idSezione) ,
     check(exists(select * from Sezione_Tipologia
                  where Sezione_Tipologia.idSezione = idSezione)),
     foreign key (idSezionePadre) references Sezione,
     foreign key (idMuseo) references Museo);

create table Sezione_Tipologia (
     idSezione numeric(1) not null,
     idTipoSezione numeric(1) not null,
     primary key (idTipoSezione, idSezione),
     foreign key (idTipoSezione) references TipoSezione,
     foreign key (idSezione) references Sezione);

create table Statistiche (
     idStatistiche numeric(1) not null,
     MeseAnno char(1) not null,
     SpeseTotali numeric(1) not null,
     Fatturato numeric(1) not null,
     NumBigliettiVenduti numeric(1) not null,
     NumPresenzeTotali numeric(1) not null,
     NumManutenzioni numeric(1) not null,
     NumContenutiNuovi numeric(1) not null,
     NumChiusure numeric(1) not null,
     primary key (idStatistiche));

create table StatisticheFamigliaMusei (
     idStatistiche numeric(1) not null,
     idFamiglia char(1) not null,
     primary key (idStatistiche),
     foreign key (idStatistiche) references Statistiche,
     foreign key (idFamiglia) references FamigliaMusei);

create table StatisticheMuseo (
     idStatistiche numeric(1) not null,
     idMuseo numeric(1) not null,
     primary key (idStatistiche),
     foreign key (idStatistiche) references Statistiche,
     foreign key (idMuseo) references Museo);

create table TipoBiglietto (
     idTipoBiglietto numeric(1) not null,
     Prezzo numeric(1) not null,
     Descrizione char(1) not null,
     idMuseo numeric(1) not null,
     primary key (idTipoBiglietto),
     foreign key (idMuseo) references Museo);

create table TipoContenuto (
     idTipoContenuto numeric(1) not null,
     Descrizione char(1) not null,
     primary key (idTipoContenuto));

create table TipoMuseo (
     idTipoMuseo numeric(1) not null,
     Descrizione char(1) not null,
     primary key (idTipoMuseo));

create table TipoPersonale (
     idTipoPersonale numeric(1) not null,
     Descrizione char(1) not null,
     primary key (idTipoPersonale));

create table TipoSezione (
     idTipoSezione numeric(1) not null,
     Descrizione char(1) not null,
     primary key (idTipoSezione));


-- Index Section
-- _____________ 

create unique index ID_Biglietto
     on Biglietto (idBiglietto);

create index REF_Bigli_Museo
     on Biglietto (idMuseo);

create index REF_Bigli_TipoB
     on Biglietto (idTipoBiglietto);

create unique index ID_CalendarioApertureSpeciali
     on CalendarioApertureSpeciali (idCalendarioApertureSpeciali);

create index REF_Calen_Museo_1
     on CalendarioApertureSpeciali (idMuseo);

create unique index ID_CalendarioChiusure
     on CalendarioChiusure (idCalendarioChiusure);

create index REF_Calen_Museo
     on CalendarioChiusure (idMuseo);

create unique index ID_Contenuto
     on Contenuto (idContenuto);

create index REF_Conte_Conte
     on Contenuto (idContenutoPadre);

create index EQU_Conte_Prove
     on Contenuto (idProvenienza);

create index REF_Conte_Perio
     on Contenuto (idPeriodoStorico);

create index REF_Conte_Sezio
     on Contenuto (idSezione);

create unique index ID_Contenuto_Tipologia
     on Contenuto_Tipologia (idContenuto, idTipoContenuto);

create index REF_Conte_TipoC
     on Contenuto_Tipologia (idTipoContenuto);

create unique index ID_Creato
     on Creato (idContenuto, idCreatore);

create index EQU_Creat_Creat
     on Creato (idCreatore);

create unique index ID_Creatore
     on Creatore (idCreatore);

create unique index ID_FamigliaMusei
     on FamigliaMusei (idFamiglia);

create unique index ID_Museo
     on Museo (idMuseo);

create index EQU_Museo_Famig
     on Museo (idFamiglia);

create unique index ID_Museo_Creatore
     on Museo_Creatore (idCreatore, idMuseo);

create index REF_Museo_Museo_2
     on Museo_Creatore (idMuseo);

create unique index ID_Museo_PeriodoStorico
     on Museo_PeriodoStorico (idPeriodoStorico, idMuseo);

create index REF_Museo_Museo_1
     on Museo_PeriodoStorico (idMuseo);

create unique index ID_Museo_Provenienza
     on Museo_Provenienza (idProvenienza, idMuseo);

create index REF_Museo_Museo
     on Museo_Provenienza (idMuseo);

create unique index ID_Museo_Tipologia
     on Museo_Tipologia (idTipoMuseo, idMuseo);

create index EQU_Museo_Museo
     on Museo_Tipologia (idMuseo);

create unique index ID_PeriodoStorico
     on PeriodoStorico (idPeriodoStorico);

create unique index ID_Personale
     on Personale (idPersonale);

create index EQU_Perso_Museo
     on Personale (idMuseo);

create unique index ID_Personale_Tipologia
     on Personale_Tipologia (idPersonale, idTipoPersonale);

create index REF_Perso_TipoP
     on Personale_Tipologia (idTipoPersonale);

create unique index ID_Provenienza
     on Provenienza (idProvenienza);

create unique index ID_RegistroManutenzioni
     on RegistroManutenzioni (idManutenzione);

create unique index SID_Regis_Museo
     on RegistroManutenzioni (idMuseo);

create unique index ID_RegistroPresenze
     on RegistroPresenze (idRegistro);

create unique index SID_Regis_Perso
     on RegistroPresenze (idPersonale);

create unique index ID_Sezione
     on Sezione (idSezione);

create index REF_Sezio_Sezio
     on Sezione (idSezionePadre);

create index EQU_Sezio_Museo
     on Sezione (idMuseo);

create unique index ID_Sezione_Tipologia
     on Sezione_Tipologia (idTipoSezione, idSezione);

create index EQU_Sezio_Sezio
     on Sezione_Tipologia (idSezione);

create unique index ID_Statistiche
     on Statistiche (idStatistiche);

create unique index ID_Stati_Stati_1
     on StatisticheFamigliaMusei (idStatistiche);

create index REF_Stati_Famig
     on StatisticheFamigliaMusei (idFamiglia);

create unique index ID_Stati_Stati
     on StatisticheMuseo (idStatistiche);

create index REF_Stati_Museo
     on StatisticheMuseo (idMuseo);

create unique index ID_TipoBiglietto
     on TipoBiglietto (idTipoBiglietto);

create index EQU_TipoB_Museo
     on TipoBiglietto (idMuseo);

create unique index ID_TipoContenuto
     on TipoContenuto (idTipoContenuto);

create unique index ID_TipoMuseo
     on TipoMuseo (idTipoMuseo);

create unique index ID_TipoPersonale
     on TipoPersonale (idTipoPersonale);

create unique index ID_TipoSezione
     on TipoSezione (idTipoSezione);

