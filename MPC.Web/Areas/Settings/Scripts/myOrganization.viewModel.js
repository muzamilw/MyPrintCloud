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
                       //Active markup
                    selectedMarkup = ko.observable(),
                    //Active Chart Of Accounts
                    selectedChartOfAccounts = ko.observable(),
                    //Orgnization Image
                    orgnizationImage = ko.observable(),
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
                    //Error List
                    errorList = ko.observableArray([]),
                    //Markups
                    markups = ko.observableArray([]),
                    //Markup List For Drop Down
                    markupsForDropDown = ko.observableArray([]),
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
                       selectedMyOrganization().id(2);
                        getMyOrganizationById(selectedMyOrganization());
                        view.initializeForm();
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
                                if (callBack && typeof callBack === 'function') {
                                    callBack();
                                }
                            },
                            error: function () {
                                toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                            }
                        });
                    },
                       // Template Chooser For Markup
                    templateToUseMarkup = function (markup) {
                        return (markup === selectedMarkup() ? 'editMarkupTemplate' : 'itemMarkupTemplate');
                    },
                    // Template Chooser For Chart Of Accounts
                    templateToUseChartOfAccounts = function (chartOfAcc) {
                        return (chartOfAcc === selectedChartOfAccounts() ? 'editChartOfAccountsTemplate' : 'itemChartOfAccountsTemplate');
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
                       //Create Markup
                     onCreateNewMarkup = function () {
                         var markup = markups()[0];
                         if (markup.name() !== undefined && markup.rate() !== undefined) {
                             markups.splice(0, 0, model.Markup());
                             selectedMarkup(markups()[0]);
                         }
                     },
                     //Create Chart Of Account
                    onCreateChartOfAccounts = function () {
                        var chartOfAcc = chartOfAccounts()[0];
                        if (chartOfAcc.name() !== undefined && chartOfAcc.accountNo() !== undefined) {
                            chartOfAccounts.splice(0, 0, model.ChartOfAccount());
                            selectedChartOfAccounts(chartOfAccounts()[0]);
                        }
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
                    //Get Organization By Id
                    getMyOrganizationById = function (org) {
                        isLoadingMyOrganization(true);
                        dataservice.getMyOrganizationDetail(model.OrganizationServerMapperForId(org), {
                            success: function (data) {
                                selectedMyOrganization(model.CompanySitesClientMapper(data));
                                orgnizationImage(data.ImageSource);
                                view.initializeForm();
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
                        errorList.removeAll();
                        if (doBeforeSave() & doBeforeSaveMarkups() & doBeforeSaveChartOfAccounts()) {
                            //Markup List
                            if (myOrg.markupsInMyOrganization.length !== 0) {
                                myOrg.markupsInMyOrganization.removeAll();
                            }
                            ko.utils.arrayPushAll(myOrg.markupsInMyOrganization(), markups());
                            //Chart of Accounts List
                            if (myOrg.chartOfAccountsInMyOrganization.length !== 0) {
                                myOrg.chartOfAccountsInMyOrganization.removeAll();
                            }
                            ko.utils.arrayPushAll(myOrg.chartOfAccountsInMyOrganization(), chartOfAccounts());
                            saveMyOrganization(myOrg);
                        }
                    },
                    // Do Before Logic
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedMyOrganization().isValid()) {
                            selectedMyOrganization().errors.showAllMessages();
                            if (selectedMyOrganization().email.error != null) {
                                errorList.push({ fieldId: "txtEmail", tabId: 1, name: "Email" });
                            }
                            flag = false;
                        }
                        return flag;
                    },
                    // Do Before Logic
                    doBeforeSaveMarkups = function () {
                        var flag = true;
                        _.each(markups(), function (markup, index) {
                            if (!markup.isValid()) {
                                markup.errors.showAllMessages();
                                if (flag) {
                                    if (markup.name.error != null || markup.rate.error != null) {
                                        errorList.push({ fieldId: 'markupName,' + index, tabId: 2, name: "Invalid Markups" });
                                    }
                                }
                                flag = false;
                            }
                        });
                        return flag;
                    },
                     // Do Before Logic
                    doBeforeSaveChartOfAccounts = function () {
                        var flag = true;
                        _.each(chartOfAccounts(), function (chartofAcc, index) {
                            if (!chartofAcc.isValid()) {
                                chartofAcc.errors.showAllMessages();
                                if (flag) {
                                    if (chartofAcc.name.error != null || chartofAcc.accountNo.error != null) {
                                        errorList.push({ fieldId: 'nominalCodeName,' + index, tabId: 3, name: "Invalid Nominal Codes" });
                                    }
                                    flag = false;
                                }
                            }
                        });

                        return flag;
                    },

                    selectTab = function (property) {
                        //$('#myTab li:eq(2) a').tab('show');
                        if (property.tabId === 1) {
                            $('#myTab a[href="#tab-ORGDetail"]').tab('show');
                            $("#" + property.fieldId).focus();
                        }
                        if (property.tabId === 2) {
                            $('#myTab a[href="#tab-RegionalSettings"]').tab('show');
                            var index1 = property.fieldId.split(',')[1];
                            selectedMarkup(markups()[index1]);
                        }
                        if (property.tabId === 3) {
                            $('#myTab a[href="#tab-NominalCodes"]').tab('show');
                            var index = property.fieldId.split(',')[1];
                            selectedChartOfAccounts(chartOfAccounts()[index]);
                        }

                    },
                    // Save My Organization
                    saveMyOrganization = function (myOrg) {
                        dataservice.saveMyOrganization(model.CompanySitesServerMapper(myOrg), {
                            success: function (data) {
                                var orgId = data.OrganizationId;
                                if (selectedMyOrganization().id() > 0) {
                                    //Update IDs of Newly added Chart of Accounts
                                    _.each(data.ChartOfAccounts, function (item) {
                                        var chartOfAcc = new model.ChartOfAccountClientMapper(item);
                                        _.each(chartOfAccounts(), function (chartOfAccount) {
                                            if (chartOfAccount.id() === undefined) {
                                                if (chartOfAcc.name() === chartOfAccount.name() && chartOfAcc.accountNo() === chartOfAccount.accountNo()) {
                                                    chartOfAccount.id(chartOfAcc.id());
                                                }
                                            }
                                        });
                                    });
                                    //Update IDs of Newly added Markups
                                    _.each(data.Markups, function (item) {
                                        var markup = new model.MarkupClientMapper(item);
                                        _.each(markups(), function (markupListItem) {
                                            if (markupListItem.id() === undefined) {
                                                if (markup.name() === markupListItem.name() && markup.rate() === markupListItem.rate()) {
                                                    markupListItem.id(markup.id());
                                                }
                                            }
                                        });
                                    });

                                } else {
                                    selectedMyOrganization(), id(orgId);
                                }
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
                    selectedMarkup: selectedMarkup,
                    selectedChartOfAccounts: selectedChartOfAccounts,
                    orgnizationImage: orgnizationImage,
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
                    markupsForDropDown: markupsForDropDown,
                    errorList: errorList,
                    // Utility Methods
                    initialize: initialize,
                    pager: pager,
                    // Utility Methods
                    onSaveMyOrganization: onSaveMyOrganization,
                    templateToUseMarkup: templateToUseMarkup,
                    templateToUseChartOfAccounts: templateToUseChartOfAccounts,
                    selectMarkup: selectMarkup,
                    selectChartOfAccounts: selectChartOfAccounts,
                    onDeleteMarkup: onDeleteMarkup,
                    onDeleteChartOfAccounts: onDeleteChartOfAccounts,
                    onCreateNewMarkup: onCreateNewMarkup,
                    onCreateChartOfAccounts: onCreateChartOfAccounts,
                    selectTab: selectTab,
                };
            })()
        };
        return ist.myOrganization.viewModel;
    });
