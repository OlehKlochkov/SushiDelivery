CREATE TABLE [dbo].[ProductIngredients]
(
	[ProductId] UNIQUEIDENTIFIER NOT NULL, 
    [IngredientId] UNIQUEIDENTIFIER NOT NULL, 
    [Count] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_ProductIngrediants] PRIMARY KEY ([ProductId], [IngredientId]), 
    CONSTRAINT [CK_ProductIngrediants_Count] CHECK (Count > 0), 
    CONSTRAINT [FK_ProductIngrediants_Products] FOREIGN KEY ([ProductId]) REFERENCES [Products]([Id]),
    CONSTRAINT [FK_ProductIngrediants_Ingredients] FOREIGN KEY ([IngredientId]) REFERENCES [Ingredients]([Id])
)
