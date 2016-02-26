/*
    Module with the view model for the live Jobs.
*/
define("deliveryNotes/deliveryNotes.viewModel",
    ["jquery", "amplify", "ko", "deliveryNotes/deliveryNotes.dataservice", "deliveryNotes/deliveryNotes.model", "common/pagination", "common/companySelector.viewModel", "common/confirmation.viewModel", "common/reportManager.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, companySelector, confirmation, reportManager) {
        var ist = window.ist || {};
        ist.deliveryNotes = {
            viewModel: (function () {
                var // the view 
                    view,
                    //Currency Symbol
                    currencySymbol = ko.observable(),
                    // #region Arrays
                    //Items
                    deliverNoteListView = ko.observableArray([]),
                        // company contacts
                    companyContacts = ko.observableArray([]),
                    // Company Addresses
                    companyAddresses = ko.observableArray([]),
                     // flag colors
                    sectionFlags = ko.observableArray([]),
                    // System Users
                    systemUsers = ko.observableArray([]),
                    loggedInUser = ko.observable(),
                    // Delivery Carriers
                    deliveryCarriers = ko.observableArray([]),
                    // Errors List
                    errorList = ko.observableArray([]),
                    // #endregion
                    // is editor visible 
                    isEditorVisible = ko.observable(false),
                    // is edit SCREEN
                     isEditCall = ko.observable(false),
                    // selected Cimpnay
                    selectedCompany = ko.observable(),
                    defaultHeadNote = ko.observable(),
                    defaultFootNote = ko.observable(),
                    deliveryNoteEditorHeader = ko.observable(),
                    currentTab = ko.observable(19),
                    // #region Observables
                    selectedDeliveryNote = ko.observable(model.DeliveryNote()),
                    // For List View
                    selectedDeliveryNoteForListView = ko.observable(),
                    // Active Delivery Note Detail
                    selectedDeliveryNoteDetail = ko.observable(),
                    // Search Filter
                    searchFilter = ko.observable(),
                    //Pager
                    pager = ko.observable(),
                    //phone
                    CarriarPhone = ko.observable(),
                     //Sort On
                    sortOn = ko.observable(4),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(false),
                     // Is Company Base Data Loaded
                    isCompanyBaseDataLoaded = ko.observable(false),

                    //Category Filter
                    carrierFilter = ko.observable(),
                    deliveryNoteIdFromOrder = ko.observable(),
                      // is open report
                     isOpenReport = ko.observable(false),
                      // is open report Email
                     isOpenReportEmail = ko.observable(false),
                     // Tax Rate
                    selectedCompanyTaxRate = ko.observable(),
                      // Default Address
                    defaultAddress = ko.observable(model.Address.Create({})),
                      // Default Company Contact
                    defaultCompanyContact = ko.observable(model.CompanyContact.Create({})),
                     // Selected Address
                    selectedAddress = ko.computed(function () {
                        if (!selectedDeliveryNote() || !selectedDeliveryNote().addressId() || companyAddresses().length === 0) {
                            return defaultAddress();
                        }

                        var addressResult = companyAddresses.find(function (address) {
                            return address.id === selectedDeliveryNote().addressId();
                        });

                        return addressResult || defaultAddress();
                    }),
                    // Selected Company Contact
                    selectedCompanyContact = ko.computed(function () {
                        if (!selectedDeliveryNote() || !selectedDeliveryNote().contactId() || companyContacts().length === 0) {
                            return defaultCompanyContact();
                        }

                        var contactResult = companyContacts.find(function (contact) {
                            return contact.id === selectedDeliveryNote().contactId();
                        });

                        return contactResult || defaultCompanyContact();
                    }),
                    // #endregion
                    getDeliveryNotesOnTabChange = function (currentTabStatus) {
                        searchFilter(undefined);
                        currentTab(currentTabStatus);
                        pager(new pagination.Pagination({ PageSize: 5 }, deliverNoteListView, getdeliveryNotes));
                        pager().reset();
                        getdeliveryNotes();
                    },
                      openReport = function (isFromEditor) {
                          reportManager.show(ist.reportCategoryEnums.Delivery, isFromEditor == true ? true : false, 0);
                      },
                // Get Delivery Notes
                getdeliveryNotes = function () {
                    dataservice.getdeliveryNotes({
                        SearchString: searchFilter(),
                        PageSize: pager().pageSize(),
                        PageNo: pager().currentPage(),
                        SortBy: sortOn(),
                        Status: currentTab(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {
                            deliverNoteListView.removeAll();
                            if (data !== null && data !== undefined) {
                                var itemList = [];
                                _.each(data.DeliveryNotes, function (item) {
                                    itemList.push(model.deliverNoteListView.Create(item));
                                });
                                ko.utils.arrayPushAll(deliverNoteListView(), itemList);
                                deliverNoteListView.valueHasMutated();

                                pager().totalCount(data.TotalCount);
                                if (deliveryNoteIdFromOrder() != undefined && deliveryNoteIdFromOrder() > 0) {
                                    getDetaildeliveryNote(deliveryNoteIdFromOrder());
                                    deliveryNoteEditorHeader('Modify Delivery Notes');
                                    isEditorVisible(true);
                                    errorList.removeAll();
                                    deliveryNoteIdFromOrder(undefined);
                                }
                                defaultHeadNote(data.HeadNote);
                                defaultFootNote(data.FootNote);
                            }

                        },
                        error: function () {
                            toastr.error("Failed to Items.");
                        }
                    });
                },
                  formatSelection = function (state) {
                      return "<span style=\"height:20px;width:20px;float:left;margin-right:10px;margin-top:5px;background-color:" + $(state.element).data("color") + "\"></span><span>" + state.text + "</span>";
                  },
                    formatResult = function (state) {
                        return "<div style=\"height:20px;margin-right:10px;width:20px;float:left;background-color:" + $(state.element).data("color") + "\"></div><div>" + state.text + "</div>";
                    },
                // Get Delivery Note By ID
                getDetaildeliveryNote = function (id) {
                    isCompanyBaseDataLoaded(false);
                    dataservice.getDetaildeliveryNote({
                        deliverNoteId: id
                    }, {
                        success: function (data) {
                            if (data !== null && data !== undefined) {

                                isEditCall(true);
                                var dNote = model.DeliveryNote.Create(data);
                                selectedDeliveryNote(dNote);
                                CarriarPhone(dNote.supplierTelNo());
                                carrierFilter(dNote.supplierId());
                                selectedDeliveryNote().companyName(data.CompanyName);
                                // Get Base Data For Company
                                if (data.CompanyId) {
                                    var storeId = 0;
                                    if (data.IsCustomer !== 3 && data.StoreId) {
                                        storeId = data.StoreId;
                                        selectedDeliveryNote().storeId(storeId);
                                    } else {
                                        storeId = data.CompanyId;
                                    }
                                    selectedDeliveryNote().reset();
                                    getBaseForCompany(data.CompanyId, storeId);
                                }
                                if (deliveryNoteIdFromOrder() != undefined && deliveryNoteIdFromOrder() > 0) {
                                    selectedDeliveryNoteForListView(data);
                                }
                                
                            }

                        },
                        error: function () {
                            toastr.error("Failed to Items.");
                        }
                    });
                },
                        // Get Items
                    downloadArtwork = function () {
                        dataservice.downloadArtwork({
                            success: function (data) {


                            },
                            error: function () {
                                toastr.error("Failed to Items.");
                            }
                        });

                    },
                    searchData = function () {
                        pager().reset();
                        getdeliveryNotes();
                    },
                    onEditDeliverNote = function (item) {
                        selectedDeliveryNoteForListView(item);
                        getDetaildeliveryNote(item.deliveryNoteId());
                        deliveryNoteEditorHeader('Modify Delivery Notes');
                        isEditorVisible(true);
                        errorList.removeAll();
                    },
                    onCloseEditor = function () {
                        if (selectedDeliveryNote().hasChanges() && selectedDeliveryNote().isStatus() !== 20) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(function () {
                                onSaveDeliveryNotes();
                            });
                            confirmation.afterCancel(function () {
                                isEditorVisible(false);
                            });
                            confirmation.show();
                            return;
                        }
                        isEditorVisible(false);
                    },

                     // report preview
                    openExternalReportsDelivery = function () {

                        reportManager.outputTo("preview");
                        if (selectedDeliveryNote().hasChanges()) {
                            isOpenReport(true);
                            isOpenReportEmail(false);
                            onSaveDeliveryNotes();
                        }
                        else {
                            reportManager.OpenExternalReport(ist.reportCategoryEnums.Delivery, 1, selectedDeliveryNote().deliveryNoteId());
                        }


                        



                        //reportManager.SetOrderData(selectedOrder().orderReportSignedBy(), selectedOrder().contactId(), selectedOrder().id(),"");
                        //reportManager.show(ist.reportCategoryEnums.Orders, 1, selectedOrder().id(), selectedOrder().companyName(), selectedOrder().orderCode(), selectedOrder().name());


                    },

                    openExternalEmailDeliveryReport = function () {
                        reportManager.outputTo("email");

                        if (selectedDeliveryNote().hasChanges()) {

                            isOpenReport(true);
                            isOpenReportEmail(true);
                            onSaveDeliveryNotes();
                        }
                        else {
                            reportManager.SetOrderData(selectedDeliveryNote().raisedBy(), selectedDeliveryNote().contactId(), selectedDeliveryNote().deliveryNoteId(), 5, selectedDeliveryNote().deliveryNoteId(), "");
                            reportManager.OpenExternalReport(ist.reportCategoryEnums.Delivery, 1, selectedDeliveryNote().deliveryNoteId());

                        }

                       


                    },


                     // Open Company Dialog
                    openCompanyDialog = function () {
                        companySelector.show(onSelectCompany, [0, 1, 3]);
                    },
                      // On Select Company
                    onSelectCompany = function (company) {
                        if (!company) {
                            return;
                        }
                        if (selectedDeliveryNote().companyId() === company.id) {
                            return;
                        }

                        selectedDeliveryNote().companyId(company.id);
                        selectedDeliveryNote().companyName(company.name);
                        selectedCompany(company);

                        if (company.isCustomer !== 3 && company.storeId) {
                            selectedDeliveryNote().storeId(company.storeId);
                        }
                        // Get Company Address and Contacts
                        getBaseForCompany(company.id, (selectedDeliveryNote().storeId() === null || selectedDeliveryNote().storeId() === undefined) ? company.id :
                            selectedDeliveryNote().storeId());
                    },
                    // Get Company Base Data
                    getBaseForCompany = function (id, storeId) {
                        isCompanyBaseDataLoaded(false);
                        dataservice.getBaseDataForCompany({
                            id: id,
                            storeId: storeId
                        }, {
                            success: function (data) {
                                companyAddresses.removeAll();
                                companyContacts.removeAll();
                                if (data) {
                                    if (data.CompanyAddresses) {
                                        mapList(companyAddresses, data.CompanyAddresses, model.Address);
                                        setDefaultAddressForCompany();
                                    }
                                    if (data.CompanyContacts) {
                                        mapList(companyContacts, data.CompanyContacts, model.CompanyContact);
                                        setDefaultContactForCompany();
                                    }
                                    selectedCompanyTaxRate(data.TaxRate);
                                    selectedDeliveryNote().reset();
                                }
                                isCompanyBaseDataLoaded(true);
                            },
                            error: function (response) {
                                isCompanyBaseDataLoaded(true);
                                toastr.error("Failed to load details for selected company" + response);
                            }
                        });
                    },
                     // Map List
                    mapList = function (observableList, data, factory) {
                        var list = [];
                        _.each(data, function (item) {
                            list.push(factory.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(observableList(), list);
                        observableList.valueHasMutated();
                    }, // Select Default Address For Company in case of new Delivery Note
                    setDefaultAddressForCompany = function () {
                        if (selectedDeliveryNote().deliveryNoteId() > 0) {
                            return;
                        }
                        var defaultCompanyAddress = companyAddresses.find(function (address) {
                            return address.isDefault;
                        });
                        if (defaultCompanyAddress) {
                            selectedDeliveryNote().addressId(defaultCompanyAddress.id);
                        }
                    },
                    // Select Default Contact For Company in case of new Delivery Note
                    setDefaultContactForCompany = function () {
                        if (selectedDeliveryNote().deliveryNoteId() > 0) {
                            return;
                        }
                        var defaultContact = companyContacts.find(function (contact) {
                            return contact.isDefault;
                        });
                        if (defaultContact) {
                            selectedDeliveryNote().contactId(defaultContact.id);
                        }
                    },
                     getBaseData = function () {
                         dataservice.getBaseData({}, {
                             success: function (data) {

                                 if (data.SectionFlags) {
                                     mapList(sectionFlags, data.SectionFlags, model.SectionFlag);
                                 }
                                 if (data.SystemUsers) {
                                     mapList(systemUsers, data.SystemUsers, model.SystemUser);
                                 }
                                 if (data.DeliveryCarriers) {
                                     ko.utils.arrayPushAll(deliveryCarriers(), data.DeliveryCarriers);
                                     deliveryCarriers.valueHasMutated();
                                 }
                                 loggedInUser(data.LoggedInUser);

                             },
                             error: function (response) {
                                 toastr.error("Failed to load base data" + response);
                             }
                         });
                     },
                     // Add New Delivery Notes
                     addDeliveryNotes = function () {
                         var deliveryNotes = model.DeliveryNote();
                         deliveryNotes.isStatus(19);
                         deliveryNotes.raisedBy(loggedInUser());
                         deliveryNotes.comments(defaultHeadNote());
                         deliveryNotes.userNotes(defaultFootNote());
                         selectedDeliveryNote(deliveryNotes);
                         
                         deliveryNoteEditorHeader('Add Delivery Notes');
                         isEditorVisible(true);
                         errorList.removeAll();
                     },
                     // add Delivery Note Detail
                     addDeliveryNoteDetail = function () {
                         var deliveyNoteDetail = model.DeliveryNoteDetail();
                         selectedDeliveryNoteDetail(deliveyNoteDetail);
                         selectedDeliveryNote().deliveryNoteDetails.splice(0, 0, deliveyNoteDetail);
                     },
                     // Template Chooser For Delivery Note Detail
                    templateToUseDeliveryNoteDetail = function (deliveryNoteDetail) {
                        return (deliveryNoteDetail === selectedDeliveryNoteDetail() ? 'editDeliveryNoteDetailemplate' : 'itemDeliveryNoteDetailTemplate');
                    },
                    selectDeliveryNoteDetail = function (deliveryNoteDetail) {
                        selectedDeliveryNoteDetail(deliveryNoteDetail);
                    },
                    // Delete Delivery Notes
                    onDeleteDeliveryNoteDetail = function (deliveryNoteDetail) {
                        confirmation.messageText("WARNING - Item will be removed from the system and you won’t be able to recover.  There is no undo!");
                        confirmation.afterProceed(function () {
                            selectedDeliveryNote().deliveryNoteDetails.remove(deliveryNoteDetail);
                        });
                        confirmation.afterCancel(function () {

                        });
                        confirmation.show();
                        return;
                    },
                    // Save Delivery Notes
                    onSaveDeliveryNotes = function (deliveryNote) {
                        if (!dobeforeSave()) {
                            return;
                        }
                        var phone = CarriarPhone();
                        selectedDeliveryNote().supplierTelNo(phone);
                        var deliveryNotes = selectedDeliveryNote().convertToServerData();
                        _.each(selectedDeliveryNote().deliveryNoteDetails(), function (item) {
                            deliveryNotes.DeliveryNoteDetails.push(item.convertToServerData(item));
                        });
                        saveDeliveryNote(deliveryNotes);
                    },
                    onPostDeliveryNote = function (deliveryNote) {
                        if (!dobeforeSave()) {
                            return;
                        }

                        confirmation.messageText("Are you sure you want to Post Delivery Note?");
                        confirmation.afterProceed(function () {
                            selectedDeliveryNote().isStatus(20);
                            var deliveryNotes = selectedDeliveryNote().convertToServerData();
                            _.each(selectedDeliveryNote().deliveryNoteDetails(), function (item) {
                                deliveryNotes.DeliveryNoteDetails.push(item.convertToServerData(item));
                            });
                            saveDeliveryNote(deliveryNotes);
                        });
                        confirmation.afterCancel(function () {

                        });
                        confirmation.show();
                        return;
                    },
                    // Delete Delivry Notes
                onDeleteDeliveryNote = function () {
                    confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
                    confirmation.afterProceed(function () {
                        deleteDeliveryNote(selectedDeliveryNote().convertToServerData());
                    });
                    confirmation.afterCancel(function () {

                    });
                    confirmation.show();
                    return;
                },
                deleteDeliveryNote = function (deliveryNote) {
                    dataservice.deleteDeliveryNote(deliveryNote, {
                        success: function (data) {
                            deliverNoteListView.remove(selectedDeliveryNoteForListView());
                            isEditorVisible(false);
                            toastr.success("Delete Successfully.");
                        },
                        error: function (exceptionMessage, exceptionType) {
                            if (exceptionType === ist.exceptionType.MPCGeneralException) {
                                toastr.error(exceptionMessage);
                            } else {
                                toastr.error("Failed to delete.");
                            }
                        }
                    });
                },
                // Save Delivery Notes
                saveDeliveryNote = function (deliveryNote) {
                    dataservice.saveDeliveryNote(deliveryNote, {
                        success: function (data) {

                            if (isOpenReport() == true) {
                                if (selectedDeliveryNote().deliveryNoteId() == 0 || selectedDeliveryNote().deliveryNoteId() == undefined) {
                                    selectedDeliveryNote().deliveryNoteId(data.DeliveryNoteId);
                                }
                                if (isOpenReportEmail() == true) {
                                    reportManager.SetOrderData(selectedDeliveryNote().raisedBy(), selectedDeliveryNote().contactId(), selectedDeliveryNote().deliveryNoteId(), 5, selectedDeliveryNote().deliveryNoteId(), "");
                                    reportManager.OpenExternalReport(ist.reportCategoryEnums.Delivery, 1, selectedDeliveryNote().deliveryNoteId());
                                }
                                else {
                                    reportManager.OpenExternalReport(ist.reportCategoryEnums.Delivery, 1, selectedDeliveryNote().deliveryNoteId());
                                }
                                getDetaildeliveryNote(selectedDeliveryNote().deliveryNoteId());
                                isOpenReport(false);
                            }
                            else {
                                //For Add New

                                if (selectedDeliveryNote().deliveryNoteId() === undefined || selectedDeliveryNote().deliveryNoteId() === 0) {
                                    var dNote = model.deliverNoteListView.Create(data);
                                    dNote.companyName(selectedDeliveryNote().companyName());
                                    deliverNoteListView.splice(0, 0, dNote);
                                } else {
                                    if (selectedDeliveryNoteForListView() != undefined) {
                                        selectedDeliveryNoteForListView().deliveryDate(data.DeliveryDate !== null ? moment(data.DeliveryDate).toDate() : undefined);
                                        selectedDeliveryNoteForListView().flagId(data.FlagId);
                                        selectedDeliveryNoteForListView().contactCompany(data.ContactCompany);
                                        selectedDeliveryNoteForListView().companyName(data.CompanyName);
                                        selectedDeliveryNoteForListView().flagColor(data.FlagColor);
                                        selectedDeliveryNoteForListView().orderReff(data.OrderReff);
                                        selectedDeliveryNoteForListView().itemsCount(data.DeliveryNoteDetails != null ? data.DeliveryNoteDetails.length : 0);
                                        selectedDeliveryNoteForListView().creationDateTime(data.CreationDateTime !== null ? moment(data.CreationDateTime).toDate() : undefined);
                                        if (currentTab() !== data.IsStatus) {
                                            deliverNoteListView.remove(selectedDeliveryNoteForListView());
                                        }
                                    }
                                    
                                }
                                isEditorVisible(false);
                                toastr.success("Saved Successfully.");
                            }
                           
                        },
                        error: function (exceptionMessage, exceptionType) {
                            if (exceptionType === ist.exceptionType.MPCGeneralException) {
                                toastr.error(exceptionMessage);
                            } else {
                                toastr.error("Failed to save.");
                            }
                        }
                    });
                },
                dobeforeSave = function () {
                    var flag = true;
                    if (!selectedDeliveryNote().isValid()) {
                        selectedDeliveryNote().showAllErrors();
                        selectedDeliveryNote().setValidationSummary(errorList);
                        flag = false;
                    }
                    return flag;
                },
                // Go To Element
                gotoElement = function (validation) {
                    view.gotoElement(validation.element);
                },
               
                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    pager(new pagination.Pagination({ PageSize: 5 }, deliverNoteListView, getdeliveryNotes));
                    getBaseData();
                    getdeliveryNotes();
                    var idfromOrders = $('#DeliveryNoteId').val();
                    if (idfromOrders != undefined && idfromOrders > 0) {
                        deliveryNoteIdFromOrder(idfromOrders);
                    }
                    carrierFilter.subscribe(function (carrier) {
                        _.each(deliveryCarriers(), function (Dcarrier) {
                            if (Dcarrier.CarrierId == carrier) {
                                
                                CarriarPhone(Dcarrier.CarrierPhone);
                                
                            }
                        });

                        selectedDeliveryNote().supplierId(carrier);
                    });
                  };
               
                //#endregion 


                return {
                    initialize: initialize,
                    searchFilter: searchFilter,
                    onEditDeliverNote: onEditDeliverNote,
                    searchData: searchData,
                    selectedDeliveryNote: selectedDeliveryNote,
                    selectedDeliveryNoteDetail: selectedDeliveryNoteDetail,
                    pager: pager,
                    deliverNoteListView: deliverNoteListView,
                    getdeliveryNotes: getdeliveryNotes,
                    downloadArtwork: downloadArtwork,
                    isEditorVisible: isEditorVisible,
                    onCloseEditor: onCloseEditor,
                    openCompanyDialog: openCompanyDialog,
                    selectedCompany: selectedCompany,
                    isCompanyBaseDataLoaded: isCompanyBaseDataLoaded,
                    companyContacts: companyContacts,
                    companyAddresses: companyAddresses,
                    selectedAddress: selectedAddress,
                    selectedCompanyContact: selectedCompanyContact,
                    sectionFlags: sectionFlags,
                    systemUsers: systemUsers,
                    errorList: errorList,
                    getBaseData: getBaseData,
                    addDeliveryNotes: addDeliveryNotes,
                    addDeliveryNoteDetail: addDeliveryNoteDetail,
                    templateToUseDeliveryNoteDetail: templateToUseDeliveryNoteDetail,
                    selectDeliveryNoteDetail: selectDeliveryNoteDetail,
                    onDeleteDeliveryNoteDetail: onDeleteDeliveryNoteDetail,
                    onSaveDeliveryNotes: onSaveDeliveryNotes,
                    gotoElement: gotoElement,
                    deliveryCarriers: deliveryCarriers,
                    onDeleteDeliveryNote: onDeleteDeliveryNote,
                    onPostDeliveryNote: onPostDeliveryNote,
                    currentTab: currentTab,
                    getDeliveryNotesOnTabChange: getDeliveryNotesOnTabChange,
                    openReport: openReport,
                    openExternalReportsDelivery: openExternalReportsDelivery,
                    openExternalEmailDeliveryReport: openExternalEmailDeliveryReport,
                    formatSelection: formatSelection,
                    formatResult: formatResult,
                    deliveryNoteEditorHeader: deliveryNoteEditorHeader,
                    carrierFilter: carrierFilter,
                    CarriarPhone: CarriarPhone

                };
            })()
        };
        return ist.deliveryNotes.viewModel;
    });
