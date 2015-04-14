/*
    Module with the model for the Item Job Status
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    // #region __________________  Item   ______________________

    // ReSharper disable once InconsistentNaming
     Item = function (specifiedItemId, specifiedEstimateId, specifiedCode, specifiedCompanyName, specifiedProductName, specifiedQty1, specifiedStatusId,
         specifiedJobEstimatedCompletionDateTime, specifiedQty1NetTotal) {

         var self,
             id = ko.observable(specifiedItemId),
             estimateId = ko.observable(specifiedEstimateId),
             code = ko.observable(specifiedCode),
             companyName = ko.observable(specifiedCompanyName),
             productName = ko.observable(specifiedProductName),
             qty1 = ko.observable(specifiedQty1),
             statusId = ko.observable(specifiedStatusId),
             jobEstimatedCompletionDateTime = ko.observable((specifiedJobEstimatedCompletionDateTime !== null && specifiedJobEstimatedCompletionDateTime !== undefined) ? moment(specifiedJobEstimatedCompletionDateTime, ist.datePattern).toDate() : undefined),
             qty1NetTotal = ko.observable(specifiedQty1NetTotal),
             convertToServerData = function () {
                 return {
                     ItemId: id(),
                     StatusId: statusId()
                 }
             };

         self = {
             id: id,
             estimateId: estimateId,
             code: code,
             companyName: companyName,
             productName: productName,
             qty1: qty1,
             statusId: statusId,
             jobEstimatedCompletionDateTime: jobEstimatedCompletionDateTime,
             qty1NetTotal: qty1NetTotal,
             convertToServerData:convertToServerData
         };
         return self;
     };

    Item.Create = function (source) {
        return new Item(source.ItemId, source.EstimateId, source.Code, source.ComparnyName, source.ProductName, source.Qty1, source.StatusId, source.JobEstimatedCompletionDateTime,
            source.Qty1NetTotal);

    };
    // #endregion __________________  Item   ______________________


    return {
        Item: Item
    };
});