﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Data.Mapping.EntityViewGenerationAttribute(typeof(Edm_EntityMappingGeneratedViews.ViewsForBaseEntitySets0766735C9C473A44ACBE6BCCEADEF1A3B25B441BCEA25CB0B996C80235403D8F))]

namespace Edm_EntityMappingGeneratedViews
{
    
    
    /// <Summary>
    /// The type contains views for EntitySets and AssociationSets that were generated at design time.
    /// </Summary>
    public sealed class ViewsForBaseEntitySets0766735C9C473A44ACBE6BCCEADEF1A3B25B441BCEA25CB0B996C80235403D8F : System.Data.Mapping.EntityViewContainer
    {
        
        /// <Summary>
        /// The constructor stores the views for the extents and also the hash values generated based on the metadata and mapping closure and views.
        /// </Summary>
        public ViewsForBaseEntitySets0766735C9C473A44ACBE6BCCEADEF1A3B25B441BCEA25CB0B996C80235403D8F()
        {
            this.EdmEntityContainerName = "BaseDbContext";
            this.StoreEntityContainerName = "DomainModelsStoreContainer";
            this.HashOverMappingClosure = "b602cfc7e6b5a3898858b9ff638823e4380316ffb52125a7cc31378ef11ec135";
            this.HashOverAllExtentViews = "265b2aef2f8646c32dadaf295af317a3cabdc57c277ef099619604d9e5d5208e";
            this.ViewCount = 8;
        }
        
        /// <Summary>
        /// The method returns the view for the index given.
        /// </Summary>
        protected override System.Collections.Generic.KeyValuePair<string, string> GetViewAt(int index)
        {
            if ((index == 0))
            {
                return GetView0();
            }
            if ((index == 1))
            {
                return GetView1();
            }
            if ((index == 2))
            {
                return GetView2();
            }
            if ((index == 3))
            {
                return GetView3();
            }
            if ((index == 4))
            {
                return GetView4();
            }
            if ((index == 5))
            {
                return GetView5();
            }
            if ((index == 6))
            {
                return GetView6();
            }
            if ((index == 7))
            {
                return GetView7();
            }
            throw new System.IndexOutOfRangeException();
        }
        
        /// <Summary>
        /// return view for DomainModelsStoreContainer.tbl_chartofaccount
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView0()
        {
            return new System.Collections.Generic.KeyValuePair<string, string>("DomainModelsStoreContainer.tbl_chartofaccount", "\r\n    SELECT VALUE -- Constructing tbl_chartofaccount\r\n        [DomainModels.Stor" +
                    "e.tbl_chartofaccount](T1.[tbl_chartofaccount.ID], T1.[tbl_chartofaccount.Account" +
                    "No], T1.[tbl_chartofaccount.Name], T1.[tbl_chartofaccount.OpeningBalance], T1.[t" +
                    "bl_chartofaccount.OpeningBalanceType], T1.[tbl_chartofaccount.TypeID], T1.[tbl_c" +
                    "hartofaccount.SubTypeID], T1.[tbl_chartofaccount.Description], T1.[tbl_chartofac" +
                    "count.Nature], T1.[tbl_chartofaccount.IsActive], T1.[tbl_chartofaccount.IsFixed]" +
                    ", T1.[tbl_chartofaccount.LastActivityDate], T1.[tbl_chartofaccount.IsForReconcil" +
                    "iation], T1.[tbl_chartofaccount.Balance], T1.[tbl_chartofaccount.IsRead], T1.[tb" +
                    "l_chartofaccount.SystemSiteID], T1.[tbl_chartofaccount.UserDomainKey])\r\n    FROM" +
                    " (\r\n        SELECT \r\n            T.Id AS [tbl_chartofaccount.ID], \r\n            " +
                    "T.AccountNo AS [tbl_chartofaccount.AccountNo], \r\n            T.Name AS [tbl_char" +
                    "tofaccount.Name], \r\n            T.OpeningBalance AS [tbl_chartofaccount.OpeningB" +
                    "alance], \r\n            T.OpeningBalanceType AS [tbl_chartofaccount.OpeningBalanc" +
                    "eType], \r\n            T.TypeId AS [tbl_chartofaccount.TypeID], \r\n            T.S" +
                    "ubTypeId AS [tbl_chartofaccount.SubTypeID], \r\n            T.Description AS [tbl_" +
                    "chartofaccount.Description], \r\n            T.Nature AS [tbl_chartofaccount.Natur" +
                    "e], \r\n            T.IsActive AS [tbl_chartofaccount.IsActive], \r\n            T.I" +
                    "sFixed AS [tbl_chartofaccount.IsFixed], \r\n            T.LastActivityDate AS [tbl" +
                    "_chartofaccount.LastActivityDate], \r\n            T.IsForReconciliation AS [tbl_c" +
                    "hartofaccount.IsForReconciliation], \r\n            T.Balance AS [tbl_chartofaccou" +
                    "nt.Balance], \r\n            T.IsRead AS [tbl_chartofaccount.IsRead], \r\n          " +
                    "  T.SystemSiteId AS [tbl_chartofaccount.SystemSiteID], \r\n            T.UserDomai" +
                    "nKey AS [tbl_chartofaccount.UserDomainKey], \r\n            True AS _from0\r\n      " +
                    "  FROM BaseDbContext.ChartOfAccounts AS T\r\n    ) AS T1");
        }
        
        /// <Summary>
        /// return view for BaseDbContext.ChartOfAccounts
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView1()
        {
            return new System.Collections.Generic.KeyValuePair<string, string>("BaseDbContext.ChartOfAccounts", "\r\n    SELECT VALUE -- Constructing ChartOfAccounts\r\n        [DomainModels.ChartOf" +
                    "Account](T1.ChartOfAccount_Id, T1.ChartOfAccount_AccountNo, T1.ChartOfAccount_Na" +
                    "me, T1.ChartOfAccount_OpeningBalance, T1.ChartOfAccount_OpeningBalanceType, T1.C" +
                    "hartOfAccount_TypeId, T1.ChartOfAccount_SubTypeId, T1.ChartOfAccount_Description" +
                    ", T1.ChartOfAccount_Nature, T1.ChartOfAccount_IsActive, T1.ChartOfAccount_IsFixe" +
                    "d, T1.ChartOfAccount_LastActivityDate, T1.ChartOfAccount_IsForReconciliation, T1" +
                    ".ChartOfAccount_Balance, T1.ChartOfAccount_IsRead, T1.ChartOfAccount_SystemSiteI" +
                    "d, T1.ChartOfAccount_UserDomainKey)\r\n    FROM (\r\n        SELECT \r\n            T." +
                    "ID AS ChartOfAccount_Id, \r\n            T.AccountNo AS ChartOfAccount_AccountNo, " +
                    "\r\n            T.Name AS ChartOfAccount_Name, \r\n            T.OpeningBalance AS C" +
                    "hartOfAccount_OpeningBalance, \r\n            T.OpeningBalanceType AS ChartOfAccou" +
                    "nt_OpeningBalanceType, \r\n            T.TypeID AS ChartOfAccount_TypeId, \r\n      " +
                    "      T.SubTypeID AS ChartOfAccount_SubTypeId, \r\n            T.Description AS Ch" +
                    "artOfAccount_Description, \r\n            T.Nature AS ChartOfAccount_Nature, \r\n   " +
                    "         T.IsActive AS ChartOfAccount_IsActive, \r\n            T.IsFixed AS Chart" +
                    "OfAccount_IsFixed, \r\n            T.LastActivityDate AS ChartOfAccount_LastActivi" +
                    "tyDate, \r\n            T.IsForReconciliation AS ChartOfAccount_IsForReconciliatio" +
                    "n, \r\n            T.Balance AS ChartOfAccount_Balance, \r\n            T.IsRead AS " +
                    "ChartOfAccount_IsRead, \r\n            T.SystemSiteID AS ChartOfAccount_SystemSite" +
                    "Id, \r\n            T.UserDomainKey AS ChartOfAccount_UserDomainKey, \r\n           " +
                    " True AS _from0\r\n        FROM DomainModelsStoreContainer.tbl_chartofaccount AS T" +
                    "\r\n    ) AS T1");
        }
        
        /// <Summary>
        /// return view for DomainModelsStoreContainer.tbl_taxrate
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView2()
        {
            return new System.Collections.Generic.KeyValuePair<string, string>("DomainModelsStoreContainer.tbl_taxrate", @"
    SELECT VALUE -- Constructing tbl_taxrate
        [DomainModels.Store.tbl_taxrate](T1.[tbl_taxrate.TaxID], T1.[tbl_taxrate.TaxCode], T1.[tbl_taxrate.TaxName], T1.[tbl_taxrate.SourceStateID], T1.[tbl_taxrate.DestinationStateID], T1.[tbl_taxrate.Tax1], T1.[tbl_taxrate.Tax2], T1.[tbl_taxrate.Tax3], T1.[tbl_taxrate.IsFixed], T1.[tbl_taxrate.UserDomainKey])
    FROM (
        SELECT 
            T.TaxId AS [tbl_taxrate.TaxID], 
            T.TaxCode AS [tbl_taxrate.TaxCode], 
            T.TaxName AS [tbl_taxrate.TaxName], 
            T.SourceStateId AS [tbl_taxrate.SourceStateID], 
            T.DestinationStateId AS [tbl_taxrate.DestinationStateID], 
            T.Tax1 AS [tbl_taxrate.Tax1], 
            T.Tax2 AS [tbl_taxrate.Tax2], 
            T.Tax3 AS [tbl_taxrate.Tax3], 
            T.IsFixed AS [tbl_taxrate.IsFixed], 
            T.UserDomainKey AS [tbl_taxrate.UserDomainKey], 
            True AS _from0
        FROM BaseDbContext.TaxRates AS T
    ) AS T1");
        }
        
        /// <Summary>
        /// return view for BaseDbContext.TaxRates
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView3()
        {
            return new System.Collections.Generic.KeyValuePair<string, string>("BaseDbContext.TaxRates", @"
    SELECT VALUE -- Constructing TaxRates
        [DomainModels.TaxRate](T1.TaxRate_TaxId, T1.TaxRate_TaxCode, T1.TaxRate_TaxName, T1.TaxRate_SourceStateId, T1.TaxRate_DestinationStateId, T1.TaxRate_Tax1, T1.TaxRate_Tax2, T1.TaxRate_Tax3, T1.TaxRate_IsFixed, T1.TaxRate_UserDomainKey)
    FROM (
        SELECT 
            T.TaxID AS TaxRate_TaxId, 
            T.TaxCode AS TaxRate_TaxCode, 
            T.TaxName AS TaxRate_TaxName, 
            T.SourceStateID AS TaxRate_SourceStateId, 
            T.DestinationStateID AS TaxRate_DestinationStateId, 
            T.Tax1 AS TaxRate_Tax1, 
            T.Tax2 AS TaxRate_Tax2, 
            T.Tax3 AS TaxRate_Tax3, 
            T.IsFixed AS TaxRate_IsFixed, 
            T.UserDomainKey AS TaxRate_UserDomainKey, 
            True AS _from0
        FROM DomainModelsStoreContainer.tbl_taxrate AS T
    ) AS T1");
        }
        
        /// <Summary>
        /// return view for DomainModelsStoreContainer.Markup
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView4()
        {
            return new System.Collections.Generic.KeyValuePair<string, string>("DomainModelsStoreContainer.Markup", @"
    SELECT VALUE -- Constructing Markup
        [DomainModels.Store.Markup](T1.Markup_MarkUpID, T1.Markup_MarkUpName, T1.Markup_MarkUpRate, T1.Markup_IsFixed, T1.Markup_IsDefault, T1.Markup_UserDomainKey)
    FROM (
        SELECT 
            T.MarkUpId AS Markup_MarkUpID, 
            T.MarkUpName AS Markup_MarkUpName, 
            T.MarkUpRate AS Markup_MarkUpRate, 
            T.IsFixed AS Markup_IsFixed, 
            T.IsDefault AS Markup_IsDefault, 
            T.UserDomainKey AS Markup_UserDomainKey, 
            True AS _from0
        FROM BaseDbContext.Markups AS T
    ) AS T1");
        }
        
        /// <Summary>
        /// return view for BaseDbContext.Markups
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView5()
        {
            return new System.Collections.Generic.KeyValuePair<string, string>("BaseDbContext.Markups", @"
    SELECT VALUE -- Constructing Markups
        [DomainModels.Markup](T1.Markup_MarkUpId, T1.Markup_MarkUpName, T1.Markup_MarkUpRate, T1.Markup_IsFixed, T1.Markup_IsDefault, T1.Markup_UserDomainKey)
    FROM (
        SELECT 
            T.MarkUpID AS Markup_MarkUpId, 
            T.MarkUpName AS Markup_MarkUpName, 
            T.MarkUpRate AS Markup_MarkUpRate, 
            T.IsFixed AS Markup_IsFixed, 
            T.IsDefault AS Markup_IsDefault, 
            T.UserDomainKey AS Markup_UserDomainKey, 
            True AS _from0
        FROM DomainModelsStoreContainer.Markup AS T
    ) AS T1");
        }
        
        /// <Summary>
        /// return view for DomainModelsStoreContainer.Organisation
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView6()
        {
            System.Text.StringBuilder viewString = new System.Text.StringBuilder(2519);
            viewString.Append("\r\n    SELECT VALUE -- Constructing Organisation\r\n        [DomainModels.Store.Org");
            viewString.Append("anisation](T1.Organisation_OrganisationID, T1.Organisation_OrganisationName, T1.");
            viewString.Append("Organisation_Address1, T1.Organisation_Address2, T1.Organisation_Address3, T1.Or");
            viewString.Append("ganisation_City, T1.Organisation_State, T1.Organisation_Country, T1.Organisation");
            viewString.Append("_ZipCode, T1.Organisation_Tel, T1.Organisation_Fax, T1.Organisation_Mobile, T1.O");
            viewString.Append("rganisation_Email, T1.Organisation_URL, T1.Organisation_WebsiteLogo, T1.Organisa");
            viewString.Append("tion_MISLogo, T1.Organisation_TaxRegistrationNo, T1.Organisation_LicenseLevel, T");
            viewString.Append("1.Organisation_CustomerAccountNumber, T1.Organisation_SmtpServer, T1.Organisatio");
            viewString.Append("n_SmtpUserName, T1.Organisation_SmtpPassword, T1.Organisation_VATRegNumber, T1.O");
            viewString.Append("rganisation_SystemLengthUnit, T1.Organisation_SystemWeightUnit, T1.Organisation_");
            viewString.Append("CurrencyID, T1.Organisation_LanguageID, T1.Organisation_UserDomainKey)\r\n    FROM");
            viewString.Append(" (\r\n        SELECT \r\n            T.OrganisationId AS Organisation_OrganisationID");
            viewString.Append(", \r\n            T.OrganisationName AS Organisation_OrganisationName, \r\n         ");
            viewString.Append("   T.Address1 AS Organisation_Address1, \r\n            T.Address2 AS Organisation");
            viewString.Append("_Address2, \r\n            T.Address3 AS Organisation_Address3, \r\n            T.Ci");
            viewString.Append("ty AS Organisation_City, \r\n            T.State AS Organisation_State, \r\n        ");
            viewString.Append("    T.Country AS Organisation_Country, \r\n            T.ZipCode AS Organisation_Z");
            viewString.Append("ipCode, \r\n            T.Tel AS Organisation_Tel, \r\n            T.Fax AS Organisa");
            viewString.Append("tion_Fax, \r\n            T.Mobile AS Organisation_Mobile, \r\n            T.Email A");
            viewString.Append("S Organisation_Email, \r\n            T.URL AS Organisation_URL, \r\n            T.W");
            viewString.Append("ebsiteLogo AS Organisation_WebsiteLogo, \r\n            T.MISLogo AS Organisation_");
            viewString.Append("MISLogo, \r\n            T.TaxRegistrationNo AS Organisation_TaxRegistrationNo, \r\n");
            viewString.Append("            T.LicenseLevel AS Organisation_LicenseLevel, \r\n            T.Custome");
            viewString.Append("rAccountNumber AS Organisation_CustomerAccountNumber, \r\n            T.SmtpServer");
            viewString.Append(" AS Organisation_SmtpServer, \r\n            T.SmtpUserName AS Organisation_SmtpUs");
            viewString.Append("erName, \r\n            T.SmtpPassword AS Organisation_SmtpPassword, \r\n           ");
            viewString.Append(" T.VATRegNumber AS Organisation_VATRegNumber, \r\n            T.SystemLengthUnit A");
            viewString.Append("S Organisation_SystemLengthUnit, \r\n            T.SystemWeightUnit AS Organisatio");
            viewString.Append("n_SystemWeightUnit, \r\n            T.CurrencyId AS Organisation_CurrencyID, \r\n   ");
            viewString.Append("         T.LanguageId AS Organisation_LanguageID, \r\n            T.UserDomainKey ");
            viewString.Append("AS Organisation_UserDomainKey, \r\n            True AS _from0\r\n        FROM BaseDb");
            viewString.Append("Context.Organisations AS T\r\n    ) AS T1");
            return new System.Collections.Generic.KeyValuePair<string, string>("DomainModelsStoreContainer.Organisation", viewString.ToString());
        }
        
        /// <Summary>
        /// return view for BaseDbContext.Organisations
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView7()
        {
            System.Text.StringBuilder viewString = new System.Text.StringBuilder(2526);
            viewString.Append("\r\n    SELECT VALUE -- Constructing Organisations\r\n        [DomainModels.Organisa");
            viewString.Append("tion](T1.Organisation_OrganisationId, T1.Organisation_OrganisationName, T1.Organ");
            viewString.Append("isation_Address1, T1.Organisation_Address2, T1.Organisation_Address3, T1.Organis");
            viewString.Append("ation_City, T1.Organisation_State, T1.Organisation_Country, T1.Organisation_ZipC");
            viewString.Append("ode, T1.Organisation_Tel, T1.Organisation_Fax, T1.Organisation_Mobile, T1.Organi");
            viewString.Append("sation_Email, T1.Organisation_URL, T1.Organisation_WebsiteLogo, T1.Organisation_");
            viewString.Append("MISLogo, T1.Organisation_TaxRegistrationNo, T1.Organisation_LicenseLevel, T1.Org");
            viewString.Append("anisation_CustomerAccountNumber, T1.Organisation_SmtpServer, T1.Organisation_Smt");
            viewString.Append("pUserName, T1.Organisation_SmtpPassword, T1.Organisation_VATRegNumber, T1.Organi");
            viewString.Append("sation_SystemLengthUnit, T1.Organisation_SystemWeightUnit, T1.Organisation_Curre");
            viewString.Append("ncyId, T1.Organisation_LanguageId, T1.Organisation_UserDomainKey)\r\n    FROM (\r\n ");
            viewString.Append("       SELECT \r\n            T.OrganisationID AS Organisation_OrganisationId, \r\n ");
            viewString.Append("           T.OrganisationName AS Organisation_OrganisationName, \r\n            T.");
            viewString.Append("Address1 AS Organisation_Address1, \r\n            T.Address2 AS Organisation_Addr");
            viewString.Append("ess2, \r\n            T.Address3 AS Organisation_Address3, \r\n            T.City AS");
            viewString.Append(" Organisation_City, \r\n            T.State AS Organisation_State, \r\n            T");
            viewString.Append(".Country AS Organisation_Country, \r\n            T.ZipCode AS Organisation_ZipCod");
            viewString.Append("e, \r\n            T.Tel AS Organisation_Tel, \r\n            T.Fax AS Organisation_");
            viewString.Append("Fax, \r\n            T.Mobile AS Organisation_Mobile, \r\n            T.Email AS Org");
            viewString.Append("anisation_Email, \r\n            T.URL AS Organisation_URL, \r\n            T.Websit");
            viewString.Append("eLogo AS Organisation_WebsiteLogo, \r\n            T.MISLogo AS Organisation_MISLo");
            viewString.Append("go, \r\n            T.TaxRegistrationNo AS Organisation_TaxRegistrationNo, \r\n     ");
            viewString.Append("       T.LicenseLevel AS Organisation_LicenseLevel, \r\n            T.CustomerAcco");
            viewString.Append("untNumber AS Organisation_CustomerAccountNumber, \r\n            T.SmtpServer AS O");
            viewString.Append("rganisation_SmtpServer, \r\n            T.SmtpUserName AS Organisation_SmtpUserNam");
            viewString.Append("e, \r\n            T.SmtpPassword AS Organisation_SmtpPassword, \r\n            T.VA");
            viewString.Append("TRegNumber AS Organisation_VATRegNumber, \r\n            T.SystemLengthUnit AS Org");
            viewString.Append("anisation_SystemLengthUnit, \r\n            T.SystemWeightUnit AS Organisation_Sys");
            viewString.Append("temWeightUnit, \r\n            T.CurrencyID AS Organisation_CurrencyId, \r\n        ");
            viewString.Append("    T.LanguageID AS Organisation_LanguageId, \r\n            T.UserDomainKey AS Or");
            viewString.Append("ganisation_UserDomainKey, \r\n            True AS _from0\r\n        FROM DomainModel");
            viewString.Append("sStoreContainer.Organisation AS T\r\n    ) AS T1");
            return new System.Collections.Generic.KeyValuePair<string, string>("BaseDbContext.Organisations", viewString.ToString());
        }
    }
}


