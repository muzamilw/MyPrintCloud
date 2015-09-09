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
             qty1 = ko.observable(specifiedQty1 || 0),
             statusId = ko.observable(specifiedStatusId),
             isDirectSale = ko.observable(specifiedisDirectSale),
             estimateDate = ko.observable(specifiedEstimateDate),
             statusName = ko.observable(specifiedStatusName),
             jobManagerId = ko.observable(specifiedJobManagerId),
             jobManagerName = ko.observable(),
             jobCode = ko.observable(specifiedJobCode),
             companyId = ko.observable(specifiedCompanyId),
             isSelected = ko.observable(false),
             itemAttachments = ko.observableArray([]),
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
             jobManagerName: jobManagerName,
             jobCode: jobCode,
             isSelected: isSelected,
             jobManagerId: jobManagerId,
             companyId: companyId,
             itemAttachments: itemAttachments,
             convertToServerData: convertToServerData
         };
         return self;
     };

    ItemAttachment = function (specifiedFileType) {
        var self,
            fileType = ko.observable(specifiedFileType || "");
        self = {
            fileType: fileType
        };

        return self;
    },
    ItemAttachment.Create = function (source) {
        return new ItemAttachment(source.FileType);
    }

    Item.Create = function (source) {
        var item = new Item(source.ItemId, source.EstimateId, source.CompanyName, source.ProductName, source.Qty1, source.JobStatusId, '',
            source.JobManagerId, source.isDirectSale, source.EstimateDate, source.JobCode, source.CompanyId);

        _.each(source.ItemAttachments, function (attach) {
            item.itemAttachments.push(ItemAttachment.Create(attach));
        });
        return item;
    };
    // #endregion __________________  Item   ______________________


    return {
        Item: Item
    };
});