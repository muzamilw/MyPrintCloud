CREATE PROCEDURE [dbo].[sp_Activity_Get_RemindersByUser]
	@SystemUserID int,
	@AlarmDate datetime,
	@AlarmTime datetime
AS
	/*select tbl_activity.activityid as ID,cast(tbl_activity.activitytypeid as char) as Type,tbl_activity.activityref as Subject,
                tbl_activity.activityStartTime  as StartTime,
                tbl_activity.activityendtime  as EndTime,
                tbl_activity.alarmdate as AlarmDate,tbl_activity.alarmtime as AlarmTime,
                tbl_activity.SystemUserID as Owner FROM tbl_activity 
                where (systemuserid=@SystemUserID and iscomplete =0 and isactivityalarm=1 and 
                (DAY(alarmdate)=DAY(@AlarmDate) AND 
                MONTH(alarmdate)=MONTH(@AlarmDate) AND 
                YEAR(alarmdate)=YEAR(@AlarmDate) AND 
                DATEPART(HOUR,alarmtime)<=DATEPART(HOUR,@AlarmTime) AND
                DATEPART(MINUTE,alarmtime)<=DATEPART(MINUTE,@AlarmTime) AND
                DATEPART(SECOND,alarmtime)<=DATEPART(SECOND,@AlarmTime) AND
				IsLocked=0) or 
                (systemuserid=@SystemUserID and iscomplete =0 and activitylink=1 and isactivityalarm=1 and 
                DAY(alarmdate)<DAY(@AlarmDate) AND 
                MONTH(alarmdate)<MONTH(@AlarmDate) AND 
                YEAR(alarmdate)<YEAR(@AlarmDate) 
				and IsLocked=0)) */
					select tbl_activity.activityid as ID,cast(tbl_activity.activitytypeid as char) as Type,tbl_activity.activityref as Subject,
                tbl_activity.activityStartTime  as StartTime,
                tbl_activity.activityendtime  as EndTime,
                tbl_activity.alarmdate as AlarmDate,tbl_activity.alarmtime as AlarmTime,
                tbl_activity.SystemUserID as Owner FROM tbl_activity 
                where 
(

(systemuserid=@SystemUserID and iscomplete =0 /*and activitylink<>0*/ and isactivityalarm<>0 and 
(/*                DAY(alarmtime)<=DAY(@AlarmTime) AND 
                MONTH(alarmtime)<=MONTH(@AlarmTime) AND 
                YEAR(alarmtime)<=YEAR(@AlarmTime) AND 
                DATEPART(HOUR,AlarmTime)<=DATEPART(HOUR,@AlarmTime) and 
                DATEPART(MINUTE,AlarmTime)<=DATEPART(MINUTE,@AlarmTime) and 
                DATEPART(SECOND,AlarmTime)<=DATEPART(SECOND,@AlarmTime) */
                tbl_activity.alarmtime<=@AlarmTime
)
				and IsLocked=0)
) 
                 UNION
                select tbl_tasks.TaskID as ID,'Task' as Type,tbl_tasks.Subject as Subject,
                tbl_tasks.StartDate as StartTime,tbl_tasks.DueDate as EndTime,
                tbl_tasks.taskAlarmDate as AlarmDate,tbl_tasks.TaskAlarmTime as AlarmTime,
                tbl_tasks.Owner as Owner FROM tbl_tasks 
                where 
                (owner=@SystemUserID and istaskAlarmed<>0 and 
                (/*DAY(taskalarmtime)<=DAY(@AlarmTime) AND 
                MONTH(taskalarmtime)<=MONTH(@AlarmTime) AND 
                YEAR(taskalarmtime)<=YEAR(@AlarmTime) AND
                DATEPART(HOUR,taskalarmtime)<=DATEPART(HOUR,@AlarmTime) AND 
                DATEPART(MINUTE,taskalarmtime)<=DATEPART(MINUTE,@AlarmTime) AND 
                DATEPART(SECOND,taskalarmtime)<=DATEPART(SECOND,@AlarmTime)*/
                tbl_tasks.taskalarmtime<=@AlarmTime
                )and IsLocked=0)
	RETURN