using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MigrationUtility
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            long OrganizationId = 1;
            string MPCContentBasePath = @"D:\GitHub\Usman\MyPrintCloud\MyPrintCloud\MPC.Web\MPC_Content\";

            string PinkCardsStoredImagesBasePath = @"E:\Development\MyPrintCloud\Bantex\Server\EPA.Web\StoredImages\";

            //D:\GitHub\Usman\MyPrintCloud\MyPrintCloud\MPC.Web\MPC_Content\Organisations\Organisation1\Organisation1_infinity-vehicle.jpg.jpeg
            //ensure directory created

            if (! Directory.Exists (MPCContentBasePath + "Organisations" ))
            {
                Directory.CreateDirectory(MPCContentBasePath + "Organisations");
            }


              if (! Directory.Exists (MPCContentBasePath + @"Organisations\Organisation" + OrganizationId.ToString() ))
            {
                Directory.CreateDirectory(MPCContentBasePath + @"Organisations\Organisation" + OrganizationId.ToString());
            }


            using (pinkcardsEntities PCContext = new pinkcardsEntities())
            {
                using (MPCPreviewEntities MPCContext = new MPCPreviewEntities())
                {


                    tbl_company PCCompany = PCContext.tbl_company.FirstOrDefault();


                Organisation MPCOrg = new Organisation();
                    MPCOrg.OrganisationName = PCCompany.CompanyName;
                MPCOrg.Address1 = PCCompany.Address1;
                MPCOrg.Address2 = PCCompany.Address2;
                MPCOrg.Address3 = PCCompany.Address3;
                MPCOrg.BleedAreaSize = 5;
                MPCOrg.City = PCCompany.City;
                    MPCOrg.StateId = PCCompany.State;

                MPCOrg.CmsSkinPageWidgets = null;///???

                    MPCOrg.CountryId = PCCompany.Country;
                    MPCOrg.ZipCode = PCCompany.ZipCode;
                    MPCOrg.CurrencyId = 1;
                    MPCOrg.CustomerAccountNumber = null;
                    MPCOrg.Email = PCCompany.Email;
                    MPCOrg.Fax = PCCompany.Fax;
                    MPCOrg.Tel = PCCompany.Tel;

                    MPCOrg.Mobile = PCCompany.Mobile;
                    MPCOrg.URL = PCCompany.URL;

                   

                    string logoname = Path.GetFileName(PCCompany.MISLogo);
                    File.Copy(PinkCardsStoredImagesBasePath + PCCompany.MISLogo, MPCContentBasePath + @"Organisations\Organisation\" + logoname);
                    MPCOrg.MISLogo = MPCContentBasePath + @"Organisations\Organisation\" + logoname;

                    MPCOrg.TaxRegistrationNo = PCCompany.TaxNo;
                    MPCOrg.VATRegNumber = PCCompany.TaxNo;
                    MPCOrg.SmtpPassword = "p@ssw0rd";
                    MPCOrg.SmtpServer = "smtp.sendgrid.net";
                    MPCOrg.SmtpUserName = "myprintcloud.com";

                    MPCOrg.SystemLengthUnit = 1;
                    MPCOrg.SystemWeightUnit = 2;
                    MPCOrg.CurrencyId = 1;
                    MPCOrg.LanguageId = 1;


                    MPCContext.Organisations.Add(MPCOrg);



                }
            }

           
         


        }
    }
}
