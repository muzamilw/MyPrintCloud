using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class CompanyResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// Count of new Orders
        /// </summary>
        public int NewOrdersCount { get; set; }

        /// <summary>
        /// Count of new Users
        /// </summary>
        public int NewUsersCount { get; set; }

        /// <summary>
        /// List of Companies
        /// </summary>
        public IEnumerable<CompanyListViewModel> Companies { get; set; }

        /// <summary>
        /// Company To be editted
        /// </summary>
        public Company Company { get; set; }
        /// <summary>
        /// Took List of Com. Territories on editting of any company
        /// </summary>
        public CompanyTerritoryResponse CompanyTerritoryResponse { get; set; }
        /// <summary>
        /// Took List of Addresses Territories on editting of any company
        /// </summary>
        public AddressResponse AddressResponse { get; set; }
        /// <summary>
        /// Took List of Contact Companies on editting of any company
        /// </summary>
        public CompanyContactResponse CompanyContactResponse { get; set; }

        /// <summary>
        /// Secondary Page Response
        /// </summary>
        public SecondaryPageResponse SecondaryPageResponse { get; set; }
        /// <summary>
        /// Items/ Products
        /// </summary>
        public ItemSearchResponse Items { get; set; }

        public IEnumerable<SkinForTheme> Themes { get; set; }

        public string CurrencySymbol { get; set; }
    }
}