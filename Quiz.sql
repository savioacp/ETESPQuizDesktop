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
	CorEquipe nvarchar(8),
	Token varchar(64)
)

create table tblIntegrante(
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

--create table tblDica (
--	IdPergunta int foreign key references tblPergunta,
--	Texto nvarchar(512)
--)

create table tblResposta(
	IdPergunta int foreign key references tblPergunta,
	Texto nvarchar(256),
	Correta bit
)

insert into tblEquipe values ('Equipe 01', '#abe2ff', '')
insert into tblIntegrante values (1, 'Integrante 01')
insert into tblIntegrante values (1, 'Integrante 02')
insert into tblIntegrante values (1, 'Integrante 03')
insert into tblIntegrante values (1, 'Integrante 04')
insert into tblIntegrante values (1, 'Integrante 05')


insert into tblEquipe values ('Equipe 02', '#ff7926', '')
insert into tblIntegrante values (2, 'Integrante 01')
insert into tblIntegrante values (2, 'Integrante 02')
insert into tblIntegrante values (2, 'Integrante 03')
insert into tblIntegrante values (2, 'Integrante 04')
insert into tblIntegrante values (2, 'Integrante 05')

insert into tblEquipe values ('Equipe 03', '#fcfa60', '')
insert into tblIntegrante values (3, 'Integrante 01')
insert into tblIntegrante values (3, 'Integrante 02')
insert into tblIntegrante values (3, 'Integrante 03')
insert into tblIntegrante values (3, 'Integrante 04')
insert into tblIntegrante values (3, 'Integrante 05')

insert into tblEquipe values ('Equipe 04', '#92f571', '')
insert into tblIntegrante values (4, 'Integrante 01')
insert into tblIntegrante values (4, 'Integrante 02')
insert into tblIntegrante values (4, 'Integrante 03')
insert into tblIntegrante values (4, 'Integrante 04')
insert into tblIntegrante values (4, 'Integrante 05')

insert into tblEquipe values ('Equipe 05', '#bb87fa', '')
insert into tblIntegrante values (5, 'Integrante 01')
insert into tblIntegrante values (5, 'Integrante 02')
insert into tblIntegrante values (5, 'Integrante 03')
insert into tblIntegrante values (5, 'Integrante 04')
insert into tblIntegrante values (5, 'Integrante 05')

insert into tblEquipe values ('Equipe 06', '#f67efc', '')
insert into tblIntegrante values (6, 'Integrante 01')
insert into tblIntegrante values (6, 'Integrante 02')
insert into tblIntegrante values (6, 'Integrante 03')
insert into tblIntegrante values (6, 'Integrante 04')
insert into tblIntegrante values (6, 'Integrante 05')

insert into tblEquipe values ('Equipe 07', '#ffc08c', '')
insert into tblIntegrante values (7, 'Integrante 01')
insert into tblIntegrante values (7, 'Integrante 02')
insert into tblIntegrante values (7, 'Integrante 03')
insert into tblIntegrante values (7, 'Integrante 04')
insert into tblIntegrante values (7, 'Integrante 05')

insert into tblEquipe values ('Equipe 08', '#ff0f0f', '')
insert into tblIntegrante values (8, 'Stop')
insert into tblIntegrante values (8, 'Integrante 02')
insert into tblIntegrante values (8, 'Integrante 03')
insert into tblIntegrante values (8, 'Integrante 04')
insert into tblIntegrante values (8, 'Integrante 05')

insert into tblEquipe values ('Equipe 09', '#2e46ff', '')
insert into tblIntegrante values (9, 'Integrante 01')
insert into tblIntegrante values (9, 'Integrante 02')
insert into tblIntegrante values (9, 'Integrante 03')
insert into tblIntegrante values (9, 'Integrante 04')
insert into tblIntegrante values (9, 'Integrante 05')

insert into tblEquipe values ('Equipe 10', '#00ffdc', '')
insert into tblIntegrante values (10, 'Integrante 01')
insert into tblIntegrante values (10, 'Integrante 02')
insert into tblIntegrante values (10, 'Integrante 03')
insert into tblIntegrante values (10, 'Integrante 04')
insert into tblIntegrante values (10, 'Integrante 05')

insert into tblEquipe values ('Equipe 11', '#1486ff', '')
insert into tblIntegrante values (11, 'Integrante 01')
insert into tblIntegrante values (11, 'Integrante 02')
insert into tblIntegrante values (11, 'Integrante 03')
insert into tblIntegrante values (11, 'Integrante 04')
insert into tblIntegrante values (11, 'Integrante 05')

insert into tblEquipe values ('Equipe 12', '#c98f38', '1234')
insert into tblIntegrante values (12, 'Integrante 01')
insert into tblIntegrante values (12, 'Integrante 02')
insert into tblIntegrante values (12, 'Integrante 03')
insert into tblIntegrante values (12, 'Integrante 04')
insert into tblIntegrante values (12, 'Integrante 05')

insert into tblEquipe values ('Equipe 13', '#5e8c6a', '123')
insert into tblIntegrante values (13, 'Integrante 01')
insert into tblIntegrante values (13, 'Integrante 02')
insert into tblIntegrante values (13, 'Integrante 03')
insert into tblIntegrante values (13, 'Integrante 04')
insert into tblIntegrante values (13, 'Integrante 05')

insert into tblEquipe values ('Equipe 14', '#adaba8', '')
insert into tblIntegrante values (14, 'Integrante 01')
insert into tblIntegrante values (14, 'Integrante 02')
insert into tblIntegrante values (14, 'Integrante 03')
insert into tblIntegrante values (14, 'Integrante 04')
insert into tblIntegrante values (14, 'Integrante 05')

insert into tblEquipe values ('Equipe 15', '#875656','') 
insert into tblIntegrante values (15, 'Snas')
insert into tblIntegrante values (15, 'Integrante 02')
insert into tblIntegrante values (15, 'Integrante 03')
insert into tblIntegrante values (15, 'Integrante 04')
insert into tblIntegrante values (15, 'Integrante 05')


insert into tblPergunta select 'Quem é o personagem apresentado na imagem?', BulkColumn, '1' from openrowset(bulk N'G:\proyetitos\QuizV2\sans_thumbnail.png', SINGLE_BLOB) as img
insert into tblResposta values  (1, 'O Esqueletóide, de Untale', 0),
								(1, 'O Esqueletomano, de Touhou', 0),	
								(1, 'Sans, de Undertale', 1),
								(1, 'Papyrus, de Deltarune', 0)

insert into tblPergunta select 'Com base exclusivamente nos dados apresentados no gráfico quanto à cotação do dólar comercial no último dia útil de cada mês de 2015, assinale a alternativa correta.', BulkColumn, '1' from openrowset(bulk N'G:\proyetitos\QuizV2\GRAFICO000.png', SINGLE_BLOB) as img
insert into tblResposta values  (2, 'Em dezembro de 2014, a cotação do dólar comercial foi menor que 2,689', 0),
								(2, 'O maior valor para a cotação do dólar comercial foi verificado em 28 de setembro.', 0),
								(2, 'A função que representa o valor da cotação do dólar comercial em relação ao tempo é crescente, no intervalo apresentado no gráfico.', 0),
								(2, 'A diferença entre os valores da cotação do dólar comercial de maio e de março foi menor que um centavo de real.', 1)

insert into tblPergunta values  ('De quem é a famosa frase “Penso, logo existo”?', NULL, '1')
insert into tblResposta values  (3, 'Platão', 0),
								(3, 'Descartes', 1),
								(3, 'Sócrates', 0),
								(3, 'Galileu Galilei', 0)

insert into tblPergunta values  ('Quais o menor e o maior país do mundo?', NULL, '1')
insert into tblResposta values  (4, 'Vaticano e Rússia', 1),
								(4, 'Mônaco e Canadá', 0),
								(4, 'Mônaco e Rússia', 0),
								(4, 'Vaticano e China', 0)

insert into tblPergunta values  ('O que a palavra legend significa em português?', NULL, '0')
insert into tblResposta values  (5, 'Legenda', 0),
								(5, 'Legendário', 0),
								(5, 'Lenda', 1),
								(5, 'Conto', 0)

insert into tblPergunta values  ('Quem pintou "Guernica"?', NULL, '0')
insert into tblResposta values  (6, 'Paul Cézanne', 0),
								(6, 'Diego Rivera', 0),
								(6, 'Pablo Picasso', 1),
								(6, 'Tarsila do Amaral', 0)

insert into tblPergunta values  ('Quais são os três predadores do reino animal reconhecidos pela habilidade de caçar em grupo, se camuflar para surpreender as presas e possuir sentidos apurados, respectivamente:', NULL, '0')
insert into tblResposta values  (7, 'Tubarão branco, crocodilo e sucuri', 0),
								(7, 'Hiena, urso branco e lobo cinzento', 1),
								(7, 'Tigre, gavião e orca', 0),
								(7, 'Leão, tubarão branco e urso cinzento', 0)

insert into tblPergunta values  ('Em qual local da Ásia o português é língua oficial?', NULL, '0')
insert into tblResposta values  (8, 'Índia', 0),
								(8, 'Moçambique', 0),
								(8, 'Portugal', 0),
								(8, 'Macau', 1)

select * from tblEquipe
select * from tblPergunta

 

