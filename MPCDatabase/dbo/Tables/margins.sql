CREATE TABLE [dbo].[margins] (
    [Id]                       INT            IDENTITY (1, 1) NOT NULL,
    [Ccode]                    FLOAT (53)     NULL,
    [Bcode]                    FLOAT (53)     NULL,
    [Department_short]         NVARCHAR (255) NULL,
    [Department]               NVARCHAR (255) NULL,
    [Buy Unit]                 NVARCHAR (255) NULL,
    [RQ]                       FLOAT (53)     NULL,
    [COGS]                     MONEY          NULL,
    [M1]                       FLOAT (53)     NULL,
    [Bantex Sell Price ex GST] MONEY          NULL,
    [Margin]                   FLOAT (53)     NULL,
    [F11]                      FLOAT (53)     NULL,
    [M2]                       FLOAT (53)     NULL,
    [Customer Sell ex GST]     MONEY          NULL,
    [Sell inc GST]             MONEY          NULL,
    [Customer Sell Inc GST]    MONEY          NULL,
    [Description]              NVARCHAR (255) NULL,
    CONSTRAINT [PK_margins] PRIMARY KEY CLUSTERED ([Id] ASC)
);

