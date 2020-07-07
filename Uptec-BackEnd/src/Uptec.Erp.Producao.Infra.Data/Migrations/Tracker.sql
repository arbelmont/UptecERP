CREATE TABLE [Trackers] (
    [Id] uniqueidentifier NOT NULL,
	[Date] smalldatetime NOT NULL,
	[Resume] varchar(100) NOT NULL,
    [Trace] varchar(max),
    
    CONSTRAINT [PK_Trackers] PRIMARY KEY ([Id])
);

GO