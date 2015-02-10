CREATE TABLE [dbo].[Groups] (
    [GroupId]              INT            IDENTITY (1, 1) NOT NULL,
    [GroupName]            VARCHAR (256)  NOT NULL,
    [GroupDescription]     VARCHAR (1000) NULL,
    [CreationDateTime]     DATETIME       NULL,
    [CreatedBy]            INT            NULL,
    [LastModifiedDateTime] DATETIME       NULL,
    [LastModifiedBy]       INT            NULL,
    [SystemSiteId]         INT            NULL,
    [IsPrivate]            BIT            CONSTRAINT [DF_tbl_groups_IsPrivate] DEFAULT (1) NULL,
    [Notes]                NTEXT          NULL,
    CONSTRAINT [PK_tbl_groups] PRIMARY KEY CLUSTERED ([GroupId] ASC)
);

