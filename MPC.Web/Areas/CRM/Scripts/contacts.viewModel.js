/*
    Module with the view model for the Compnay Contacts
*/
define("crm/contacts.viewModel",
    ["jquery", "amplify", "ko", "crm/contacts.dataservice", "crm/contacts.model", "common/confirmation.viewModel", "common/pagination", "common/sharedNavigation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVm) {
        var ist = window.ist || {};
        ist.contacts = {
            viewModel: (function () {
                var //View
                view,
                // Search filter 
                searchFilter = ko.observable(),
                // Customers array for list view
                companyContactsForListView = ko.observableArray(),
                // address list
                contactAddresses = ko.observableArray(),
                // company Territories array for list view
                companyTerritories = ko.observableArray(),
                // Pager for pagging
                pager = ko.observable(),
                // Sort On
                sortOn = ko.observable(1),
                // Sort In Ascending
                sortIsAsc = ko.observable(true),
                // Gets customers for list view
                getCompanyContacts = function() {
                    dataservice.getContactsForListView({
                        SearchFilter: searchFilter(),
                        PageSize: pager().pageSize(),
                        PageNo: pager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    },
                    {
                        success: function (data) {
                            if (data != null) {
                            companyContactsForListView.removeAll();
                            pager().totalCount(data.RowCount);
                            _.each(data.CompanyContacts, function (customer) {
                                var contactModel = new model.companyContactViewListModel.Create(customer);
                                companyContactsForListView.push(contactModel);
                            });
                        }
                    },
                    error: function() {
                            toastr.error("Error: Failed To load Customers!");
                        }
                    });
                },
                // Search button handler
                searchButtonHandler = function () {
                    getCompanyContacts();
                },
                //  Reset button handler
                resetButtonHandler=function() {
                    searchFilter(null);
                    getCompanyContacts();
                },
                deleteContactbuttonHandler = function(contact) {
                    // Ask for confirmation
                    confirmation.afterProceed(function() {
                        dataservice.deleteContact({
                            CompanyContactId: contact.contactId(),
                        },
                        {
                            success: function(data) {
                                toastr.success("Contact successfuly deleted!");
                            },
                            error: function() {
                                toastr.error("Error: Failed To delete Contact!");
                            }
                        });
                    });
                    confirmation.show();
                },
                editContactbuttonHandler = function (contact) {
                    getContactDetail(contact.cmpanyId());
                    view.showCompanyContactDetailDialog();
                },
                 // Gets Base Data
                getBaseData = function () {
                    dataservice.getbaseData({},
                    {
                        success: function (data) {
                            debugger;
                        },
                        error: function () {
                            toastr.error("Error: Failed To load Base data!");
                        }
                    });
                },
                getContactDetail = function(companyId) {
                    dataservice.getContactsDetail({ companyId: companyId },
                    {
                        success: function (data) {
                            debugger;
                        },
                        error: function() {
                            toastr.error("Error: Failed To load Base data!");
                        }
                    });
                },
                //Initialize
               initialize = function (specifiedView) {
                   view = specifiedView;
                   ko.applyBindings(view.viewModel, view.bindingRoot);
                   pager(new pagination.Pagination({ PageSize: 5 }, companyContactsForListView, getCompanyContacts));
                   getBaseData();
                   getCompanyContacts();
               };
                return {
                    initialize: initialize,
                    pager:pager,
                    searchFilter: searchFilter,
                    companyContactsForListView: companyContactsForListView,
                    searchButtonHandler: searchButtonHandler,
                    resetButtonHandler: resetButtonHandler,
                    sharedNavigationVm: sharedNavigationVm,
                    deleteContactbuttonHandler: deleteContactbuttonHandler,
                    editContactbuttonHandler: editContactbuttonHandler,
                    contactAddresses: contactAddresses,
                    companyTerritories: companyTerritories
                };
            })()
        };
        return ist.contacts.viewModel;
    });
