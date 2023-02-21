CREATE TABLE [dbo].[Ingredients] (
    [Id]          UNIQUEIDENTIFIER   NOT NULL,
    [Name]        NVARCHAR (1024)    NOT NULL,
    [Description] NVARCHAR (2048)    NULL,
    [CreatedDate] DATETIMEOFFSET (7) DEFAULT (getutcdate()) NOT NULL,
    [UpdatedDate] DATETIMEOFFSET (7) DEFAULT (getutcdate()) NOT NULL,
    [DeletedDate] DATETIMEOFFSET (7) NULL,
    [IsDeleted]   BIT                DEFAULT ((0)) NOT NULL,
    [TimeStamp]   ROWVERSION         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

