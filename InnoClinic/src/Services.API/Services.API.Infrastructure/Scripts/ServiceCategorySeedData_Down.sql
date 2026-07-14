USE [ServiceDb];
GO
IF EXISTS (SELECT 1 FROM [dbo].[ServiceCategories] WHERE [Id] = CAST('a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1' AS uniqueidentifier))
BEGIN
    DELETE FROM [dbo].[ServiceCategories] WHERE [Id] = CAST('a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1' AS uniqueidentifier);
END
GO
IF EXISTS (SELECT 1 FROM [dbo].[ServiceCategories] WHERE [Id] = CAST('a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2' AS uniqueidentifier))
BEGIN
    DELETE FROM [dbo].[ServiceCategories] WHERE [Id] = CAST('a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2' AS uniqueidentifier);
END
GO
IF EXISTS (SELECT 1 FROM [dbo].[ServiceCategories] WHERE [Id] = CAST('a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3' AS uniqueidentifier))
BEGIN
    DELETE FROM [dbo].[ServiceCategories] WHERE [Id] = CAST('a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3' AS uniqueidentifier);
END
GO