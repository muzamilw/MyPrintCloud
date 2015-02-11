CREATE PROCEDURE dbo.sp_users_update_useremailsetting
(
@SmtpServer varchar(200),
@SmtpUserName varchar(200),
@SmtpPassword varchar(200),
@Pop3Server varchar(200),
@Pop3UserName varchar(200),
@Pop3Password varchar(200),
@SystemUserPreferenceID int
)
AS
	update tbl_systemuser_preferences set SmtpServer=@SmtpServer,SmtpUserName=@SmtpUserName,SmtpPassword=@SmtpPassword,Pop3Server=@Pop3Server,Pop3UserName=@Pop3UserName,Pop3Password=@Pop3Password 
	 where (SystemUserPreferenceID=@SystemUserPreferenceID)
	RETURN