CREATE TABLE [dbo].[Customers] (
    [Id]          UNIQUEIDENTIFIER   NOT NULL,
    [LoginName]   NVARCHAR (256)     NOT NULL,
    [Phone]       NVARCHAR (100)     NULL,
    [Address]     NVARCHAR (100)     NULL,
    [CreatedDate] DATETIMEOFFSET (7) DEFAULT (getutcdate()) NOT NULL,
    [UpdatedDate] DATETIMEOFFSET (7) DEFAULT (getutcdate()) NOT NULL,
    [DeletedDate] DATETIMEOFFSET (7) NULL,
    [IsDeleted]   BIT                DEFAULT ((0)) NOT NULL,
    [TimeStamp]   ROWVERSION         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_Customes_LoginName] UNIQUE NONCLUSTERED ([LoginName] ASC)
);

