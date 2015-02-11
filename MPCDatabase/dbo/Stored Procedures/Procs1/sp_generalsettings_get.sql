
CREATE PROCEDURE [dbo].[sp_generalsettings_get]
(@CompanyID int)
                  AS
SELECT tbl_general_settings.CompanyID,tbl_general_settings.ID,tbl_general_settings.AttachmentAddress,tbl_general_settings.SystemLengthUnit,tbl_general_settings.SystemWeightUnit,
    tbl_general_settings.IsShowGoodsForm,tbl_general_settings.IsRoundUp,tbl_general_settings.IsJobDescriptionAsInvoiceDescription,tbl_general_settings.IsEstimateTitleMandatory,
    tbl_weightconversion.Items as WeightName,tbl_lengthconversion.Items as LengthName, Region
    FROM tbl_lengthconversion 
    INNER JOIN tbl_general_settings ON (tbl_lengthconversion.ID = tbl_general_settings.SystemLengthUnit) 
    INNER JOIN tbl_weightconversion ON (tbl_general_settings.SystemWeightUnit = tbl_weightconversion.ID)
     where tbl_general_settings.CompanyID=@CompanyID
return