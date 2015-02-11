CREATE PROCEDURE dbo.sp_Campaigns_Update_Campaign

	@CampaignID Int,@Private Bit,@CampaignName VarChar(255),@CampaignDescription VarChar(255),
    @EnableWriteBackTab Bit,@EnableClickThruTab Bit,@EnableUnsubscribeEmail Bit,@EnableBouncedEmail Bit,
    @EnableSubscribeEmail Bit,@EnableOpenedEmail Bit,@DataSourceType Int,@SQLQuery ntext,@IncludeCustomers Bit,
    @IncludeSuppliers Bit,@IncludeProspects Bit,@IncludeKeyword Bit,@SearchKeyword varchar(200),@IncludeType Bit,
    @IncludeFlag Bit,@SendEmailTo Int,@SendToAll Int,@MessageType Int,@FromAddress VarChar(255),
    @ReturnPathAddress VarChar(255),@ReplyToAddress VarChar(255),@SubjectA VarChar(255),@HTMLMessageA NText,
    @SubjectB VarChar(255), @HTMLMessageB NText,@PlainTextMessageA NText,@PlainTextMessageB NText,
    @SendingFeatureSetSelector Int,@SMTPServer VarChar(255),@SMTPUserName VarChar(255),@SMTPPassword VarChar(255),
    @EnableSchedule Bit,@StartDateTime DateTime,@SMTPDelaySeconds Int,@EnableBadEmailFormat Bit,
    @DisableEmailFilter Bit,@WrappingCharacters Int,@EmailLogFileAddress VarChar(255),@EmailLogFileAddress2 VarChar(255),
    @EmailLogFileAddress3 VarChar(255),@EmailLogFile Bit,@IncludeName bit,@IncludeContactName bit,@IncludeAddress bit,@ClearCounters bit,@RunCampaignFor smallint
    
    
AS
	UPDATE tbl_campaigns set Private=@Private ,CampaignName=@CampaignName ,Description=@CampaignDescription ,
    EnableWriteBackTab=@EnableWriteBackTab ,EnableClickThruTab=@EnableClickThruTab ,EnableUnsubscribeEmail=@EnableUnsubscribeEmail ,EnableBouncedEmail=@EnableBouncedEmail ,
    EnableSubscribeEmail=@EnableSubscribeEmail ,EnableOpenedEmail=@EnableOpenedEmail ,DataSourceType=@DataSourceType ,SQLQuery=@SQLQuery ,IncludeCustomers=@IncludeCustomers ,
    IncludeSuppliers=@IncludeSuppliers ,IncludeProspects=@IncludeProspects ,IncludeKeyword=@IncludeKeyword ,SearchKeyword=@SearchKeyword ,IncludeType=@IncludeType ,
    IncludeFlag=@IncludeFlag ,SendEmailTo=@SendEmailTo ,SendToAll=@SendToAll ,MessageType=@MessageType ,FromAddress=@FromAddress ,
    ReturnPathAddress=@ReturnPathAddress ,ReplyToAddress=@ReplyToAddress ,SubjectA=@SubjectA ,HTMLMessageA=@HTMLMessageA ,
    SubjectB=@SubjectB , HTMLMessageB=@HTMLMessageB ,PlainTextMessageA=@PlainTextMessageA ,PlainTextMessageB=@PlainTextMessageB ,
    SendingFeatureSetSelector=@SendingFeatureSetSelector ,SMTPServer=@SMTPServer ,SMTPUserName=@SMTPUserName ,SMTPPassword=@SMTPPassword ,
    EnableSchedule=@EnableSchedule ,StartDateTime=@StartDateTime ,SMTPDelaySeconds=@SMTPDelaySeconds ,EnableBadEmailFormat=@EnableBadEmailFormat ,
    DisableEmailFilter=@DisableEmailFilter ,WrappingCharacters=@WrappingCharacters ,EmailLogFileAddress=@EmailLogFileAddress ,EmailLogFileAddress2=@EmailLogFileAddress2 ,
    EmailLogFileAddress3=@EmailLogFileAddress3 ,EmailLogFile=@EmailLogFile,
    IncludeName = @IncludeName , IncludeContactName =@IncludeContactName , IncludeAddress =@IncludeAddress,
    ClearCounters=@ClearCounters,RunCampaignFor =@RunCampaignFor
                
	where tbl_campaigns.CampaignID=@CampaignID                
	RETURN