USE [ServiceDb];
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[ServiceCategories] WHERE [Id] = CAST('a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1' AS uniqueidentifier))
BEGIN
    INSERT INTO [dbo].[ServiceCategories] ([Id], [Name])
    VALUES (CAST('a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1' AS uniqueidentifier), 'Analyses');
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[ServiceCategories] WHERE [Id] = CAST('a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2' AS uniqueidentifier))
BEGIN
    INSERT INTO [dbo].[ServiceCategories] ([Id], [Name])
    VALUES (CAST('a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2' AS uniqueidentifier), 'Consultation');
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[ServiceCategories] WHERE [Id] = CAST('a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3' AS uniqueidentifier))
BEGIN
    INSERT INTO [dbo].[ServiceCategories] ([Id], [Name])
    VALUES (CAST('a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3' AS uniqueidentifier), 'Diagnostics');
END
GO