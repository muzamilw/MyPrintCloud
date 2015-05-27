/*
    Module with the model for the Live Jobs
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    // #region __________________  Delivery Note List View   ______________________

    // ReSharper disable once InconsistentNaming
     deliverNoteListView = function (specifieddeliveryNoteId, specifiedcode, specifieddeliveryDate, specifiedflagId, specifiedcontactCompany,
     specifiedOrderReff, specifiedCreationDateTime) {

         var self,
             deliveryNoteId = ko.observable(specifieddeliveryNoteId),
             code = ko.observable(specifiedcode),
              deliveryDate = ko.observable(specifieddeliveryDate !== null ? moment(specifieddeliveryDate).toDate() : undefined),
             flagId = ko.observable(specifiedflagId),
             contactCompany = ko.observable(specifiedcontactCompany),
             orderReff = ko.observable(specifiedOrderReff),
             creationDateTime = ko.observable(specifiedCreationDateTime !== null ? moment(specifiedCreationDateTime).toDate() : undefined),

             convertToServerData = function () {
                 return {

                 };
             };

         self = {
             deliveryNoteId: deliveryNoteId,
             code: code,
             deliveryDate: deliveryDate,
             flagId: flagId,
             contactCompany: contactCompany,
             orderReff: orderReff,
             creationDateTime: creationDateTime,
             convertToServerData: convertToServerData
         };
         return self;
     },
     // #endregion __________________  deliverNoteListView create   ______________________

     SystemUser = function (specifiedId, specifiedName, specifiedFullName) {
         return {
             id: specifiedId,
             name: specifiedName,
             fullName: specifiedFullName
         };
     },
         // Section Flag Entity        
        // ReSharper disable InconsistentNaming
        SectionFlag = function (specifiedId, specifiedFlagName, specifiedFlagColor) {
            // ReSharper restore InconsistentNaming
            return {
                id: specifiedId,
                name: specifiedFlagName,
                color: specifiedFlagColor
            };
        },

          // #region __________________  Delivery Note  ______________________
    // ReSharper disable once InconsistentNaming
     DeliveryNote = function (specifieddeliveryNoteId, specifiedcode, specifieddeliveryDate, specifiedflagId, specifiedcontactCompany,
     specifiedOrderReff, specifiedCreationDateTime, spcCompanyId, spcFootnote, spcComments, spcLockedBy, spcIsStatus, spcContactId, spcCustomerOrderReff, spcAddressId,
    spcCreatedBy, spcSupplierId, spcSupplierTelNo, spcSupplierURL, spcEstimateId, spcJobId, spcInvoiceId, spcOrderId, spcUserNotes,
    spcNotesUpdateDateTime, spcNotesUpdatedByUserId, spcSystemSiteId, spcIsRead, spcIsPrinted, spcCsNo) {

         var self,
             deliveryNoteId = ko.observable(specifieddeliveryNoteId),
             code = ko.observable(specifiedcode),
              deliveryDate = ko.observable(specifieddeliveryDate !== null ? moment(specifieddeliveryDate).toDate() : undefined),
             flagId = ko.observable(specifiedflagId),
             contactCompany = ko.observable(specifiedcontactCompany),
             orderReff = ko.observable(specifiedOrderReff),
             creationDateTime = ko.observable(specifiedCreationDateTime !== null ? moment(specifiedCreationDateTime).toDate() : undefined),
             companyId = ko.observable(spcCompanyId),
             footnote = ko.observable(spcFootnote),
              comments = ko.observable(spcComments),
             lockedBy = ko.observable(spcLockedBy),
             isStatus = ko.observable(spcIsStatus),
             contactId = ko.observable(spcContactId),
             customerOrderReff = ko.observable(spcCustomerOrderReff),

             addressId = ko.observable(spcAddressId),
             createdBy = ko.observable(spcCreatedBy),
             supplierId = ko.observable(spcSupplierId),
             supplierTelNo = ko.observable(spcSupplierTelNo),
             supplierUrl = ko.observable(spcSupplierURL),
             estimateId = ko.observable(spcEstimateId),
             jobId = ko.observable(spcJobId),

             invoiceId = ko.observable(spcInvoiceId),
             orderId = ko.observable(spcOrderId),
             userNotes = ko.observable(spcUserNotes),
             notesUpdateDateTime = ko.observable(spcNotesUpdateDateTime),
             notesUpdatedByUserId = ko.observable(spcNotesUpdatedByUserId),
             systemSiteId = ko.observable(spcSystemSiteId),
             isRead = ko.observable(spcIsRead),
             isPrinted = ko.observable(spcIsPrinted),
             companyName = ko.observable(spcIsPrinted),
             csNo = ko.observable(spcCsNo),

             convertToServerData = function () {
                 return {
                     DeliveryNoteId: deliveryNoteId,
                     Code: code,
                     DeliveryDate: deliveryDate,
                     FlagId: flagId,
                     ContactCompany: contactCompany,
                     OrderReff: orderReff,
                     CreationDateTime: creationDateTime,
                     CompanyId: companyId,
                     footnote: footnote,
                     Comments: comments,
                     LockedBy: lockedBy,
                     IsStatus: isStatus,
                     ContactId: contactId,
                     CustomerOrderReff: customerOrderReff,
                     AddressId: addressId,
                     CreatedBy: createdBy,
                     SupplierId: supplierId,
                     SupplierTelNo: supplierTelNo,
                     SupplierURL: supplierUrl,
                     EstimateId: estimateId,
                     JobId: jobId,
                     InvoiceId: invoiceId,
                     OrderId: orderId,
                     UserNotes: userNotes,
                     NotesUpdateDateTime: notesUpdateDateTime,
                     NotesUpdatedByUserId: notesUpdatedByUserId,
                     SystemSiteId: systemSiteId,
                     IsRead: isRead,
                     IsPrinted: isPrinted,
                     CsNo: csNo
                 };
             };

         self = {
             deliveryNoteId: deliveryNoteId,
             code: code,
             deliveryDate: deliveryDate,
             flagId: flagId,
             contactCompany: contactCompany,
             orderReff: orderReff,
             creationDateTime: creationDateTime,
             companyId: companyId,
             footnote: footnote,
             comments: comments,
             lockedBy: lockedBy,
             isStatus: isStatus,
             contactId: contactId,
             customerOrderReff: customerOrderReff,
             addressId: addressId,
             createdBy: createdBy,
             supplierId: supplierId,
             supplierTelNo: supplierTelNo,
             supplierUrl: supplierUrl,
             estimateId: estimateId,
             jobId: jobId,
             invoiceId: invoiceId,
             orderId: orderId,
             userNotes: userNotes,
             notesUpdateDateTime: notesUpdateDateTime,
             notesUpdatedByUserId: notesUpdatedByUserId,
             systemSiteId: systemSiteId,
             isRead: isRead,
             isPrinted: isPrinted,
             csNo: csNo,
             companyName: companyName,
             convertToServerData: convertToServerData
         };
         return self;
     },
          // Address Entity
        Address = function (specifiedId, specifiedName, specifiedAddress1, specifiedAddress2, specifiedTelephone1, specifiedIsDefault) {
            // ReSharper restore InconsistentNaming
            return {
                id: specifiedId,
                name: specifiedName,
                address1: specifiedAddress1 || "",
                address2: specifiedAddress2 || "",
                telephone1: specifiedTelephone1 || "",
                isDefault: specifiedIsDefault
            };
        }
    ,  // Company Contact Entity
        CompanyContact = function (specifiedId, specifiedName, specifiedEmail, specifiedIsDefault) {
            // ReSharper restore InconsistentNaming
            return {
                id: specifiedId,
                name: specifiedName,
                email: specifiedEmail || "",
                isDefault: specifiedIsDefault
            };
        };



    deliverNoteListView.Create = function (source) {
        return new deliverNoteListView(source.DeliveryNoteId, source.Code, source.DeliveryDate, source.FlagId, source.ContactCompany, source.OrderReff,
            source.CreationDateTime);


    };

    // Delivery Notes Factory
    DeliveryNote.Create = function (source) {
        return new DeliveryNote(source.DeliveryNoteId, source.Code, source.DeliveryDate, source.FlagId, source.ContactCompany, source.OrderReff,
         source.CreationDateTime, source.CompanyId, source.Footnote, source.Comments, source.LockedBy, source.IsStatus, source.ContactId, source.CustomerOrderReff,
            source.AddressId, source.CreatedBy, source.SupplierId, source.SupplierTelNo, source.SupplierURL, source.EstimateId, source.JobId, source.InvoiceId,
            source.OrderId, source.UserNotes, source.NotesUpdateDateTime, source.NotesUpdatedByUserId, source.SystemSiteId, source.IsRead, source.IsPrinted, source.CsNo);
    };

    // Address Factory
    Address.Create = function (source) {
        return new Address(source.AddressId, source.AddressName, source.Address1, source.Address2, source.Tel1, source.IsDefaultAddress);
    };
    // Section Flag Factory
    SectionFlag.Create = function (source) {
        return new SectionFlag(source.SectionFlagId, source.FlagName, source.FlagColor);
    };
    // System User Factory
    SystemUser.Create = function (source) {
        return new SystemUser(source.SystemUserId, source.UserName, source.FullName);
    };

    // Company Contact Factory
    CompanyContact.Create = function (source) {
        return new CompanyContact(source.ContactId, source.Name, source.Email, source.IsDefaultContact);
    };
    return {
        deliverNoteListView: deliverNoteListView,
        Address: Address,
        CompanyContact: CompanyContact,
        DeliveryNote: DeliveryNote,
        SectionFlag: SectionFlag,
        SystemUser: SystemUser
    };
});