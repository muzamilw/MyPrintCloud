CREATE PROCEDURE dbo.sp_users_add_USER_PREFERENCE
(@SystemUserID int,
@ShowPublic bit,
@CallColor varchar(50),
@AppointmentColor varchar(50),
@NextActionColor varchar(50),
@OtherActionColor varchar(50),
@SaleColor varchar(50),
@EventColor varchar(50),
@ToDoColor varchar(50),@RecordRetrivalLimit int,
@EnquiryRefreshTime smallint,
@EstimatesRefreshTime smallint,
@OrdersRefreshTime smallint,
@JobsRefreshTime smallint,
@SchedulingRefreshTime smallint,@CRMReminderRefreshTime smallint,@DefaultCalendar smallint)
AS
	insert into tbl_systemuser_preferences (SystemUserID,CallColor,AppointmentColor,NextActionColor,OtherActionColor,SaleColor,EventColor,ToDoColor,RecordRetrievalLimit,EnquiryRefreshTime, EstimatesRefreshTime, OrdersRefreshTime, JobsRefreshTime, SchedulingRefreshTime,ShowPublic,CRMReminderRefreshTime,DefaultCalendar) VALUES (@SystemUserID,@CallColor,@AppointmentColor,@NextActionColor,@OtherActionColor,@SaleColor,@EventColor,@ToDoColor,@RecordRetrivalLimit,@EnquiryRefreshTime, @EstimatesRefreshTime, @OrdersRefreshTime, @JobsRefreshTime, @SchedulingRefreshTime,@ShowPublic,@CRMReminderRefreshTime,@DefaultCalendar);Select @@identity as SystemUserPreferenceID
	RETURN