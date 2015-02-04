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
            ImageSource : ImageSource,
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
            ColourHeads = ko.observable(),
            isPerfecting = ko.observable(),
            SetupCharge = ko.observable(),
            WashupPrice = ko.observable(),
            WashupCost = ko.observable(),
            MinInkDuctqty = ko.observable(),
            worknturncharge = ko.observable(),
            MakeReadyCost = ko.observable(),
            DefaultFilmId = ko.observable(),
            DefaultPlateId = ko.observable(),
            DefaultPaperId = ko.observable(),
            isfilmused = ko.observable(),
            isplateused = ko.observable(),
            ismakereadyused = ko.observable(),
            iswashupused = ko.observable(),
            maximumsheetweight = ko.observable(),
            maximumsheetheight = ko.observable(),
            maximumsheetwidth = ko.observable(),
            minimumsheetheight = ko.observable(),
            minimumsheetwidth = ko.observable(),
            gripdepth = ko.observable(),
            gripsideorientaion = ko.observable(),
            Orientation = ko.observableArray([
                    new OrientationModel({ id: "1", name: "Long Side" }),
                    new OrientationModel({ id: "2", name: "Short Side" })]),
            lookupList = ko.observableArray([]),
            gutterdepth = ko.observable(),
            headdepth = ko.observable(),
            Va = ko.observable(),
            PressSizeRatio = ko.observable(),
            Description = ko.observable(),
            Priority = ko.observable(),
            DirectCost = ko.observable(),
            Image = ko.observable(),
            MinimumCharge = ko.observable(),
            CostPerCut = ko.observable(),
            PricePerCut = ko.observable(),
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
            MakeReadyPrice = ko.observable(),
            InkChargeForUniqueColors = ko.observable(),
            CompanyId = ko.observable(),
            FlagId = ko.observable(),
            IsScheduleable = ko.observable(),
            SystemSiteId = ko.observable(),
            SpoilageType = ko.observable(),
            SetupTime = ko.observable(),
            TimePerCut = ko.observable(),
            MakeReadyTime = ko.observable(),
            WashupTime = ko.observable(),
            ReelMakereadyTime = ko.observable(),
            Maximumsheetweight = ko.observable(),
            Maximumsheetheight = ko.observable(),
            Maximumsheetwidth = ko.observable(),
            Minimumsheetheight = ko.observable(),
            Minimumsheetwidth = ko.observable(),
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
            Va : Va,
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
            lookupList: lookupList,
            dirtyFlag: dirtyFlag,
            errors: errors,
            isValid: isValid,
            hasChanges: hasChanges,
            reset: reset
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
    var lookupMethodListClientMapper = function (source) {
        var olookup = new lookupMethod();
        olookup.MethodId(source.MethodId);
        olookup.Name(source.Name);
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
        return omachineList;

    };

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
        omachine.WashupPrice(source.machine.WashupPrice);
        omachine.WashupCost(source.machine.WashupCost);
        omachine.MinInkDuctqty(source.machine.MinInkDuctqty);
        omachine.worknturncharge(source.machine.worknturncharge);
        omachine.MakeReadyCost(source.machine.MakeReadyCost);
        omachine.DefaultFilmId(source.machine.DefaultFilmId);
        omachine.DefaultPlateId(source.machine.DefaultPlateId);
        omachine.DefaultPaperId(source.machine.DefaultPaperId);
        omachine.isfilmused(source.machine.isfilmused);
        omachine.isplateused(source.machine.isplateused);
        omachine.ismakereadyused(source.machine.ismakereadyused);
        omachine.iswashupused(source.machine.iswashupused);
        omachine.maximumsheetweight(source.machine.maximumsheetweight);
        omachine.maximumsheetheight(source.machine.maximumsheetheight);
        omachine.maximumsheetwidth(source.machine.maximumsheetwidth);
        omachine.minimumsheetheight(source.machine.minimumsheetheight);
        omachine.minimumsheetwidth(source.machine.minimumsheetwidth);
        omachine.gripdepth(source.machine.gripdepth);
        omachine.gripsideorientaion(source.machine.gripsideorientaion);
        omachine.gutterdepth(source.machine.gutterdepth);
        omachine.headdepth(source.machine.headdepth);
        omachine.Va(source.machine.Va);
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
        omachine.MakeReadyPrice(source.machine.MakeReadyPrice);
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
        omachine.Maximumsheetweight(source.machine.Maximumsheetweight);
        omachine.Maximumsheetheight(source.machine.Maximumsheetheight);
        omachine.Maximumsheetwidth(source.machine.Maximumsheetwidth);
        omachine.Minimumsheetheight(source.machine.Minimumsheetheight);
        omachine.Minimumsheetwidth(source.machine.Minimumsheetwidth);
        omachine.LookupMethodId(source.machine.LookupMethodId);
        omachine.lookupList.removeAll();
        ko.utils.arrayPushAll(omachine.lookupList(), source.lookupMethods);
        omachine.lookupList.valueHasMutated();
        
        return omachine;
    };
    return {
        machineList: machineList,
        machineListClientMapper: machineListClientMapper,
        machineListServerMapper: machineListServerMapper,
        machine: machine,
        lookupMethod: lookupMethod,
        lookupMethodListClientMapper:lookupMethodListClientMapper,
        machineClientMapper: machineClientMapper
    };
});