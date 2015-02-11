
--Exec usp_DashboardAnalytics 5
CREATE PROCEDURE [dbo].[usp_DashboardAnalytics]
	@RecordID int
AS
BEGIN
/*
0 = Estimate
1 = Sales
2 = Registered Users
3 = orders
4 = Newsletter Subscriber
5 = items
*/
if(@RecordID = 0)
	begin
		select 1 as Id,
		(select COUNT(EstimateID) 
			from tbl_estimates
			where CreationDate >= CONVERT(varchar, getdate(), 101) and isestimate = 1) As TodayCounts,

		(select COUNT(EstimateID) 
				from tbl_estimates 
			    where CreationDate between DATEADD(DAY, -1, convert(varchar, getdate(), 101)) and convert(varchar, getdate(), 101) and isEstimate = 1) As YesterdayCounts,
				
		(select COUNT(EstimateID) 
				from tbl_estimates 
				where CreationDate >= DATEADD(DAY, -7, convert(varchar, getdate(), 101)) and isEstimate = 1) As WeakCounts,
				
		(select COUNT(EstimateID) 
				from tbl_estimates 
				where CreationDate >= DATEADD(MONTH, -1, convert(varchar, getdate(), 101)) and isEstimate = 1)As MonthCounts
	end
else if(@RecordID = 1)
	begin
		select 1 as Id,
		(select COALESCE(sum(cast(Estimate_Total as int)), 0) 
			from tbl_estimates
			where CreationDate >= CONVERT(varchar, getdate(), 101)) As TodayCounts,

		(select COALESCE(sum(cast(Estimate_Total as int)), 0) 
				from tbl_estimates 
			    where CreationDate between DATEADD(DAY, -1, convert(varchar, getdate(), 101)) and convert(varchar, getdate(), 101)) As YesterdayCounts,
				
		(select COALESCE(sum(cast(Estimate_Total as int)), 0) 
				from tbl_estimates 
				where CreationDate >= DATEADD(DAY, -7, GETDATE())) As WeakCounts,
				
		(select COALESCE(sum(cast(Estimate_Total as int)), 0)  
				from tbl_estimates 
				where CreationDate >= DATEADD(Month, -1, GETDATE()))As MonthCounts
	end
else if(@RecordID = 2)
    begin
		select 1 as Id,
		(select COUNT(ContactCompanyID) 
			from tbl_contactcompanies
			where typeID <>53 and CreationDate >= CONVERT(varchar, getdate(), 101)) As TodayCounts,

		(select COUNT(ContactCompanyID) 
				from tbl_contactcompanies 
			    where typeID <>53 and CreationDate between DATEADD(DAY, -1, convert(varchar, getdate(), 101)) and convert(varchar, getdate(), 101)) As YesterdayCounts,
				
		(select COUNT(ContactCompanyID) 
				from tbl_contactcompanies 
				where typeID <>53 and CreationDate >= DATEADD(DAY, -7, GETDATE())) As WeakCounts,
				
		(select COUNT(ContactCompanyID) 
				from tbl_contactcompanies 
				where typeID <>53 and CreationDate >= DATEADD(Month, -1, GETDATE()))As MonthCounts
	end 
else if(@RecordID = 3) -- pending orders
	  begin
		select 1 as Id,
		(select COUNT(EstimateID) 
			from tbl_estimates
			where CreationDate >= CONVERT(varchar, getdate(), 101) and isestimate = 0 and StatusID <> 3 and StatusID = 4) As TodayCounts,

		(select COUNT(EstimateID) 
				from tbl_estimates 
			    where CreationDate between DATEADD(DAY, -1, convert(varchar, getdate(), 101)) and convert(varchar, getdate(), 101) and isestimate = 0 and StatusID <> 3 and StatusID = 4) As YesterdayCounts,
				
		(select COUNT(EstimateID) 
				from tbl_estimates 
				where CreationDate >= DATEADD(DAY, -7, GETDATE()) and isestimate = 0 and StatusID <> 3 and StatusID = 4) As WeakCounts,
				
		(select COUNT(EstimateID) 
				from tbl_estimates 
				where CreationDate >= DATEADD(Month, -1, GETDATE()) and isestimate = 0 and StatusID <> 3 and StatusID = 4)As MonthCounts
	end
else if(@RecordID = 4) -- confirmed orders
	  begin
		select 1 as Id,
		(select COUNT(EstimateID) 
			from tbl_estimates
			where LastUpdateDate >= CONVERT(varchar, getdate(), 101) and isestimate = 0 and StatusID <> 3 and StatusID = 5) As TodayCounts,

		(select COUNT(EstimateID) 
				from tbl_estimates 
			    where LastUpdateDate between DATEADD(DAY, -1, convert(varchar, getdate(), 101)) and convert(varchar, getdate(), 101) and isestimate = 0 and StatusID <> 3 and StatusID = 5) As YesterdayCounts,
				
		(select COUNT(EstimateID) 
				from tbl_estimates 
				where LastUpdateDate >= DATEADD(DAY, -7, GETDATE()) and isestimate = 0 and StatusID <> 3 and StatusID = 5) As WeakCounts,
				
		(select COUNT(EstimateID) 
				from tbl_estimates 
				where LastUpdateDate >= DATEADD(Month, -1, GETDATE()) and isestimate = 0 and StatusID <> 3 and StatusID = 5)As MonthCounts
	end  
	else if(@RecordID = 5) -- in production orders
	  begin
		select 1 as Id,
		(select COUNT(EstimateID) 
			from tbl_estimates
			where LastUpdateDate >= CONVERT(varchar, getdate(), 101) and isestimate = 0 and StatusID <> 3 and StatusID = 6) As TodayCounts,

		(select COUNT(EstimateID) 
				from tbl_estimates 
			    where LastUpdateDate between DATEADD(DAY, -1, convert(varchar, getdate(), 101)) and convert(varchar, getdate(), 101) and isestimate = 0 and StatusID <> 3 and StatusID = 6) As YesterdayCounts,
				
		(select COUNT(EstimateID) 
				from tbl_estimates 
				where LastUpdateDate >= DATEADD(DAY, -7, GETDATE()) and isestimate = 0 and StatusID <> 3 and StatusID = 6) As WeakCounts,
				
		(select COUNT(EstimateID) 
				from tbl_estimates 
				where LastUpdateDate >= DATEADD(Month, -1, GETDATE()) and isestimate = 0 and StatusID <> 3 and StatusID = 6)As MonthCounts
	end
else if(@RecordID = 6) -- ready for shipping orders
	  begin
		select 1 as Id,
		(select COUNT(EstimateID) 
			from tbl_estimates
			where LastUpdateDate >= CONVERT(varchar, getdate(), 101) and isestimate = 0 and StatusID <> 3 and StatusID = 7) As TodayCounts,

		(select COUNT(EstimateID) 
				from tbl_estimates 
			    where LastUpdateDate between DATEADD(DAY, -1, convert(varchar, getdate(), 101)) and convert(varchar, getdate(), 101) and isestimate = 0 and StatusID <> 3 and StatusID = 7) As YesterdayCounts,
				
		(select COUNT(EstimateID) 
				from tbl_estimates 
				where LastUpdateDate >= DATEADD(DAY, -7, GETDATE()) and isestimate = 0 and StatusID <> 3 and StatusID = 7) As WeakCounts,
				
		(select COUNT(EstimateID) 
				from tbl_estimates 
				where LastUpdateDate >= DATEADD(Month, -1, GETDATE()) and isestimate = 0 and StatusID <> 3 and StatusID = 7)As MonthCounts
	end  
	else if(@RecordID = 7) -- Invoiced orders
	  begin
		select 1 as Id,
		(select COUNT(EstimateID) 
			from tbl_estimates
			where LastUpdateDate >= CONVERT(varchar, getdate(), 101) and isestimate = 0 and StatusID <> 3 and StatusID = 10) As TodayCounts,

		(select COUNT(EstimateID) 
				from tbl_estimates 
			    where LastUpdateDate between DATEADD(DAY, -1, convert(varchar, getdate(), 101)) and convert(varchar, getdate(), 101) and isestimate = 0 and StatusID <> 3 and StatusID = 10) As YesterdayCounts,
				
		(select COUNT(EstimateID) 
				from tbl_estimates 
				where LastUpdateDate >= DATEADD(DAY, -7, GETDATE()) and isestimate = 0 and StatusID <> 3 and StatusID = 10) As WeakCounts,
				
		(select COUNT(EstimateID) 
				from tbl_estimates 
				where LastUpdateDate >= DATEADD(Month, -1, GETDATE()) and isestimate = 0 and StatusID <> 3 and StatusID = 10)As MonthCounts
	end 
	else if(@RecordID = 8) -- cencelled orders
	  begin
		select 1 as Id,
		(select COUNT(EstimateID) 
			from tbl_estimates
			where LastUpdateDate >= CONVERT(varchar, getdate(), 101) and isestimate = 0 and StatusID <> 3 and StatusID = 9) As TodayCounts,

		(select COUNT(EstimateID) 
				from tbl_estimates 
			    where LastUpdateDate between DATEADD(DAY, -1, convert(varchar, getdate(), 101)) and convert(varchar, getdate(), 101) and isestimate = 0 and StatusID <> 3 and StatusID = 9) As YesterdayCounts,
				
		(select COUNT(EstimateID) 
				from tbl_estimates 
				where LastUpdateDate >= DATEADD(DAY, -7, GETDATE()) and isestimate = 0 and StatusID <> 3 and StatusID = 9) As WeakCounts,
				
		(select COUNT(EstimateID) 
				from tbl_estimates 
				where LastUpdateDate >= DATEADD(Month, -1, GETDATE()) and isestimate = 0 and StatusID <> 3 and StatusID = 9)As MonthCounts
	end 
	
else if(@RecordID = 9)
	  begin
		select 1 as Id,
		(select COUNT(SubscriberID)
			from tbl_newslettersubscribers
			where SubscribeDate >= CONVERT(varchar, getdate(), 101)) As TodayCounts,

		(select COUNT(SubscriberID) 
				from tbl_newslettersubscribers 
			    where SubscribeDate between DATEADD(DAY, -1, convert(varchar, getdate(), 101)) and convert(varchar, getdate(), 101)) As YesterdayCounts,
				
		(select COUNT(SubscriberID) 
				from tbl_newslettersubscribers 
				where SubscribeDate >= DATEADD(DAY, -7, GETDATE())) As WeakCounts,
				
		(select COUNT(SubscriberID) 
				from tbl_newslettersubscribers 
				where SubscribeDate >= DATEADD(Month, -1, GETDATE()))As MonthCounts
	end 
else if(@RecordID = 10)
	  begin
		select 1 as Id,
		(select COUNT(ItemID)
			from tbl_items where EstimateID in(
			select EstimateID from tbl_estimates
			where StatusID in(5,6,7)
			and CreationDate >= CONVERT(varchar, getdate(), 101) and isestimate = 0)) As TodayCounts,
		
		(select COUNT(ItemID)
			from tbl_items where EstimateID in(
			select EstimateID from tbl_estimates
			where StatusID in(5,6,7)
			and CreationDate between DATEADD(DAY, -1, convert(varchar, getdate(), 101)) and convert(varchar, getdate(), 101) and isestimate = 0)) As YesterdayCounts,
			
		 (select COUNT(ItemID)
			from tbl_items where EstimateID in(
			select EstimateID from tbl_estimates
			where StatusID in(5,6,7)
			and CreationDate >= DATEADD(day, -7, getdate()) and isestimate = 0)) As WeakCounts,
			
		 (select COUNT(ItemID)
			from tbl_items where EstimateID in(
			select EstimateID from tbl_estimates
			where StatusID in(5,6,7)
			and CreationDate >= DATEADD(Month, -1, getdate())and isestimate = 0)) As MonthCounts
	
	end 
			RETURN 
	END