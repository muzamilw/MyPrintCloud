﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ITemplateBackgroundImagesService
    {
        void DeleteTemplateBackgroundImages(long productID, long organizationID);
    }
}
