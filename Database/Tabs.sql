﻿CREATE TABLE [dbo].[Tabs]
(
	[TabId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TabName] NVARCHAR(50) NOT NULL, 
    [Order] INT NULL
)
