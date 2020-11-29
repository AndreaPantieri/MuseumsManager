-- *********************************************
-- * Standard SQL generation                   
-- *--------------------------------------------
-- * DB-MAIN version: 11.0.1              
-- * Generator date: Dec  4 2018              
-- * Generation date: Sun Nov 29 12:36:38 2020 
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
     constraint ID_Biglietto_ID primary key (idBiglietto));

create table CalendarioApertureSpeciali (
     idCalendarioApertureSpeciali numeric(1) not null,
     Data date not null,
     OrarioApertura date not null,
     OrarioChiusura date not null,
     NumBigliettiMax char(1) not null,
     idMuseo numeric(1) not null,
     constraint ID_CalendarioApertureSpeciali_ID primary key (idCalendarioApertureSpeciali));

create table CalendarioChiusure (
     idCalendarioChiusure numeric(1) not null,
     Data date not null,
     idMuseo numeric(1) not null,
     constraint ID_CalendarioChiusure_ID primary key (idCalendarioChiusure));

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
     constraint ID_Contenuto_ID primary key (idContenuto));

create table Contenuto_Tipologia (
     idContenuto numeric(1) not null,
     idTipoContenuto numeric(1) not null,
     constraint ID_Contenuto_Tipologia_ID primary key (idContenuto, idTipoContenuto));

create table Creato (
     idContenuto numeric(1) not null,
     idCreatore numeric(1) not null,
     constraint ID_Creato_ID primary key (idContenuto, idCreatore));

create table Creatore (
     idCreatore numeric(1) not null,
     Nome varchar(150) not null,
     Cognome varchar(150) not null,
     Descrizione varchar(150) not null,
     AnnoNascita numeric(1) not null,
     constraint ID_Creatore_ID primary key (idCreatore));

create table FamigliaMusei (
     idFamiglia char(1) not null,
     Nome char(1) not null,
     constraint ID_FamigliaMusei_ID primary key (idFamiglia));

create table Museo (
     idMuseo numeric(1) not null,
     Nome char(1) not null,
     Luogo char(1) not null,
     OrarioAperturaGenerale date not null,
     OrarioChiusuraGenerale date not null,
     NumBigliettiMaxGenerale numeric(1) not null,
     idFamiglia char(1),
     constraint ID_Museo_ID primary key (idMuseo));

create table Museo_Creatore (
     idCreatore numeric(1) not null,
     idMuseo numeric(1) not null,
     constraint ID_Museo_Creatore_ID primary key (idCreatore, idMuseo));

create table Museo_PeriodoStorico (
     idMuseo numeric(1) not null,
     idPeriodoStorico numeric(1) not null,
     constraint ID_Museo_PeriodoStorico_ID primary key (idPeriodoStorico, idMuseo));

create table Museo_Provenienza (
     idMuseo numeric(1) not null,
     idProvenienza numeric(1) not null,
     constraint ID_Museo_Provenienza_ID primary key (idProvenienza, idMuseo));

create table Museo_Tipologia (
     idMuseo numeric(1) not null,
     idTipoMuseo numeric(1) not null,
     constraint ID_Museo_Tipologia_ID primary key (idTipoMuseo, idMuseo));

create table PeriodoStorico (
     idPeriodoStorico numeric(1) not null,
     Nome varchar(150) not null,
     Descrizione varchar(150) not null,
     AnnoInizio numeric(1) not null,
     AnnoFine char(1) not null,
     constraint ID_PeriodoStorico_ID primary key (idPeriodoStorico));

create table Personale (
     idPersonale numeric(1) not null,
     Nome char(1) not null,
     Cognome char(1) not null,
     Cellulare char(1) not null,
     Email char(1) not null,
     StipendioOra numeric(1) not null,
     idMuseo numeric(1),
     constraint ID_Personale_ID primary key (idPersonale));

create table Personale_Tipologia (
     idPersonale numeric(1) not null,
     idTipoPersonale numeric(1) not null,
     constraint ID_Personale_Tipologia_ID primary key (idPersonale, idTipoPersonale));

create table Provenienza (
     idProvenienza numeric(1) not null,
     Nome varchar(150) not null,
     Descrizione varchar(150) not null,
     constraint ID_Provenienza_ID primary key (idProvenienza));

create table RegistroManutenzioni (
     idManutenzione numeric(1) not null,
     idMuseo numeric(1) not null,
     Data date not null,
     Descrizione varchar(1) not null,
     Prezzo numeric(1) not null,
     constraint ID_RegistroManutenzioni_ID primary key (idManutenzione),
     constraint FKRegistareManutenzione_ID unique (idMuseo));

create table RegistroPresenze (
     idRegistro numeric(1) not null,
     idPersonale numeric(1) not null,
     DataEntrata date not null,
     DataUscita date not null,
     constraint ID_RegistroPresenze_ID primary key (idRegistro),
     constraint FKCompila_ID unique (idPersonale));

create table Sezione (
     idSezione numeric(1) not null,
     Nome char(1) not null,
     Descrizione char(1) not null,
     idSezionePadre numeric(1) not null,
     idMuseo numeric(1) not null,
     constraint ID_Sezione_ID primary key (idSezione));

create table Sezione_Tipologia (
     idSezione numeric(1) not null,
     idTipoSezione numeric(1) not null,
     constraint ID_Sezione_Tipologia_ID primary key (idTipoSezione, idSezione));

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
     constraint ID_Statistiche_ID primary key (idStatistiche));

create table StatisticheFamigliaMusei (
     idStatistiche numeric(1) not null,
     idFamiglia char(1) not null,
     constraint FKStatistiche_FamigliaMuseo_ID primary key (idStatistiche));

create table StatisticheMuseo (
     idStatistiche numeric(1) not null,
     idMuseo numeric(1) not null,
     constraint FKStatistiche_Museo_ID primary key (idStatistiche));

create table TipoBiglietto (
     idTipoBiglietto numeric(1) not null,
     Prezzo numeric(1) not null,
     Descrizione char(1) not null,
     idMuseo numeric(1) not null,
     constraint ID_TipoBiglietto_ID primary key (idTipoBiglietto));

create table TipoContenuto (
     idTipoContenuto numeric(1) not null,
     Descrizione char(1) not null,
     constraint ID_TipoContenuto_ID primary key (idTipoContenuto));

create table TipoMuseo (
     idTipoMuseo numeric(1) not null,
     Descrizione char(1) not null,
     constraint ID_TipoMuseo_ID primary key (idTipoMuseo));

create table TipoPersonale (
     idTipoPersonale numeric(1) not null,
     Descrizione char(1) not null,
     constraint ID_TipoPersonale_ID primary key (idTipoPersonale));

create table TipoSezione (
     idTipoSezione numeric(1) not null,
     Descrizione char(1) not null,
     constraint ID_TipoSezione_ID primary key (idTipoSezione));


-- Constraints Section
-- ___________________ 

alter table Biglietto add constraint FKVenduto_FK
     foreign key (idMuseo)
     references Museo;

alter table Biglietto add constraint FKBiglietto_Tipologia_FK
     foreign key (idTipoBiglietto)
     references TipoBiglietto;

alter table CalendarioApertureSpeciali add constraint FKAperto_FK
     foreign key (idMuseo)
     references Museo;

alter table CalendarioChiusure add constraint FKChiuso_FK
     foreign key (idMuseo)
     references Museo;

alter table Contenuto add constraint ID_Contenuto_CHK
     check(exists(select * from Contenuto_Tipologia
                  where Contenuto_Tipologia.idContenuto = idContenuto)); 

alter table Contenuto add constraint FKSottocontenuto_FK
     foreign key (idContenutoPadre)
     references Contenuto;

alter table Contenuto add constraint FKProviene_FK
     foreign key (idProvenienza)
     references Provenienza;

alter table Contenuto add constraint FKPeriodo_FK
     foreign key (idPeriodoStorico)
     references PeriodoStorico;

alter table Contenuto add constraint FKContiene_FK
     foreign key (idSezione)
     references Sezione;

alter table Contenuto_Tipologia add constraint FKCon_Tip_FK
     foreign key (idTipoContenuto)
     references TipoContenuto;

alter table Contenuto_Tipologia add constraint FKCon_Con
     foreign key (idContenuto)
     references Contenuto;

alter table Creato add constraint FKCre_Cre_FK
     foreign key (idCreatore)
     references Creatore;

alter table Creato add constraint FKCre_Con
     foreign key (idContenuto)
     references Contenuto;

alter table Creatore add constraint ID_Creatore_CHK
     check(exists(select * from Creato
                  where Creato.idCreatore = idCreatore)); 

alter table Creatore add constraint ID_Creatore_CHK
     check(exists(select * from Museo_Creatore
                  where Museo_Creatore.idCreatore = idCreatore)); 

alter table FamigliaMusei add constraint ID_FamigliaMusei_CHK
     check(exists(select * from Museo
                  where Museo.idFamiglia = idFamiglia)); 

alter table Museo add constraint ID_Museo_CHK
     check(exists(select * from Sezione
                  where Sezione.idMuseo = idMuseo)); 

alter table Museo add constraint ID_Museo_CHK
     check(exists(select * from Personale
                  where Personale.idMuseo = idMuseo)); 

alter table Museo add constraint ID_Museo_CHK
     check(exists(select * from Museo_Tipologia
                  where Museo_Tipologia.idMuseo = idMuseo)); 

alter table Museo add constraint ID_Museo_CHK
     check(exists(select * from TipoBiglietto
                  where TipoBiglietto.idMuseo = idMuseo)); 

alter table Museo add constraint FKMuseo_FamigliaMusei_FK
     foreign key (idFamiglia)
     references FamigliaMusei;

alter table Museo_Creatore add constraint FKMus_Mus_1_FK
     foreign key (idMuseo)
     references Museo;

alter table Museo_Creatore add constraint FKMus_Cre
     foreign key (idCreatore)
     references Creatore;

alter table Museo_PeriodoStorico add constraint FKMus_Per
     foreign key (idPeriodoStorico)
     references PeriodoStorico;

alter table Museo_PeriodoStorico add constraint FKMus_Mus_FK
     foreign key (idMuseo)
     references Museo;

alter table Museo_Provenienza add constraint FKMus_Pro
     foreign key (idProvenienza)
     references Provenienza;

alter table Museo_Provenienza add constraint FKMus_Mus_2_FK
     foreign key (idMuseo)
     references Museo;

alter table Museo_Tipologia add constraint FKMus_Tip
     foreign key (idTipoMuseo)
     references TipoMuseo;

alter table Museo_Tipologia add constraint FKMus_Mus_3_FK
     foreign key (idMuseo)
     references Museo;

alter table PeriodoStorico add constraint ID_PeriodoStorico_CHK
     check(exists(select * from Museo_PeriodoStorico
                  where Museo_PeriodoStorico.idPeriodoStorico = idPeriodoStorico)); 

alter table Personale add constraint ID_Personale_CHK
     check(exists(select * from Personale_Tipologia
                  where Personale_Tipologia.idPersonale = idPersonale)); 

alter table Personale add constraint FKLavorano_FK
     foreign key (idMuseo)
     references Museo;

alter table Personale_Tipologia add constraint FKPer_Tip_FK
     foreign key (idTipoPersonale)
     references TipoPersonale;

alter table Personale_Tipologia add constraint FKPer_Per
     foreign key (idPersonale)
     references Personale;

alter table Provenienza add constraint ID_Provenienza_CHK
     check(exists(select * from Museo_Provenienza
                  where Museo_Provenienza.idProvenienza = idProvenienza)); 

alter table Provenienza add constraint ID_Provenienza_CHK
     check(exists(select * from Contenuto
                  where Contenuto.idProvenienza = idProvenienza)); 

alter table RegistroManutenzioni add constraint FKRegistareManutenzione_FK
     foreign key (idMuseo)
     references Museo;

alter table RegistroPresenze add constraint FKCompila_FK
     foreign key (idPersonale)
     references Personale;

alter table Sezione add constraint ID_Sezione_CHK
     check(exists(select * from Sezione_Tipologia
                  where Sezione_Tipologia.idSezione = idSezione)); 

alter table Sezione add constraint FKSottosezione_FK
     foreign key (idSezionePadre)
     references Sezione;

alter table Sezione add constraint FKDiviso_FK
     foreign key (idMuseo)
     references Museo;

alter table Sezione_Tipologia add constraint FKSez_Tip
     foreign key (idTipoSezione)
     references TipoSezione;

alter table Sezione_Tipologia add constraint FKSez_Sez_FK
     foreign key (idSezione)
     references Sezione;

alter table StatisticheFamigliaMusei add constraint FKStatistiche_FamigliaMuseo_FK
     foreign key (idStatistiche)
     references Statistiche;

alter table StatisticheFamigliaMusei add constraint FKFamigliaMusei_Statistiche_FK
     foreign key (idFamiglia)
     references FamigliaMusei;

alter table StatisticheMuseo add constraint FKStatistiche_Museo_FK
     foreign key (idStatistiche)
     references Statistiche;

alter table StatisticheMuseo add constraint FKMuseo_Statistiche_FK
     foreign key (idMuseo)
     references Museo;

alter table TipoBiglietto add constraint FKMuseo_TipoBiglietto_FK
     foreign key (idMuseo)
     references Museo;


-- Index Section
-- _____________ 

create unique index ID_Biglietto_IND
     on Biglietto (idBiglietto);

create index FKVenduto_IND
     on Biglietto (idMuseo);

create index FKBiglietto_Tipologia_IND
     on Biglietto (idTipoBiglietto);

create unique index ID_CalendarioApertureSpeciali_IND
     on CalendarioApertureSpeciali (idCalendarioApertureSpeciali);

create index FKAperto_IND
     on CalendarioApertureSpeciali (idMuseo);

create unique index ID_CalendarioChiusure_IND
     on CalendarioChiusure (idCalendarioChiusure);

create index FKChiuso_IND
     on CalendarioChiusure (idMuseo);

create unique index ID_Contenuto_IND
     on Contenuto (idContenuto);

create index FKSottocontenuto_IND
     on Contenuto (idContenutoPadre);

create index FKProviene_IND
     on Contenuto (idProvenienza);

create index FKPeriodo_IND
     on Contenuto (idPeriodoStorico);

create index FKContiene_IND
     on Contenuto (idSezione);

create unique index ID_Contenuto_Tipologia_IND
     on Contenuto_Tipologia (idContenuto, idTipoContenuto);

create index FKCon_Tip_IND
     on Contenuto_Tipologia (idTipoContenuto);

create unique index ID_Creato_IND
     on Creato (idContenuto, idCreatore);

create index FKCre_Cre_IND
     on Creato (idCreatore);

create unique index ID_Creatore_IND
     on Creatore (idCreatore);

create unique index ID_FamigliaMusei_IND
     on FamigliaMusei (idFamiglia);

create unique index ID_Museo_IND
     on Museo (idMuseo);

create index FKMuseo_FamigliaMusei_IND
     on Museo (idFamiglia);

create unique index ID_Museo_Creatore_IND
     on Museo_Creatore (idCreatore, idMuseo);

create index FKMus_Mus_1_IND
     on Museo_Creatore (idMuseo);

create unique index ID_Museo_PeriodoStorico_IND
     on Museo_PeriodoStorico (idPeriodoStorico, idMuseo);

create index FKMus_Mus_IND
     on Museo_PeriodoStorico (idMuseo);

create unique index ID_Museo_Provenienza_IND
     on Museo_Provenienza (idProvenienza, idMuseo);

create index FKMus_Mus_2_IND
     on Museo_Provenienza (idMuseo);

create unique index ID_Museo_Tipologia_IND
     on Museo_Tipologia (idTipoMuseo, idMuseo);

create index FKMus_Mus_3_IND
     on Museo_Tipologia (idMuseo);

create unique index ID_PeriodoStorico_IND
     on PeriodoStorico (idPeriodoStorico);

create unique index ID_Personale_IND
     on Personale (idPersonale);

create index FKLavorano_IND
     on Personale (idMuseo);

create unique index ID_Personale_Tipologia_IND
     on Personale_Tipologia (idPersonale, idTipoPersonale);

create index FKPer_Tip_IND
     on Personale_Tipologia (idTipoPersonale);

create unique index ID_Provenienza_IND
     on Provenienza (idProvenienza);

create unique index ID_RegistroManutenzioni_IND
     on RegistroManutenzioni (idManutenzione);

create unique index FKRegistareManutenzione_IND
     on RegistroManutenzioni (idMuseo);

create unique index ID_RegistroPresenze_IND
     on RegistroPresenze (idRegistro);

create unique index FKCompila_IND
     on RegistroPresenze (idPersonale);

create unique index ID_Sezione_IND
     on Sezione (idSezione);

create index FKSottosezione_IND
     on Sezione (idSezionePadre);

create index FKDiviso_IND
     on Sezione (idMuseo);

create unique index ID_Sezione_Tipologia_IND
     on Sezione_Tipologia (idTipoSezione, idSezione);

create index FKSez_Sez_IND
     on Sezione_Tipologia (idSezione);

create unique index ID_Statistiche_IND
     on Statistiche (idStatistiche);

create unique index FKStatistiche_FamigliaMuseo_IND
     on StatisticheFamigliaMusei (idStatistiche);

create index FKFamigliaMusei_Statistiche_IND
     on StatisticheFamigliaMusei (idFamiglia);

create unique index FKStatistiche_Museo_IND
     on StatisticheMuseo (idStatistiche);

create index FKMuseo_Statistiche_IND
     on StatisticheMuseo (idMuseo);

create unique index ID_TipoBiglietto_IND
     on TipoBiglietto (idTipoBiglietto);

create index FKMuseo_TipoBiglietto_IND
     on TipoBiglietto (idMuseo);

create unique index ID_TipoContenuto_IND
     on TipoContenuto (idTipoContenuto);

create unique index ID_TipoMuseo_IND
     on TipoMuseo (idTipoMuseo);

create unique index ID_TipoPersonale_IND
     on TipoPersonale (idTipoPersonale);

create unique index ID_TipoSezione_IND
     on TipoSezione (idTipoSezione);

