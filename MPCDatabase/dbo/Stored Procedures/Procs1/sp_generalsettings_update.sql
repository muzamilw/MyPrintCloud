CREATE PROCEDURE dbo.sp_generalsettings_update
(@AttachmentAddress text,
@SystemLengthUnit int,
@SystemWeightUnit int,
@IsShowGoodsForm bit,
@IsRoundUp bit,
@IsJobDescriptionAsInvoiceDescription bit,
@IsEstimateTitleMandatory bit,
@CompanyID int

)
                  AS
Update tbl_general_settings set AttachmentAddress=@AttachmentAddress,SystemLengthUnit=@SystemLengthUnit,
SystemWeightUnit=@SystemWeightUnit,IsShowGoodsForm=@IsShowGoodsForm,IsRoundUp=@IsRoundUp,
IsJobDescriptionAsInvoiceDescription=@IsJobDescriptionAsInvoiceDescription,
IsEstimateTitleMandatory=@IsEstimateTitleMandatory where CompanyID=@CompanyID
return