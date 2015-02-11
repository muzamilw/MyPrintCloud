CREATE TABLE [dbo].[State] (
    [StateId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [CountryId] BIGINT        CONSTRAINT [DF__tbl_state__Count__231F2AE2] DEFAULT ((0)) NOT NULL,
    [StateCode] VARCHAR (100) NULL,
    [StateName] VARCHAR (100) NULL,
    CONSTRAINT [PK_tbl_state] PRIMARY KEY CLUSTERED ([StateId] ASC)
);

