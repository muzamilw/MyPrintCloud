using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.Common;

using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using MPC.Interfaces.Common;


namespace MPC.Interfaces.WebStoreServices
{
    public interface ICostCentreService
    {
        Organisation GetOrganisation(long costCentreId);

        object CompileBinaries(string sOutputPath, string Source, string CompanyName);

        List<CostCentre> GetDeliveryCostCentersList();

        List<CostCentre> GetCorporateDeliveryCostCentersList(long CompanyID);

        void CompileCostCentreTest();

        void SaveCostCentre(long _CostCentreID, long OrganisationId, string OrganisationName);

       // double ExecuteVariable(ref object[] oParamsArray, int VariableID);

       //// double ExecuteResource(ref object[] oParamsArray, long ResourceID, string ReturnValue);

       // double ExecuteUserStockItem(int StockID, StockPriceType StockPriceType, out double Price, out double PerQtyQty);
       // double ExecuteQuestion(ref object[] oParamsArray, int QuestionID, long CostCentreID);
       // double ExecuteMatrix(ref object[] oParamsArray, int MatrixID, long CostCentreID);

       CostCentre GetCostCentreByID(long CostCentreID);

       // CostCentre GetCostCentreSummaryByID(long CostCentreID);

       // CostCentre GetSystemCostCentre(long SystemTypeID, long OrganisationID);
       // string test();
       // CostCentre GetCostCentersByID(long costCenterID);
    }


    public class CostCentreLoaderFactory : MarshalByRefObject
    {


        private const BindingFlags bfi = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
        //public CostCentreLoaderFactory() {}'

        ///// <summary> Factory method to create an instance of the type whose name is specified,
        ///// using the named assembly file and the constructor that best matches the specified parameters. </summary>
        ///// <param name="assemblyFile"> The name of a file that contains an assembly where the type named typeName is sought. </param>
        ///// <param name="typeName"> The name of the preferred type. </param>
        ///// <param name="constructArgs"> An array of arguments that match in number, order, and type the parameters of the constructor to invoke, or null for default constructor. </param>
        ///// <returns> The return value is the created object represented as ILiveInterface. </returns>
        public ICostCentreLoader Create(string assemblyFile, string typeName, object[] constructArgs)
        {
            try
            {
                return (ICostCentreLoader)Activator.CreateInstanceFrom(assemblyFile, typeName, false, bfi, null, constructArgs, null, null, null).Unwrap();


                //return (ICostCentreLoader)Activator.CreateInstanceFrom(.Unwrap();
            }
            catch (Exception ex)
            {
                throw new Exception("CostCentreLoaderFactory.Create", ex);
            }
        }

        //Public Function Create(ByVal assemblyFile As String, ByVal typeName As String, ByVal constructArgs As Object()) As Object
        //    Try
        //        Return Activator.CreateInstanceFrom(assemblyFile, typeName, False, bfi, Nothing, constructArgs, Nothing, Nothing, Nothing).Unwrap()
        //    Catch ex As Exception
        //        Throw New Exception("CostCentreLoaderFactory.Create", ex)
        //    End Try
        //End Function

        public override object InitializeLifetimeService()
        {
            ILease lease = (ILease)base.InitializeLifetimeService();
            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.FromMinutes(10);
                lease.SponsorshipTimeout = TimeSpan.FromMinutes(5);
                lease.RenewOnCallTime = TimeSpan.FromSeconds(10);
            }
            return lease;
        }



    }
}
