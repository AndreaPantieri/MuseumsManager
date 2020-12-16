-- Database Section
-- ________________ 

drop database if exists MuseumsManagerDB;

create database MuseumsManagerDB;
GO
use MuseumsManagerDB

-- Tables Section
-- _____________ 

create table Biglietto (
     idBiglietto int not null primary key identity(1,1),
     DataValidita date not null,
	 PrezzoAcquisto float not null,
     idMuseo int not null,
     idTipoBiglietto int not null);

create table CalendarioApertureSpeciali (
     idCalendarioApertureSpeciali int not null primary key identity(1,1),
     Data date not null,
     OrarioApertura time not null,
     OrarioChiusura time not null,
     NumBigliettiMax int,
     idMuseo int not null,
	 unique(Data, idMuseo));

create table CalendarioChiusure (
     idCalendarioChiusure int not null primary key identity(1,1),
     Data date not null,
     idMuseo int not null,
	 unique(Data, idMuseo));

create table Contenuto (
     idContenuto int not null primary key identity(1,1),
     Nome nvarchar(255) not null,
     Descrizione nvarchar(512) not null,
     DataRitrovamento date not null,
     DataArrivoMuseo date not null,
     idContenutoPadre int,
     idProvenienza int not null,
     idPeriodoStorico int not null,
     idSezione int not null);

create table Contenuto_Tipologia (
     idContenuto int not null,
     idTipoContenuto int not null,
     primary key (idContenuto, idTipoContenuto));

create table Creato (
     idContenuto int not null,
     idCreatore int not null,
     primary key (idContenuto, idCreatore));

create table Creatore (
     idCreatore int not null primary key identity(1,1),
     Nome nvarchar(255) not null,
     Cognome nvarchar(255) not null,
     Descrizione nvarchar(512) not null,
     AnnoNascita int not null);

create table FamigliaMusei (
     idFamiglia int not null primary key identity(1,1),
     Nome nvarchar(255) not null);

create table Museo (
     idMuseo int not null primary key identity(1,1),
     Nome nvarchar(255) not null,
     Luogo nvarchar(255) not null,
     OrarioAperturaGenerale time not null,
     OrarioChiusuraGenerale time not null,
     NumBigliettiMaxGenerale int,
     idFamiglia int);

create table Museo_Creatore (
     idCreatore int not null,
     idMuseo int not null,
     primary key (idCreatore, idMuseo));

create table Museo_PeriodoStorico (
     idMuseo int not null,
     idPeriodoStorico int not null,
     primary key (idPeriodoStorico, idMuseo));

create table Museo_Provenienza (
     idMuseo int not null,
     idProvenienza int not null,
     primary key (idProvenienza, idMuseo));

create table Museo_Tipologia (
     idMuseo int not null,
     idTipoMuseo int not null,
     primary key (idTipoMuseo, idMuseo));

create table PeriodoStorico (
     idPeriodoStorico int not null primary key identity(1,1),
     Nome nvarchar(255) not null,
     Descrizione nvarchar(512) not null,
     AnnoInizio int not null,
     AnnoFine int not null);

create table Personale (
     idPersonale int not null primary key identity(1,1),
     Nome nvarchar(255) not null,
     Cognome nvarchar(255) not null,
     Cellulare nvarchar(255) not null,
     Email nvarchar(255) not null,
     StipendioOra float not null,
     idMuseo int);

create table Personale_Tipologia (
     idPersonale int not null,
     idTipoPersonale int not null,
     primary key (idPersonale, idTipoPersonale));

create table Provenienza (
     idProvenienza int not null primary key identity(1,1),
     Nome nvarchar(255) not null,
     Descrizione nvarchar(512) not null);

create table RegistroManutenzioni (
     idManutenzione int not null primary key identity(1,1),
     idMuseo int not null unique,
     Data date not null,
     Descrizione nvarchar(512) not null,
     Prezzo float not null);

create table RegistroPresenze (
     idRegistro int not null primary key identity(1,1),
     idPersonale int not null unique,
     DataEntrata datetime2 not null,
     DataUscita datetime2 not null);

create table Sezione (
     idSezione int not null primary key identity(1,1),
     Nome nvarchar(255) not null,
     Descrizione nvarchar(512) not null,
     idSezionePadre int,
     idMuseo int not null);

create table Sezione_Tipologia (
     idSezione int not null,
     idTipoSezione int not null,
     primary key (idTipoSezione, idSezione));

create table Statistiche (
     idStatistiche int not null primary key identity(1,1),
     MeseAnno date not null,
     SpeseTotali float not null,
     Fatturato float not null,
     NumBigliettiVenduti int not null,
     NumPresenzeTotali int not null,
     NumManutenzioni int not null,
     NumContenutiNuovi int not null,
     NumChiusure int not null);

create table StatisticheFamigliaMusei (
     idStatistiche int not null,
     idFamiglia int not null,
     primary key (idStatistiche));

create table StatisticheMuseo (
     idStatistiche int not null,
     idMuseo int not null,
     primary key (idStatistiche));

create table TipoBiglietto (
     idTipoBiglietto int not null primary key identity(1,1),
	 Nome nvarchar(255) not null,
     Prezzo float not null,
     Descrizione nvarchar(512) not null,
     idMuseo int not null);

create table TipoContenuto (
     idTipoContenuto int not null primary key identity(1,1),
     Descrizione nvarchar(512) not null unique);

create table TipoMuseo (
     idTipoMuseo int not null primary key identity(1,1),
     Descrizione nvarchar(512) not null unique);

create table TipoPersonale (
     idTipoPersonale int not null primary key identity(1,1),
     Descrizione nvarchar(512) not null unique);

create table TipoSezione (
     idTipoSezione int not null primary key identity(1,1),
     Descrizione nvarchar(512) not null unique);


-- Constraints Section
-- SQLINES DEMO *** __ 

alter table Biglietto add constraint REF_Bigli_Museo_FK
     foreign key (idMuseo)
     references Museo (idMuseo);

alter table Biglietto add constraint REF_Bigli_TipoB_FK
     foreign key (idTipoBiglietto)
     references TipoBiglietto (idTipoBiglietto) on delete cascade;

alter table CalendarioApertureSpeciali add constraint REF_Calen_Museo_1_FK
     foreign key (idMuseo)
     references Museo (idMuseo) on delete cascade;

alter table CalendarioChiusure add constraint REF_Calen_Museo_FK
     foreign key (idMuseo)
     references Museo (idMuseo) on delete cascade;

alter table Contenuto add constraint EQU_Conte_Prove_FK
     foreign key (idProvenienza)
     references Provenienza (idProvenienza);

alter table Contenuto add constraint REF_Conte_Perio_FK
     foreign key (idPeriodoStorico)
     references PeriodoStorico (idPeriodoStorico);

alter table Contenuto add constraint REF_Conte_Sezio_FK
     foreign key (idSezione)
     references Sezione (idSezione) on delete cascade;

alter table Contenuto_Tipologia add constraint REF_Conte_TipoC_FK
     foreign key (idTipoContenuto)
     references TipoContenuto (idTipoContenuto) on delete cascade;

alter table Contenuto_Tipologia add constraint EQU_Conte_Conte
     foreign key (idContenuto)
     references Contenuto (idContenuto) on delete cascade;

alter table Creato add constraint EQU_Creat_Creat_FK
     foreign key (idCreatore)
     references Creatore (idCreatore) on delete cascade;

alter table Creato add constraint REF_Creat_Conte
     foreign key (idContenuto)
     references Contenuto (idContenuto);

alter table Museo add constraint EQU_Museo_Famig_FK
     foreign key (idFamiglia)
     references FamigliaMusei (idFamiglia) on delete cascade;

alter table Museo_Creatore add constraint REF_Museo_Museo_2_FK
     foreign key (idMuseo)
     references Museo (idMuseo) on delete cascade;

alter table Museo_Creatore add constraint EQU_Museo_Creat
     foreign key (idCreatore)
     references Creatore (idCreatore) on delete cascade;

alter table Museo_PeriodoStorico add constraint EQU_Museo_Perio
     foreign key (idPeriodoStorico)
     references PeriodoStorico (idPeriodoStorico) on delete cascade;

alter table Museo_PeriodoStorico add constraint REF_Museo_Museo_1_FK
     foreign key (idMuseo)
     references Museo (idMuseo) on delete cascade;

alter table Museo_Provenienza add constraint EQU_Museo_Prove
     foreign key (idProvenienza)
     references Provenienza (idProvenienza) on delete cascade; 

alter table Museo_Provenienza add constraint REF_Museo_Museo_FK
     foreign key (idMuseo)
     references Museo (idMuseo) on delete cascade;

alter table Museo_Tipologia add constraint REF_Museo_TipoM
     foreign key (idTipoMuseo)
     references TipoMuseo (idTipoMuseo) on delete cascade;

alter table Museo_Tipologia add constraint EQU_Museo_Museo_FK
     foreign key (idMuseo)
     references Museo (idMuseo) on delete cascade;

alter table Personale add constraint EQU_Perso_Museo_FK
     foreign key (idMuseo)
     references Museo (idMuseo) on delete cascade;

alter table Personale_Tipologia add constraint REF_Perso_TipoP_FK
     foreign key (idTipoPersonale)
     references TipoPersonale (idTipoPersonale) on delete cascade;

alter table Personale_Tipologia add constraint EQU_Perso_Perso
     foreign key (idPersonale)
     references Personale (idPersonale) on delete cascade;

alter table RegistroManutenzioni add constraint SID_Regis_Museo_FK
     foreign key (idMuseo)
     references Museo (idMuseo) on delete cascade;

alter table RegistroPresenze add constraint SID_Regis_Perso_FK
     foreign key (idPersonale)
     references Personale (idPersonale) on delete cascade;

alter table Sezione add constraint REF_Sezio_Sezio_FK
     foreign key (idSezionePadre)
     references Sezione (idSezione);

alter table Sezione add constraint EQU_Sezio_Museo_FK
     foreign key (idMuseo)
     references Museo (idMuseo) on delete cascade;

alter table Sezione_Tipologia add constraint REF_Sezio_TipoS
     foreign key (idTipoSezione)
     references TipoSezione (idTipoSezione) on delete cascade;

alter table Sezione_Tipologia add constraint EQU_Sezio_Sezio_FK
     foreign key (idSezione)
     references Sezione (idSezione) on delete cascade;

alter table StatisticheFamigliaMusei add constraint ID_Stati_Stati_1_FK
     foreign key (idStatistiche)
     references Statistiche (idStatistiche) on delete cascade;

alter table StatisticheFamigliaMusei add constraint REF_Stati_Famig_FK
     foreign key (idFamiglia)
     references FamigliaMusei (idFamiglia) on delete cascade;

alter table StatisticheMuseo add constraint ID_Stati_Stati_FK
     foreign key (idStatistiche)
     references Statistiche (idStatistiche) on delete cascade;

alter table StatisticheMuseo add constraint REF_Stati_Museo_FK
     foreign key (idMuseo)
     references Museo (idMuseo) on delete cascade;

alter table TipoBiglietto add constraint EQU_TipoB_Museo_FK
     foreign key (idMuseo)
     references Museo (idMuseo) on delete cascade;

