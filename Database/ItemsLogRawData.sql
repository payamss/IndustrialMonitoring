﻿CREATE TABLE [dbo].[ItemsLogRawData]
(
	[ItemLogRawDataId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ItemId] INT NOT NULL, 
    [Value] NVARCHAR(50) NOT NULL, 
    [Time] DATETIME2 NOT NULL, 
    [TimeStamp] TIMESTAMP NOT NULL, 
    [OutlierId] INT NULL, 
    CONSTRAINT [FK_ItemsLogRawData_Items] FOREIGN KEY ([ItemId]) REFERENCES [Items]([ItemId]), 
    CONSTRAINT [FK_ItemsLogRawData_LogOutleirs] FOREIGN KEY ([OutlierId]) REFERENCES [LogOutliers]([OutlierId]), 
)
