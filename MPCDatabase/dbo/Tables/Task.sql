﻿CREATE TABLE [dbo].[Task] (
    [TaskId]               INT           IDENTITY (1, 1) NOT NULL,
    [Subject]              VARCHAR (500) NULL,
    [DueDate]              DATETIME      NULL,
    [StartDate]            DATETIME      NULL,
    [Status]               INT           NULL,
    [Priority]             INT           NULL,
    [Completion]           INT           NULL,
    [IsTaskAlarmed]        INT           NULL,
    [TaskAlarmDate]        DATETIME      NULL,
    [TaskAlarmTime]        DATETIME      NULL,
    [Owner]                INT           NULL,
    [IsComplete]           INT           NULL,
    [CompletionDate]       DATETIME      NULL,
    [CompletionTime]       DATETIME      NULL,
    [TotalWorkHours]       INT           NULL,
    [ActualWorkHours]      INT           NULL,
    [Notes]                NTEXT         NULL,
    [IsPrivate]            INT           NULL,
    [IsTaskLinked]         INT           NULL,
    [LinkType]             INT           NULL,
    [LinkId]               INT           NULL,
    [CreationDateTime]     DATETIME      NULL,
    [CreatedBy]            INT           NULL,
    [LastUpdationDateTime] DATETIME      NULL,
    [LastUpdatedBy]        INT           NULL,
    [IsLocked]             SMALLINT      CONSTRAINT [DF__tbl_tasks__IsLoc__0A1E72EE] DEFAULT (0) NULL,
    [LockedBy]             INT           CONSTRAINT [DF_tbl_tasks_LockedBy] DEFAULT (0) NULL,
    [SystemSiteId]         INT           NULL,
    CONSTRAINT [PK_tbl_tasks] PRIMARY KEY CLUSTERED ([TaskId] ASC)
);

