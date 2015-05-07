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
    class TemplateVariableService : ITemplateVariableService
    {
        #region private
        public readonly IFieldVariableRepository _fieldVariableRepository;
        public readonly ITemplateVariableRepository _templateVariableRepository;
        public readonly ITemplateObjectRepository _templateObjectRepository;
        #endregion
        #region constructor
        public TemplateVariableService(IFieldVariableRepository fieldVariableRepository,ITemplateVariableRepository templateVariableRepository,ITemplateObjectRepository templateObjectRepository)
        {
            this._fieldVariableRepository = fieldVariableRepository;
            this._templateVariableRepository = templateVariableRepository;
            this._templateObjectRepository = templateObjectRepository;
        }
        #endregion

        #region public
        public bool UpdateTemplateVariablesList(long templateId,long companyId)
        {
            try
            {


                List<TemplateVariable> listTemplateVariables = _templateVariableRepository.getVariablesList(templateId);
                List<FieldVariable> listFieldVariables = _fieldVariableRepository.GetSystemAndCompanyVariables(companyId);
                List<TemplateObject> listTemplateObjects = _templateObjectRepository.GetProductObjects(templateId);
                List<TemplateVariable> lstTempVariablestoAdd = new List<TemplateVariable>();
                foreach (var obj in listFieldVariables)
                {
                    if (obj.VariableTag != null)
                    {
                        string lowerCaseVariable = obj.VariableTag.ToLower();
                        string upperCaseVariable = obj.VariableTag.ToUpper();
                        string variable = obj.VariableTag;

                        foreach (var templateObj in listTemplateObjects)
                        {
                            if (templateObj.ContentString.Contains(variable) || templateObj.ContentString.Contains(upperCaseVariable) || templateObj.ContentString.Contains(lowerCaseVariable))
                            {
                                var objVariable = listTemplateVariables.Where(g => g.VariableId == obj.VariableId).SingleOrDefault();
                                if (objVariable == null)
                                {
                                    TemplateVariable objNewVariable = new TemplateVariable(); 
                                    objNewVariable.VariableId = obj.VariableId;
                                    objNewVariable.TemplateId = templateId;
                                    lstTempVariablestoAdd.Add(objNewVariable);
                                }
                            }
                        }
                    }
                }
                if (lstTempVariablestoAdd != null && lstTempVariablestoAdd.Count != 0)
                {
                    _templateVariableRepository.InsertTemplateVariables(lstTempVariablestoAdd);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        #endregion
    }
}
