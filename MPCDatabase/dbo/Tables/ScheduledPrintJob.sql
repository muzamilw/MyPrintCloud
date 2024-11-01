﻿CREATE TABLE [dbo].[ScheduledPrintJob] (
    [ScheduledPrintJobId] INT           IDENTITY (1, 1) NOT NULL,
    [ItemSectionId]       INT           CONSTRAINT [DF__tbl_sched__ItemS__41D8BC2C] DEFAULT (0) NOT NULL,
    [MachineId]           INT           CONSTRAINT [DF__tbl_sched__Machi__42CCE065] DEFAULT (0) NOT NULL,
    [MakeReadyType]       INT           CONSTRAINT [DF__tbl_sched__MakeR__43C1049E] DEFAULT (0) NOT NULL,
    [Speed]               FLOAT (53)    CONSTRAINT [DF__tbl_sched__Speed__44B528D7] DEFAULT (0) NOT NULL,
    [RepeatDays]          INT           CONSTRAINT [DF__tbl_sched__Repea__45A94D10] DEFAULT (0) NOT NULL,
    [RepeatOccurrencs]    INT           CONSTRAINT [DF__tbl_sched__Repea__469D7149] DEFAULT (0) NOT NULL,
    [Notes]               TEXT          NOT NULL,
    [CustomerId]          INT           CONSTRAINT [DF__tbl_sched__Custo__47919582] DEFAULT (0) NOT NULL,
    [JobId]               INT           NOT NULL,
    [JobCode]             VARCHAR (255) CONSTRAINT [DF__tbl_sched__JobCo__4885B9BB] DEFAULT ('0') NOT NULL,
    [Quantity]            INT           CONSTRAINT [DF__tbl_sched__Quant__4979DDF4] DEFAULT (0) NOT NULL,
    [NoOfUp]              INT           CONSTRAINT [DF__tbl_sched__NoOfU__4A6E022D] DEFAULT (0) NOT NULL,
    [MachineName]         VARCHAR (255) NOT NULL,
    [MachineType]         INT           NOT NULL,
    [Estimate_Code]       VARCHAR (255) NOT NULL,
    [CustomerName]        VARCHAR (255) NOT NULL,
    [GullotineId]         INT           NOT NULL,
    [Pagination]          INT           NOT NULL,
    [ItemSectionName]     VARCHAR (255) NOT NULL,
    [Discription]         TEXT          NOT NULL,
    [SelectedQtyIndex]    INT           NOT NULL,
    [StartTime]           DATETIME      NOT NULL,
    [EndTime]             DATETIME      NOT NULL,
    [TargetPrintDate]     DATETIME      NULL,
    [ArtworkByDate]       DATETIME      NULL,
    [DataByDate]          DATETIME      NULL,
    [TargetBindDate]      DATETIME      NULL,
    [GullotineName]       VARCHAR (255) NULL,
    CONSTRAINT [PK_tbl_scheduled_printjobs] PRIMARY KEY CLUSTERED ([ScheduledPrintJobId] DESC)
);

