USE master;
GO

IF DB_ID (N'AeroAPI') IS NOT NULL
DROP DATABASE AeroAPI;
GO
CREATE DATABASE AeroAPI;
GO

----------------------------------------------------
USE AeroAPI;
GO

-- DROP TABLE ControleAcesso;
CREATE TABLE ControleAcesso
(
	ID int identity(1,1) not null,
	Login varchar(100) not null,
	Senha varchar(100) not null,
	TipoUsuario int,
	DataCriacao datetime,
	CONSTRAINT PK_ControleAcesso PRIMARY KEY (ID)
);

INSERT INTO dbo.ControleAcesso
           (Login
           ,Senha
           ,TipoUsuario
           ,DataCriacao)
     VALUES
           ('sistema'
           ,'123456'
           ,1
           ,GETDATE())
GO

INSERT INTO dbo.ControleAcesso
           (Login
           ,Senha
           ,TipoUsuario
           ,DataCriacao)
     VALUES
           ('usuario'
           ,'123456'
           ,2
           ,GETDATE())
GO



-- DROP TABLE Passageiro;
CREATE TABLE Passageiro
(
	ID int identity(1,1) not null,
	Nome nvarchar(50) not null,
	Idade integer,
	Celular nvarchar(20) not null,
	DataCriacao datetime,
	CONSTRAINT PK_Passageiro PRIMARY KEY (ID)
);
