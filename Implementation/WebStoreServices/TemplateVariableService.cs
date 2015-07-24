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
                List<TemplateVariableExtension> lstVaraibaleExtensions = new List<TemplateVariableExtension>();
                foreach (var obj in listFieldVariables)
                {
                    if (obj.VariableTag != null)
                    {
                        string lowerCaseVariable = obj.VariableTag.ToLower();
                        string upperCaseVariable = obj.VariableTag.ToUpper();
                        string variable = obj.VariableTag;

                        string variablePreFix ="{{"+ obj.VariableTag.Replace("{{","").Replace("}}","") + "_pre}}";
                        string variablePostFix ="{{"+ obj.VariableTag.Replace("{{","").Replace("}}","") + "_post}}";
                        
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
                            bool hasPreFix = false;
                            bool hasPostFix = false;
                            if (templateObj.ContentString.Contains(variablePreFix) || templateObj.ContentString.Contains(variablePreFix.ToUpper()) || templateObj.ContentString.Contains(variablePreFix.ToLower()))
                            {
                                hasPreFix = true;
                            }
                            if (templateObj.ContentString.Contains(variablePostFix) || templateObj.ContentString.Contains(variablePostFix.ToUpper()) || templateObj.ContentString.Contains(variablePostFix.ToLower()))
                            {
                                hasPostFix = true;
                            }
                            if (hasPreFix == true || hasPostFix == true)
                            {
                                TemplateVariableExtension objExt = new TemplateVariableExtension();
                                objExt.HasPrefix = hasPreFix;
                                objExt.HasPostFix = hasPostFix;
                                objExt.TemplateId = templateId;
                                objExt.FieldVariableId =obj.VariableId;
                                lstVaraibaleExtensions.Add(objExt);
                            }
                        }
                    }
                }

                _templateVariableRepository.InsertTemplateVariables(lstTempVariablestoAdd, lstVaraibaleExtensions);

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
