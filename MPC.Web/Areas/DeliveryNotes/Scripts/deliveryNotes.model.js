/*
    Module with the model for the  Delivery Notes
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

        // #region __________________  Delivery Note List View   ______________________
        deliverNoteListView = function (specifieddeliveryNoteId, specifiedcode, specifieddeliveryDate, specifiedflagId, specifiedcontactCompany,
            specifiedOrderReff, specifiedCreationDateTime, specifiedCompanyName, specifiedFlagColor, specifiedCount, specifiedContactName, specifiedStatus) {

            var self,
                deliveryNoteId = ko.observable(specifieddeliveryNoteId),
                code = ko.observable(specifiedcode),
                deliveryDate = ko.observable(specifieddeliveryDate !== null ? moment(specifieddeliveryDate).toDate() : undefined),
                flagId = ko.observable(specifiedflagId),
                contactCompany = ko.observable(specifiedcontactCompany),
                companyName = ko.observable(specifiedCompanyName),
                orderReff = ko.observable(specifiedOrderReff),
                flagColor = ko.observable(specifiedFlagColor),
                itemsCount = ko.observable(specifiedCount),
                contactName = ko.observable(specifiedContactName),
                status = ko.observable(specifiedStatus),
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
                flagColor: flagColor,
                contactCompany: contactCompany,
                companyName: companyName,
                orderReff: orderReff,
                itemsCount : itemsCount,
                creationDateTime: creationDateTime,
                convertToServerData: convertToServerData,
                contactName: contactName,
                status: status
            };
            return self;
        },
        // #endregion __________________  dDelivery Note List View    ______________________

        // #region __________________  System User   ______________________
        SystemUser = function (specifiedId, specifiedName, specifiedFullName) {
            return {
                id: specifiedId,
                name: specifiedName,
                fullName: specifiedFullName
            };
        },
        // #endregion __________________  Section Flag  ______________________

        // #region __________________  Section Flag   ______________________
        // Section Flag Entity        
        SectionFlag = function (specifiedId, specifiedFlagName, specifiedFlagColor) {
            // ReSharper restore InconsistentNaming
            return {
                id: specifiedId,
                name: specifiedFlagName,
                color: specifiedFlagColor
            };
        },
        // #endregion __________________  Section Flag  ______________________

        // #region __________________  Delivery Note  ______________________
        DeliveryNote = function (specifieddeliveryNoteId, specifiedcode, specifieddeliveryDate, specifiedflagId, specifiedcontactCompany,
            specifiedOrderReff, specifiedCreationDateTime, spcCompanyId, spcFootnote, spcComments, spcLockedBy, spcIsStatus, spcContactId, spcCustomerOrderReff, spcAddressId,
            spcCreatedBy, spcSupplierId, spcSupplierTelNo, spcSupplierURL, spcEstimateId, spcJobId, spcInvoiceId, spcOrderId, spcUserNotes,
            spcNotesUpdateDateTime, spcNotesUpdatedByUserId, spcSystemSiteId, spcIsRead, spcIsPrinted, spcCsNo, spcRaisedBy, spcContactName) {

            var self,
                deliveryNoteId = ko.observable(specifieddeliveryNoteId),
                code = ko.observable(specifiedcode),
                deliveryDate = ko.observable(specifieddeliveryDate !== null ? moment(specifieddeliveryDate).toDate() : undefined),
                flagId = ko.observable(specifiedflagId),
                contactCompany = ko.observable(specifiedcontactCompany),
                orderReff = ko.observable(specifiedOrderReff),
                creationDateTime = ko.observable(specifiedCreationDateTime !== null ? moment(specifiedCreationDateTime).toDate() : undefined),
                companyId = ko.observable(spcCompanyId).extend({ required: true }),
                footnote = ko.observable(spcFootnote),
                comments = ko.observable(spcComments),
                lockedBy = ko.observable(spcLockedBy),
                isStatus = ko.observable(spcIsStatus),
                contactId = ko.observable(spcContactId),
                customerOrderReff = ko.observable(spcCustomerOrderReff),
                // Store Id
                storeId = ko.observable(undefined),
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
                companyName = ko.observable(undefined),
                contactName = ko.observable(spcContactName),
                csNo = ko.observable(spcCsNo),
                raisedBy = ko.observable(spcRaisedBy),
                deliveryNoteDetails = ko.observableArray([]),
                // Set Validation Summary
                setValidationSummary = function (validationSummaryList) {
                    validationSummaryList.removeAll();
                    if (companyId.error) {
                        validationSummaryList.push({ name: "Customer", element: companyId.domElement });
                    }
                },
                 // Errors
                errors = ko.validation.group({
                    companyId: companyId
                }),
                // Is Valid 
                isValid = ko.computed(function () {
                    return errors().length === 0 ? true : false;
                }),
                // Show All Error Messages
                showAllErrors = function () {
                    // Show Item Errors
                    errors.showAllMessages();
                },
                 // True if the order has been changed
                  dirtyFlag = new ko.dirtyFlag({
                      deliveryDate: deliveryDate,
                      flagId: flagId,
                      orderReff: orderReff,
                      companyId: companyId,
                      footnote: footnote,
                      comments: comments,
                      contactId: contactId,
                      supplierId: supplierId,
                      userNotes: userNotes,
                      supplierTelNo: supplierTelNo,
                      csNo: csNo,
                      raisedBy: raisedBy,
                      deliveryNoteDetails: deliveryNoteDetails,

                  }),
                // Has Changes
                hasChanges = ko.computed(function () {
                    return dirtyFlag.isDirty();
                }),
                // Reset
                reset = function () {
                    dirtyFlag.reset();
                },
                convertToServerData = function () {
                    return {
                        DeliveryNoteId: deliveryNoteId(),
                        Code: code(),
                        DeliveryDate: deliveryDate() ? moment(deliveryDate()).format(ist.utcFormat) + 'Z' : undefined,
                        FlagId: flagId(),
                        ContactCompany: contactCompany(),
                        OrderReff: orderReff(),
                        CreationDateTime: creationDateTime() ? moment(creationDateTime()).format(ist.utcFormat) + 'Z' : undefined,
                        CompanyId: companyId(),
                        Comments: comments(),
                        IsStatus: isStatus(),
                        ContactId: contactId(),
                        AddressId: addressId(),
                        SupplierId: supplierId(),
                        SupplierTelNo: supplierTelNo(),
                        UserNotes: userNotes(),
                        SystemSiteId: systemSiteId(),
                        OrderId: orderId(),
                        CsNo: csNo(),
                        RaisedBy: raisedBy(),
                        DeliveryNoteDetails: []
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
                storeId: storeId,
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
                setValidationSummary: setValidationSummary,
                deliveryNoteDetails: deliveryNoteDetails,
                companyName: companyName,
                raisedBy: raisedBy,
                convertToServerData: convertToServerData,
                errors: errors,
                isValid: isValid,
                showAllErrors: showAllErrors,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                reset: reset,
                contactName: contactName
            };
            return self;
        },
        // #endregion __________________  Delivery Note  ______________________

        // #region __________________  Delivery Note Detail ______________________
         DeliveryNoteDetail = function (specifiedId, specifiedDescription) {
             var self,
                id = ko.observable(specifiedId),
                description = ko.observable(specifiedDescription),

                convertToServerData = function (source) {
                    return {
                        DeliveryDetailid: source.id(),
                        Description: source.description(),
                    };
                };

             self = {
                 id: id,
                 description: description,
                 convertToServerData: convertToServerData
             };
             return self;
         },

         // #endregion __________________  Delivery Note Detail  ______________________

        // #region __________________  Address ______________________
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
        },
        // #endregion __________________  Address  ______________________

        // #region __________________  Company Contact ______________________
        // Company Contact Entity
        CompanyContact = function (specifiedId, specifiedName, specifiedEmail, specifiedIsDefault) {
            // ReSharper restore InconsistentNaming
            return {
                id: specifiedId,
                name: specifiedName,
                email: specifiedEmail || "",
                isDefault: specifiedIsDefault
            };
        };
    // #endregion __________________  Compan yContact  ______________________

    // Delivery Notes Detail factory
    DeliveryNoteDetail.Create = function (source) {
        return new DeliveryNoteDetail(source.DeliveryDetailid, source.Description);
    };

    // Delivery Notes List View Factory
    deliverNoteListView.Create = function (source) {
        return new deliverNoteListView(source.DeliveryNoteId, source.Code, source.DeliveryDate, source.FlagId, source.ContactCompany, source.OrderReff,
            source.CreationDateTime, source.CompanyName, source.FlagColor, source.DeliveryNoteDetails != null ? source.DeliveryNoteDetails.length : 0, source.ContactName, source.IsStatus);
    };

    // Delivery Notes Factory
    DeliveryNote.Create = function (source) {
        var deliveryNote = new DeliveryNote(source.DeliveryNoteId, source.Code, source.DeliveryDate, source.FlagId, source.ContactCompany, source.OrderReff,
          source.CreationDateTime, source.CompanyId, source.Footnote, source.Comments, source.LockedBy, source.IsStatus, source.ContactId, source.CustomerOrderReff,
             source.AddressId, source.CreatedBy, source.SupplierId, source.SupplierTelNo, source.SupplierURL, source.EstimateId, source.JobId, source.InvoiceId,
             source.OrderId, source.UserNotes, source.NotesUpdateDateTime, source.NotesUpdatedByUserId, source.SystemSiteId, source.IsRead, source.IsPrinted, source.CsNo,
             source.RaisedBy);

        _.each(source.DeliveryNoteDetails, function (dNoteDetail) {
            deliveryNote.deliveryNoteDetails.push(DeliveryNoteDetail.Create(dNoteDetail));
        });
        return deliveryNote;
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
        SystemUser: SystemUser,
        DeliveryNoteDetail: DeliveryNoteDetail
    };
});