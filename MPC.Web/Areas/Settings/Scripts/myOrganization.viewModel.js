/*
    Module with the view model for the My Organization.
*/
define("myOrganization/myOrganization.viewModel",
    ["jquery", "amplify", "ko", "myOrganization/myOrganization.dataservice", "myOrganization/myOrganization.model", "common/confirmation.viewModel", "common/pagination"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination) {
        var ist = window.ist || {};
        ist.myOrganization = {
            viewModel: (function () {
                var // the view 
                    view,
                    // Active
                    selectedMyOrganization = ko.observable(),
                    //Active Tax Rate
                    selectedTaxRate = ko.observable(),
                    // #region Arrays
                    //Currency Symbol list
                    currencySymbols = ko.observableArray([]),
                    //Language Pack List
                    languagePacks = ko.observableArray([]),
                    //Unit Length List
                    unitLengths = ko.observableArray([]),
                    //Unit Weights
                    unitWeights = ko.observableArray([]),
                    //Chart Of Accounts
                    chartOfAccounts = ko.observableArray([]),
                    //Markups
                    markups = ko.observableArray([]),
                    //Tax Rates
                    taxRates = ko.observableArray([]),
                    // #endregion Arrays
                    // #region Busy Indicators
                    isLoadingMyOrganization = ko.observable(false),
                    // #endregion Busy Indicators
                    // #region Observables
                    // Sort On
                    sortOn = ko.observable(1),
                    // Sort Order -  true means asc, false means desc
                    sortIsAsc = ko.observable(true),
                    // Sort On Hiregroup
                    sortOnHg = ko.observable(1),
                    // Sort Order -  true means asc, false means desc
                    sortIsAscHg = ko.observable(true),
                    // Pagination
                    pager = ko.observable(),
                    // #region Utility Functions
                    // Initialize the view model
                    initialize = function(specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        getBase();
                        // Set Pager
                        //pager(new pagination.Pagination({}, additionalTypeCharges, getAdditionalCharges));
                        //getAdditionalCharges();
                        selectedMyOrganization(new model.CompanySites());
                    },
                    // Get Base
                    getBase = function(callBack) {
                        dataservice.getMyOrganizationBase({
                            success: function(data) {
                                //Currency
                                currencySymbols.removeAll();
                                ko.utils.arrayPushAll(currencySymbols(), currencySymbolsGlobal);
                                currencySymbols.valueHasMutated();
                                //Language Packs
                                languagePacks.removeAll();
                                ko.utils.arrayPushAll(languagePacks(), languagePacksGlobal);
                                languagePacks.valueHasMutated();
                                //unit Lengths
                                unitLengths.removeAll();
                                ko.utils.arrayPushAll(unitLengths(), unitLengthsGlobal);
                                unitLengths.valueHasMutated();
                                //unit Weights
                                unitWeights.removeAll();
                                ko.utils.arrayPushAll(unitWeights(), unitWeightsGlobal);
                                unitWeights.valueHasMutated();
                                //Chart Of Accounts
                                chartOfAccounts.removeAll();
                                ko.utils.arrayPushAll(chartOfAccounts(), data.ChartOfAccounts);
                                chartOfAccounts.valueHasMutated();
                                //Markups
                                markups.removeAll();
                                ko.utils.arrayPushAll(markups(), data.Markups);
                                markups.valueHasMutated();
                                //Tax Rates
                                taxRates.removeAll();
                                var taxRateList = [];
                                _.each(data.TaxRates, function(item) {
                                    var taxRate = new model.TaxRateClientMapper(item);
                                    taxRateList.push(taxRate);
                                });
                                ko.utils.arrayPushAll(taxRates(), taxRateList);
                                taxRates.valueHasMutated();
                                if (callBack && typeof callBack === 'function') {
                                    callBack();
                                }
                            },
                            error: function() {
                                toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                            }
                        });
                    },
                    // Template Chooser
                    templateToUse = function(taxRate) {
                        return (taxRate === selectedTaxRate() ? 'editTaxRateTemplate' : 'itemTaxRateTemplate');
                    },
                    makeEditable = ko.observable(false),
                     // Select a Tax Rate
                    selectTaxRate = function (taxRate) {
                        if (selectedTaxRate() !== taxRate) {
                            selectedTaxRate(taxRate);
                        }
                        //if (selectedTaxRate() === taxRate) {
                        //    makeEditable(true);
                        //    return (selectedTaxRate() === taxRate && taxRate() ? "editTaxRateTemplate" : "itemTaxRateTemplate");
                        //}
                        //isEditable(true);
                    },
                    onCreateNewTaxRate = function () {
                        var taxRate = taxRates()[0];
                        if (taxRate.name() !== undefined && taxRate.tax1() !== undefined) {
                            taxRates.splice(0, 0, model.TaxRate());
                        }
                    },
                     // Delete a tax Rate
                    onDeleteTaxRate = function (taxRate) {
                        if (!taxRate.id()) {
                            taxRates.remove(taxRate);
                            return;
                        }
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            taxRates.remove(taxRate);
                        });
                        confirmation.show();
                    },
                    //Get Additional Charge By Id
                    getMyOrganizationById = function (addChrg) {
                        isLoadingMyOrganization(true);
                        dataservice.getMyOrganizationDetail(model.AdditionalChrgServerMapperForId(addChrg), {
                            success: function (data) {
                                isLoadingMyOrganization(false);
                            },
                            error: function () {
                                isLoadingMyOrganization(false);
                                toastr.error(ist.resourceText.loadAddChargeDetailFailedMsg);
                            }
                        });
                    },
                    // Save My Organization
                    onSaveMyOrganization = function (myOrg) {
                        if (doBeforeSave()) {

                            //addCharge.additionalChargesList.removeAll();
                            //ko.utils.arrayPushAll(addCharge.additionalChargesList(), additionalCharges());
                            saveMyOrganization(myOrg);
                        }
                    },
                    // Do Before Logic
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedMyOrganization().isValid()) {
                            selectedMyOrganization().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                    // Save My Organization
                    saveMyOrganization = function (myOrg) {
                        dataservice.saveMyOrganization(model.CompanySitesServerMapper(myOrg), {
                            success: function (data) {
                                var additionalCharge = data;
                                //if (selectedAdditionalChargeType().id() > 0) {
                                //    selectedAdditionalChargeType().isEditable(additionalCharge.isEditable()),
                                //        closeAdditionalChargeEditor();
                                //} else {
                                //    additionalTypeCharges.splice(0, 0, additionalCharge);
                                //    closeAdditionalChargeEditor();
                                //}
                                toastr.success("Successfully save.");
                            },
                            error: function (exceptionMessage, exceptionType) {

                                if (exceptionType === ist.exceptionType.CaresGeneralException) {

                                    toastr.error(exceptionMessage);

                                } else {

                                    toastr.error("Failed to save.");

                                }

                            }
                        });
                    };
                // #endregion Service Calls

                return {
                    // Observables
                    selectedMyOrganization: selectedMyOrganization,
                    selectedTaxRate: selectedTaxRate,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    sortOnHg: sortOnHg,
                    sortIsAscHg: sortIsAscHg,
                    //Arrays
                    currencySymbols: currencySymbols,
                    languagePacks: languagePacks,
                    unitLengths: unitLengths,
                    unitWeights: unitWeights,
                    markups: markups,
                    chartOfAccounts: chartOfAccounts,
                    taxRates: taxRates,
                    // Utility Methods
                    initialize: initialize,
                    pager: pager,
                    // Utility Methods
                    onSaveMyOrganization: onSaveMyOrganization,
                    templateToUse: templateToUse,
                    selectTaxRate: selectTaxRate,
                    onDeleteTaxRate: onDeleteTaxRate,
                    onCreateNewTaxRate: onCreateNewTaxRate,
                };
            })()
        };
        return ist.myOrganization.viewModel;
    });
