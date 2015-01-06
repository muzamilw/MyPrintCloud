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

        public List<TemplateColorStyle> GetColorStyle(int ProductId, int CustomerID)
        {
            var colors= _templateColorStyleRepository.GetColorStyle(ProductId,CustomerID);
            return colors;
        }
        
        public List<TemplateColorStyle> GetColorStyle(int ProductId)
        {
            var colors = _templateColorStyleRepository.GetColorStyle(ProductId);
            return colors;
        }
        public int SaveCorpColor(int C, int M, int Y, int K, string Name, int CustomerID)
        {
            return _templateColorStyleRepository.SaveCorpColor(C, M, Y, K, Name, CustomerID);
        }
        public string UpdateCorpColor(int id, string type)
        {
            return _templateColorStyleRepository.UpdateCorpColor(id,type);
        }
        #endregion
    }
}
