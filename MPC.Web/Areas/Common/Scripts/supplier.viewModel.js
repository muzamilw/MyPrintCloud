/*
    Module with the view model for Supplier
*/
define("common/supplier.viewModel",
    ["jquery", "amplify", "ko", "common/pagination", "common/supplier.dataservice", "common/supplier.model"], function ($, amplify, ko, pagination, dataservice, model) {
        var ist = window.ist || {};
        ist.supplier = {
            viewModel: (function () {
                var // The view 
                    view,
                    //Active Supplier
                    selectedSupplier = ko.observable(),
                    //Add New Supplier
                    addSupplier = ko.observable(),
                    // True if we are loading data
                    isLoading = ko.observable(false),
                    //Is Supplier Editor Visible
                    isSupplierEditorVisible = ko.observable(false),
                    //Sort On
                    sortOn = ko.observable(1),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    //Pager
                    supplierPager = ko.observable(),
                    // Currency Symbol
                    currencySymbol = ko.observable(),
                    //Search Filter
                    searchSupplierFilter = ko.observable(),
                    //#region Array
                    suppliers = ko.observableArray([]),
                //Company Types
                companyTypes = ko.observableArray([]),
                //Markups
                markups = ko.observableArray([]),
                //Nominal Codes
                nominalCodes = ko.observableArray([]),
                //System Users
               systemUsers = ko.observableArray([]),
                //Flags
               flags = ko.observableArray([]),
                //Price Flags
                priceFlags = ko.observableArray([]),
                //Registration Questions
                registrationQuestions = ko.observableArray([]),
                //Error List
                supplierErrorList = ko.observableArray([]),
                //Account Status 
                accountStatusList = ko.observableArray([{ Id: 1, Text: 'Accounts Clear' },
                                                { Id: 2, Text: 'Accounts on Hold' },
                                                { Id: 2, Text: 'Performa Accounts' }
                ]),
                //#endregion
                // Show the dialog
                   show = function () {
                       isLoading(true);
                       isSupplierEditorVisible(false);
                       view.showSupplierDialog();
                   },
                // Hide the dialog
                    hide = function () {
                        view.hideSupplierDialog();
                    },
                //Get Suppliers
                    getSuppliers = function () {
                        isLoading(true);
                        dataservice.getSuppliers({
                            SearchString: searchSupplierFilter(),
                            PageSize: supplierPager().pageSize(),
                            PageNo: supplierPager().currentPage(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc()
                        }, {
                            success: function (data) {
                                suppliers.removeAll();
                                var supplierList = [];
                                _.each(data.Suppliers, function (item) {
                                    var supplier = new model.SupplierListView.Create(item);
                                    supplierList.push(supplier);
                                });
                                ko.utils.arrayPushAll(suppliers(), supplierList);
                                suppliers.valueHasMutated();
                                supplierPager().totalCount(data.TotalCount);
                                isLoading(false);
                            },
                            error: function () {
                                isLoading(false);
                                toastr.error("Failed to load suppliers.");
                            }
                        });
                    },
                //Search Supplier
                    searchSupplier = function () {
                        getSuppliers();
                    },
                //Reset
                    reset = function () {
                        searchSupplierFilter(undefined);
                        getSuppliers();
                    },
                //On select Supplier close supplier dialog
                    onSelectSupplierColseDialog = ko.computed(function () {
                        if (selectedSupplier() !== undefined) {
                            hide();
                        }
                    }, this),

                // Go To Element
                  gotoElement = function (validation) {
                      view.gotoElement(validation.element);
                  },
                // close Supplier Editor
                    closeSupplierEditor = function () {
                        isSupplierEditorVisible(false);
                    },
                //Create New Supplier
                    onCreateSupplier = function () {
                        supplierErrorList([]);
                        addSupplier(model.Supplier.Create());
                        view.initializeForm();
                        isSupplierEditorVisible(true);
                        view.initializeLabelPopovers();
                    },
                // Get Base
                    getBase = function () {
                        dataservice.getBaseData({
                            success: function (data) {

                                //Company Types
                                companyTypes.removeAll();
                                ko.utils.arrayPushAll(companyTypes(), data.CompanyTypes);
                                companyTypes.valueHasMutated();
                                //Markups
                                markups.removeAll();
                                ko.utils.arrayPushAll(markups(), data.Markups);
                                markups.valueHasMutated();
                                //Nominal Codes
                                nominalCodes.removeAll();
                                ko.utils.arrayPushAll(nominalCodes(), data.NominalCodes);
                                nominalCodes.valueHasMutated();
                                //System Users
                                systemUsers.removeAll();
                                ko.utils.arrayPushAll(systemUsers(), data.SystemUsers);
                                systemUsers.valueHasMutated();
                                //Flags
                                flags.removeAll();
                                ko.utils.arrayPushAll(flags(), data.Flags);
                                flags.valueHasMutated();
                                //price flags
                                priceFlags.removeAll();
                                ko.utils.arrayPushAll(priceFlags(), data.PriceFlags);
                                priceFlags.valueHasMutated();
                                //Registration Questions
                                registrationQuestions.removeAll();
                                ko.utils.arrayPushAll(registrationQuestions(), data.RegistrationQuestions);
                                registrationQuestions.valueHasMutated();
                                currencySymbol(data.CurrencySymbol);
                            },
                            error: function () {
                                toastr.error("Failed to base data.");
                            }
                        });
                    },
                //Call function for Save Supplier
                    onSaveSupplier = function (supplier) {
                        supplierErrorList.removeAll();
                        if (doBeforeSave()) {
                            if (addSupplier().addresses().length !== 0) {
                                addSupplier().addresses([]);
                            }
                            if (addSupplier().companyContacts().length !== 0) {
                                addSupplier().companyContacts([]);
                            }
                            addSupplier().addresses().push(addSupplier().addressInSupplier().convertToServerData());
                            addSupplier().companyContacts().push(addSupplier().companyContact().convertToServerData());
                            saveSupplier(supplier);
                        }
                    },
                // Save Supplier
                    saveSupplier = function (supplier) {
                        dataservice.saveSupplier(addSupplier().convertToServerData(supplier), {
                            success: function (data) {
                                var supplierResult = new model.SupplierListView.Create(data);
                                addSupplier().id(data.SupplierId);
                                suppliers.splice(0, 0, supplierResult);
                                closeSupplierEditor();
                                toastr.success("Successfully save.");
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
                // Do Before Logic
                    doBeforeSave = function () {
                        var flag = true;
                        if (!addSupplier().isValid() || !addSupplier().addressInSupplier().isValid() || !addSupplier().companyContact().isValid()) {
                            addSupplier().errors.showAllMessages();
                            addSupplier().addressInSupplier().errors.showAllMessages();
                            addSupplier().companyContact().errors.showAllMessages();
                            if (addSupplier().name.error != null) {
                                supplierErrorList.push({ name: "Name", element: addSupplier().name.domElement });
                            }
                            if (addSupplier().addressInSupplier().addressName.error != null) {
                                supplierErrorList.push({ name: "Address Name", element: addSupplier().addressInSupplier().addressName.domElement });
                            }
                            if (addSupplier().addressInSupplier().address1.error != null) {
                                supplierErrorList.push({ name: "Address", element: addSupplier().addressInSupplier().address1.domElement });
                            }
                            if (addSupplier().addressInSupplier().email.error != null) {
                                supplierErrorList.push({ name: "Email", element: addSupplier().addressInSupplier().email.domElement });
                            }
                            if (addSupplier().addressInSupplier().city.error != null) {
                                supplierErrorList.push({ name: "City", element: addSupplier().addressInSupplier().city.domElement });
                            }
                            //Company Contact 
                            if (addSupplier().companyContact().password.error != null) {
                                supplierErrorList.push({ name: "Password", element: addSupplier().companyContact().password.domElement });
                            }
                            if (addSupplier().companyContact().email.error != null) {
                                supplierErrorList.push({ name: "Email", element: addSupplier().companyContact().email.domElement });
                            }
                            if (addSupplier().companyContact().firstName.error != null) {
                                supplierErrorList.push({ name: "First Name", element: addSupplier().companyContact().firstName.domElement });
                            }
                            flag = false;
                        }
                        return flag;
                    },


                format = function (item) {
                    return $ + item.FlagName;
                },
                //Media Library File Loaded Call back
                supplierLogoLoadedCallback = function (file, data) {
                    addSupplier().logoSource(data);
                    addSupplier().logoName(file.name);
                },
                // Initialize the view model
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    getBase();
                    supplierPager(pagination.Pagination({ PageSize: 5 }, suppliers, getSuppliers));
                    view.initializeForm();
                };

                return {
                    selectedSupplier: selectedSupplier,
                    addSupplier: addSupplier,
                    isLoading: isLoading,
                    isSupplierEditorVisible: isSupplierEditorVisible,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    supplierPager: supplierPager,
                    show: show,
                    searchSupplierFilter: searchSupplierFilter,
                    //Arrays
                    suppliers: suppliers,
                    companyTypes: companyTypes,
                    markups: markups,
                    nominalCodes: nominalCodes,
                    systemUsers: systemUsers,
                    flags: flags,
                    priceFlags: priceFlags,
                    registrationQuestions: registrationQuestions,
                    accountStatusList: accountStatusList,
                    supplierErrorList: supplierErrorList,
                    //Utilities
                    hide: hide,
                    initialize: initialize,
                    getSuppliers: getSuppliers,
                    searchSupplier: searchSupplier,
                    reset: reset,
                    closeSupplierEditor: closeSupplierEditor,
                    onCreateSupplier: onCreateSupplier,
                    onSaveSupplier: onSaveSupplier,
                    format: format,
                    gotoElement: gotoElement,
                    supplierLogoLoadedCallback: supplierLogoLoadedCallback,
                    currencySymbol: currencySymbol
                };
            })()
        };

        return ist.supplier.viewModel;
    });

//http://mpc/mis/../../Api/Controllers/ReportManagerController.cs