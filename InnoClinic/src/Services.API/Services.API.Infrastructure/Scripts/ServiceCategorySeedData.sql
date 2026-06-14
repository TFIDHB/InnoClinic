USE [ServiceDb];
GO
IF NOT EXISTS (SELECT 1 FROM [dbo].[ServiceCategories] WHERE [Id] = CAST('a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1' AS uniqueidentifier))
BEGIN
    INSERT INTO [dbo].[ServiceCategories] ([Id], [Name], [TimeSlotSize])
    VALUES (CAST('a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1' AS uniqueidentifier), 'Analyses', 1);
END
GO
IF NOT EXISTS (SELECT 1 FROM [dbo].[ServiceCategories] WHERE [Id] = CAST('a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2' AS uniqueidentifier))
BEGIN
    INSERT INTO [dbo].[ServiceCategories] ([Id], [Name], [TimeSlotSize])
    VALUES (CAST('a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2' AS uniqueidentifier), 'Consultation', 2);
END
GO
IF NOT EXISTS (SELECT 1 FROM [dbo].[ServiceCategories] WHERE [Id] = CAST('a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3' AS uniqueidentifier))
BEGIN
    INSERT INTO [dbo].[ServiceCategories] ([Id], [Name], [TimeSlotSize])
    VALUES (CAST('a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3' AS uniqueidentifier), 'Diagnostics', 3);
END
GO