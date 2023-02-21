CREATE TABLE [dbo].[Products] (
    [Id]          UNIQUEIDENTIFIER   NOT NULL,
    [Name]        NVARCHAR (1024)    NOT NULL,
    [Price]       MONEY              DEFAULT ((0)) NOT NULL,
    [IsAvailable] BIT                DEFAULT ((1)) NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) DEFAULT (getutcdate()) NOT NULL,
    [UpdatedDate] DATETIMEOFFSET (7) DEFAULT (getutcdate()) NOT NULL,
    [DeletedDate] DATETIMEOFFSET (7) NULL,
    [IsDeleted]   BIT                DEFAULT ((0)) NOT NULL,
    [TimeStamp]   ROWVERSION         NOT NULL,
    [CategoryId] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Products_Price] CHECK ([Price]>=(0)), 
    CONSTRAINT [FK_Products_Catgories] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id])
);

