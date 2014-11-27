﻿using System;
using System.Linq;
using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Models;

namespace MPC.MIS.ModelMappers
{
    public static class StockCategoryMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.StockCategory CreateFrom(this DomainModels.StockCategory source)
        {
            return new ApiModels.StockCategory
            {
                CategoryId = source.CategoryId,
                Code = source.Code,
                Name = source.Name,
                Description = source.Description,
                Fixed = source.Fixed,
                ItemWeight = source.ItemWeight,
                ItemColour = source.ItemColour,
                ItemSizeCustom = source.ItemSizeCustom,
                ItemPaperSize = source.ItemPaperSize,
                ItemCoatedType = source.ItemCoatedType,
                ItemCoated = source.ItemCoated,
                ItemExposure = source.ItemExposure,
                ItemCharge = source.ItemCharge,
                RecLock = source.RecLock,
                TaxId = source.TaxId,
                Flag1 = source.Flag1,
                Flag2 = source.Flag2,
                Flag3 = source.Flag3,
                Flag4 = source.Flag4,
                CompanyId = source.CompanyId,
                StockSubCategories = source.StockSubCategories.Select(x => x.CreateFrom()).ToList()
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.StockCategoryDropDown CreateFromDropDown(this DomainModels.StockCategory source)
        {
            return new ApiModels.StockCategoryDropDown
            {
                CategoryId = source.CategoryId,
                Name = source.Name,
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.StockCategory CreateFrom(this ApiModels.StockCategory source)
        {
            return new DomainModels.StockCategory
            {
                CategoryId = source.CategoryId,
                Code = source.Code,
                Name = source.Name,
                Description = source.Description,
                Fixed = source.Fixed,
                ItemWeight = source.ItemWeight,
                ItemColour = source.ItemColour,
                ItemSizeCustom = source.ItemSizeCustom,
                ItemPaperSize = source.ItemPaperSize,
                ItemCoatedType = source.ItemCoatedType,
                ItemCoated = source.ItemCoated,
                ItemExposure = source.ItemExposure,
                ItemCharge = source.ItemCharge,
                RecLock = source.RecLock,
                TaxId = source.TaxId,
                Flag1 = source.Flag1,
                Flag2 = source.Flag2,
                Flag3 = source.Flag3,
                Flag4 = source.Flag4,
                CompanyId = source.CompanyId
            };
        }
    }
}