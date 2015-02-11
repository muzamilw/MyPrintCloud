CREATE PROCEDURE dbo.sp_users_update_userpreferences
(@ShowPublic bit, @CallColor varchar(50),
@AppointmentColor varchar(50),
@NextActionColor varchar(50),
@OtherActionColor varchar(50),
@SaleColor varchar(50),
@EventColor varchar(50),
@ToDoColor varchar(50),
@SystemUserPreferenceID int,@RecordRetrivalLimit int,
@EnquiryRefreshTime smallint,
@EstimatesRefreshTime smallint,
@OrdersRefreshTime smallint,
@JobsRefreshTime smallint,
@SchedulingRefreshTime smallint,@CRMReminderRefreshTime smallint,@DefaultCalendar smallint

)
AS
	update tbl_systemuser_preferences set CallColor=@CallColor,AppointmentColor=@AppointmentColor,NextActionColor=@NextActionColor,OtherActionColor=@OtherActionColor,SaleColor=@SaleColor,EventColor=@EventColor,ToDoColor=@ToDoColor,RecordRetrievalLimit=@RecordRetrivalLimit
	,EnquiryRefreshTime=@EnquiryRefreshTime, EstimatesRefreshTime=@EstimatesRefreshTime, OrdersRefreshTime=@OrdersRefreshTime, JobsRefreshTime=@JobsRefreshTime, SchedulingRefreshTime=@SchedulingRefreshTime,ShowPublic =@ShowPublic, CRMReminderRefreshTime=@CRMReminderRefreshTime,DefaultCalendar=@DefaultCalendar
	 where (SystemUserPreferenceID=@SystemUserPreferenceID)
	RETURN