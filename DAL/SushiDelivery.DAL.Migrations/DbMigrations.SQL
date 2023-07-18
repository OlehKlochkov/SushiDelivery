IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230227134627_CreateCustomers')
BEGIN
    CREATE TABLE [Customers] (
        [Id] uniqueidentifier NOT NULL,
        [LoginName] nvarchar(256) NOT NULL,
        [Phone] nvarchar(100) NULL,
        [Address] nvarchar(256) NULL,
        [CreatedDate] datetimeoffset NOT NULL DEFAULT (getutcdate()),
        [UpdatedDate] datetimeoffset NOT NULL DEFAULT (getutcdate()),
        [DeletedDate] datetimeoffset NOT NULL DEFAULT (getutcdate()),
        [IsDeleted] bit NOT NULL DEFAULT (0),
        [TimeStamp] rowversion NOT NULL,
        CONSTRAINT [PK_Customer_Id] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [AK_Customers_LoginName] UNIQUE NONCLUSTERED ([LoginName])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230227134627_CreateCustomers')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230227134627_CreateCustomers', N'7.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230310085628_CreateProductsAndIngredients')
BEGIN
    CREATE TABLE [Ingredients] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [CreatedDate] datetimeoffset NOT NULL DEFAULT (getutcdate()),
        [UpdatedDate] datetimeoffset NOT NULL DEFAULT (getutcdate()),
        [DeletedDate] datetimeoffset NOT NULL DEFAULT (getutcdate()),
        [IsDeleted] bit NOT NULL DEFAULT (0),
        [TimeStamp] rowversion NOT NULL,
        CONSTRAINT [PK__Ingredient_Id] PRIMARY KEY CLUSTERED ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230310085628_CreateProductsAndIngredients')
BEGIN
    CREATE TABLE [Products] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Price] decimal(5,2) NOT NULL,
        [IsAvailable] bit NOT NULL,
        [Category] int NOT NULL,
        [CreatedDate] datetimeoffset NOT NULL DEFAULT (getutcdate()),
        [UpdatedDate] datetimeoffset NOT NULL DEFAULT (getutcdate()),
        [DeletedDate] datetimeoffset NOT NULL DEFAULT (getutcdate()),
        [IsDeleted] bit NOT NULL DEFAULT (0),
        [TimeStamp] rowversion NOT NULL,
        CONSTRAINT [PK__Product_Id] PRIMARY KEY CLUSTERED ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230310085628_CreateProductsAndIngredients')
BEGIN
    CREATE TABLE [ProductIngredients] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] uniqueidentifier NOT NULL,
        [IngredientId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK__ProductIngredient_Id] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [FK_ProductIngredients_Ingredients_IngredientId] FOREIGN KEY ([IngredientId]) REFERENCES [Ingredients] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductIngredients_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230310085628_CreateProductsAndIngredients')
BEGIN
    CREATE INDEX [IX_ProductIngredients_IngredientId] ON [ProductIngredients] ([IngredientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230310085628_CreateProductsAndIngredients')
BEGIN
    CREATE INDEX [IX_ProductIngredients_ProductId] ON [ProductIngredients] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230310085628_CreateProductsAndIngredients')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230310085628_CreateProductsAndIngredients', N'7.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230323115702_AddUniqueConstraint')
BEGIN
    DROP INDEX [IX_ProductIngredients_ProductId] ON [ProductIngredients];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230323115702_AddUniqueConstraint')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Price');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Products] ALTER COLUMN [Price] decimal(18,2) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230323115702_AddUniqueConstraint')
BEGIN
    ALTER TABLE [ProductIngredients] ADD CONSTRAINT [AK_ProductIngredients_ProductId_IngredientId] UNIQUE NONCLUSTERED ([ProductId], [IngredientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230323115702_AddUniqueConstraint')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230323115702_AddUniqueConstraint', N'7.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704085350_NewSequentialId')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Id');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Products] ADD DEFAULT (NEWSEQUENTIALID()) FOR [Id];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704085350_NewSequentialId')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ingredients]') AND [c].[name] = N'Id');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Ingredients] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Ingredients] ADD DEFAULT (NEWSEQUENTIALID()) FOR [Id];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704085350_NewSequentialId')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'Id');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Customers] ADD DEFAULT (NEWSEQUENTIALID()) FOR [Id];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704085350_NewSequentialId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230704085350_NewSequentialId', N'7.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704183355_ChangeDateDeleted')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'DeletedDate');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var4 + '];');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704183355_ChangeDateDeleted')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ingredients]') AND [c].[name] = N'DeletedDate');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Ingredients] DROP CONSTRAINT [' + @var5 + '];');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704183355_ChangeDateDeleted')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'DeletedDate');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var6 + '];');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704183355_ChangeDateDeleted')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230704183355_ChangeDateDeleted', N'7.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704184403_MakeDateDeletedNotRequired')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'DeletedDate');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Products] ALTER COLUMN [DeletedDate] datetimeoffset NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704184403_MakeDateDeletedNotRequired')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ingredients]') AND [c].[name] = N'DeletedDate');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Ingredients] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Ingredients] ALTER COLUMN [DeletedDate] datetimeoffset NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704184403_MakeDateDeletedNotRequired')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'DeletedDate');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Customers] ALTER COLUMN [DeletedDate] datetimeoffset NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704184403_MakeDateDeletedNotRequired')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230704184403_MakeDateDeletedNotRequired', N'7.0.8');
END;
GO

COMMIT;
GO

