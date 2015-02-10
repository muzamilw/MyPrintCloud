CREATE TABLE [dbo].[SystemUserCheckin] (
    [Id]               BIGINT   IDENTITY (1, 1) NOT NULL,
    [UserId]           INT      CONSTRAINT [DF__tbl_syste__UserI__7DB89C09] DEFAULT (0) NOT NULL,
    [CheckInDateTime]  DATETIME NULL,
    [CheckOutDateTime] DATETIME NULL,
    [ReasonId]         SMALLINT NULL,
    CONSTRAINT [PK_tbl_systemuser_checkins] PRIMARY KEY CLUSTERED ([Id] ASC)
);

