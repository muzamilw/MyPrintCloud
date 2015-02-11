CREATE TABLE [dbo].[SectionCostCentresFeedback] (
    [Id]                      INT      IDENTITY (1, 1) NOT NULL,
    [ItemSectionCostcentreId] INT      NULL,
    [dDateTime]               DATETIME NULL,
    [Comments]                NTEXT    NULL,
    [SystemUserId]            INT      NULL
);

