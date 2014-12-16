﻿using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Currency> DbSet
        {
            get
            {
                  return db.Currencies;
            }
        }

        public string GetCurrencyCodeById(long currencyId)
        {
            return db.Currencies.Where(c => c.CurrencyId == currencyId).Select(n => n.CurrencyCode).FirstOrDefault();
        }
    }
}