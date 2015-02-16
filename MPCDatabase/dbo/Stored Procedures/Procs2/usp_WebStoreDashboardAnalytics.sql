-- =============================================
-- Author:		Muhammad Naveed
-- Create date: 25-09-2013
-- Description:	To Get Web Store Dashboard Analytics
-- =============================================

--Exec usp_WebStoreDashboardAnalytics 1, 11022
CREATE PROCEDURE [dbo].[usp_WebStoreDashboardAnalytics]
	@RecordID int,
	@ContactCompanyID int
AS
BEGIN
/*
1 = All
2 = This week
3 = This month
4 = Last month

*/
if(@RecordID = 1)
	begin
		select 1 as ID,
		(select	count(c.contactid) from tbl_contacts c
		inner join tbl_contactcompanies cc on c.contactcompanyid = cc.contactcompanyid
		and cc.brokercontactcompanyid = @ContactCompanyID) As RegUsersCount,
		
		(select count(SubscriberID) from tbl_NewsLetterSubscribers
		 where ContactCompanyID = @ContactCompanyID) As SubscribersCount,

		(select count(InquiryID) from tbl_Inquiry
		 where BrokerContactcompanyID = @ContactCompanyID) As RFQsCount,
		 
		(select	count(e.EstimateID) from tbl_estimates e
		
		where	isEstimate = 0
		and		e.brokerid = @ContactCompanyID
		and		e.StatusID <> 3) As OrdersCount
	end
else if(@RecordID = 2)
    begin
		select 1 as ID,
		(select	count(c.contactid) from tbl_contacts c
		inner join tbl_contactcompanies cc on c.contactcompanyid = cc.contactcompanyid
		and cc.brokercontactcompanyid = @ContactCompanyID
		and datepart(week, cc.creationdate) = datepart(week, getdate())) As RegUsersCount,
		
		(select count(SubscriberID) from tbl_NewsLetterSubscribers
		 where ContactCompanyID = @ContactCompanyID
		 and datepart(week, SubscribeDate) = datepart(week, getdate())) As SubscribersCount,
		 
		 (select count(InquiryID) from tbl_Inquiry 
		 where BrokerContactcompanyID = @ContactCompanyID
		 and datepart(week, CreatedDate) = datepart(week, getdate())) As RFQsCount,
		 
		 (select	count(e.EstimateID) from tbl_estimates e
		
		where	isEstimate = 0
		and		e.brokerid = @ContactCompanyID
		and		e.StatusID <> 3
		and datepart(week, e.CreationDate) = datepart(week, getdate())) As OrdersCount
		 
	end 
else if(@RecordID = 3)
	  begin
		-- this month
		select 1 as ID,
		(select count(c.contactid) from tbl_contacts c
		inner join tbl_contactcompanies cc on c.contactcompanyid = cc.contactcompanyid
		and cc.brokercontactcompanyid = @ContactCompanyID
		and datepart(month, cc.creationdate) = datepart(month, getdate())) As RegUsersCount,
		
		(select count(SubscriberID) from tbl_NewsLetterSubscribers
		 where ContactCompanyID = @ContactCompanyID
		 and datepart(month, SubscribeDate) = datepart(month, getdate())) As SubscribersCount,
		 
		 (select count(InquiryID) from tbl_Inquiry
		 where BrokerContactcompanyID = @ContactCompanyID
		 and datepart(month, CreatedDate) = datepart(month, getdate())) As RFQsCount,
		 
		 (select	count(e.EstimateID) from tbl_estimates e
		
		where	isEstimate = 0
		and		e.brokerid = @ContactCompanyID
		and		e.StatusID <> 3
		and datepart(month, e.CreationDate) = datepart(month, getdate())) As OrdersCount
	end 
else if(@RecordID = 4)
	  begin
		-- last month
		select 1 as ID,
		(select count(c.contactid) from tbl_contacts c
		inner join tbl_contactcompanies cc on c.contactcompanyid = cc.contactcompanyid
		and cc.brokercontactcompanyid = @ContactCompanyID
		and datepart(month, cc.creationdate) = datepart(month, getdate()) - 1) As RegUsersCount,
		
		(select count(SubscriberID) from tbl_NewsLetterSubscribers
		 where ContactCompanyID = @ContactCompanyID
		 and datepart(month, SubscribeDate) = datepart(month, getdate()) - 1) As SubscribersCount,
		 
		 (select count(InquiryID) from tbl_Inquiry
		 where BrokerContactcompanyID = @ContactCompanyID
		 and datepart(month, CreatedDate) = datepart(month, getdate()) -1) As RFQsCount,
		 
		 (select	count(e.EstimateID) from tbl_estimates e
		
		where	isEstimate = 0
		and		e.brokerid = @ContactCompanyID
		and		e.StatusID <> 3
		and datepart(month, e.CreationDate) = datepart(month, getdate()) - 1) As OrdersCount
	end 

	RETURN 
END