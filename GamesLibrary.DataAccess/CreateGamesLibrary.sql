create database GamesLibrary
go

use GamesLibrary

create table Companies(
	Id int primary key identity,
	[Name] nvarchar(100) not null unique
)

create unique nonclustered index IX_Companies_Name on Companies([Name] asc)

create table Games(
	Id int primary key identity,
	[Name] nvarchar(100) not null,
	CompanyNameId int foreign key references Companies(Id),
)

create unique nonclustered index IX_Games_Name on Games([Name] asc)

create table Genres(
	Id int primary key identity,
	[Name] nvarchar(100) not null unique
)

create unique nonclustered index IX_Genres_Name on Genres([Name] asc)

create table GamesMappingGenres(
	Id int primary key identity,
	GamesId int foreign key references Games(Id),
	GenresId int foreign key references Genres(Id),
)

create nonclustered index IX_GamesMappingGenres_GamesId on GamesMappingGenres(GamesId asc) include (GenresId)
create nonclustered index IX_GamesMappingGenres_GenresId on GamesMappingGenres(GenresId asc) include (GamesId)

go

insert into Companies([Name]) values
('Bethesda'), 
('EA'), 
('Valve')

insert into Genres([Name]) values
('rpg'),
('sport simulator'),
('econimic strategy'),
('open world')

insert into Games([Name], CompanyNameId) values
('Skyrim', 1),
('NFL 2020', 2),
('DOTA 2', 3)

insert into GamesMappingGenres(GamesId, GenresId) values
(1, 1),
(1, 4),
(2, 2),
(2, 3),
(3, 1),
(3, 3)
go

select * from Companies
select * from Genres
select * from Games
select * from GamesMappingGenres
