﻿define(["ko", "underscore", "underscore-ko"], function (ko) {
    ko.bindingHandlers.autoNumeric = {
        init: function (el, valueAccessor, bindingsAccessor, viewModel) {
            var $el = $(el),
              bindings = bindingsAccessor(),
              settings = bindings.settings,
              value = valueAccessor();
         
            $el.autoNumeric(settings);
            $el.autoNumeric('set', parseFloat(ko.utils.unwrapObservable(value()), 10));
            $el.change(function () {
                value(parseFloat($el.autoNumeric('get'), 10));
            });
        },
        update: function (el, valueAccessor, bindingsAccessor, viewModel) {
            var $el = $(el),
              newValue = ko.utils.unwrapObservable(valueAccessor()),
              elementValue = $el.autoNumeric('get'),
              valueHasChanged = (newValue != elementValue);

            if ((newValue === 0) && (elementValue !== 0) && (elementValue !== "0")) {
                valueHasChanged = true;
            }
                        
            if (valueHasChanged) {
                if (newValue != undefined) {
                    $el.autoNumeric('set', newValue);
                }
                
                
            }
        }
    };



    var machineList = function () {
        var
            self,
            MachineId = ko.observable(),
            MachineCatId = ko.observable(),
            MachineName = ko.observable(),
            Description = ko.observable(),
            ImageSource = ko.observable(),
            LookupMethodName=ko.observable(),
            maximumsheetwidth = ko.observable(),
            maximumsheetheight = ko.observable(),
            minimumsheetwidth = ko.observable(),
            minimumsheetheight = ko.observable(),
            LookupMethodId = ko.observable(),
            errors = ko.validation.group({
                name: MachineName,
                type: MachineCatId,
            }),
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),
            dirtyFlag = new ko.dirtyFlag({
                name: MachineName,
                type: MachineCatId,
            }),
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),

            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            MachineId : MachineId,
            MachineCatId : MachineCatId,
            MachineName : MachineName,
            Description: Description,
            ImageSource: ImageSource,
            LookupMethodId:LookupMethodId,
            dirtyFlag: dirtyFlag,
            errors: errors,
            isValid: isValid,
            hasChanges: hasChanges,
            maximumsheetwidth:maximumsheetwidth,
            maximumsheetheight: maximumsheetheight,
            minimumsheetwidth: minimumsheetwidth,
            minimumsheetheight: minimumsheetheight,
            LookupMethodName:LookupMethodName,
            reset: reset
        };
        return self;
    };
    var machine = function () {
        var
            self,
            MachineId = ko.observable(),
            MachineName = ko.observable(),
            MachineCatId = ko.observable(),
            ColourHeads = ko.observable(0),
            isPerfecting = ko.observable(),
            SetupCharge = ko.observable(0),
            WashupPrice = ko.observable(0),
            WashupCost = ko.observable(0),
            MinInkDuctqty = ko.observable(),
            worknturncharge = ko.observable(0),
            MakeReadyCost = ko.observable(0),
            DefaultFilmId = ko.observable(),
            DefaultPlateId = ko.observable(),
            DefaultPaperId = ko.observable(28509),
            isfilmused = ko.observable(),
            isplateused = ko.observable(),
            ismakereadyused = ko.observable(),
            iswashupused = ko.observable(),
            maximumsheetweight = ko.observable(0),
            maximumsheetheight = ko.observable(0),
            maximumsheetwidth = ko.observable(0),
            minimumsheetheight = ko.observable(50),
            minimumsheetwidth = ko.observable(50),
            gripdepth = ko.observable(10),
            gripsideorientaion = ko.observable(),
            Orientation = ko.observableArray([
                    new OrientationModel({ id: "1", name: "Long Side" }),
                    new OrientationModel({ id: "2", name: "Short Side" })]),
            lookupList = ko.observableArray([]),
            markupList = ko.observableArray([]),
            deFaultPaperSizeName = ko.observable(),
            deFaultPlatesName = ko.observable(),
            MachineSpoilageItems = ko.observableArray([]),
            MachineLookupMethods = ko.observableArray([]),
            MachineInkCoverages = ko.observableArray([]),
            gutterdepth = ko.observable(10),
            headdepth = ko.observable(10),
            MarkupId = ko.observable(),
            PressSizeRatio = ko.observable(),
            Description = ko.observable().extend({required: true}), 
            Priority = ko.observable(0),
            DirectCost = ko.observable(),
            Image = ko.observable(),
            MinimumCharge = ko.observable(0),
            CostPerCut = ko.observable(0),
            PricePerCut = ko.observable(0),
            IsAdditionalOption = ko.observable(),
            IsDisabled = ko.observable(),
            LockedBy = ko.observable(),
            CylinderSizeId = ko.observable(),
            MaxItemAcrossCylinder = ko.observable(),
            Web1MRCost = ko.observable(),
            Web1MRPrice = ko.observable(),
            Web2MRCost = ko.observable(),
            Web2MRPrice = ko.observable(),
            ReelMRCost = ko.observable(),
            ReelMRPrice = ko.observable(),
            IsMaxColorLimit = ko.observable(),
            PressUtilization = ko.observable(),
            MakeReadyPrice = ko.observable(0),
            InkChargeForUniqueColors = ko.observable(0),
            CompanyId = ko.observable(),
            FlagId = ko.observable(),
            IsScheduleable = ko.observable(),
            SystemSiteId = ko.observable(),
            SpoilageType = ko.observable(),
            SetupTime = ko.observable(),
            TimePerCut = ko.observable(),
            MakeReadyTime = ko.observable(),
            WashupTime = ko.observable(),
            RunningSpoilage = ko.observable(),
            SetupSpoilage = ko.observable(),
            CoverageHigh = ko.observable(),
            CoverageMedium = ko.observable(),
            CoverageLow = ko.observable(),
            isSheetFed = ko.observable(),
            Passes = ko.observable(),
            ReelMakereadyTime = ko.observable(),
            Maximumsheetweight = ko.observable(),
            Maximumsheetheight = ko.observable(),
            Maximumsheetwidth = ko.observable(),
            Minimumsheetheight = ko.observable(),
            Minimumsheetwidth = ko.observable(),
            LookupMethodId = ko.observable(),
            CurrencySymbol = ko.observable(),
            WeightUnit = ko.observable(),
            LengthUnit = ko.observable(),
            onSelectStockItem = function (ostockItem) {
                if (ostockItem.category == "Plates") {
                    deFaultPlatesName(ostockItem.name);
                    DefaultPlateId(ostockItem.id);
                }
                //else if (ostockItem.category == "Paper") {
                //    DefaultPaperId(ostockItem.id);
                //    deFaultPaperSizeName(ostockItem.name);
                //}
            },
            errors = ko.validation.group({
                Description: Description,
            }),
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),
            
            dirtyFlag = new ko.dirtyFlag({
                MachineName: MachineName,
                MachineCatId: MachineCatId,
                isPerfecting: isPerfecting,
                SetupCharge: SetupCharge,
                WashupPrice: WashupPrice,
                WashupCost: WashupCost,
                MinInkDuctqty: MinInkDuctqty,
                worknturncharge: worknturncharge,
                MakeReadyCost: MakeReadyCost,
                DefaultPlateId: DefaultPlateId,
                DefaultPaperId: DefaultPaperId,
                isfilmused: isfilmused,
                isplateused: isplateused,
                ismakereadyused: ismakereadyused,
                iswashupused: iswashupused,
                maximumsheetweight: maximumsheetweight,
                maximumsheetheight: maximumsheetheight,
                maximumsheetwidth: maximumsheetwidth,
                minimumsheetheight: minimumsheetheight,
                minimumsheetwidth: minimumsheetwidth,
                gripdepth: gripdepth,
                gripsideorientaion: gripsideorientaion,
                Orientation: Orientation,
                gutterdepth: gutterdepth,
                headdepth: headdepth,
                MarkupId: MarkupId,
                PressSizeRatio: PressSizeRatio,
                Description: Description,
                Priority: Priority,
                DirectCost: DirectCost,
                Image: Image,
                MinimumCharge: MinimumCharge,
                CostPerCut: CostPerCut,
                PricePerCut: PricePerCut,
                IsAdditionalOption: IsAdditionalOption,
                IsDisabled: IsDisabled,
                LockedBy: LockedBy,
                CylinderSizeId: CylinderSizeId,
                MaxItemAcrossCylinder: MaxItemAcrossCylinder,
                Web1MRCost: Web1MRCost,
                Web1MRPrice: Web1MRPrice,
                Web2MRCost: Web2MRCost,
                Web2MRPrice: Web2MRPrice,
                ReelMRCost: ReelMRCost,
                ReelMRPrice: ReelMRPrice,
                IsMaxColorLimit: IsMaxColorLimit,
                PressUtilization: PressUtilization,
                MakeReadyPrice: MakeReadyPrice,
                InkChargeForUniqueColors: InkChargeForUniqueColors,
                CompanyId: CompanyId,
                FlagId: FlagId,
                IsScheduleable: IsScheduleable,
                SystemSiteId: SystemSiteId,
                SpoilageType: SpoilageType,
                SetupTime: SetupTime,
                TimePerCut: TimePerCut,
                MakeReadyTime: MakeReadyTime,
                WashupTime: WashupTime,
                RunningSpoilage: RunningSpoilage,
                SetupSpoilage: SetupSpoilage,
                CoverageHigh: CoverageHigh,
                CoverageMedium: CoverageMedium,
                CoverageLow: CoverageLow,
                isSheetFed: isSheetFed,
                Passes: Passes,
                ReelMakereadyTime: ReelMakereadyTime,
                Maximumsheetweight: Maximumsheetweight,
                Maximumsheetheight: Maximumsheetheight,
                Maximumsheetwidth: Maximumsheetwidth,
                Minimumsheetheight: Minimumsheetheight,
                Minimumsheetwidth: Minimumsheetwidth,
                LookupMethodId: LookupMethodId,
                MachineSpoilageItems: MachineSpoilageItems,
                MachineLookupMethods: MachineLookupMethods,
                MachineInkCoverages: MachineInkCoverages
            }),
            hasChanges = ko.computed(function () {

                return dirtyFlag.isDirty();
            }),

            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            MachineId:MachineId,
            MachineName :MachineName ,
            MachineCatId :MachineCatId ,
            ColourHeads : ColourHeads,
            isPerfecting :isPerfecting ,
            SetupCharge :SetupCharge ,
            WashupPrice :WashupPrice ,
            WashupCost :WashupCost ,
            MinInkDuctqty : MinInkDuctqty,
            worknturncharge : worknturncharge,
            MakeReadyCost : MakeReadyCost,
            DefaultFilmId : DefaultFilmId,
            DefaultPlateId : DefaultPlateId,
            DefaultPaperId :DefaultPaperId ,
            isfilmused :isfilmused ,
            isplateused : isplateused,
            ismakereadyused : ismakereadyused,
            iswashupused : iswashupused,
            maximumsheetweight : maximumsheetweight,
            maximumsheetheight :maximumsheetheight ,
            maximumsheetwidth : maximumsheetwidth,
            minimumsheetheight : minimumsheetheight,
            minimumsheetwidth : minimumsheetwidth,
            gripdepth : gripdepth,
            gripsideorientaion: gripsideorientaion,
            Orientation:Orientation,
            gutterdepth : gutterdepth,
            headdepth : headdepth,
            MarkupId: MarkupId,
            PressSizeRatio : PressSizeRatio,
            Description : Description,
            Priority : Priority,
            DirectCost : DirectCost,
            Image : Image,
            MinimumCharge : MinimumCharge,
            CostPerCut : CostPerCut,
            PricePerCut : PricePerCut,
            IsAdditionalOption : IsAdditionalOption,
            IsDisabled : IsDisabled,
            LockedBy : LockedBy,
            CylinderSizeId : CylinderSizeId,
            MaxItemAcrossCylinder : MaxItemAcrossCylinder,
            Web1MRCost : Web1MRCost,
            Web1MRPrice : Web1MRPrice,
            Web2MRCost : Web2MRCost,
            Web2MRPrice : Web2MRPrice,
            ReelMRCost : ReelMRCost,
            ReelMRPrice : ReelMRPrice,
            IsMaxColorLimit : IsMaxColorLimit,
            PressUtilization : PressUtilization,
            MakeReadyPrice : MakeReadyPrice,
            InkChargeForUniqueColors : InkChargeForUniqueColors,
            CompanyId : CompanyId,
            FlagId : FlagId,
            IsScheduleable :IsScheduleable ,
            SystemSiteId : SystemSiteId,
            SpoilageType : SpoilageType,
            SetupTime : SetupTime,
            TimePerCut : TimePerCut,
            MakeReadyTime : MakeReadyTime,
            WashupTime : WashupTime,
            ReelMakereadyTime  : ReelMakereadyTime,    
            Maximumsheetweight : Maximumsheetweight,
            Maximumsheetheight : Maximumsheetheight,
            Maximumsheetwidth : Maximumsheetwidth,
            Minimumsheetheight : Minimumsheetheight,
            Minimumsheetwidth : Minimumsheetwidth,
            LookupMethodId: LookupMethodId,
            RunningSpoilage: RunningSpoilage,
            SetupSpoilage: SetupSpoilage,
            CoverageHigh: CoverageHigh,
            CoverageMedium: CoverageMedium,
            CoverageLow: CoverageLow,
            isSheetFed: isSheetFed,
            Passes: Passes,
            lookupList: lookupList,
            dirtyFlag: dirtyFlag,
            errors: errors,
            isValid: isValid,
            hasChanges: hasChanges,
            reset: reset,
            markupList: markupList,
            deFaultPlatesName:deFaultPlatesName,
            deFaultPaperSizeName: deFaultPaperSizeName,
            MachineInkCoverages: MachineInkCoverages,
            MachineSpoilageItems: MachineSpoilageItems,
            MachineLookupMethods: MachineLookupMethods,
            onSelectStockItem: onSelectStockItem,
            CurrencySymbol: CurrencySymbol,
            WeightUnit: WeightUnit,
            LengthUnit: LengthUnit
          
        };
        return self;
    };
    var OrientationModel = function (data) {
        var self = this;
        self.id = ko.observable(data.id);
        self.name = ko.observable(data.name);
    };

    var lookupMethod = function (data) {
        var self = this;
        self.MethodId = ko.observable(data.MethodId);
        self.Name = ko.observable(data.Name);
    }

    var MachineLookupMethods = function (data) {
        var self = this;
        self.Id = ko.observable();
        self.MachineId = ko.observable();
        self.MethodId = ko.observable();
        self.DefaultMethod = ko.observable();

    }

  

   var lookupMethodListClientMapper = function (source) {
        var olookup = new lookupMethod();
        olookup.MethodId(source.MethodId);
        olookup.Name(source.Name);
    };
    CreateMachineInkCoverage = function (specifiedId, specifiedName, specifiedCategoryName, specifiedLocation, specifiedWeight, specifiedDescription) {
        return {
            //Id = source.Id,
            //SideInkOrder = source.SideInkOrder,
            //SideInkOrderCoverage = source.SideInkOrderCoverage,
            //MachineId = source.MachineId
        };
    }

    var MachineInkCoveragesListClientMapper = function (source, StockItemforInkList, InkCoveragItems) {
        var
            Id = source.Id,
            SideInkOrder = ko.observable(source.SideInkOrder || undefined),
            SideInkOrderCoverage = ko.observable(source.SideInkOrderCoverage || undefined),
            MachineId = source.MachineId,
            StockItemforInkList = StockItemforInkList,
            InkCoveragItems = InkCoveragItems,
             errors = ko.validation.group({
             }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            dirtyFlag = new ko.dirtyFlag({
                SideInkOrder: SideInkOrder,
                SideInkOrderCoverage: SideInkOrderCoverage
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };

        return {
            Id : Id,
            SideInkOrder: SideInkOrder,
            SideInkOrderCoverage: SideInkOrderCoverage,
            MachineId: MachineId,
            StockItemforInkList: StockItemforInkList,
            InkCoveragItems: InkCoveragItems,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset
        };
    };
    var MachineInkCoveragesServerMapper = function (source) {
        return {
            Id: source.Id,
            SideInkOrder: source.SideInkOrder,
            SideInkOrderCoverage: source.SideInkOrderCoverage,
            MachineId: source.MachineId
        };
    };
    var machineListClientMapper = function (source) {

        var omachineList = new machineList();
        omachineList.Description(source.Description);
        omachineList.MachineId(source.MachineId);
        omachineList.MachineCatId(source.MachineCatId);
        omachineList.MachineName(source.MachineName);
        omachineList.ImageSource(source.ImageSource);
        omachineList.maximumsheetwidth(source.maximumsheetwidth);
        omachineList.maximumsheetheight(source.maximumsheetheight);
        omachineList.minimumsheetwidth(source.minimumsheetwidth);
        omachineList.minimumsheetheight(source.minimumsheetheight);
        omachineList.LookupMethodName(source.LookupMethodName);
        omachineList.LookupMethodId(source.LookupMethodId);
        
        return omachineList;

    };

    var machineListClientMapperSelectedItem = function (source) {

        var omachineList = new machineList();
        omachineList.Description(source.Description());
        omachineList.MachineId(source.MachineId());
        omachineList.MachineCatId(source.MachineCatId());
        omachineList.MachineName(source.MachineName());
       
        omachineList.maximumsheetwidth(source.maximumsheetwidth());
        omachineList.maximumsheetheight(source.maximumsheetheight());
        omachineList.minimumsheetwidth(source.minimumsheetwidth());
        omachineList.minimumsheetheight(source.minimumsheetheight());
        
        omachineList.LookupMethodId(source.LookupMethodId());

        return omachineList;

    };
    var StockItemMapper = function (source) {
        return {
            id: source.StockItemId,
            name: source.ItemName,
            category: source.CategoryName,
            location: source.StockLocation,
            weight: source.ItemWeight
        };


       // return new CreateStockItem(source.StockItemId, source.ItemName, source.CategoryName, source.StockLocation, source.ItemWeight, source.ItemDescription);
    };



    var MachineSpoilageItemsMapper = function (source) {
        var 
            MachineSpoilageId = source.MachineSpoilageId,
            MachineId = source.MachineId,
            SetupSpoilage = ko.observable(source.SetupSpoilage),
            RunningSpoilage = ko.observable(source.RunningSpoilage),
            NoOfColors = source.NoOfColors,
             errors = ko.validation.group({
             }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            dirtyFlag = new ko.dirtyFlag({
                SetupSpoilage: SetupSpoilage,
                RunningSpoilage: RunningSpoilage
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };

        return {
            MachineSpoilageId: MachineSpoilageId,
            MachineId:MachineId,
            SetupSpoilage : SetupSpoilage,
            RunningSpoilage : RunningSpoilage,
            NoOfColors: NoOfColors,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset
        };
        
    }

    var MachineLookupMethodsItemsMapper = function (source) {
        var
            Id = source.Id,
            MachineId = source.MachineId,
            MethodId = ko.observable(source.MethodId),
            DefaultMethod = ko.observable(source.DefaultMethod),
          
             errors = ko.validation.group({
             }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            dirtyFlag = new ko.dirtyFlag({
                DefaultMethod: DefaultMethod
       
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };

        return {
            Id: Id,
            MachineId: MachineId,
            MethodId: MethodId,
            DefaultMethod: DefaultMethod,
           
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset
        };

    }


    var machineListServerMapper = function (source) {
        var result = {};
        result.Description= source.description;
        result.MachineId= source.MachineId;
        result.MachineCatId= source.MachineCatId;
        result.MachineName= source.MachineName;
        result.ImageSource = source.ImageSource;
        result.maximumsheetwidth = source.maximumsheetwidth;
        result.maximumsheetheight = source.maximumsheetheight;
        result.minimumsheetwidth = source.minimumsheetwidth;
        result.minimumsheetheight = source.minimumsheetheight;
        resultLookupMethodName = source.LookupMethodName;
        return result;
    };
    var machineClientMapper = function (source) {
        var omachine = new machine();
        omachine.MachineId(source.machine.MachineId);
        omachine.MachineName(source.machine.MachineName);
        omachine.MachineCatId(source.machine.MachineCatId);
        omachine.ColourHeads(source.machine.ColourHeads);
        omachine.isPerfecting(source.machine.isPerfecting);
        omachine.SetupCharge(source.machine.SetupCharge);
        
        omachine.WashupCost(source.machine.WashupCost);
        omachine.MinInkDuctqty(source.machine.MinInkDuctqty);
        omachine.worknturncharge(source.machine.worknturncharge);
        omachine.MakeReadyCost(source.machine.MakeReadyCost);
        omachine.DefaultFilmId(source.machine.DefaultFilmId);
        omachine.DefaultPlateId(source.machine.DefaultPlateId);
        //omachine.DefaultPaperId(source.machine.DefaultPaperId);
        omachine.isfilmused(source.machine.isfilmused);
        omachine.isplateused(source.machine.isplateused);
        if (omachine.isplateused()) {
            omachine.ismakereadyused(source.machine.ismakereadyused);
            omachine.iswashupused(source.machine.iswashupused);
            omachine.deFaultPlatesName(source.deFaultPlatesName);
        } else {
            omachine.deFaultPlatesName(null);
            omachine.iswashupused(false);
            omachine.ismakereadyused(false);
        }
        if (omachine.ismakereadyused()) {
            omachine.MakeReadyPrice(source.machine.MakeReadyPrice);
        } else {
            omachine.MakeReadyPrice(0);
        }
        if (omachine.iswashupused()) {
            omachine.WashupPrice(source.machine.WashupPrice);
        } else {
            omachine.WashupPrice(0);
        }
        
        omachine.CurrencySymbol(source.CurrencySymbol);
        omachine.WeightUnit(source.WeightUnit);
        omachine.LengthUnit(source.LengthUnit);
       
        omachine.maximumsheetweight(source.machine.maximumsheetweight);
        omachine.maximumsheetheight(source.machine.maximumsheetheight);
        omachine.maximumsheetwidth(source.machine.maximumsheetwidth);

        if (!(source.machine.gripdepth == 0 || source.machine.gripdepth == null || source.machine.gripdepth == undefined)) {
            omachine.gripdepth(source.machine.gripdepth);
        }
        if (!(source.machine.headdepth == 0 || source.machine.headdepth == null || source.machine.headdepth == undefined)) {
            omachine.headdepth(source.machine.headdepth);
        }
        if (!(source.machine.gutterdepth == 0 || source.machine.gutterdepth == null || source.machine.gutterdepth == undefined)) {
            omachine.gutterdepth(source.machine.gutterdepth);
        }
        omachine.gripsideorientaion(source.machine.gripsideorientaion);
        
        
        omachine.MarkupId(source.machine.MarkupId);
        omachine.PressSizeRatio(source.machine.PressSizeRatio);
        omachine.Description(source.machine.Description);
        omachine.Priority(source.machine.Priority);
        omachine.DirectCost(source.machine.DirectCost);
        omachine.Image(source.machine.Image);
        omachine.MinimumCharge(source.machine.MinimumCharge);
        omachine.CostPerCut(source.machine.CostPerCut);
        omachine.PricePerCut(source.machine.PricePerCut);
        omachine.IsAdditionalOption(source.machine.IsAdditionalOption);
        omachine.IsDisabled(source.machine.IsDisabled);
        omachine.LockedBy(source.machine.LockedBy);
        omachine.CylinderSizeId(source.machine.CylinderSizeId);
        omachine.MaxItemAcrossCylinder(source.machine.MaxItemAcrossCylinder);
        omachine.Web1MRCost(source.machine.Web1MRCost);
        omachine.Web1MRPrice(source.machine.Web1MRPrice);
        omachine.Web2MRCost(source.machine.Web2MRCost);
        omachine.Web2MRPrice(source.machine.Web2MRPrice);
        omachine.ReelMRCost(source.machine.ReelMRCost);
        omachine.ReelMRPrice(source.machine.ReelMRPrice);
        omachine.IsMaxColorLimit(source.machine.IsMaxColorLimit);
        omachine.PressUtilization(source.machine.PressUtilization);
        
        omachine.InkChargeForUniqueColors(source.machine.InkChargeForUniqueColors);
        omachine.CompanyId(source.machine.CompanyId);
        omachine.FlagId(source.machine.FlagId);
        omachine.IsScheduleable(source.machine.IsScheduleable);
        omachine.SystemSiteId(source.machine.SystemSiteId);
        omachine.SpoilageType(source.machine.SpoilageType);
        omachine.SetupTime(source.machine.SetupTime);
        omachine.TimePerCut(source.machine.TimePerCut);
        omachine.MakeReadyTime(source.machine.MakeReadyTime);
        omachine.WashupTime(source.machine.WashupTime);
        omachine.ReelMakereadyTime(source.machine.ReelMakereadyTime);
        omachine.RunningSpoilage(source.machine.RunningSpoilage);
        omachine.SetupSpoilage(source.machine.SetupSpoilage);
        omachine.CoverageHigh(source.machine.CoverageHigh);
        omachine.CoverageMedium(source.machine.CoverageMedium);
        omachine.CoverageLow(source.machine.CoverageLow);
        omachine.isSheetFed(source.machine.isSheetFed);
        omachine.Passes(source.machine.Passes);
        //omachine.Maximumsheetweight(source.machine.Maximumsheetweight);
        //omachine.Maximumsheetheight(source.machine.Maximumsheetheight);
        //omachine.Maximumsheetwidth(source.machine.Maximumsheetwidth);
        //omachine.Minimumsheetheight(source.machine.Minimumsheetheight);
        //omachine.Minimumsheetwidth(source.machine.Minimumsheetwidth);
        omachine.LookupMethodId(source.machine.LookupMethodId);
        omachine.deFaultPaperSizeName(source.deFaultPaperSizeName);
        
        omachine.lookupList.removeAll();
        ko.utils.arrayPushAll(omachine.lookupList(), source.lookupMethods);
        omachine.lookupList.valueHasMutated();

        //omachine.markupList.removeAll();
        //ko.utils.arrayPushAll(omachine.markupList(), source.Markups);
        //omachine.markupList.valueHasMutated();
       
        
        
        //_.each(source.MachineSpoilageItems, function (item) {
        //    omachine.MachineSpoilageItems.push(MachineSpoilageItemsMapper(item));
        //});


        
        var StockItemforInkList = ko.observableArray([]);
        StockItemforInkList.removeAll();
        ko.utils.arrayPushAll(StockItemforInkList(), source.StockItemforInk);
        StockItemforInkList.valueHasMutated();

        //var InkCoveragItemsList = ko.observableArray([]);
        //InkCoveragItemsList.removeAll();
        //ko.utils.arrayPushAll(InkCoveragItemsList(), source.InkCoveragItems);
        //InkCoveragItemsList.valueHasMutated();
       


        //_.each(source.machine.MachineInkCoverages, function (item) {
        //    var module = MachineInkCoveragesListClientMapper(item, StockItemforInkList, InkCoveragItemsList);
        //    omachine.MachineInkCoverages.push(module);




        //})

        _.each(source.machine.MachineLookupMethods, function (item) {
            omachine.MachineLookupMethods.push(MachineLookupMethodsItemsMapper(item));
        })

          return omachine;
    };
    var machineServerMapper = function (machine) {
        var omachine = {};
        omachine.MachineId = machine.MachineId();
        omachine.MachineName = machine.MachineName();
        omachine.MachineCatId = machine.MachineCatId();
        omachine.ColourHeads = machine.ColourHeads();
        omachine.isPerfecting = machine.isPerfecting();
        omachine.SetupCharge = machine.SetupCharge();
        omachine.WashupPrice = machine.WashupPrice();
        omachine.WashupCost = machine.WashupCost();
        omachine.MinInkDuctqty = machine.MinInkDuctqty();
        omachine.worknturncharge = machine.worknturncharge();
        omachine.MakeReadyCost = machine.MakeReadyCost();
        omachine.DefaultFilmId = machine.DefaultFilmId();
        omachine.DefaultPlateId = machine.DefaultPlateId();
        omachine.DefaultPaperId = machine.DefaultPaperId();
        omachine.isfilmused = machine.isfilmused();
        omachine.isplateused = machine.isplateused();
        omachine.ismakereadyused = machine.ismakereadyused();
        omachine.iswashupused = machine.iswashupused();
        omachine.maximumsheetweight = machine.maximumsheetweight();
        omachine.maximumsheetheight = machine.maximumsheetheight();
        omachine.maximumsheetwidth = machine.maximumsheetwidth();
        omachine.minimumsheetheight = machine.minimumsheetheight();
        omachine.minimumsheetwidth = machine.minimumsheetwidth();
        omachine.gripdepth = machine.gripdepth();
        omachine.gripsideorientaion = machine.gripsideorientaion();
        omachine.gutterdepth = machine.gutterdepth();
        omachine.headdepth = machine.headdepth();
        //omachine.MarkupId = machine.MarkupId();
        omachine.PressSizeRatio = machine.PressSizeRatio();
        omachine.Description = machine.Description();
        omachine.Priority = machine.Priority();
        omachine.DirectCost = machine.DirectCost();
        omachine.Image = machine.Image();
        omachine.MinimumCharge = machine.MinimumCharge();
        omachine.CostPerCut = machine.CostPerCut();
        omachine.PricePerCut = machine.PricePerCut();
        omachine.IsAdditionalOption = machine.IsAdditionalOption();
        omachine.IsDisabled = machine.IsDisabled();
        omachine.LockedBy = machine.LockedBy();
        omachine.CylinderSizeId = machine.CylinderSizeId();
        omachine.MaxItemAcrossCylinder = machine.MaxItemAcrossCylinder();
        omachine.Web1MRCost = machine.Web1MRCost();
        omachine.Web1MRPrice = machine.Web1MRPrice();
        omachine.Web2MRCost = machine.Web2MRCost();
        omachine.Web2MRPrice = machine.Web2MRPrice();
        omachine.ReelMRCost = machine.ReelMRCost();
        omachine.ReelMRPrice = machine.ReelMRPrice();
        omachine.IsMaxColorLimit = machine.IsMaxColorLimit();
        omachine.PressUtilization = machine.PressUtilization();
        omachine.MakeReadyPrice = machine.MakeReadyPrice();
        omachine.InkChargeForUniqueColors = machine.InkChargeForUniqueColors();
        omachine.CompanyId = machine.CompanyId();
        omachine.FlagId = machine.FlagId();
        omachine.IsScheduleable = machine.IsScheduleable();
        omachine.SystemSiteId = machine.SystemSiteId();
        omachine.SpoilageType = machine.SpoilageType();
        omachine.SetupTime = machine.SetupTime();
        omachine.TimePerCut = machine.TimePerCut();
        omachine.MakeReadyTime = machine.MakeReadyTime();
        omachine.WashupTime = machine.WashupTime();
        omachine.ReelMakereadyTime = machine.ReelMakereadyTime();
        omachine.Maximumsheetweight = machine.Maximumsheetweight();
        omachine.Maximumsheetheight = machine.Maximumsheetheight();
        omachine.Maximumsheetwidth = machine.Maximumsheetwidth();
        omachine.Minimumsheetheight = machine.Minimumsheetheight();
        omachine.Minimumsheetwidth = machine.Minimumsheetwidth();
        omachine.LookupMethodId = machine.LookupMethodId();
        omachine.SetupSpoilage = machine.SetupSpoilage();
        omachine.RunningSpoilage = machine.RunningSpoilage();
        omachine.CoverageHigh = machine.CoverageHigh();
        omachine.CoverageMedium = machine.CoverageMedium();
        omachine.CoverageLow = machine.CoverageLow();
        omachine.isSheetFed = machine.isSheetFed();
        omachine.Passes = machine.Passes();
        //omachine.MachineInkCoverages = [];
        //_.each(machine.MachineInkCoverages(), function (item) {
        //    var module = MachineInkCoveragesListServerMapper(item);
        //    omachine.MachineInkCoverages.push(module);
        //});
       

        //var MachineSpoilageItemsList = [];
        //_.each(machine.MachineSpoilageItems(), function (item) {
        //    var module = MachineSpoilageServerMapper(item);
        //    MachineSpoilageItemsList.push(module);
        //});

        var MachineLookupdsList = [];
        _.each(machine.MachineLookupMethods(), function (item) {
            var module = MachineLookupMethodsServerMapper(item);
            omachine.MachineLookupMethods.push(module);
        });

       
        return {
            machine: omachine,
           // MachineSpoilages: MachineSpoilageItemsList
        }
        
    };
    var newMachineClientMapper = function (source) {
        var omachine = new machine();

        omachine.lookupList.removeAll();
        ko.utils.arrayPushAll(omachine.lookupList(), source.lookupMethods);
        omachine.lookupList.valueHasMutated();

        //omachine.markupList.removeAll();
        //ko.utils.arrayPushAll(omachine.markupList(), source.Markups);
        //omachine.markupList.valueHasMutated();
        omachine.CurrencySymbol(source.CurrencySymbol);
        omachine.WeightUnit(source.WeightUnit);
        omachine.LengthUnit(source.LengthUnit);

        //for (i = 0; i < 8; i++) {
        //    omachine.MachineSpoilageItems.push(newMachineSpoilageItemsMapper(i));
        //  }

        var StockItemforInkList = ko.observableArray([]);
        StockItemforInkList.removeAll();
        ko.utils.arrayPushAll(StockItemforInkList(), source.StockItemforInk);
        StockItemforInkList.valueHasMutated();

        //var InkCoveragItemsList = ko.observableArray([]);
        //InkCoveragItemsList.removeAll();
        //ko.utils.arrayPushAll(InkCoveragItemsList(), source.InkCoveragItems);
        //InkCoveragItemsList.valueHasMutated();



        //for (i = 0; i < 8; i++) {
        //    omachine.MachineInkCoverages.push(newMachineInkCoveragesListClientMapper(StockItemforInkList, InkCoveragItemsList));
        //}

        return omachine;
    };
    var newMachineSpoilageItemsMapper = function (i) {
        var
            MachineSpoilageId = ko.observable(),
            MachineId = ko.observable(),
            SetupSpoilage = ko.observable(10),
            RunningSpoilage = ko.observable(10),
            NoOfColors = i+1,
             errors = ko.validation.group({
             }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            dirtyFlag = new ko.dirtyFlag({
                SetupSpoilage: SetupSpoilage,
                RunningSpoilage: RunningSpoilage
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };

        return {
            MachineSpoilageId: MachineSpoilageId,
            MachineId: MachineId,
            SetupSpoilage: SetupSpoilage,
            RunningSpoilage: RunningSpoilage,
            NoOfColors: NoOfColors,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset
        };

    }
    var newMachineInkCoveragesListClientMapper = function (StockItemforInkList, InkCoveragItems) {
        var
            Id = 0,
            SideInkOrder = ko.observable(undefined),
            SideInkOrderCoverage = ko.observable(undefined),
            MachineId = 0,
            StockItemforInkList = StockItemforInkList,
            InkCoveragItems = InkCoveragItems,
             errors = ko.validation.group({
             }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            dirtyFlag = new ko.dirtyFlag({
                SideInkOrder: SideInkOrder,
                SideInkOrderCoverage: SideInkOrderCoverage
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };

        return {
            Id: Id,
            SideInkOrder: SideInkOrder,
            SideInkOrderCoverage: SideInkOrderCoverage,
            MachineId: MachineId,
            StockItemforInkList: StockItemforInkList,
            InkCoveragItems: InkCoveragItems,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset
        };
    };


    //Convert Server To Client
    var MachineLookupClientMapper = function (source) {
        var machineLookup = new MachineLookupMethods();
        machineLookup.Id(source.Id === null ? undefined : source.Id);
        machineLookup.MachineId(source.MachineId === null ? undefined : source.MachineId);
        machineLookup.MethodId(source.MethodId === null ? undefined : source.MethodId);
        machineLookup.DefaultMethod(source.DefaultMethod === null ? undefined : source.DefaultMethod);
        return machineLookup;
    };

    var MachineSpoilageServerMapper = function (source) {
        var MachineSpoilageItem = {};
        MachineSpoilageItem.MachineSpoilageId = source.MachineSpoilageId;
        MachineSpoilageItem.MachineId = source.MachineId;
        MachineSpoilageItem.SetupSpoilage = source.SetupSpoilage();
        MachineSpoilageItem.RunningSpoilage = source.RunningSpoilage();
        MachineSpoilageItem.NoOfColors = source.NoOfColors;
        return MachineSpoilageItem;

    }


    var MachineLookupMethodsServerMapper = function (source) {
        var MachineLookupItem = {};
        MachineLookupItem.Id = source.Id;
        MachineLookupItem.MachineId = source.MachineId;
        MachineLookupItem.MethodId = source.MethodId;
        MachineLookupItem.DefaultMethod = source.DefaultMethod;
      
        return MachineLookupItem;

    }
    var MachineInkCoveragesListServerMapper = function (source) {
        var InkCoveragesItem = {};
        InkCoveragesItem.Id = source.Id;
        InkCoveragesItem.SideInkOrder = source.SideInkOrder();
        InkCoveragesItem.SideInkOrderCoverage = source.SideInkOrderCoverage();
        InkCoveragesItem.MachineId = source.MachineId;

        return InkCoveragesItem;

    }

    return {
        machineList: machineList,
        machineListClientMapper: machineListClientMapper,
        machineListServerMapper: machineListServerMapper,
        machine: machine,
        lookupMethod: lookupMethod,
        lookupMethodListClientMapper:lookupMethodListClientMapper,
        machineClientMapper: machineClientMapper,
        StockItemMapper: StockItemMapper,
        MachineInkCoveragesListClientMapper: MachineInkCoveragesListClientMapper,
        MachineInkCoveragesServerMapper: MachineInkCoveragesServerMapper,
        machineServerMapper: machineServerMapper,
        MachineInkCoveragesListServerMapper: MachineInkCoveragesListServerMapper,
        MachineSpoilageServerMapper: MachineSpoilageServerMapper,
        newMachineClientMapper: newMachineClientMapper,
        newMachineSpoilageItemsMapper: newMachineSpoilageItemsMapper,
        newMachineInkCoveragesListClientMapper: newMachineInkCoveragesListClientMapper,
        machineListClientMapperSelectedItem: machineListClientMapperSelectedItem,
        MachineLookupClientMapper: MachineLookupClientMapper
    };
});