/*
    Module with the model for the Live Jobs
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    // #region __________________  Item   ______________________

    // ReSharper disable once InconsistentNaming
     Item = function (specifiedItemId, specifiedEstimateId, specifiedCompanyName, specifiedProductName, specifiedQty1, specifiedStatusId,
         specifiedStatusName, specifiedJobManagerId, specifiedisDirectSale, specifiedEstimateDate, specifiedJobCode, specifiedCompanyId) {

         var self,
             id = ko.observable(specifiedItemId),
             estimateId = ko.observable(specifiedEstimateId),
              companyName = ko.observable(specifiedCompanyName),
             productName = ko.observable(specifiedProductName),
             qty1 = ko.observable(specifiedQty1),
             statusId = ko.observable(specifiedStatusId),
             isDirectSale = ko.observable(specifiedisDirectSale),
             estimateDate = ko.observable(specifiedEstimateDate),
             statusName = ko.observable(specifiedStatusName),
             jobManagerId = ko.observable(specifiedJobManagerId),
             jobCode = ko.observable(specifiedJobCode),
             companyId = ko.observable(specifiedCompanyId),
             isSelected = ko.observable(false),
             convertToServerData = function () {
                 return {
                     ItemId: id(),
                     StatusId: statusId()
                 }
             };

         self = {
             id: id,
             estimateId: estimateId,
             companyName: companyName,
             productName: productName,
             qty1: qty1,
             statusId: statusId,
             statusName: statusName,
             isDirectSale: isDirectSale,
             estimateDate: estimateDate,
             jobCode: jobCode,
             isSelected: isSelected,
             companyId: companyId,
             convertToServerData: convertToServerData
         };
         return self;
     };

    Item.Create = function (source) {
        return new Item(source.ItemId, source.EstimateId, source.CompanyName, source.ProductName, source.Qty1, source.StatusId, source.StatusName,
            source.JobManagerId, source.isDirectSale, source.EstimateDate, source.JobCode, source.CompanyId);

    };
    // #endregion __________________  Item   ______________________


    return {
        Item: Item
    };
});