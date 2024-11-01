/* -*- Mode: C++; tab-width: 8; indent-tabs-mode: nil; c-basic-offset: 2 -*- */
/*
//@line 38 "k:\Hudson\jobs\k2-mozilla20\workspace\toolkit\mozapps\update\nsUpdateServiceStub.js"
*/
Components.utils.import("resource://gre/modules/XPCOMUtils.jsm");
Components.utils.import("resource://gre/modules/FileUtils.jsm");

const Ci = Components.interfaces;

const DIR_UPDATES         = "updates";
const FILE_UPDATE_STATUS  = "update.status";

const KEY_APPDIR          = "XCurProcD";

//@line 56 "k:\Hudson\jobs\k2-mozilla20\workspace\toolkit\mozapps\update\nsUpdateServiceStub.js"

//@line 58 "k:\Hudson\jobs\k2-mozilla20\workspace\toolkit\mozapps\update\nsUpdateServiceStub.js"
const KEY_UPDROOT         = "UpdRootD";
//@line 60 "k:\Hudson\jobs\k2-mozilla20\workspace\toolkit\mozapps\update\nsUpdateServiceStub.js"

/**
//@line 68 "k:\Hudson\jobs\k2-mozilla20\workspace\toolkit\mozapps\update\nsUpdateServiceStub.js"
 */
function getUpdateDirNoCreate(pathArray) {
//@line 71 "k:\Hudson\jobs\k2-mozilla20\workspace\toolkit\mozapps\update\nsUpdateServiceStub.js"
  try {
    let dir = FileUtils.getDir(KEY_UPDROOT, pathArray, false);
    return dir;
  } catch (e) {
  }
//@line 77 "k:\Hudson\jobs\k2-mozilla20\workspace\toolkit\mozapps\update\nsUpdateServiceStub.js"
  return FileUtils.getDir(KEY_APPDIR, pathArray, false);
}

function UpdateServiceStub() {
  let statusFile = getUpdateDirNoCreate([DIR_UPDATES, "0"]);
  statusFile.append(FILE_UPDATE_STATUS);
  // If the update.status file exists then initiate post update processing.
  if (statusFile.exists()) {
    let aus = Components.classes["@mozilla.org/updates/update-service;1"].
              getService(Ci.nsIApplicationUpdateService).
              QueryInterface(Ci.nsIObserver);
    aus.observe(null, "post-update-processing", "");
  }
}
UpdateServiceStub.prototype = {
  observe: function(){},
  classID: Components.ID("{e43b0010-04ba-4da6-b523-1f92580bc150}"),
  QueryInterface: XPCOMUtils.generateQI([Ci.nsIObserver])
};

var NSGetFactory = XPCOMUtils.generateNSGetFactory([UpdateServiceStub]);
