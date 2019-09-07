use master
if exists(select null from sys.databases where name='Quiz')
	drop database Quiz
go
create database Quiz
go
use Quiz

-- se houver login do QUIZ:
create table tblUsuario (
	IdUsuario int identity(1, 1) primary key,
	NomeUsuario varchar(25),
	Senha char(64)
)

-- Equipes e Integrantes
create table tblEquipe(
	IdEquipe int identity(1, 1) primary key,
	NomeEquipe varchar(25),
	CorEquipe varchar(8)
)

create table tblIntegrante(
	--IdIntegrante int primary key, -- RM, senão inútil
	IdEquipe int foreign key references tblEquipe,
	NomeIntegrante varchar (20)
)

-- Perguntas e Respostas
create table tblPergunta(
	IdPergunta int identity(1, 1) primary key,
	Imagem varbinary(MAX),
	TopQuiz bit,
	Pontuacao int
)

create table tblResposta(
	IdPergunta int foreign key references tblPergunta,
	Texto varchar(48),
	Correta bit
)

insert into tblEquipe values ('Equipe 01', '#75faf1')
insert into tblIntegrante values (1, 'Integrante 01')
insert into tblIntegrante values (1, 'Integrante 02')
insert into tblIntegrante values (1, 'Integrante 03')
insert into tblIntegrante values (1, 'Integrante 04')
insert into tblIntegrante values (1, 'Integrante 05')


insert into tblEquipe values ('Equipe 02', '#faa175')
insert into tblIntegrante values (2, 'Integrante 01')
insert into tblIntegrante values (2, 'Integrante 02')
insert into tblIntegrante values (2, 'Integrante 03')
insert into tblIntegrante values (2, 'Integrante 04')
insert into tblIntegrante values (2, 'Integrante 05')

insert into tblEquipe values ('Equipe 03', '#fcfa60')
insert into tblIntegrante values (3, 'Integrante 01')
insert into tblIntegrante values (3, 'Integrante 02')
insert into tblIntegrante values (3, 'Integrante 03')
insert into tblIntegrante values (3, 'Integrante 04')
insert into tblIntegrante values (3, 'Integrante 05')

insert into tblEquipe values ('Equipe 04', '#92f571')
insert into tblIntegrante values (4, 'Integrante 01')
insert into tblIntegrante values (4, 'Integrante 02')
insert into tblIntegrante values (4, 'Integrante 03')
insert into tblIntegrante values (4, 'Integrante 04')
insert into tblIntegrante values (4, 'Integrante 05')

insert into tblEquipe values ('Equipe 05', '#bb87fa')
insert into tblIntegrante values (5, 'Integrante 01')
insert into tblIntegrante values (5, 'Integrante 02')
insert into tblIntegrante values (5, 'Integrante 03')
insert into tblIntegrante values (5, 'Integrante 04')
insert into tblIntegrante values (5, 'Integrante 05')

insert into tblEquipe values ('Equipe 06', '#f67efc')
insert into tblIntegrante values (6, 'Integrante 01')
insert into tblIntegrante values (6, 'Integrante 02')
insert into tblIntegrante values (6, 'Integrante 03')
insert into tblIntegrante values (6, 'Integrante 04')
insert into tblIntegrante values (6, 'Integrante 05')

insert into tblEquipe values ('Equipe 07', '#fc7e9e')
insert into tblIntegrante values (7, 'Integrante 01')
insert into tblIntegrante values (7, 'Integrante 02')
insert into tblIntegrante values (7, 'Integrante 03')
insert into tblIntegrante values (7, 'Integrante 04')
insert into tblIntegrante values (7, 'Integrante 05')

insert into tblEquipe values ('Equipe 08', '#f2463d')
insert into tblIntegrante values (8, 'Stop')
insert into tblIntegrante values (8, 'Integrante 02')
insert into tblIntegrante values (8, 'Integrante 03')
insert into tblIntegrante values (8, 'Integrante 04')
insert into tblIntegrante values (8, 'Integrante 05')

insert into tblEquipe values ('Equipe 09', '#09abe6')
insert into tblIntegrante values (9, 'Integrante 01')
insert into tblIntegrante values (9, 'Integrante 02')
insert into tblIntegrante values (9, 'Integrante 03')
insert into tblIntegrante values (9, 'Integrante 04')
insert into tblIntegrante values (9, 'Integrante 05')

insert into tblEquipe values ('Equipe 10', '#adf041')
insert into tblIntegrante values (10, 'Integrante 01')
insert into tblIntegrante values (10, 'Integrante 02')
insert into tblIntegrante values (10, 'Integrante 03')
insert into tblIntegrante values (10, 'Integrante 04')
insert into tblIntegrante values (10, 'Integrante 05')

insert into tblEquipe values ('Equipe 11', '#1bc492')
insert into tblIntegrante values (11, 'Integrante 01')
insert into tblIntegrante values (11, 'Integrante 02')
insert into tblIntegrante values (11, 'Integrante 03')
insert into tblIntegrante values (11, 'Integrante 04')
insert into tblIntegrante values (11, 'Integrante 05')

insert into tblEquipe values ('Equipe 12', '#c98f38')
insert into tblIntegrante values (12, 'Integrante 01')
insert into tblIntegrante values (12, 'Integrante 02')
insert into tblIntegrante values (12, 'Integrante 03')
insert into tblIntegrante values (12, 'Integrante 04')
insert into tblIntegrante values (12, 'Integrante 05')

insert into tblEquipe values ('Equipe 13', '#5e8c6a')
insert into tblIntegrante values (13, 'Integrante 01')
insert into tblIntegrante values (13, 'Integrante 02')
insert into tblIntegrante values (13, 'Integrante 03')
insert into tblIntegrante values (13, 'Integrante 04')
insert into tblIntegrante values (13, 'Integrante 05')

insert into tblEquipe values ('Equipe 14', '#adaba8')
insert into tblIntegrante values (14, 'Integrante 01')
insert into tblIntegrante values (14, 'Integrante 02')
insert into tblIntegrante values (14, 'Integrante 03')
insert into tblIntegrante values (14, 'Integrante 04')
insert into tblIntegrante values (14, 'Integrante 05')

insert into tblEquipe values ('Equipe 15', '#875656') 
insert into tblIntegrante values (15, 'Snas')
insert into tblIntegrante values (15, 'Integrante 02')
insert into tblIntegrante values (15, 'Integrante 03')
insert into tblIntegrante values (15, 'Integrante 04')
insert into tblIntegrante values (15, 'Integrante 05')







