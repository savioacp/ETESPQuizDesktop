use master
if exists(select null from sys.databases where name='Quiz')
	drop database Quiz
go
create database Quiz
go
use Quiz

-- se houver login do QUIZ:
--create table tblUsuario (
--	IdUsuario int identity(1, 1) primary key,
--	NomeUsuario nvarchar(25),
--	Senha char(64)
--)

-- Equipes e Integrantes
create table tblEquipe(
	IdEquipe int identity(1, 1) primary key,
	NomeEquipe nvarchar(25),
	CorEquipe nvarchar(8)
)

create table tblIntegrante(
	--IdIntegrante int primary key, -- RM, senão inútil
	IdEquipe int foreign key references tblEquipe,
	NomeIntegrante nvarchar (20)
)

-- Perguntas e Respostas
create table tblPergunta(
	IdPergunta int identity(1, 1) primary key,
	Texto nvarchar(512),
	Imagem varbinary(MAX),
	TopQuiz bit
)

create table tblResposta(
	IdPergunta int foreign key references tblPergunta,
	Texto nvarchar(256),
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


insert into tblPergunta select 'Quem é o personagem Sans, de Undertale?', BulkColumn, '1' from openrowset(bulk N'E:\proyetitos\QuizV2\sans_thumbnail.png', SINGLE_BLOB) as img
--insert into tblPergunta values ('Quem é o personagem Sans, de Undertale?', null, '1')
insert into tblResposta values  (1, 'O Esqueleto', 0),
								(1, 'Um Esqueleto', 0),	
								(1, 'Ele', 1),
								(1, 'SnaS', 0)

insert into tblPergunta select 'Com base exclusivamente nos dados apresentados no gráfico quanto à cotação do dólar comercial no último dia útil de cada mês de 2015, assinale a alternativa correta.', BulkColumn, '1' from openrowset(bulk N'E:\proyetitos\QuizV2\GRAFICO000.png', SINGLE_BLOB) as img
insert into tblResposta values  (2, 'Em dezembro de 2014, a cotação do dólar comercial foi menor que 2,689', 0),
								(2, 'O maior valor para a cotação do dólar comercial foi verificado em 28 de setembro.', 0),
								(2, 'A função que representa o valor da cotação do dólar comercial em relação ao tempo é crescente, no intervalo apresentado no gráfico.', 0),
								(2, 'A diferença entre os valores da cotação do dólar comercial de maio e de março foi menor que um centavo de real.', 1)

select * from tblEquipe
select * from tblIntegrante where IdEquipe=1

select * from tblPergunta 
select * from tblResposta where IdPergunta=1

