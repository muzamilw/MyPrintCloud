CREATE TABLE [dbo].[ScheduledActivitySpliteDetail] (
    [ScheduleJobBindId]   INT      IDENTITY (1, 1) NOT NULL,
    [StartTime1]          DATETIME NULL,
    [EndTime1]            DATETIME NULL,
    [StartTime2]          DATETIME NULL,
    [EndTime2]            DATETIME NULL,
    [MidleTimeActivityId] INT      CONSTRAINT [DF__tbl_sched__Midle__36670980] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_scheduled_activitysplite_details] PRIMARY KEY CLUSTERED ([ScheduleJobBindId] ASC)
);

