define(["ko", "underscore", "underscore-ko"], function (ko) {
    
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
            isSheetFed = ko.observable(),
            isSpotColor = ko.observable(),
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
            LookupMethodId: LookupMethodId,
            isSheetFed: isSheetFed,
            dirtyFlag: dirtyFlag,
            errors: errors,
            isValid: isValid,
            hasChanges: hasChanges,
            maximumsheetwidth:maximumsheetwidth,
            maximumsheetheight: maximumsheetheight,
            minimumsheetwidth: minimumsheetwidth,
            minimumsheetheight: minimumsheetheight,
            LookupMethodName: LookupMethodName,
            isSpotColor:isSpotColor,
            reset: reset
        };
        return self;
    };
    var machine = function (callbacks) {
        var self,
            MachineId = ko.observable(),
            MachineName = ko.observable().extend({ required: true }),
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
            isClickChargezone = ko.observable('true'),
            isClickChargezoneUi = ko.computed({
                read: function () {

                    return isClickChargezone();
                },
                write: function (value) {
                    var zone = value;
                    if (zone === isClickChargezone()) {
                        return;
                    }
                    if (callbacks && typeof callbacks === "function") {
                        callbacks();
                    }
                    isClickChargezone(zone);
                }
            }),
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
            Description = ko.observable().extend({ required: true }),
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
            RunningSpoilage = ko.observable().extend({ number: true }),
            SetupSpoilage = ko.observable().extend({ number: true}),
            CoverageHigh = ko.observable().extend({ number: true, max: 10, min: 0, message: 'Max value can be 10'}),
            CoverageMedium = ko.observable().extend({number: true, max: 10, min: 0, message:'Max value can be 10'}),
            CoverageLow = ko.observable().extend({number: true, max: 10, min: 0, message:'Max value can be 10'}),
            isSheetFed = ko.observable(),
            isDigitalPress = ko.observable(),
            Passes = ko.observable(),
            ReelMakereadyTime = ko.observable(),
            Maximumsheetweight = ko.observable(),
            Maximumsheetheight = ko.observable(),
            Maximumsheetwidth = ko.observable(),
            LookupMethodId = ko.observable(),
            CurrencySymbol = ko.observable(),
            lookupMethod = ko.observable(),
            WeightUnit = ko.observable(),
            LengthUnit = ko.observable(),
            IsSpotColor = ko.observable(),
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
                isClickChargezone: isClickChargezone,
                Passes: Passes,
                IsSpotColor: IsSpotColor,
                ReelMakereadyTime: ReelMakereadyTime,
                Maximumsheetweight: Maximumsheetweight,
                Maximumsheetheight: Maximumsheetheight,
                Maximumsheetwidth: Maximumsheetwidth,
                LookupMethodId: LookupMethodId,
                MachineSpoilageItems: MachineSpoilageItems,
                MachineLookupMethods: MachineLookupMethods,
                MachineInkCoverages: MachineInkCoverages,
                lookupMethod: lookupMethod,
                isDigitalPress: isDigitalPress
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
            WashupTime: WashupTime,
            IsSpotColor: IsSpotColor,
            ReelMakereadyTime  : ReelMakereadyTime,    
            Maximumsheetweight : Maximumsheetweight,
            Maximumsheetheight : Maximumsheetheight,
            Maximumsheetwidth : Maximumsheetwidth,
            LookupMethodId: LookupMethodId,
            RunningSpoilage: RunningSpoilage,
            SetupSpoilage: SetupSpoilage,
            CoverageHigh: CoverageHigh,
            CoverageMedium: CoverageMedium,
            CoverageLow: CoverageLow,
            isSheetFed: isSheetFed,
            isClickChargezone:isClickChargezone,
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
            LengthUnit: LengthUnit,
            lookupMethod: lookupMethod,
            isClickChargezoneUi: isClickChargezoneUi,
            isDigitalPress: isDigitalPress
          
        };
        return self;
    };
    var OrientationModel = function (data) {
        var self = this;
        self.id = ko.observable(data.id);
        self.name = ko.observable(data.name);
    };

    var lookupMethod = function(data) {
        var self = this;
        self.MethodId = ko.observable(data.MethodId);
        self.Name = ko.observable(data.Name);
        self.Type = ko.observable(data.Type);
        self.SpedWeightLookups = ko.observable([]);
        if (data.Type == 3) {
            _.each(data.MachineSpeedWeightLookups, function (item) {
                self.SpedWeightLookups().push(SpeedWeightLookup(item));
            });
        }
        
        return self;
    };

    var MachineLookupMethods = function(data) {
        var self = this;
        self.Id = ko.observable();
        self.MachineId = ko.observable();
        self.MethodId = ko.observable();
        self.DefaultMethod = ko.observable();

    };

  

   var lookupMethodListClientMapper = function (source) {
        var olookup = new lookupMethod();
        olookup.MethodId(source.MethodId);
        olookup.Name(source.Name);
    };
    CreateMachineInkCoverage = function(specifiedId, specifiedName, specifiedCategoryName, specifiedLocation, specifiedWeight, specifiedDescription) {
        return {
            //Id = source.Id,
            //SideInkOrder = source.SideInkOrder,
            //SideInkOrderCoverage = source.SideInkOrderCoverage,
            //MachineId = source.MachineId
            
        };
    };

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
        omachineList.isSheetFed(source.isSheetFed);
        omachineList.isSpotColor(source.IsSpotColor);
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
        omachineList.isSheetFed(source.isSheetFed());
        omachineList.isSpotColor(source.isSpotColor());
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
        result.isSheetFed = source.isSheetFed;
        return result;
    };
    var machineClientMapper = function (source, callback) {
        var omachine = new machine(callback);
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
        omachine.minimumsheetheight(source.machine.minimumsheetheight);
        omachine.minimumsheetwidth(source.machine.minimumsheetwidth);
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
        omachine.IsSpotColor(source.machine.IsSpotColor);
        //omachine.Maximumsheetweight(source.machine.Maximumsheetweight);
        //omachine.Maximumsheetheight(source.machine.Maximumsheetheight);
        //omachine.Maximumsheetwidth(source.machine.Maximumsheetwidth);
        omachine.LookupMethodId(source.machine.LookupMethodId);
        omachine.deFaultPaperSizeName(source.deFaultPaperSizeName);
        omachine.isClickChargezone(source.machine.isSheetFed ? 'true' : 'false');
        omachine.isDigitalPress(source.machine.IsDigitalPress || source.machine.IsDigitalPress == null ? 'true' : 'false');
        //omachine.lookupList.removeAll();
        //ko.utils.arrayPushAll(omachine.lookupList(), source.lookupMethods);
        //omachine.lookupList.valueHasMutated();

        //omachine.markupList.removeAll();
        //ko.utils.arrayPushAll(omachine.markupList(), source.Markups);
        //omachine.markupList.valueHasMutated();
       
        
        
        //_.each(source.MachineSpoilageItems, function (item) {
        //    omachine.MachineSpoilageItems.push(MachineSpoilageItemsMapper(item));
        //});


        
        //var StockItemforInkList = ko.observableArray([]);
        //StockItemforInkList.removeAll();
        //ko.utils.arrayPushAll(StockItemforInkList(), source.StockItemforInk);
        //StockItemforInkList.valueHasMutated();

        //var InkCoveragItemsList = ko.observableArray([]);
        //InkCoveragItemsList.removeAll();
        //ko.utils.arrayPushAll(InkCoveragItemsList(), source.InkCoveragItems);
        //InkCoveragItemsList.valueHasMutated();
       


        //_.each(source.machine.MachineInkCoverages, function (item) {
        //    var module = MachineInkCoveragesListClientMapper(item, StockItemforInkList, InkCoveragItemsList);
        //    omachine.MachineInkCoverages.push(module);




        //})

        //_.each(source.machine.LookupMethod, function (item) {
        //    omachine.MachineLookupMethods.push(MachineLookupMethodsItemsMapper(item));
        //});

          return omachine;
    };
    var machineServerMapper = function (machine, ClickChargeZone, MeterPerHourClickCharge, GuillotineClickCharge, Type, speedWeightCal) {
        var oType = 0;
        oType = Type;
        var oMeterPerHour = {};
        var oGuillotineZone = {};
        var oGuillotinePtvList = [];
        var omachine = {};
        var oSpeedWeightLookup = {};
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
        omachine.LookupMethodId = machine.LookupMethodId();
        omachine.SetupSpoilage = machine.SetupSpoilage();
        omachine.RunningSpoilage = machine.RunningSpoilage();
        omachine.CoverageHigh = machine.CoverageHigh();
        omachine.CoverageMedium = machine.CoverageMedium();
        omachine.CoverageLow = machine.CoverageLow();
        omachine.isSheetFed = machine.isSheetFed();
        omachine.Passes = machine.Passes();
        omachine.IsSpotColor = machine.IsSpotColor();
        omachine.IsDigitalPress = machine.isDigitalPress();
        //omachine.LookupMethod = machine.lookupMethod();
        oMeterPerHour = MeterPerHourClickCharge;
        oGuillotineZone = GuillotineClickCharge;
        oSpeedWeightLookup = speedWeightCal;
        if (GuillotineClickCharge != null)
        {
            oGuillotinePtvList = GuillotineClickCharge.GuillotinePtvList;
        }

        
        
        var ClickChargeZoneLookup = {};
        if (ClickChargeZone != undefined) {
            ClickChargeZoneLookup.Id = ClickChargeZone.Id();
            ClickChargeZoneLookup.MethodId = ClickChargeZone.MethodId();
            ClickChargeZoneLookup.From1 = ClickChargeZone.From1();
            ClickChargeZoneLookup.To1 = ClickChargeZone.To1();
            ClickChargeZoneLookup.Sheets1 = ClickChargeZone.Sheets1();
            ClickChargeZoneLookup.SheetCost1 = ClickChargeZone.SheetCost1();
            ClickChargeZoneLookup.SheetPrice1 = ClickChargeZone.SheetPrice1();
            ClickChargeZoneLookup.From2 = ClickChargeZone.From2();
            ClickChargeZoneLookup.To2 = ClickChargeZone.To2();
            ClickChargeZoneLookup.Sheets2 = ClickChargeZone.Sheets2();
            ClickChargeZoneLookup.SheetCost2 = ClickChargeZone.SheetCost2();
            ClickChargeZoneLookup.SheetPrice2 = ClickChargeZone.SheetPrice2();
            ClickChargeZoneLookup.From3 = ClickChargeZone.From3();
            ClickChargeZoneLookup.To3 = ClickChargeZone.To3();
            ClickChargeZoneLookup.Sheets3 = ClickChargeZone.Sheets3();
            ClickChargeZoneLookup.SheetCost3 = ClickChargeZone.SheetCost3();
            ClickChargeZoneLookup.SheetPrice3 = ClickChargeZone.SheetPrice3();
            ClickChargeZoneLookup.From4 = ClickChargeZone.From4();
            ClickChargeZoneLookup.To4 = ClickChargeZone.To4();
            ClickChargeZoneLookup.Sheets4 = ClickChargeZone.Sheets4();
            ClickChargeZoneLookup.SheetCost4 = ClickChargeZone.SheetCost4();
            ClickChargeZoneLookup.SheetPrice4 = ClickChargeZone.SheetPrice4();
            ClickChargeZoneLookup.From5 = ClickChargeZone.From5();
            ClickChargeZoneLookup.To5 = ClickChargeZone.To5();
            ClickChargeZoneLookup.Sheets5 = ClickChargeZone.Sheets5();
            ClickChargeZoneLookup.SheetCost5 = ClickChargeZone.SheetCost5();
            ClickChargeZoneLookup.SheetPrice5 = ClickChargeZone.SheetPrice5();
            ClickChargeZoneLookup.From6 = ClickChargeZone.From6();
            ClickChargeZoneLookup.To6 = ClickChargeZone.To6();
            ClickChargeZoneLookup.Sheets6 = ClickChargeZone.Sheets6();
            ClickChargeZoneLookup.SheetCost6 = ClickChargeZone.SheetCost6();
            ClickChargeZoneLookup.SheetPrice6 = ClickChargeZone.SheetPrice6();
            ClickChargeZoneLookup.From7 = ClickChargeZone.From7();
            ClickChargeZoneLookup.To7 = ClickChargeZone.To7();
            ClickChargeZoneLookup.Sheets7 = ClickChargeZone.Sheets7();
            ClickChargeZoneLookup.SheetCost7 = ClickChargeZone.SheetCost7();
            ClickChargeZoneLookup.SheetPrice7 = ClickChargeZone.SheetPrice7();
            ClickChargeZoneLookup.From8 = ClickChargeZone.From8();
            ClickChargeZoneLookup.To8 = ClickChargeZone.To8();
            ClickChargeZoneLookup.Sheets8 = ClickChargeZone.Sheets8();
            ClickChargeZoneLookup.SheetCost8 = ClickChargeZone.SheetCost8();
            ClickChargeZoneLookup.SheetPrice8 = ClickChargeZone.SheetPrice8();
            ClickChargeZoneLookup.From9 = ClickChargeZone.From9();
            ClickChargeZoneLookup.To9 = ClickChargeZone.To9();
            ClickChargeZoneLookup.Sheets9 = ClickChargeZone.Sheets9();
            ClickChargeZoneLookup.SheetCost9 = ClickChargeZone.SheetCost9();
            ClickChargeZoneLookup.SheetPrice9 = ClickChargeZone.SheetPrice9();
            ClickChargeZoneLookup.From10 = ClickChargeZone.From10();
            ClickChargeZoneLookup.To10 = ClickChargeZone.To10();
            ClickChargeZoneLookup.Sheets10 = ClickChargeZone.Sheets10();
            ClickChargeZoneLookup.SheetCost10 = ClickChargeZone.SheetCost10();
            ClickChargeZoneLookup.SheetPrice10 = ClickChargeZone.SheetPrice10();
            ClickChargeZoneLookup.From11 = ClickChargeZone.From11();
            ClickChargeZoneLookup.To11 = ClickChargeZone.To11();
            ClickChargeZoneLookup.Sheets11 = ClickChargeZone.Sheets11();
            ClickChargeZoneLookup.SheetCost11 = ClickChargeZone.SheetCost11();
            ClickChargeZoneLookup.SheetPrice11 = ClickChargeZone.SheetPrice11();
            ClickChargeZoneLookup.From12 = ClickChargeZone.From12();
            ClickChargeZoneLookup.To12 = ClickChargeZone.To12();
            ClickChargeZoneLookup.Sheets12 = ClickChargeZone.Sheets12();
            ClickChargeZoneLookup.SheetCost12 = ClickChargeZone.SheetCost12();
            ClickChargeZoneLookup.SheetPrice12 = ClickChargeZone.SheetPrice12();
            ClickChargeZoneLookup.From13 = ClickChargeZone.From13();
            ClickChargeZoneLookup.To13 = ClickChargeZone.To13();
            ClickChargeZoneLookup.Sheets13 = ClickChargeZone.Sheets13();
            ClickChargeZoneLookup.SheetCost13 = ClickChargeZone.SheetCost13();
            ClickChargeZoneLookup.SheetPrice13 = ClickChargeZone.SheetPrice13();
            ClickChargeZoneLookup.From14 = ClickChargeZone.From14();
            ClickChargeZoneLookup.To14 = ClickChargeZone.To14();
            ClickChargeZoneLookup.Sheets14 = ClickChargeZone.Sheets14();
            ClickChargeZoneLookup.SheetCost14 = ClickChargeZone.SheetCost14();
            ClickChargeZoneLookup.SheetPrice14 = ClickChargeZone.SheetPrice14();
            ClickChargeZoneLookup.From15 = ClickChargeZone.From15();
            ClickChargeZoneLookup.To15 = ClickChargeZone.To15();
            ClickChargeZoneLookup.Sheets15 = ClickChargeZone.Sheets15();
            ClickChargeZoneLookup.SheetCost15 = ClickChargeZone.SheetCost15();
            ClickChargeZoneLookup.SheetPrice15 = ClickChargeZone.SheetPrice15();
            ClickChargeZoneLookup.isaccumulativecharge = ClickChargeZone.isaccumulativecharge();
            ClickChargeZoneLookup.IsRoundUp = ClickChargeZone.IsRoundUp();
            ClickChargeZoneLookup.TimePerHour = ClickChargeZone.TimePerHour();
        }
        return {
            Type : oType,
            machine: omachine,
            ClickChargeZone: ClickChargeZoneLookup,
            MeterPerHourLookup: oMeterPerHour,
            GuillotineCalc: oGuillotineZone,
            GuilotinePtv: oGuillotinePtvList,
            SpeedWeightCal: oSpeedWeightLookup
        };

    };
    var newMachineClientMapper = function (source, callback) {
        var omachine = new machine(callback);

        omachine.lookupList.removeAll();
        ko.utils.arrayPushAll(omachine.lookupList(), source.lookupMethods);
        omachine.lookupList.valueHasMutated();
        omachine.SetupSpoilage(source.machine.SetupSpoilage);
        omachine.RunningSpoilage(source.machine.RunningSpoilage);
        omachine.Passes(source.machine.Passes);
        omachine.MachineId(source.machine.MachineId);
        omachine.MachineName(source.machine.MachineName);
        omachine.ColourHeads(8);
        omachine.isDigitalPress('true');
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
        if (source.StockItemforInk != null) {
            ko.utils.arrayPushAll(StockItemforInkList(), source.StockItemforInk);
            StockItemforInkList.valueHasMutated();
        }
        

        var InkCoveragItemsList = ko.observableArray([]);
        InkCoveragItemsList.removeAll();
        if (source.InkCoveragItems != null) {
            ko.utils.arrayPushAll(InkCoveragItemsList(), source.InkCoveragItems);
            InkCoveragItemsList.valueHasMutated();
        }
        



        for (i = 0; i < 8; i++) {
            omachine.MachineInkCoverages.push(newMachineInkCoveragesListClientMapper(StockItemforInkList, InkCoveragItemsList));
        }

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
    var GuilotinePtvServerMapper = function (GuillotineClickChargePTV) {
        var GuilotinePtv = {};
        GuilotinePtv.Id = GuillotineClickChargePTV.Id();
        GuilotinePtv.NoofSections = GuillotineClickChargePTV.NoofSections();
        GuilotinePtv.NoofUps = GuillotineClickChargePTV.NoofUps();
        GuilotinePtv.Noofcutswithoutgutters = GuillotineClickChargePTV.Noofcutswithoutgutters();
        GuilotinePtv.Noofcutswithgutters = GuillotineClickChargePTV.Noofcutswithgutters();
        GuilotinePtv.GuilotineId = GuillotineClickChargePTV.GuilotineId();
        return GuilotinePtv;
    }
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
    var MachineInkCoveragesListServerMapper = function(source) {
        var InkCoveragesItem = {};
        InkCoveragesItem.Id = source.Id;
        InkCoveragesItem.SideInkOrder = source.SideInkOrder();
        InkCoveragesItem.SideInkOrderCoverage = source.SideInkOrderCoverage();
        InkCoveragesItem.MachineId = source.MachineId;

        return InkCoveragesItem;

    },
        SpeedWeightLookup = function(source) {
            var Id = ko.observable(source != undefined ? source.Id : undefined),
                MethodId = ko.observable(source != undefined ? source.MethodId : undefined),
                SheetsQty1 = ko.observable(source != undefined ? source.SheetsQty1 : undefined),
                SheetsQty2 = ko.observable(source != undefined ? source.SheetsQty2 : undefined),
                SheetsQty3 = ko.observable(source != undefined ? source.SheetsQty3 : undefined),
                SheetsQty4 = ko.observable(source != undefined ? source.SheetsQty4 : undefined),
                SheetsQty5 = ko.observable(source != undefined ? source.SheetsQty5 : undefined),
                SheetWeight1 = ko.observable(source != undefined ? source.SheetWeight1 : undefined),
                speedqty11 = ko.observable(source != undefined ? source.speedqty11 : undefined),
                speedqty12 = ko.observable(source != undefined ? source.speedqty12 : undefined),
                speedqty13 = ko.observable(source != undefined ? source.speedqty13 : undefined),
                speedqty14 = ko.observable(source != undefined ? source.speedqty14 : undefined),
                speedqty15 = ko.observable(source != undefined ? source.speedqty15 : undefined),
                SheetWeight2 = ko.observable(source != undefined ? source.SheetWeight2 : undefined),
                speedqty21 = ko.observable(source != undefined ? source.speedqty21 : undefined),
                speedqty22 = ko.observable(source != undefined ? source.speedqty22 : undefined),
                speedqty23 = ko.observable(source != undefined ? source.speedqty23 : undefined),
                speedqty24 = ko.observable(source != undefined ? source.speedqty24 : undefined),
                speedqty25 = ko.observable(source != undefined ? source.speedqty25 : undefined),
                SheetWeight3 = ko.observable(source != undefined ? source.SheetWeight3 : undefined),
                speedqty31 = ko.observable(source != undefined ? source.speedqty31 : undefined),
                speedqty32 = ko.observable(source != undefined ? source.speedqty32 : undefined),
                speedqty33 = ko.observable(source != undefined ? source.speedqty33 : undefined),
                speedqty34 = ko.observable(source != undefined ? source.speedqty34 : undefined),
                speedqty35 = ko.observable(source != undefined ? source.speedqty35 : undefined),
                hourlyCost = ko.observable(source != undefined ? source.hourlyCost : undefined),
                hourlyPrice = ko.observable(source != undefined ? source.hourlyPrice : undefined),
                errors = ko.validation.group({                
                    
                }),
                dirtyFlag = new ko.dirtyFlag({
                    Id: Id,
                    MethodId: MethodId,
                    SheetsQty1: SheetsQty1,
                    SheetsQty2: SheetsQty2,
                    SheetsQty3: SheetsQty3,
                    SheetsQty4: SheetsQty4,
                    SheetsQty5: SheetsQty5,
                    SheetWeight1: SheetWeight1,
                    speedqty11: speedqty11,
                    speedqty12: speedqty12,
                    speedqty13: speedqty13,
                    speedqty14: speedqty14,
                    speedqty15: speedqty15,
                    SheetWeight2: SheetWeight2,
                    speedqty21: speedqty21,
                    speedqty22: speedqty22,
                    speedqty23: speedqty23,
                    speedqty24: speedqty24,
                    speedqty25: speedqty25,
                    SheetWeight3: SheetWeight3,
                    speedqty31: speedqty31,
                    speedqty32: speedqty32,
                    speedqty33: speedqty33,
                    speedqty34: speedqty34,
                    speedqty35: speedqty35,
                    hourlyCost: hourlyCost,
                    hourlyPrice: hourlyPrice
                }),
                // Is Valid
                isValid = ko.computed(function() {
                    return errors().length === 0;
                }),        
                // Has Changes
                hasChanges = ko.computed(function() {
                    return dirtyFlag.isDirty();
                }),            
                // Reset
                reset = function() {
                    dirtyFlag.reset();
                };

            return {
                Id: Id,
                MethodId: MethodId,
                SheetsQty1: SheetsQty1,
                SheetsQty2: SheetsQty2,
                SheetsQty3: SheetsQty3,
                SheetsQty4: SheetsQty4,
                SheetsQty5: SheetsQty5,
                SheetWeight1: SheetWeight1,
                speedqty11: speedqty11,
                speedqty12: speedqty12,
                speedqty13: speedqty13,
                speedqty14: speedqty14,
                speedqty15: speedqty15,
                SheetWeight2: SheetWeight2,
                speedqty21: speedqty21,
                speedqty22: speedqty22,
                speedqty23: speedqty23,
                speedqty24: speedqty24,
                speedqty25: speedqty25,
                SheetWeight3: SheetWeight3,
                speedqty31: speedqty31,
                speedqty32: speedqty32,
                speedqty33: speedqty33,
                speedqty34: speedqty34,
                speedqty35: speedqty35,
                hourlyCost: hourlyCost,
                hourlyPrice: hourlyPrice,
                errors: errors,
                isValid: isValid,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                reset: reset
            };


        },
       
    
        speedWeightconvertToServerData = function(source) {
            return {
                Id: source.Id(),
                MethodId: source.MethodId(),
                SheetsQty1: source.SheetsQty1(),
                SheetsQty2: source.SheetsQty2(),
                SheetsQty3: source.SheetsQty3(),
                SheetsQty4: source.SheetsQty4(),
                SheetsQty5: source.SheetsQty5(),

                SheetWeight1: source.SheetWeight1(),
                speedqty11: source.speedqty11(),
                speedqty12: source.speedqty12(),
                speedqty13: source.speedqty13(),
                speedqty14: source.speedqty14(),
                speedqty15: source.speedqty15(),

                SheetWeight2: source.SheetWeight2(),
                speedqty21: source.speedqty21(),
                speedqty22: source.speedqty22(),
                speedqty23: source.speedqty23(),
                speedqty24: source.speedqty24(),
                speedqty25: source.speedqty25(),

                SheetWeight3: source.SheetWeight3(),
                speedqty31: source.speedqty31(),
                speedqty32: source.speedqty32(),
                speedqty33: source.speedqty33(),
                speedqty34: source.speedqty34(),
                speedqty35: source.speedqty35(),
                hourlyCost: source.hourlyCost(),
                hourlyPrice: source.hourlyPrice(),
            };
        };
    SpeedWeightLookup.create = function() {
        var speedweight = new SpeedWeightLookup({
            ID: 0,
            MethodId: 0,
            SheetsQty1: 2500,
            SheetsQty2: 5000,
            SheetsQty3: 10000,
            SheetsQty4: 20000,
            SheetsQty5: 50000,
            SheetWeight1: 100,
            speedqty11: 4000,
            speedqty12: 7000,
            speedqty13: 8000,
            speedqty14: 9000,
            speedqty15: 12000,
            SheetWeight2: 170,
            speedqty21: 3000,
            speedqty22: 6000,
            speedqty23: 7000,
            speedqty24: 8000,
            speedqty25: 9000,
            SheetWeight3: 300,
            speedqty31: 2000,
            speedqty32: 5000,
            speedqty33: 6000,
            speedqty34: 7000,
            speedqty35: 8000,
            hourlyCost: 1,
            hourlyPrice: 2
        });
        return speedweight;
    },
    ClickChargeZoneLookup = function(source) {
        var Id = ko.observable(source != undefined ? source.Id : undefined),
            MethodId = ko.observable(source != undefined ? source.MethodId : undefined),
            From1 = ko.observable(source != undefined ? source.From1 : undefined),
            To1 = ko.observable(source != undefined ? source.To1 : undefined),
            Sheets1 = ko.observable(source != undefined ? source.Sheets1 : undefined),
            SheetCost1 = ko.observable(source != undefined ? source.SheetCost1 : undefined),
            SheetPrice1 = ko.observable(source != undefined ? source.SheetPrice1 : undefined),
            From2 = ko.observable(source != undefined ? source.From2 : undefined),
            To2 = ko.observable(source != undefined ? source.To2 : undefined).extend({ number: true }),
            Sheets2 = ko.observable(source != undefined ? source.Sheets2 : undefined),
            SheetCost2 = ko.observable(source != undefined ? source.SheetCost2 : undefined),
            SheetPrice2 = ko.observable(source != undefined ? source.SheetPrice2 : undefined),
            From3 = ko.observable(source != undefined ? source.From3 : undefined),
            To3 = ko.observable(source != undefined ? source.To3 : undefined),
            Sheets3 = ko.observable(source != undefined ? source.Sheets3 : undefined),
            SheetCost3 = ko.observable(source != undefined ? source.SheetCost3 : undefined),
            SheetPrice3 = ko.observable(source != undefined ? source.SheetPrice3 : undefined),
            From4 = ko.observable(source != undefined ? source.From4 : undefined),
            To4 = ko.observable(source != undefined ? source.To4 : undefined),
            Sheets4 = ko.observable(source != undefined ? source.Sheets4 : undefined),
            SheetCost4 = ko.observable(source != undefined ? source.SheetCost4 : undefined),
            SheetPrice4 = ko.observable(source != undefined ? source.SheetPrice4 : undefined),
            From5 = ko.observable(source != undefined ? source.From5 : undefined),
            To5 = ko.observable(source != undefined ? source.To5 : undefined),
            Sheets5 = ko.observable(source != undefined ? source.Sheets5 : undefined),
            SheetCost5 = ko.observable(source != undefined ? source.SheetCost5 : undefined),
            SheetPrice5 = ko.observable(source != undefined ? source.SheetPrice5 : undefined),
            From6 = ko.observable(source != undefined ? source.From6 : undefined),
            To6 = ko.observable(source != undefined ? source.To6 : undefined),
            Sheets6 = ko.observable(source != undefined ? source.Sheets6 : undefined),
            SheetCost6 = ko.observable(source != undefined ? source.SheetCost6 : undefined),
            SheetPrice6 = ko.observable(source != undefined ? source.SheetPrice6 : undefined),
            From7 = ko.observable(source != undefined ? source.From7 : undefined),
            To7 = ko.observable(source != undefined ? source.To7 : undefined),
            Sheets7 = ko.observable(source != undefined ? source.Sheets7 : undefined),
            SheetCost7 = ko.observable(source != undefined ? source.SheetCost7 : undefined),
            SheetPrice7 = ko.observable(source != undefined ? source.SheetPrice7 : undefined),
            From8 = ko.observable(source != undefined ? source.From8 : undefined),
            To8 = ko.observable(source != undefined ? source.To8 : undefined),
            Sheets8 = ko.observable(source != undefined ? source.Sheets8 : undefined),
            SheetCost8 = ko.observable(source != undefined ? source.SheetCost8 : undefined),
            SheetPrice8 = ko.observable(source != undefined ? source.SheetPrice8 : undefined),
            From9 = ko.observable(source != undefined ? source.From9 : undefined),
            To9 = ko.observable(source != undefined ? source.To9 : undefined),
            Sheets9 = ko.observable(source != undefined ? source.Sheets9 : undefined),
            SheetCost9 = ko.observable(source != undefined ? source.SheetCost9 : undefined),
            SheetPrice9 = ko.observable(source != undefined ? source.SheetPrice9 : undefined),
            From10 = ko.observable(source != undefined ? source.From10 : undefined),
            To10 = ko.observable(source != undefined ? source.To10 : undefined),
            Sheets10 = ko.observable(source != undefined ? source.Sheets10 : undefined),
            SheetCost10 = ko.observable(source != undefined ? source.SheetCost10 : undefined),
            SheetPrice10 = ko.observable(source != undefined ? source.SheetPrice10 : undefined),
            From11 = ko.observable(source != undefined ? source.From11 : undefined),
            To11 = ko.observable(source != undefined ? source.To11 : undefined),
            Sheets11 = ko.observable(source != undefined ? source.Sheets11 : undefined),
            SheetCost11 = ko.observable(source != undefined ? source.SheetCost11 : undefined),
            SheetPrice11 = ko.observable(source != undefined ? source.SheetPrice11 : undefined),
            From12 = ko.observable(source != undefined ? source.From12 : undefined),
            To12 = ko.observable(source != undefined ? source.To12 : undefined),
            Sheets12 = ko.observable(source != undefined ? source.Sheets12 : undefined),
            SheetCost12 = ko.observable(source != undefined ? source.SheetCost12 : undefined),
            SheetPrice12 = ko.observable(source != undefined ? source.SheetPrice12 : undefined),
            From13 = ko.observable(source != undefined ? source.From13 : undefined),
            To13 = ko.observable(source != undefined ? source.To13 : undefined),
            Sheets13 = ko.observable(source != undefined ? source.Sheets13 : undefined),
            SheetCost13 = ko.observable(source != undefined ? source.SheetCost13 : undefined),
            SheetPrice13 = ko.observable(source != undefined ? source.SheetPrice13 : undefined),
            From14 = ko.observable(source != undefined ? source.From14 : undefined),
            To14 = ko.observable(source != undefined ? source.To14 : undefined),
            Sheets14 = ko.observable(source != undefined ? source.Sheets14 : undefined),
            SheetCost14 = ko.observable(source != undefined ? source.SheetCost14 : undefined),
            SheetPrice14 = ko.observable(source != undefined ? source.SheetPrice14 : undefined),
            From15 = ko.observable(source != undefined ? source.From15 : undefined),
            To15 = ko.observable(source != undefined ? source.To15 : undefined),
            Sheets15 = ko.observable(source != undefined ? source.Sheets15 : undefined),
            SheetCost15 = ko.observable(source != undefined ? source.SheetCost15 : undefined),
            SheetPrice15 = ko.observable(source != undefined ? source.SheetPrice15 : undefined),
            isaccumulativecharge = ko.observable(source != undefined ? source.isaccumulativecharge : undefined),
            IsRoundUp = ko.observable(source != undefined ? source.IsRoundUp : undefined),
            TimePerHour = ko.observable(source != undefined ? source.TimePerHour : undefined),
            errors = ko.validation.group({                
                
            }),
            dirtyFlag = new ko.dirtyFlag({
                Id: Id,
                MethodId: MethodId,
                From1: From1,
                To1: To1,
                Sheets1: Sheets1,
                SheetCost1: SheetCost1,
                SheetPrice1: SheetPrice1,
                From2: From2,
                To2: To2,
                Sheets2: Sheets2,
                SheetCost2: SheetCost2,
                SheetPrice2: SheetPrice2,
                From3: From3,
                To3: To3,
                Sheets3: Sheets3,
                SheetCost3: SheetCost3,
                SheetPrice3: SheetPrice3,
                From4: From4,
                To4: To4,
                Sheets4: Sheets4,
                SheetCost4: SheetCost4,
                SheetPrice4: SheetPrice4,
                From5: From5,
                To5: To5,
                Sheets5: Sheets5,
                SheetCost5: SheetCost5,
                SheetPrice5: SheetPrice5,
                From6: From6,
                To6: To6,
                Sheets6: Sheets6,
                SheetCost6: SheetCost6,
                SheetPrice6: SheetPrice6,
                From7: From7,
                To7: To7,
                Sheets7: Sheets7,
                SheetCost7: SheetCost7,
                SheetPrice7: SheetPrice7,
                From8: From8,
                To8: To8,
                Sheets8: Sheets8,
                SheetCost8: SheetCost8,
                SheetPrice8: SheetPrice8,
                From9: From9,
                To9: To9,
                Sheets9: Sheets9,
                SheetCost9: SheetCost9,
                SheetPrice9: SheetPrice9,
                From10: From10,
                To10: To10,
                Sheets10: Sheets10,
                SheetCost10: SheetCost10,
                SheetPrice10: SheetPrice10,
                From11: From11,
                To11: To11,
                Sheets11: Sheets11,
                SheetCost11: SheetCost11,
                SheetPrice11: SheetPrice11,
                From12: From12,
                To12: To12,
                Sheets12: Sheets12,
                SheetCost12: SheetCost12,
                SheetPrice12: SheetPrice12,
                From13: From13,
                To13: To13,
                Sheets13: Sheets13,
                SheetCost13: SheetCost13,
                SheetPrice13: SheetPrice13,
                From14: From14,
                To14: To14,
                Sheets14: Sheets14,
                SheetCost14: SheetCost14,
                SheetPrice14: SheetPrice14,
                From15: From15,
                To15: To15,
                Sheets15: Sheets15,
                SheetCost15: SheetCost15,
                SheetPrice15: SheetPrice15,
                isaccumulativecharge: isaccumulativecharge,
                IsRoundUp: IsRoundUp,
                TimePerHour: TimePerHour

            }),
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
        // Reset
       reset = function () {
           dirtyFlag.reset();
       };

        return {
            Id: Id,
            MethodId: MethodId,
            From1: From1,
            To1: To1,
            Sheets1: Sheets1,
            SheetCost1: SheetCost1,
            SheetPrice1: SheetPrice1,
            From2: From2,
            To2: To2,
            Sheets2: Sheets2,
            SheetCost2: SheetCost2,
            SheetPrice2: SheetPrice2,
            From3: From3,
            To3: To3,
            Sheets3: Sheets3,
            SheetCost3: SheetCost3,
            SheetPrice3: SheetPrice3,
            From4: From4,
            To4: To4,
            Sheets4: Sheets4,
            SheetCost4: SheetCost4,
            SheetPrice4: SheetPrice4,
            From5: From5,
            To5: To5,
            Sheets5: Sheets5,
            SheetCost5: SheetCost5,
            SheetPrice5: SheetPrice5,
            From6: From6,
            To6: To6,
            Sheets6: Sheets6,
            SheetCost6: SheetCost6,
            SheetPrice6: SheetPrice6,
            From7: From7,
            To7: To7,
            Sheets7: Sheets7,
            SheetCost7: SheetCost7,
            SheetPrice7: SheetPrice7,
            From8: From8,
            To8: To8,
            Sheets8: Sheets8,
            SheetCost8: SheetCost8,
            SheetPrice8: SheetPrice8,
            From9: From9,
            To9: To9,
            Sheets9: Sheets9,
            SheetCost9: SheetCost9,
            SheetPrice9: SheetPrice9,
            From10: From10,
            To10: To10,
            Sheets10: Sheets10,
            SheetCost10: SheetCost10,
            SheetPrice10: SheetPrice10,
            From11: From11,
            To11: To11,
            Sheets11: Sheets11,
            SheetCost11: SheetCost11,
            SheetPrice11: SheetPrice11,
            From12: From12,
            To12: To12,
            Sheets12: Sheets12,
            SheetCost12: SheetCost12,
            SheetPrice12: SheetPrice12,
            From13: From13,
            To13: To13,
            Sheets13: Sheets13,
            SheetCost13: SheetCost13,
            SheetPrice13: SheetPrice13,
            From14: From14,
            To14: To14,
            Sheets14: Sheets14,
            SheetCost14: SheetCost14,
            SheetPrice14: SheetPrice14,
            From15: From15,
            To15: To15,
            Sheets15: Sheets15,
            SheetCost15: SheetCost15,
            SheetPrice15: SheetPrice15,
            isaccumulativecharge: isaccumulativecharge,
            IsRoundUp: IsRoundUp,
            TimePerHour: TimePerHour,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset
        };
    };
    NewClickChargeZoneLookup = function() {
        return {
            Id: 0,
            MethodId: 0,
            From1: 0,
            To1: 5000,
            Sheets1: 1000,
            SheetCost1: 22,
            SheetPrice1: 22,
            From2: 5001,
            To2: 10000,
            Sheets2: 1000,
            SheetCost2: 21,
            SheetPrice2: 21,
            From3: 10001,
            To3: 20000,
            Sheets3: 1000,
            SheetCost3: 20,
            SheetPrice3: 20,
            From4: 20001,
            To4: 30000,
            Sheets4: 1000,
            SheetCost4: 19,
            SheetPrice4: 19,
            From5: 30001,
            To5: 40000,
            Sheets5: 1000,
            SheetCost5: 18,
            SheetPrice5: 18,
            From6: 40001,
            To6: 50000,
            Sheets6: 1000,
            SheetCost6: 17,
            SheetPrice6: 17,
            From7: 50001,
            To7: 60000,
            Sheets7: 1000,
            SheetCost7: 16,
            SheetPrice7: 16,
            From8: 60001,
            To8: 70000,
            Sheets8: 1000,
            SheetCost8: 15,
            SheetPrice8: 15,
            From9: 70001,
            To9: 80000,
            Sheets9: 1000,
            SheetCost9: 14,
            SheetPrice9: 14,
            From10: 80001,
            To10: 90000,
            Sheets10: 1000,
            SheetCost10: 13,
            SheetPrice10: 13,
            From11: 90001,
            To11: 100000,
            Sheets11: 1000,
            SheetCost11: 12,
            SheetPrice11: 12,
            From12: 100001,
            To12: 110000,
            Sheets12: 1000,
            SheetCost12: 11,
            SheetPrice12: 11,
            From13: 110001,
            To13: 120000,
            Sheets13: 1000,
            SheetCost13: 10,
            SheetPrice13: 10,
            From14: 120001,
            To14: 130000,
            Sheets14: 1000,
            SheetCost14: 9,
            SheetPrice14: 9,
            From15: 130001,
            To15: 140000,
            Sheets15: 1000,
            SheetCost15: 8,
            SheetPrice15: 8,
            isaccumulativecharge: 0,
            IsRoundUp: 0,
            TimePerHour: 0
        };
        
    };

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
        MachineLookupClientMapper: MachineLookupClientMapper,
        GuilotinePtvServerMapper: GuilotinePtvServerMapper,
        speedWeightconvertToServerData: speedWeightconvertToServerData,
        SpeedWeightLookup: SpeedWeightLookup,
        NewClickChargeZoneLookup: NewClickChargeZoneLookup
    };
});