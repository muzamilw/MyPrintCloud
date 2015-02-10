CREATE TABLE [dbo].[ScheduledTimeActivity] (
    [ScheduledTimeActivityId]       INT        IDENTITY (1, 1) NOT NULL,
    [CreationDateTime]              DATETIME   NULL,
    [StartTime]                     DATETIME   NULL,
    [EndTime]                       DATETIME   NULL,
    [UserLockedBy]                  INT        CONSTRAINT [DF__tbl_sched__UserL__4D4A6ED8] DEFAULT (0) NOT NULL,
    [LockDateTime]                  DATETIME   NULL,
    [DeliveryTime]                  DATETIME   NULL,
    [ActivityStatusID]              INT        CONSTRAINT [DF__tbl_sched__Activ__4E3E9311] DEFAULT (0) NOT NULL,
    [JobID]                         INT        CONSTRAINT [DF__tbl_sched__JobID__4F32B74A] DEFAULT (0) NOT NULL,
    [CostCenterId]                  INT        CONSTRAINT [DF__tbl_sched__CostC__5026DB83] DEFAULT (0) NOT NULL,
    [JobBindId]                     INT        CONSTRAINT [DF__tbl_sched__JobBi__511AFFBC] DEFAULT (0) NOT NULL,
    [IsLocked]                      SMALLINT   NULL,
    [IsCompleted]                   SMALLINT   NULL,
    [OleColorCode]                  FLOAT (53) NULL,
    [IsInEditing]                   SMALLINT   CONSTRAINT [DF__tbl_sched__IsInE__520F23F5] DEFAULT (0) NULL,
    [Mode]                          INT        CONSTRAINT [DF__tbl_schedu__Mode__5303482E] DEFAULT (0) NOT NULL,
    [LinkedScheduledTimeActivityId] INT        CONSTRAINT [DF__tbl_sched__Linke__53F76C67] DEFAULT (0) NOT NULL,
    [LockedBy]                      INT        CONSTRAINT [DF__tbl_sched__Locke__54EB90A0] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_scheduled_time_activities] PRIMARY KEY CLUSTERED ([ScheduledTimeActivityId] ASC)
);

