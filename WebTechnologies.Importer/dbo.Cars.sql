CREATE TABLE [dbo].[Cars] (
    [Id]    INT        IDENTITY (1, 1) NOT NULL,
    [Firm]  NTEXT      NOT NULL,
    [Model] NTEXT      NOT NULL,
    [Year]  INT        NOT NULL,
    [Power] INT        NOT NULL,
    [Color] NTEXT      NOT NULL,
    [Price] FLOAT (53) NOT NULL,
    CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED ([Id] ASC)
);
