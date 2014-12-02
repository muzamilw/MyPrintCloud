namespace MPC.MIS.ModelMappers
{
    public static class CompanyTypeMapper
    {
        #region public
        public static Models.CompanyType CreateFrom(this MPC.Models.DomainModels.CompanyType source)
        {
            return new Models.CompanyType
            {
               TypeId = source.TypeId,
               IsFixed = source.IsFixed,
               TypeName = source.TypeName
            };
        }
        #endregion
    }
}