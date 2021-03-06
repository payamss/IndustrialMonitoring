﻿CREATE TABLE [dbo].[Items]
(
	[ItemId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ItemName] NVARCHAR(50) NOT NULL, 
    [ItemType] INT NOT NULL, 
    [Location] NVARCHAR(MAX) NULL, 
    [SaveInItemsLogTimeInterval] INT NOT NULL DEFAULT 5, 
    [SaveInItemsLogLastesTimeInterval] INT NOT NULL DEFAULT 60, 
    [ShowInUITimeInterval] INT NOT NULL DEFAULT 5, 
    [ScanCycle] INT NOT NULL DEFAULT 100, 
    [SaveInItemsLogWhen] INT NOT NULL DEFAULT 1, 
    [SaveInItemsLogLastWhen] INT NOT NULL DEFAULT 1, 
    [DefinationType] INT NULL DEFAULT 1, 
    [Unit] NVARCHAR(50) NULL, 
    [Order] INT NULL, 
    [BACnetIP] NVARCHAR(MAX) NULL, 
    [BACnetPort] INT NULL, 
    [BACnetControllerInstance] INT NULL, 
    [BACnetItemInstance] INT NULL, 
    [BACnetItemType] INT NULL, 
    [MinRange] NVARCHAR(50) NULL, 
    [MaxRange] NVARCHAR(50) NULL, 
	[NormalizeWhenOutOfRange] BIT NULL DEFAULT 0, 
    [ThreadGroupId] INT NULL, 
    [NumberOfDataForBoxplot] INT NULL DEFAULT 0, 
    [InOut] INT NULL, 
    [ReceiveDelayedNotificationInTelegram] BIT NULL, 
    CONSTRAINT [FK_Items_ThreadGroup] FOREIGN KEY ([ThreadGroupId]) REFERENCES [ThreadGroup]([ThreadGroupId])
)
