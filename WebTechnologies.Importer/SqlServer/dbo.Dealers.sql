CREATE TABLE [dbo].[Dealers] (
    [Id]      INT        IDENTITY (1, 1) NOT NULL,
    [Name]    NTEXT      NOT NULL,
    [City]    NTEXT      NOT NULL,
    [Address] NTEXT      NOT NULL,
    [Area]    NTEXT      NOT NULL,
    [Rating]  FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
