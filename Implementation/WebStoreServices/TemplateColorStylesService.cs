using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    class TemplateColorStylesService : ITemplateColorStylesService
    {
          #region private
        public readonly ITemplateColorStylesRepository _templateColorStyleRepository;
        #endregion
        #region constructor
        public TemplateColorStylesService(ITemplateColorStylesRepository templateColorStyleRepository)
        {
            this._templateColorStyleRepository = templateColorStyleRepository;
        }
        #endregion

        #region public
        // get list of color styles based on product ID and customer ID , called from designer // added by saqib ali
        public List<TemplateColorStyle> GetColorStyle(long ProductId, long CustomerID)
        {
            var colors= _templateColorStyleRepository.GetColorStyle(ProductId,CustomerID);
            return colors;
        }
        // get list of color styles based on product id , called from designer // added by saqib ali
        public List<TemplateColorStyle> GetColorStyle(long ProductId)
        {
            var colors = _templateColorStyleRepository.GetColorStyle(ProductId);
            return colors;
        }
        // save corporate color entry in designer // added by saqib ali
        public string SaveCorpColor(int C, int M, int Y, int K, string Name, long CustomerID)
        {
            return _templateColorStyleRepository.SaveCorpColor(C, M, Y, K, Name, CustomerID);
        }
        // update corporate color status (active/deactive color) // added by saqib ali
        public string UpdateCorpColor(long id, string type)
        {
            return _templateColorStyleRepository.UpdateCorpColor(id,type);
        }
        #endregion
    }
}
