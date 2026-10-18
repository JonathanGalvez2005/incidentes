CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [fecha] DATE NULL, 
    [tipo] CHAR(50) NULL, 
    [lugar] CHAR(200) NULL, 
    [descripcion ] CHAR(500) NULL, 
    [estado] CHAR(20) NULL
)
