/*
    Module with the view model for the My Organization.
*/
define("myOrganization/myOrganization.viewModel",
    ["jquery", "amplify", "ko", "myOrganization/myOrganization.dataservice", "myOrganization/myOrganization.model", "common/confirmation.viewModel",
        "common/pagination", "common/sharedNavigation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVM) {
        var ist = window.ist || {};
        ist.myOrganization = {
            viewModel: (function () {
                var // the view 
                    view,
                    // Active
                    selectedMyOrganization = ko.observable(),

                    
                    //Counter
                    idCounter = ko.observable(-1),
                    //Active markup
                    selectedMarkup = ko.observable(),
                    //Active Markup Copy
                    selectedMarkupCopy = ko.observable(),
                    //Active Chart Of Accounts
                    selectedChartOfAccounts = ko.observable(),
                    //Active Chart Of Accounts Copy
                    selectedChartOfAccountsCopy = ko.observable(),
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
                    //Countries
                    countries = ko.observableArray([]),
                    //States
                    states = ko.observableArray([]),
                    //Filtered States
                    filteredStates = ko.observableArray([]),
                    //Filtered Markups
                    filteredMarkups = ko.observableArray([]),
                    //Filtered Nominal Codes
                    filteredNominalCodes = ko.observableArray([]),
                    //Mark up search string 
                    markupSearchString = ko.observable(),
                    //Nominal Code Search String
                    nominalCodeSearchString = ko.observable(),
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

                    // visiblity of organisation / regional settings /  markups and language editor
                    isOrganisationVisible = ko.observable(false),

                    isMarkupVisible = ko.observable(false),

                    isRegionalSettingVisible = ko.observable(false),

                    isLanguageEditorVisible = ko.observable(false),
                    isApiDetailVisible = ko.observable(false),

                    // for specifice name of screan
                    HeadingName = ko.observable(),
                    // #region Utility Functions
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        selectedMyOrganization(new model.CompanySites());
                        
                       
                        if(page == "O")
                        {
                           
                            HeadingName("Organization Detail");
                            isOrganisationVisible(true);
                        }
                        else if(page == "R")
                        {
                            //getRegionalSettings();
                            HeadingName("Currency, Size & Weight Settings");
                            isRegionalSettingVisible(true);
                        }
                        else if(page == "L")
                        {
                            HeadingName("Edit Web Store Dictionary");
                            isLanguageEditorVisible(true);
                        }
                        else if(page == "M")
                        {
                            //getMarkupsList();
                            HeadingName("Markups");
                            isMarkupVisible(true);
                        }
                        else if (page == "api") {
                            //getMarkupsList();
                            HeadingName("Agile API");
                            isApiDetailVisible(true);
                        }
                        
                        getBase();
                        view.initializeForm();
                    },
                    //getMarkupsList
                    //getMarkupsList = function(callback)
                    //{
                    //    dataservice.getMarkups({IsMarkup:true},{
                    //        success: function (data) {
                    //            //Markups
                    //            markups.removeAll();
                    //            var markupList = [];
                    //            _.each(data, function (item) {
                    //                var markup = new model.MarkupClientMapper(item);
                    //                markupList.push(markup);
                    //            });
                    //            ko.utils.arrayPushAll(markups(), markupList);
                    //            markups.valueHasMutated();
                    //            //Mark up for drop down
                    //            markupsForDropDown.removeAll();
                    //            ko.utils.arrayPushAll(markupsForDropDown(), data);
                    //            markupsForDropDown.valueHasMutated();
                    //            //Filtered Markups
                    //            filteredMarkups.removeAll();
                    //            ko.utils.arrayPushAll(filteredMarkups(), markups());
                    //            filteredMarkups.valueHasMutated();

                    //            getMyOrganizationById();
                               
                    //        },
                    //        error: function () {
                    //            view.initializeLabelPopovers();
                    //            toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                                
                    //        }

                    //    });
                    //},


                    // get regional settings
                     //getRegionalSettings = function (callback) {
                     //    dataservice.getRegionalSettings({ isRegional: true }, {
                     //        success: function (data) {

                     //            //Currency
                     //            currencySymbols.removeAll();
                     //            ko.utils.arrayPushAll(currencySymbols(), data.Currencies);
                     //            currencySymbols.valueHasMutated();

                     //           // unit Lengths
                     //            unitLengths.removeAll();
                     //            ko.utils.arrayPushAll(unitLengths(), data.LengthUnits);
                     //            unitLengths.valueHasMutated();
                     //          //  unit Weights
                     //            unitWeights.removeAll();
                     //            ko.utils.arrayPushAll(unitWeights(), data.WeightUnits);
                     //            unitWeights.valueHasMutated();


                     //            getMyOrganizationById();

                     //        },
                     //        error: function () {
                     //            view.initializeLabelPopovers();
                     //            toastr.error(ist.resourceText.loadBaseDataFailedMsg);

                     //        }

                     //    });
                     //},
                    //Get Base
                    getBase = function (callBack) {
                      //  isLoadingMyOrganization(true);
                        dataservice.getMyOrganizationBase({
                           
                            success: function (data) {
                                //Currency
                                currencySymbols.removeAll();
                                ko.utils.arrayPushAll(currencySymbols(), data.Currencies);
                                currencySymbols.valueHasMutated();
                                //Language Packs
                                languagePacks.removeAll();
                                ko.utils.arrayPushAll(languagePacks(), data.GlobalLanguages);
                                languagePacks.valueHasMutated();
                                //unit Lengths
                                unitLengths.removeAll();
                                ko.utils.arrayPushAll(unitLengths(), data.LengthUnits);
                                unitLengths.valueHasMutated();
                                //unit Weights
                                unitWeights.removeAll();
                                ko.utils.arrayPushAll(unitWeights(), data.WeightUnits);
                                unitWeights.valueHasMutated();
                                //Countries 
                                countries.removeAll();
                                ko.utils.arrayPushAll(countries(), data.Countries);
                                countries.valueHasMutated();
                                //States 
                                states.removeAll();
                                ko.utils.arrayPushAll(states(), data.States);
                                states.valueHasMutated();
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
                                //Filtered Markups
                                filteredMarkups.removeAll();
                                ko.utils.arrayPushAll(filteredMarkups(), markups());
                                filteredMarkups.valueHasMutated();
                              //  Filtered Markups
                                filteredNominalCodes.removeAll();
                                ko.utils.arrayPushAll(filteredNominalCodes(), chartOfAccounts());
                                filteredNominalCodes.valueHasMutated();

                             
                                getMyOrganizationById();
                                
                                
                               
                                if (callBack && typeof callBack === 'function') {
                                    callBack();
                                }
                                
                            },
                            error: function () {
                                
                                view.initializeLabelPopovers();
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
                        if (selectedMarkup() !== undefined && !selectedMarkup().isValid()) {
                            return;
                        }

                        if (selectedMarkup() !== markup) {
                            //update previous selected item to orginal markup list
                            if (selectedMarkup() !== undefined) {
                                _.each(markups(), function (item) {
                                    if ((item.id() === selectedMarkup().id())) {
                                        item.name(selectedMarkup().name());
                                        item.rate(selectedMarkup().rate());
                                    }
                                });
                            }
                            var markupCopy = new model.Markup();
                            markupCopy.id(markup.id());
                            markupCopy.name(markup.name());
                            markupCopy.rate(markup.rate());
                            selectedMarkupCopy(markupCopy);
                            selectedMarkup(markup);
                        }
                    },
                    // Select a Chart Of Accounts
                    selectChartOfAccounts = function (chartOfAcc) {

                        if (selectedChartOfAccounts() !== undefined && !selectedChartOfAccounts().isValid()) {
                            return;
                        }
                        if (selectedChartOfAccounts() !== chartOfAcc) {
                            //update previous selected item to orginal Chart of Account list
                            if (selectedChartOfAccounts() !== undefined) {
                                _.each(chartOfAccounts(), function (item) {
                                    if ((item.id() === selectedChartOfAccounts().id()) && (item.name() !== selectedChartOfAccountsCopy().name() || item.accountNo() !== selectedChartOfAccountsCopy().accountNo())) {
                                        item.name(selectedChartOfAccounts().name());
                                        item.accountNo(selectedChartOfAccounts().accountNo());
                                    }
                                });
                            }

                            var nominalCodeCopy = model.ChartOfAccount();
                            nominalCodeCopy.accountNo(chartOfAcc.accountNo());
                            nominalCodeCopy.name(chartOfAcc.name());
                            nominalCodeCopy.id(chartOfAcc.id());
                            selectedChartOfAccountsCopy(nominalCodeCopy);
                            selectedChartOfAccounts(chartOfAcc);
                        }

                    },
                    //Create Markup
                    onCreateNewMarkup = function () {
                        var markup = filteredMarkups()[0];
                        if ((filteredMarkups().length === 0) || (markup !== undefined && markup !== null && markup.name() !== undefined && markup.rate() !== undefined && markup.isValid())) {
                            var newMarkup = model.Markup();
                            newMarkup.id(idCounter() - 1);
                            newMarkup.rate(0);
                            //New Id
                            idCounter(idCounter() - 1);
                            markups.splice(0, 0, newMarkup);
                            filteredMarkups.splice(0, 0, newMarkup);
                            selectedMarkup(filteredMarkups()[0]);
                            selectedMyOrganization().flagForChanges("Changes occur");
                        }
                        if (markup !== undefined && markup !== null && !markup.isValid()) {
                            markup.errors.showAllMessages();
                        }
                    },
                    //Create Chart Of Account
                    onCreateChartOfAccounts = function () {
                        var chartOfAcc = filteredNominalCodes()[0];
                        if ((filteredNominalCodes().length === 0) || (chartOfAcc !== undefined && chartOfAcc !== null && chartOfAcc.name() !== undefined && chartOfAcc.accountNo() !== undefined && chartOfAcc.isValid())) {
                            var newChartOfAccount = model.ChartOfAccount();
                            newChartOfAccount.id(idCounter() - 1);
                            newChartOfAccount.accountNo(0);
                            //New Id
                            idCounter(idCounter() - 1);
                            chartOfAccounts.splice(0, 0, newChartOfAccount);
                            filteredNominalCodes.splice(0, 0, newChartOfAccount);
                            selectedChartOfAccounts(filteredNominalCodes()[0]);
                            selectedMyOrganization().flagForChanges("Changes occur");
                        }
                        if (chartOfAcc !== undefined && chartOfAcc !== null && !chartOfAcc.isValid()) {
                            chartOfAcc.errors.showAllMessages();
                        }
                    },
                    // Delete a Chart Of Accounts
                    onDeleteChartOfAccounts = function (chartOfAcc) {
                        filteredNominalCodes.remove(chartOfAcc);
                        _.each(chartOfAccounts(), function (item) {
                            if ((item.id() === chartOfAcc.id())) {
                                chartOfAccounts.remove(item);
                            }
                        });
                        selectedMyOrganization().flagForChanges("Changes occur");
                    },
                    // Delete a Markup
                    onDeleteMarkup = function (markup) {
                        if (selectedMyOrganization().markupId() === markup.id()) {
                            toastr.error("Default Markup cannot be deleted.");
                        } else {
                            confirmation.messageText("WARNING - All items will be removed from the system and you won’t be able to recover.  There is no undo");
                            confirmation.afterProceed(function() {
                                filteredMarkups.remove(markup);
                                _.each(markups(), function (item) {
                                    if ((item.id() === markup.id())) {
                                        markups.remove(item);
                                    }
                                });
                                selectedMyOrganization().flagForChanges("Changes occur");
                                var markupForDelete = _.find(markupsForDropDown(), function (item) {
                                    return item.MarkUpId === markup.id();
                                });
                                if (markupForDelete) {
                                    markupsForDropDown.remove(markupForDelete);
                                }
                            });
                            confirmation.show();
                        }
                    },
                    //Get Organization By Id
                    getMyOrganizationById = function () {
                       isLoadingMyOrganization(true);
                        dataservice.getMyOrganizationDetail({
                            success: function (data) {
                               
                                filteredStates.removeAll();
                                var org = model.CompanySitesClientMapper(data);
                                if (isLanguageEditorVisible() == true)
                                {
                                   // isLoadingMyOrganization(true);
                                    _.each(data.LanguageEditors, function (item) {
                                        org.languageEditors.push(model.LanguageEditor.Create(item));
                                    });
                                   // isLoadingMyOrganization(false);
                                }
                            
                                
                                selectedMyOrganization(org);

                                if (selectedMyOrganization().isImperical() == true)
                                {
                                    selectedMyOrganization().isImperical("true");
                                }
                                else
                                {
                                    selectedMyOrganization().isImperical("false");
                                }
                                selectedMyOrganization().reset();

                                orgnizationImage(data.ImageSource);
                                view.initializeForm();
                                isLoadingMyOrganization(false);
                                sharedNavigationVM.initialize(selectedMyOrganization, function (saveCallback) { onSaveMyOrganization(saveCallback); });
                                view.initializeLabelPopovers();
                            },
                            error: function () {
                                isLoadingMyOrganization(false);
                                view.initializeLabelPopovers();
                                toastr.error(ist.resourceText.loadAddChargeDetailFailedMsg);
                            }
                        });
                       
                    },
                    // Save My Organization
                    onSaveMyOrganization = function (callback) {
                        errorList.removeAll();

                       
                            //Selected Markup update in markup list
                            if (selectedMarkup() !== undefined) {
                                _.each(markups(), function (item) {
                                    if ((item.id() === selectedMarkup().id())) {
                                        item.name(selectedMarkup().name());
                                        item.rate(selectedMarkup().rate());
                                    }
                                });
                            }
                        
                       
                        //Selected Markup update in Chart Of Accounts list
                        if (selectedChartOfAccounts() !== undefined) {
                            _.each(chartOfAccounts(), function (item) {
                                if ((item.id() === selectedChartOfAccounts().id())) {
                                    item.name(selectedChartOfAccounts().name());
                                    item.accountNo(selectedChartOfAccounts().accountNo());
                                }
                            });
                        }

                        if (doBeforeSave() & doBeforeSaveMarkups()) {
                            //Markup List
                            if (selectedMyOrganization().markupsInMyOrganization.length !== 0) {
                                selectedMyOrganization().markupsInMyOrganization.removeAll();
                            }
                            ko.utils.arrayPushAll(selectedMyOrganization().markupsInMyOrganization(), markups());
                            //Chart of Accounts List
                            if (selectedMyOrganization().chartOfAccountsInMyOrganization.length !== 0) {
                                selectedMyOrganization().chartOfAccountsInMyOrganization.removeAll();
                            }
                            ko.utils.arrayPushAll(selectedMyOrganization().chartOfAccountsInMyOrganization(), chartOfAccounts());
                            saveMyOrganization(callback);
                        }
                    },
                    // Do Before Logic
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedMyOrganization().isValid()) {
                            selectedMyOrganization().errors.showAllMessages();
                            if (selectedMyOrganization().email.error != null) {
                                errorList.push({ name: selectedMyOrganization().email.domElement.name, element: selectedMyOrganization().email.domElement });
                            }
                            if (isMarkupVisible) {
                                if (selectedMyOrganization().markupId.error != null) {
                                    errorList.push({ name: "Markup", element: selectedMyOrganization().markupId.domElement });
                                }
                            }
                           
                            flag = false;
                        }
                        return flag;
                    },
                     // Go To Element
                    gotoElement = function (validation) {
                        view.gotoElement(validation.element);
                    },
                    // Do Before Logic
                    doBeforeSaveMarkups = function () {
                        var flag = true;
                        if (isMarkupVisible) {
                            // Show Markup Item Errors
                            var itemMarkupInvalid = markups.find(function (itemMarkup) {
                                return !itemMarkup.isValid();
                            });
                            if (itemMarkupInvalid) {
                                if (itemMarkupInvalid.name.error) {
                                    errorList.push({ name: "Name", element: itemMarkupInvalid.name.domElement });
                                    flag = false;
                                }
                                if (itemMarkupInvalid.rate.error) {
                                    errorList.push({ name: "Rate", element: itemMarkupInvalid.rate.domElement });
                                    flag = false;
                                }
                            }
                        }
                        

                        return flag;
                    },
                    // Do Before Logic
                    doBeforeSaveChartOfAccounts = function () {
                        var flag = true;
                        // Show Markup Item Errors
                        var itemChartOfAccountInvalid = chartOfAccounts.find(function (chartOfAccount) {
                            return !chartOfAccount.isValid();
                        });
                        if (itemChartOfAccountInvalid) {
                            if (itemChartOfAccountInvalid.name.error) {
                                errorList.push({ name: "Nominal Code Name", element: itemChartOfAccountInvalid.name.domElement });
                                flag = false;
                            }
                            if (itemChartOfAccountInvalid.accountNo.error) {
                                errorList.push({ name: "Account No.", element: itemChartOfAccountInvalid.accountNo.domElement });
                                flag = false;
                            }
                        }
                        return flag;
                    },
                    //Select tab click on error link
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
                    //Filter States based on Country
                    filterStates = ko.computed(function () {
                        if (selectedMyOrganization() !== undefined && selectedMyOrganization().country() !== undefined) {
                            filteredStates.removeAll();
                            _.each(states(), function (item) {
                                if (item.CountryId === selectedMyOrganization().country()) {
                                    filteredStates.push(item);
                                }
                            });
                        }
                    }, this),
                    // Save My Organization
                    saveMyOrganization = function (callback) {
                        dataservice.saveMyOrganization(model.CompanySitesServerMapper(selectedMyOrganization()), {
                            success: function (data) {
                                var orgId = data.OrganizationId;
                                if (selectedMyOrganization().id() > 0) {
                                    //Update IDs of Newly added Chart of Accounts
                                    _.each(data.ChartOfAccounts, function (item) {
                                        var chartOfAcc = new model.ChartOfAccountClientMapper(item);
                                        _.each(chartOfAccounts(), function (chartOfAccount) {
                                            if (chartOfAccount.id() < 0) {
                                                if (chartOfAcc.name() === chartOfAccount.name() && chartOfAcc.accountNo() === chartOfAccount.accountNo()) {
                                                    chartOfAccount.id(chartOfAcc.id());
                                                }
                                            }
                                        });
                                    });
                                    //Update IDs of Newly added Markups
                                    _.each(data.Markups, function (item) {
                                        var markupServer = new model.MarkupClientMapper(item);
                                        _.each(markups(), function (markupListItem) {
                                            if (markupListItem.id() < 0) {
                                                if (markupServer.name() === markupListItem.name() && markupServer.rate() === markupListItem.rate()) {
                                                    markupListItem.id(markupServer.id());
                                                }
                                            }
                                        });
                                    });

                                    _.each(data.Markups, function (item) {
                                        var markupItem = _.find(markupsForDropDown(), function (markupDropDownItem) {
                                            return markupDropDownItem.MarkUpId === item.MarkUpId;
                                        });
                                        if (markupItem === undefined) {
                                            markupsForDropDown.push(item);
                                        }
                                    });


                                } else {
                                    selectedMyOrganization(), id(orgId);
                                }
                                selectedMyOrganization().flagForChanges(undefined);
                                selectedMyOrganization().reset();
                                toastr.success("Successfully save.");
                                if (callback && typeof callback === "function") {
                                    callback();
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

                    //If change occour in any makup item then save button enable(save button show changes occur)
                    markupChange = ko.computed(function () {
                        if (selectedMarkup() !== undefined && selectedMarkupCopy() !== undefined) {
                            if ((selectedMarkup().id() === selectedMarkupCopy().id()) && (selectedMarkup().name() !== selectedMarkupCopy().name() || selectedMarkup().rate() !== selectedMarkupCopy().rate())) {
                                selectedMyOrganization().flagForChanges("Changes occur");
                            }
                        }
                    }, this),
                     //If change occour in any Nominal Code item then save button enable(save button show changes occur)
                    nominalCodeChange = ko.computed(function () {
                        if (selectedChartOfAccounts() !== undefined && selectedChartOfAccountsCopy() !== undefined) {
                            if ((selectedChartOfAccounts().id() === selectedChartOfAccountsCopy().id()) && (selectedChartOfAccounts().name() !== selectedChartOfAccountsCopy().name() || selectedChartOfAccounts().accountNo() !== selectedChartOfAccountsCopy().accountNo())) {
                                selectedMyOrganization().flagForChanges("Changes occur");
                            }
                        }
                    }, this),
                   //Search Markup 
                    searchMarkup = function () {
                        var markup = filteredMarkups()[0];
                        //New Added item remove on search, if item not have code
                        if (markup != undefined && (markup.name() === undefined || markup.name() === "") && markup.id() < 0) {
                            _.each(markups(), function (item) {
                                if (item.id() === markup.id()) {
                                    markups.remove(item);
                                }
                            });
                        }
                        filteredMarkups.removeAll();
                        if (markupSearchString() !== undefined && markupSearchString().trim() !== "") {
                            _.each(markups(), function (item) {
                                if ((item.name().toLowerCase().indexOf((markupSearchString().toLowerCase()))) !== -1) {
                                    filteredMarkups.push(item);
                                }
                            });
                        } else {
                            filteredMarkups.removeAll();
                            ko.utils.arrayPushAll(filteredMarkups(), markups());
                            filteredMarkups.valueHasMutated();
                        }
                    },
                     //get Language Editor Data By Language Id
                    getLanguageEditorDataByLanguageId1 = ko.computed(function () {
                        //if (selectedMyOrganization() !== undefined && selectedMyOrganization().languageId()) {
                        //    dataservice.getResourceFileByLanguageId({
                        //        organisationId: selectedMyOrganization().id(),
                        //        lanuageId: selectedMyOrganization().languageId()
                        //    }, {
                        //        success: function (data) {
                        //            if (data != null) {
                        //                selectedMyOrganization().languageEditor(model.LanguageEditor.Create(data));
                        //            }
                        //        },
                        //        error: function (response) {
                        //            toastr.error("Failed to Load Language Editor data . Error: " + response);
                        //        }
                        //    });
                        //}
                    }, this),

                    getLanguageEditorDataByLanguageId = function (org) {
                        if (selectedMyOrganization() !== undefined && selectedMyOrganization().languageId() && selectedMyOrganization().id() !== undefined) {
                            dataservice.getResourceFileByLanguageId({
                                organisationId: selectedMyOrganization().id(),
                                lanuageId: selectedMyOrganization().languageId()
                            }, {
                                success: function (data) {
                                    if (data != null) {
                                        selectedMyOrganization().languageEditors.removeAll();
                                        _.each(data, function (item) {
                                            selectedMyOrganization().languageEditors.push(model.LanguageEditor.Create(item));
                                        });
                                        //selectedMyOrganization().languageEditor(model.LanguageEditor.Create(data));
                                    }
                                },
                                error: function (response) {
                                    toastr.error("Failed to Load Language Editor data . Error: " + response);
                                }
                            });
                        }
                    },
                    //Store Image Files Loaded Callback
                orgImageLoadedCallback = function (file, data) {
                    selectedMyOrganization().orgnizationImage(data);
                },
                //Search Nominal Code
                searchNominalCode = function () {
                    var nominalCode = filteredNominalCodes()[0];
                    //New Added item remove on search, if item not have code
                    if (nominalCode != undefined && (nominalCode.name() === undefined || nominalCode.name() === "") && nominalCode.id() < 0) {
                        _.each(chartOfAccounts(), function (item) {
                            if (item.id() === nominalCode.id()) {
                                chartOfAccounts.remove(item);
                            }
                        });
                    }
                    filteredNominalCodes.removeAll();
                    if (nominalCodeSearchString() !== undefined && nominalCodeSearchString().trim() !== "") {
                        _.each(chartOfAccounts(), function (item) {
                            if ((item.name().toLowerCase().indexOf((nominalCodeSearchString().toLowerCase()))) !== -1) {
                                filteredNominalCodes.push(item);
                            }
                        });
                    } else {
                        filteredNominalCodes.removeAll();
                        ko.utils.arrayPushAll(filteredNominalCodes(), chartOfAccounts());
                        filteredNominalCodes.valueHasMutated();
                    }
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
                    markupSearchString: markupSearchString,
                    nominalCodeSearchString: nominalCodeSearchString,
                    //Arrays
                    currencySymbols: currencySymbols,
                    languagePacks: languagePacks,
                    unitLengths: unitLengths,
                    unitWeights: unitWeights,
                    markups: markups,
                    chartOfAccounts: chartOfAccounts,
                    markupsForDropDown: markupsForDropDown,
                    errorList: errorList,
                    countries: countries,
                    filteredStates: filteredStates,
                    filteredMarkups: filteredMarkups,
                    filteredNominalCodes: filteredNominalCodes,
                    // Utility Methods
                    initialize: initialize,
                    pager: pager,
                    isOrganisationVisible: isOrganisationVisible,
                    isMarkupVisible: isMarkupVisible,
                    isApiDetailVisible:isApiDetailVisible,
                    isRegionalSettingVisible: isRegionalSettingVisible,

                    isLanguageEditorVisible: isLanguageEditorVisible,

                    HeadingName: HeadingName,
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
                    searchNominalCode: searchNominalCode,
                    searchMarkup: searchMarkup,
                    getLanguageEditorDataByLanguageId: getLanguageEditorDataByLanguageId,
                    gotoElement: gotoElement,
                    orgImageLoadedCallback: orgImageLoadedCallback

                };
            })()
        };
        return ist.myOrganization.viewModel;
    });
