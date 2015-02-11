CREATE PROCEDURE [dbo].[sp_Campaigns_Insert_Campaign]

	@UID Int,@Private Bit,@CampaignName VarChar(255),@CampaignDescription VarChar(255),
    @EnableWriteBackTab Bit,@EnableClickThruTab Bit,@EnableUnsubscribeEmail Bit,@EnableBouncedEmail Bit,
    @EnableSubscribeEmail Bit,@EnableOpenedEmail Bit,@DataSourceType Int,@SQLQuery ntext,@IncludeCustomers Bit,
    @IncludeSuppliers Bit,@IncludeProspects Bit,@IncludeKeyword Bit,@SearchKeyword varchar(200),@IncludeType Bit,
    @IncludeFlag Bit,@SendEmailTo Int,@SendToAll Int,@MessageType Int,@FromAddress VarChar(255),
    @ReturnPathAddress VarChar(255),@ReplyToAddress VarChar(255),@SubjectA VarChar(255),@HTMLMessageA NText,
    @SubjectB VarChar(255), @HTMLMessageB NText,@PlainTextMessageA NText,@PlainTextMessageB NText,
    @SendingFeatureSetSelector Int,@SMTPServer VarChar(255),@SMTPUserName VarChar(255),@SMTPPassword VarChar(255),
    @EnableSchedule Bit,@StartDateTime DateTime,@SMTPDelaySeconds Int,@EnableBadEmailFormat Bit,
    @DisableEmailFilter Bit,@WrappingCharacters Int,@EmailLogFileAddress VarChar(255),@EmailLogFileAddress2 VarChar(255),
    @EmailLogFileAddress3 VarChar(255),@EmailLogFile Bit,@IncludeName bit,@IncludeContactName bit,@IncludeAddress bit,
    @ClearCounters bit,@CreationDate datetime,@CampaignType smallint,@RunCampaignFor smallint,@SystemSiteID int
    
AS
INSERT INTO tbl_campaigns (UID,Private,CampaignName,Description,
    EnableWriteBackTab,EnableClickThruTab,EnableUnsubscribeEmail,EnableBouncedEmail,
    EnableSubscribeEmail,EnableOpenedEmail,DataSourceType,SQLQuery,IncludeCustomers,
    IncludeSuppliers,IncludeProspects,IncludeKeyword,SearchKeyword,IncludeType,
    IncludeFlag,SendEmailTo,SendToAll,MessageType,FromAddress,
    ReturnPathAddress,ReplyToAddress,SubjectA,HTMLMessageA,
    SubjectB, HTMLMessageB,PlainTextMessageA,PlainTextMessageB,
    SendingFeatureSetSelector,SMTPServer,SMTPUserName,SMTPPassword,
    EnableSchedule,StartDateTime,SMTPDelaySeconds,EnableBadEmailFormat,
    DisableEmailFilter,WrappingCharacters,EmailLogFileAddress,EmailLogFileAddress2,
    EmailLogFileAddress3,EmailLogFile,IncludeName ,IncludeContactName ,IncludeAddress,ClearCounters,CreationDate,CampaignType,RunCampaignFor,SystemSiteID,IsSystemEmail) VALUES
    (@UID ,@Private ,@CampaignName ,@CampaignDescription ,
    @EnableWriteBackTab ,@EnableClickThruTab ,@EnableUnsubscribeEmail ,@EnableBouncedEmail ,
    @EnableSubscribeEmail ,@EnableOpenedEmail ,@DataSourceType ,@SQLQuery ,@IncludeCustomers ,
    @IncludeSuppliers ,@IncludeProspects ,@IncludeKeyword ,@SearchKeyword ,@IncludeType ,
    @IncludeFlag ,@SendEmailTo ,@SendToAll ,@MessageType ,@FromAddress ,
    @ReturnPathAddress ,@ReplyToAddress ,@SubjectA ,@HTMLMessageA ,
    @SubjectB , @HTMLMessageB ,@PlainTextMessageA ,@PlainTextMessageB ,
    @SendingFeatureSetSelector ,@SMTPServer ,@SMTPUserName ,@SMTPPassword ,
    @EnableSchedule ,@StartDateTime ,@SMTPDelaySeconds ,@EnableBadEmailFormat ,
    @DisableEmailFilter ,@WrappingCharacters ,@EmailLogFileAddress ,@EmailLogFileAddress2 ,
    @EmailLogFileAddress3 ,@EmailLogFile,@IncludeName ,@IncludeContactName ,@IncludeAddress,@ClearCounters ,@CreationDate,@CampaignType,@RunCampaignFor,@SystemSiteID,0);SELECT @@Identity AS CampaignID
    
	RETURN