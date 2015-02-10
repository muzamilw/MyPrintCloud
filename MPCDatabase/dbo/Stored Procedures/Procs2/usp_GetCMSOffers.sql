CREATE PROCEDURE [dbo].[usp_GetCMSOffers]

AS
BEGIN
		select top 1 OfferID, SpecImage1, SpecImage1Name, 
				SpecImage2, SpecImage2Name, SpecImage3, SpecImage3Name, 
				SpecImage4, SpecImage4Name, SpecImage5, SpecImage5Name, 
				ProImage1, ProImage1Name, ProImage2, ProImage2Name, 
				ProImage3, ProImage3Name, ProImage4, ProImage4Name, 
				ProImage5, ProImage5Name, AtciveInd
		from	tbl_cmsoffers
		where	AtciveInd = 1
		
END