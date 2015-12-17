using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MPC.Models.Common;
using System.Configuration;
using System.Net;
using System.Web;

namespace MPC.Implementation.MISServices
{
    public class ListingService : IListingService
    {
        
        private readonly IListingRepository listingRepository;
        private readonly ISystemUserRepository _SystemUserRepository;

        public ListingService(IListingRepository listingRepository,ISystemUserRepository _SystemUserRepository)
        {
            this.listingRepository = listingRepository;
            this._SystemUserRepository = _SystemUserRepository;
        }

        public RealEstateListViewResponse GetRealEstatePropertyCompaigns(RealEstateRequestModel request)
        {
            return listingRepository.GetRealEstatePropertyCompaigns(request);
        }

        public string SaveListingData()
        {
            string FTPServer = string.Empty;
                    string FTPUserName = string.Empty;
                    string FTPPassword = string.Empty;
                    string UnprocessedFileName = string.Empty;
            try
            {
                if (listingRepository.OrganisationId == 1)
                {
                    // Read the file as one string.
                    string XMLData = string.Empty;
                    string result = string.Empty;
                    
                    // WebClient request = new WebClient();
                    FTPServer = ConfigurationManager.AppSettings["FTPServer"];
                    FTPUserName = ConfigurationManager.AppSettings["FTPUserName"];
                    FTPPassword = ConfigurationManager.AppSettings["FTPPassword"];

                    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(FTPServer);
                    ftpRequest.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                    ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                    FtpWebResponse responsez = (FtpWebResponse)ftpRequest.GetResponse();
                    StreamReader streamReader = new StreamReader(responsez.GetResponseStream());
                    List<string> directories = new List<string>();

                    string line = streamReader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        directories.Add(line);
                        line = streamReader.ReadLine();
                    }
                    streamReader.Close();

                    using (WebClient ftpClient = new WebClient())
                    {
                        ftpClient.Credentials = new System.Net.NetworkCredential(FTPUserName, FTPPassword);

                        if (directories.Count > 0)
                        {
                            for (int i = 0; i <= directories.Count - 1; i++)
                            {
                                if (directories[i].Contains(".xml"))
                                {

                                    string path = FTPServer + directories[i].ToString();
                                    //string trnsfrpth = @"D:\\Test\" + directories[i].ToString();
                                    //string destinationDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Store/" + directories[i].ToString());

                                    byte[] newFileData = ftpClient.DownloadData(new Uri(path));
                                    XMLData = System.Text.Encoding.UTF8.GetString(newFileData);

                                    if (!string.IsNullOrEmpty(XMLData))
                                    {
                                        UnprocessedFileName = directories[i].ToString();
                                        break;
                                    }



                                }
                            }
                        }

                    }


                    // temp work
                    if (!string.IsNullOrEmpty(XMLData))
                    {

                        // System.IO.StreamReader myFile = new System.IO.StreamReader(filePaths[0]);

                        //  string fileName = Path.GetFileName(filePaths[0]);


                        string myString = XMLData;


                        ListingPropertyXML Result = new ListingPropertyXML();

                        XmlSerializer serializer = new XmlSerializer(typeof(ListingPropertyXML));
                        using (TextReader reader = new StringReader(myString))
                        {
                            Result = (ListingPropertyXML)serializer.Deserialize(reader);
                        }
                        if (Result != null)
                        {

                            if (myString.Contains("</land>"))
                            {
                                Result.Listing = Result.ListingLand;
                                Result.Listing.PropertyType = "Land";

                            }
                            else if (myString.Contains("</rental>"))
                            {
                                Result.Listing = Result.ListingRental;
                                Result.Listing.PropertyType = "Rental";
                            }
                            else
                            {
                                Result.Listing.PropertyType = "Resedential";
                            }
                            if (Result.Listing.Status != "sold")
                            {
                                result = InsertListingData(Result);
                                if (result == "Data processed successfully.")
                                {

                                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPServer + UnprocessedFileName);
                                    request.Method = WebRequestMethods.Ftp.DownloadFile;

                                    request.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                                    Stream responseStream = response.GetResponseStream();
                                    Upload(FTPServer + "ZunoProcessed/" + UnprocessedFileName, ToByteArray(responseStream), FTPUserName, FTPPassword);
                                    responseStream.Close();

                                    FtpWebRequest requestDelete = (FtpWebRequest)WebRequest.Create(FTPServer + UnprocessedFileName);
                                    requestDelete.Method = WebRequestMethods.Ftp.DeleteFile;
                                    requestDelete.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                                    FtpWebResponse responseDelete = (FtpWebResponse)requestDelete.GetResponse();



                                }
                            }
                            else
                            {
                                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPServer + UnprocessedFileName);
                                request.Method = WebRequestMethods.Ftp.DownloadFile;

                                request.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                                Stream responseStream = response.GetResponseStream();
                                Upload(FTPServer + "ZunoProcessed/" + UnprocessedFileName, ToByteArray(responseStream), FTPUserName, FTPPassword);
                                responseStream.Close();

                                FtpWebRequest requestDelete = (FtpWebRequest)WebRequest.Create(FTPServer + UnprocessedFileName);
                                requestDelete.Method = WebRequestMethods.Ftp.DeleteFile;
                                requestDelete.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                                FtpWebResponse responseDelete = (FtpWebResponse)requestDelete.GetResponse();
                            }

                        }
                    }
                    return result;
                }
                else
                {
                    return string.Empty;
                }
           
            }
            catch(Exception ex)
            {

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPServer + UnprocessedFileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                Upload(FTPServer + "ZunoInvalid/" + UnprocessedFileName, ToByteArray(responseStream), FTPUserName, FTPPassword);
                responseStream.Close();

                FtpWebRequest requestDelete = (FtpWebRequest)WebRequest.Create(FTPServer + UnprocessedFileName);
                requestDelete.Method = WebRequestMethods.Ftp.DeleteFile;
                requestDelete.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                
              //  FtpWebResponse responseDelete = (FtpWebResponse)requestDelete.GetResponse();

                using (FtpWebResponse responseDelete = (FtpWebResponse)request.GetResponse())
                {
                    string dd = responseDelete.StatusDescription;
                    throw ex;
                }   
               
            }
        }

        public static Byte[] ToByteArray(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            byte[] chunk = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(chunk, 0, chunk.Length)) > 0)
            {
                ms.Write(chunk, 0, bytesRead);
            }

            return ms.ToArray();
        }

        public static bool Upload(string FileName, byte[] Image, string FtpUsername, string FtpPassword)
        {
            try
            {
                System.Net.FtpWebRequest clsRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(FileName);
                clsRequest.Credentials = new System.Net.NetworkCredential(FtpUsername, FtpPassword);
                clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                System.IO.Stream clsStream = clsRequest.GetRequestStream();
                clsStream.Write(Image, 0, Image.Length);

                clsStream.Close();
                clsStream.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string InsertListingData(ListingPropertyXML objProperty)
        {
            long iContactCompanyID = 0;
            string dataError = string.Empty;
            bool dataProcessed = false;
            if(objProperty != null)
            {
                //if (objProperty.MpcLoginEmail == null)
                //{
                //    dataError = "Sorry,Invalid User";
                //    //  return dataError;
                //    if (objProperty.StoreCode == null)
                //    {
                //        dataError = "Store code is missing";
                //    }
                //    return dataError;
                //}
                //else
                //{
                long GetOrganisationID = 1;// 1682; //GetOrganisationIdByEmail(objProperty.MpcLoginEmail);
                    if (GetOrganisationID > 0)
                    {
                        iContactCompanyID = 32857; // GetContactCompanyID(objProperty.StoreCode, GetOrganisationID);
                    }

                    //int territoryId = GetDefaultTerritoryByContactCompanyID(objProperty.Listing.StoreCode);

                    if (iContactCompanyID == 0)
                    {
                        dataError = "Invalid Store code [" + objProperty.StoreCode + "]";
                        return dataError;
                    }
                    else
                    {
                        objProperty.Listing.CompanyId = Convert.ToString(iContactCompanyID);
                    }

                    MPC.Models.DomainModels.Listing listing = CheckListingForUpdate(objProperty.Listing.ClientListingId);
                    //here stratsss
                    if (listing != null) // update
                    {
                        dataProcessed = UpdateListingData(objProperty, listing,GetOrganisationID);
                    }
                    else
                    {
                        dataProcessed = AddListingData(objProperty, GetOrganisationID);
                    }

                    if (dataProcessed)
                        dataError = "Data processed successfully.";
                    else
                        dataError = "Error occurred while processing data.";

                //}
            }
            
            return dataError;
        }

        public long GetOrganisationIdByEmail(string SystemUserEmail)
        {
            return _SystemUserRepository.OrganisationThroughSystemUserEmail(SystemUserEmail);
        }

        private long GetContactCompanyID(string sStoreCode, long OrganistaionID)
        {
            try
            {
                return listingRepository.GetContactCompanyIDByStoreCode(sStoreCode, OrganistaionID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private MPC.Models.DomainModels.Listing CheckListingForUpdate(string clientListingID)
        {
            try
            {
                return listingRepository.CheckListingForUpdate(clientListingID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool UpdateListingData(ListingPropertyXML objProperty, MPC.Models.DomainModels.Listing listing,long OrganisationId)
        {
            try
            {
                bool dataAdded = false;



                dataAdded = listingRepository.UpdateListingXMLData(objProperty, listing,OrganisationId);

                return dataAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool AddListingData(ListingPropertyXML objProperty,long OrganisationId)
        {
            try
            {
                bool dataAdded = false;



                dataAdded = listingRepository.AddListingDataXML(objProperty, OrganisationId);

                return dataAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public void SubmitListingData(long OrganisationId)
        //{
            
        //    if (OrganisationId > 0)
        //    {
        //        Schedule(() => SaveListingData(OrganisationId))
        //           .ToRunNow().AndEvery(2).Minutes();
        //    }
        //}
    }
}
