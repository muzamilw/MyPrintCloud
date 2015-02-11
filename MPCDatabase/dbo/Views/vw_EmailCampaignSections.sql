
CREATE View [dbo].[vw_EmailCampaignSections] As
	select SectionID, SectionImage, SectionName, SecOrder from section
	where SectionID in (select SectionID from Campaignemailvariable)