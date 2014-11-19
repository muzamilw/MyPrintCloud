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
                    //Active markup
                    selectedMarkup = ko.observable(),
                    //Active Chart Of Accounts
                    selectedChartOfAccounts = ko.observable(),
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
                    //Markup List For Drop Down
                    markupsForDropDown = ko.observableArray([]),
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
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        getBase();
                        // Set Pager
                        //pager(new pagination.Pagination({}, additionalTypeCharges, getAdditionalCharges));
                        //getAdditionalCharges();
                        selectedMyOrganization(new model.CompanySites());
                    },
                    // Get Base
                    getBase = function (callBack) {
                        dataservice.getMyOrganizationBase({
                            success: function (data) {
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
                                var chartOfAccountsList = [];
                                _.each(data.ChartOfAccounts, function (item) {
                                    var chartOfAcc = new model.ChartOfAccountClientMapper(item);
                                    chartOfAccountsList.push(chartOfAcc);
                                });
                                ko.utils.arrayPushAll(chartOfAccounts(), chartOfAccountsList);
                                chartOfAccounts.valueHasMutated();

                                //Markups
                                markups.removeAll();
                                var markupList = [];
                                _.each(data.Markups, function (item) {
                                    var markup = new model.MarkupClientMapper(item);
                                    markupList.push(markup);
                                });
                                ko.utils.arrayPushAll(markups(), markupList);
                                markups.valueHasMutated();
                                //Mark up for drop down
                                markupsForDropDown.removeAll();
                                ko.utils.arrayPushAll(markupsForDropDown(), data.Markups);
                                markupsForDropDown.valueHasMutated();
                                //Tax Rates
                                taxRates.removeAll();
                                var taxRateList = [];
                                _.each(data.TaxRates, function (item) {
                                    var taxRate = new model.TaxRateClientMapper(item);
                                    taxRateList.push(taxRate);
                                });
                                ko.utils.arrayPushAll(taxRates(), taxRateList);
                                taxRates.valueHasMutated();
                                if (callBack && typeof callBack === 'function') {
                                    callBack();
                                }
                            },
                            error: function () {
                                toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                            }
                        });
                    },
                    // Template Chooser
                    templateToUse = function (taxRate) {
                        return (taxRate === selectedTaxRate() ? 'editTaxRateTemplate' : 'itemTaxRateTemplate');
                    },
                    // Template Chooser For Markup
                    templateToUseMarkup = function (markup) {
                        return (markup === selectedMarkup() ? 'editMarkupTemplate' : 'itemMarkupTemplate');
                    },
                    // Template Chooser For Chart Of Accounts
                    templateToUseChartOfAccounts = function (chartOfAcc) {
                        return (chartOfAcc === selectedChartOfAccounts() ? 'editChartOfAccountsTemplate' : 'itemChartOfAccountsTemplate');
                    },
                      // Select a Tax Rate
                    selectTaxRate = function (taxRate) {
                        if (selectedTaxRate() !== taxRate) {
                            selectedTaxRate(taxRate);
                        }
                    },
                     // Select a Markup
                    selectMarkup = function (markup) {
                        if (selectedMarkup() !== markup) {
                            selectedMarkup(markup);
                        }
                    },
                     // Select a Chart Of Accounts
                    selectChartOfAccounts = function (chartOfAcc) {
                        if (selectedChartOfAccounts() !== chartOfAcc) {
                            selectedChartOfAccounts(chartOfAcc);
                        }
                    },
                    onCreateNewTaxRate = function () {
                        var taxRate = taxRates()[0];
                        if (taxRate.name() !== undefined && taxRate.tax1() !== undefined) {
                            taxRates.splice(0, 0, model.TaxRate());
                            selectedTaxRate(taxRates()[0]);
                        }
                    },
                     onCreateNewMarkup = function () {
                         var markup = markups()[0];
                         if (markup.name() !== undefined && markup.rate() !== undefined) {
                             markups.splice(0, 0, model.Markup());
                             selectedMarkup(markups()[0]);
                         }
                     },
                      onCreateChartOfAccounts = function () {
                          var chartOfAcc = chartOfAccounts()[0];
                          if (chartOfAcc.name() !== undefined && chartOfAcc.accountNo() !== undefined) {
                              chartOfAccounts.splice(0, 0, model.ChartOfAccount());
                              selectedChartOfAccounts(chartOfAccounts()[0]);
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
                       // Delete a Chart Of Accounts
                    onDeleteChartOfAccounts = function (chartOfAcc) {
                        if (!chartOfAcc.id()) {
                            chartOfAccounts.remove(chartOfAcc);
                            return;
                        }
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            chartOfAccounts.remove(chartOfAcc);
                        });
                        confirmation.show();
                    },

                       // Delete a Markup
                    onDeleteMarkup = function (markup) {
                        if (!markup.id()) {
                            markups.remove(markup);
                            return;
                        }
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            markups.remove(markup);
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
                            //Markup List
                            if (myOrg.markupsInMyOrganization.length !== 0) {
                                myOrg.markupsInMyOrganization.removeAll();
                            }
                            ko.utils.arrayPushAll(myOrg.markupsInMyOrganization(), markups());
                            //Tax Rate List
                            if (myOrg.taxRatesInMyOrganization.length !== 0) {
                                myOrg.taxRatesInMyOrganization.removeAll();
                            }
                            ko.utils.arrayPushAll(myOrg.taxRatesInMyOrganization(), taxRates());
                            //Chart of Accounts List
                            if (myOrg.chartOfAccountsInMyOrganization.length !== 0) {
                                myOrg.chartOfAccountsInMyOrganization.removeAll();
                            }
                            ko.utils.arrayPushAll(myOrg.chartOfAccountsInMyOrganization(), chartOfAccounts);
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
                    selectedMarkup: selectedMarkup,
                    selectedChartOfAccounts: selectedChartOfAccounts,
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
                    markupsForDropDown: markupsForDropDown,
                    // Utility Methods
                    initialize: initialize,
                    pager: pager,
                    // Utility Methods
                    onSaveMyOrganization: onSaveMyOrganization,
                    templateToUse: templateToUse,
                    templateToUseMarkup: templateToUseMarkup,
                    templateToUseChartOfAccounts: templateToUseChartOfAccounts,
                    selectTaxRate: selectTaxRate,
                    selectMarkup: selectMarkup,
                    selectChartOfAccounts: selectChartOfAccounts,
                    onDeleteTaxRate: onDeleteTaxRate,
                    onDeleteMarkup: onDeleteMarkup,
                    onDeleteChartOfAccounts: onDeleteChartOfAccounts,
                    onCreateNewTaxRate: onCreateNewTaxRate,
                    onCreateNewMarkup: onCreateNewMarkup,
                    onCreateChartOfAccounts: onCreateChartOfAccounts,
                };
            })()
        };
        return ist.myOrganization.viewModel;
    });
