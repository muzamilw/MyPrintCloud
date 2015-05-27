using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Collections.Generic;
using AutoMapper;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Prefix Repository
    /// </summary>
    public class PrefixRepository : BaseRepository<Prefix>, IPrefixRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public PrefixRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Prefix> DbSet
        {
            get
            {
                return db.Prefixes;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Returns Next Job Code Prefix and increments the NextItem Value by 1
        /// </summary>
        public string GetNextJobCodePrefix(bool shouldIncrementNextItem = true)
        {
            try
            {
                Prefix prefix = DbSet.FirstOrDefault(pfx => pfx.OrganisationId == OrganisationId);
                if (prefix == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, LanguageResources.NoPrefixDefined, OrganisationId));
                }

                string nextPrefix = prefix.JobPrefix + "-001-" + prefix.JobNext;

                // Update Item Next
                prefix.JobNext += 1;

                // For Order Screen
                if (!shouldIncrementNextItem)
                {
                    return nextPrefix;
                }

                // Save Changes
                SaveChanges();

                return nextPrefix;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns Next Order Code Prefix and increments the NextItem Value by 1
        /// </summary>
        public string GetNextOrderCodePrefix()
        {
            try
            {
                Prefix prefix = DbSet.FirstOrDefault(pfx => pfx.OrganisationId == OrganisationId);
                if (prefix == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, LanguageResources.NoPrefixDefined, OrganisationId));
                }

                string nextPrefix = prefix.OrderPrefix + "-001-" + prefix.OrderNext;

                // Update Order Next
                prefix.OrderNext += 1;

                return nextPrefix;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Returns Next Delivery Note Code Prefix and increments the NextItem Value by 1
        /// </summary>
        public string GetNextDeliveryNoteCodePrefix()
        {
            try
            {
                Prefix prefix = DbSet.FirstOrDefault(pfx => pfx.OrganisationId == OrganisationId);
                if (prefix == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, LanguageResources.NoPrefixDefined, OrganisationId));
                }

                string nextPrefix = prefix.DeliveryNPrefix + "-001-" + prefix.DeliveryNNext;

                // Update Order Next
                prefix.DeliveryNNext += 1;

                return nextPrefix;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Returns Next Invoice Code Prefix and increments the NextItem Value by 1
        /// </summary>
        public string GetNextInvoiceCodePrefix()
        {
            try
            {
                Prefix prefix = DbSet.FirstOrDefault(pfx => pfx.OrganisationId == OrganisationId);
                if (prefix == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, LanguageResources.NoPrefixDefined, OrganisationId));
                }

                string nextPrefix = prefix.InvoicePrefix + "-001-" + prefix.InvoiceNext;

                // Update Invoice Next
                prefix.InvoiceNext += 1;

                return nextPrefix;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns Next Estimate Code Prefix and increments the NextItem Value by 1
        /// </summary>
        public string GetNextEstimateCodePrefix()
        {
            try
            {
                Prefix prefix = DbSet.FirstOrDefault(pfx => pfx.OrganisationId == OrganisationId);
                if (prefix == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, LanguageResources.NoPrefixDefined, OrganisationId));
                }

                string nextPrefix = prefix.EstimatePrefix + "-001-" + prefix.EstimateNext;

                // Update Estimate Next
                prefix.EstimateNext += 1;

                return nextPrefix;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Returns Next Inquiry Code Prefix and increments the NextItem Value by 1
        /// </summary>
        public string GetNextInquiryCodePrefix()
        {
            try
            {
                Prefix prefix = DbSet.FirstOrDefault(pfx => pfx.OrganisationId == OrganisationId);
                if (prefix == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, LanguageResources.NoPrefixDefined, OrganisationId));
                }

                string nextPrefix = prefix.EnquiryPrefix + "-001-" + prefix.EnquiryNext;

                // Update Enquiry Next
                prefix.EnquiryNext += 1;

                return nextPrefix;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns Next Item Code Prefix and increments the NextItem Value by 1
        /// </summary>
        public string GetNextItemCodePrefix(bool shouldIncrementNextItem = true)
        {
            try
            {
                Prefix prefix = DbSet.FirstOrDefault(pfx => pfx.OrganisationId == OrganisationId);
                if (prefix == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, LanguageResources.NoPrefixDefined, OrganisationId));
                }

                string nextPrefix = prefix.ItemPrefix + "-001-" + prefix.ItemNext;

                // Update Item Next
                prefix.ItemNext += 1;

                // For Order Screen
                if (!shouldIncrementNextItem)
                {
                    return nextPrefix;
                }

                // Save Changes
                SaveChanges();

                return nextPrefix;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Prefix GetDefaultPrefix()
        {
            try
            {
                return db.Prefixes.FirstOrDefault(c => c.SystemSiteId == 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Markup use in Prefix 
        /// </summary>
        public bool PrefixUseMarkupId(long markupId)
        {
            try
            {
                return db.Prefixes.Count(p => p.MarkupId == markupId) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Prefix> GetPrefixesByOrganisationID(long organisationID)
        {
            try
            {
                Mapper.CreateMap<Prefix, Prefix>()
                  .ForMember(x => x.Markup, opt => opt.Ignore());

                List<Prefix> prefix = db.Prefixes.Where(p => p.OrganisationId == organisationID).ToList();

                List<Prefix> oOutputPrefix = new List<Prefix>();

                if (prefix != null && prefix.Count > 0)
                {
                    foreach (var item in prefix)
                    {
                        var omappedItem = Mapper.Map<Prefix, Prefix>(item);
                        oOutputPrefix.Add(omappedItem);
                    }
                }


                return oOutputPrefix;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Prefix GetPrefixByOrganisationId(long OrgId)
        {
            try
            {
                return db.Prefixes.Where(p => p.OrganisationId == OrgId).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        #endregion



    }
}
