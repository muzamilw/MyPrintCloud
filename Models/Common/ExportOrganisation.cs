using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
namespace MPC.Models.Common
{
    public class ExportOrganisation
    {
        public Organisation Organisation { get; set; }

        public List<PaperSize> PaperSizes { get; set; }

        public List<CostCentre> CostCentre { get; set; }
        
        // add costcentrechoic table in edmx
       // public List<CostCenterChoices> CostCentre { get; set; }

        public List<CostCentreQuestion> CostCentreQuestion { get; set; }


        public List<CostCentreAnswer> CostCentreAnswer { get; set; }

        public List<CostcentreInstruction> CostcentreInstruction { get; set; }

        public List<CostcentreResource> CostcentreResource { get; set; }
        public List<CostCentreMatrix> CostCentreMatrix { get; set; }
        public List<CostCentreMatrixDetail> CostCentreMatrixDetail { get; set; }

        public List<CostcentreWorkInstructionsChoice> CostcentreWorkInstructionsChoice { get; set; }

        public List<StockCategory> StockCategory { get; set; }
        public List<StockSubCategory> StockSubCategory { get; set; }

        public List<StockItem> StockItem { get; set; }

        public List<StockCostAndPrice> StockCostAndPrice { get; set; }

        // delivery carriers missing
        //public List<StockItem> StockItem { get; set; }


        // reports table add in edmx
        //public List<Reports> Reports { get; set; }

        // report notes table add in edmx
        //public List<Reports> Reports { get; set; }

        public List<Prefix> Prefixes { get; set; }

      


        public List<Machine> Machines { get; set; }

        public List<LookupMethod> LookupMethods { get; set; }

        public List<Phrase> Phrases { get; set; }

        public List<PhraseField> PhraseField { get; set; }

        public List<SectionFlag> SectionFlags { get; set; }




















        



        


    }
}
