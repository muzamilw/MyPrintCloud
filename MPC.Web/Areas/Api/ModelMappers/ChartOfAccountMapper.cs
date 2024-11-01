﻿using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Chart Of Account Mapper
    /// </summary>
    public static class ChartOfAccountMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ChartOfAccount CreateFrom(this DomainModels.ChartOfAccount source)
        {
            return new ChartOfAccount
            {
                Id = source.Id,
                Name = source.Name,
                AccountNo = source.AccountNo,
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.ChartOfAccount CreateFrom(this ChartOfAccount source)
        {
            return new DomainModels.ChartOfAccount
            {
                Id = source.Id,
                Name = source.Name,
                AccountNo = source.AccountNo,
            };
        }

        #endregion
    }
}