using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FileSystemWatcher
{
    public partial class Form1 : Form
    {
         bool m_bIsWatching = false;
         private System.IO.FileSystemWatcher m_Watcher;
         List<string> AllFiles = new List<string>();
        private long newFolder = 0;
        private string sPath = @"D:\MPC Imp\December2016";
        private long OrganisationId { get; set; }
        private long StoreId { get; set; }

        private string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPClive;server=54.253.120.28,8999;user id=mpcaussa; password=p@$$w0rd@m!s2015;";
       // private string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPClive;server=192.168.1.22\\MSSQLSERVER2012;user id=sa; password=p@ssw0rd;";
        public Form1()
        {
            InitializeComponent();
            m_bIsWatching = false;
           // InsertDataToDam("", true);
            
           // AddedFolders = new List<FilesFolders>();
            //ParsePath(sPath, 0);
        }

        private void btnWatchFile_Click(object sender, EventArgs e)
        {
            if (m_bIsWatching)
            {
                m_bIsWatching = false;
                m_Watcher.EnableRaisingEvents = false;
                m_Watcher.Dispose();
                btnWatchFile.BackColor = Color.LightSkyBlue;
                btnWatchFile.Text = "Start Watching";

            }
            else
            {
                m_bIsWatching = true;
                btnWatchFile.BackColor = Color.Red;
                btnWatchFile.Text = "Stop Watching";

                m_Watcher = new System.IO.FileSystemWatcher();
                m_Watcher.Path = @"D:\MPC Imp\December2016";

                m_Watcher.IncludeSubdirectories = true;

                m_Watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                m_Watcher.Changed += new FileSystemEventHandler(OnChanged);
                m_Watcher.Created += new FileSystemEventHandler(OnChanged);
                m_Watcher.Deleted += new FileSystemEventHandler(OnChanged);
               
                m_Watcher.EnableRaisingEvents = true;
            }
        }
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            //if (!m_bDirty)
            //{
            //    m_Sb.Remove(0, m_Sb.Length);
            //    m_Sb.Append(e.FullPath);
            //    m_Sb.Append(" ");
            //    m_Sb.Append(e.ChangeType.ToString());
            //    m_Sb.Append("    ");
            //    m_Sb.Append(DateTime.Now.ToString());
            //    m_bDirty = true;
            //}
            InsertDataToDam(e.FullPath, e.ChangeType == WatcherChangeTypes.Created);
        }

        private void InsertDataToDam(string filePath, bool isCreated)
        {
            
            
            {
                if (isCreated)
                {
                    string sPath =  @"D:\MPC Imp\December2016";
                    string[] entries = System.IO.Directory.GetFileSystemEntries(sPath, "*", System.IO.SearchOption.AllDirectories);
                   
                    string[] SubDirs = System.IO.Directory.GetDirectories(sPath);
                    AllFiles.AddRange(SubDirs);
                    AllFiles.AddRange(System.IO.Directory.GetFiles(sPath));
                    foreach (string subdir in SubDirs)
                    {
                        string ssl = subdir;
                    }





                    //string folderName = string.Empty;
                    //string sParentFolder = string.Empty;
                    string queryString = string.Empty;
                    //DateTime currentTime = DateTime.Now;
                    //foreach (var entry in entries)
                    //{
                    //    bool isDirectory = Path.GetExtension(entry) == string.Empty;
                    //    if (isDirectory)
                    //    {
                    //        string[] SubDirs = System.IO.Directory.GetDirectories(entry);

                    //        long parentId = GetParentFolderId(sParentFolder);
                    //        if (parentId > 0)
                    //        {
                    //            folderName = entry.Substring((entry.LastIndexOf('\\') + 1));
                    //        }

                            
                    //        queryString = "INSERT INTO Folder (FolderName,Description,DisplayOrder,ParentFolderId,CompanyId, OrganisationId) VALUES('" + folderName + "','" + folderName + "',1,0,32857,1)";
                    //    }
                    //    else
                    //    {
                    //        folderName = Path.GetFileName(filePath);
                    //        queryString = "INSERT INTO Assets (AssetName,Description,ImagePath,CreationDateTime,Price, Quantity,FolderId,CompanyId) VALUES('" + folderName + "','" + folderName + "','" + filePath + "','" + currentTime.ToShortDateString() + "',0,0,40031,32857)";
                    //    }
                    //}
                    //bool isDirectory = Path.GetExtension(filePath) == string.Empty;
                    

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Create the Command and Parameter objects.
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.CommandTimeout = 500;

                        try
                        {
                            connection.Open();
                            

                            var result = command.ExecuteNonQuery();

                            //command.CommandText = "INSERT INTO [SystemUser] ([SystemUserId],[UserName],[OrganizationId],[FullName],[RoleId],[CostPerHour],[IsSystemUser],[Email])";
                            //command.CommandText += " values ('" + ID + "','" + username + "'," + siteOrganisationId + ",'" + ContactFullName + "','1',0,0,'" + Email + "')";


                            //result = command.ExecuteNonQuery();


                            connection.Close();

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                }
                
                
            }
        }

        private readonly string[] _validExtensions = { ".jpg", ".bmp", ".gif", ".png", ",jpeg" }; //  etc

        public bool IsImageExtension(string ext)
        {
            return _validExtensions.Contains(ext);
        }



        void ParsePath(string path, long parent)
        {
            AllFiles = new List<string>();
            string[] SubDirs = System.IO.Directory.GetDirectories(path);
            //AllFiles.AddRange(SubDirs);
            AllFiles.AddRange(System.IO.Directory.GetFiles(path));
            string sQry = string.Empty;
            string folderName = string.Empty;
            long parentFolderId = 0;

            string description = "";
            string keywords = "";
            string sFileName = "";
            string sFilePath = "";
            string sFileFolder = "";
            string sThumbPath = "";
            string newPathname = "";
            string newPathAbsName = "";
            string qryAssetItem = "";

            foreach (var file in AllFiles)
            {

                bool isDirectory = System.IO.Path.GetExtension(file) == string.Empty;
                if (isDirectory)
                {
                   //
                }
                else
                {
                    if (parent > 0)
                    {
                        folderName = System.IO.Path.GetFileNameWithoutExtension(file);
                        sFileName = System.IO.Path.GetFileName(file);
                        sFilePath = file.Replace(sPath, "/mpc_content/DigitalAssets/" + OrganisationId + "/" + StoreId + "/tfedamdata");
                        sFilePath = sFilePath.Replace("\\","/");
                        sFileFolder = System.IO.Path.GetDirectoryName(file);
                        if (folderName != null)
                        {

                            folderName = specialCharactersEncoder(folderName);

                            //folderName = folderName.Replace(";", "-");
                            //folderName = folderName.Replace("'", "''");
                        }
                        sFilePath = sFilePath.Replace("'", "''");
                        sThumbPath = sFilePath.Replace(sFileName, folderName + "_thumb.jpg");

                        description = specialCharactersEncoder(sFileName);
                        keywords = specialCharactersEncoder(sFileName);

                       

                        if (!sFilePath.Contains("_thumb"))
                        {
                            sQry = "INSERT INTO Assets (AssetName,ImagePath,CreationDateTime,Price, Quantity,FolderId,CompanyId, Description, keywords) VALUES('" + folderName + "','" + sThumbPath + "','" + DateTime.Now.ToShortDateString() + "',0,0," + parent + "," + StoreId + ",'"+ description + "','" + keywords + "')";
                            var assetId = InsertQry(sQry);

                            //shortening the file name and renaming the file an updating in db
                            newPathname = "/mpc_content/DigitalAssets/" + OrganisationId + "/" + StoreId + "/tfedamdata/" + sFileName.Replace(System.IO.Path.GetFileName(sFileName), "img" + assetId.ToString() + System.IO.Path.GetExtension(file));
                            newPathAbsName = sFileFolder + "\\img" + assetId.ToString() + System.IO.Path.GetExtension(file);
                            Delimon.Win32.IO.File.Move(file, file.Replace(System.IO.Path.GetFileName(file), "img" + assetId.ToString() + System.IO.Path.GetExtension(file)));
                            UpdateQry("update Assets set ImagePath='" + newPathname + "' where AssetId=" + assetId);


                            txtStatus.Text += "Asset " + folderName + " is added." + Environment.NewLine;
                            qryAssetItem = "INSERT INTO AssetItem (AssetId,FileUrl) VALUES(" + assetId + ",'" + newPathname + "')";
                            InsertQry(qryAssetItem);
                            txtStatus.Text += "Asset Attachment for " + folderName + " is added." + Environment.NewLine;

                            //if (IsImageExtension(System.IO.Path.GetExtension(newPathAbsName)))
                            //    CreateThumbnail(300, newPathAbsName, newPathAbsName);
                        }
                        
                    }
                    
                }
                Application.DoEvents();
            }
            foreach (string subdir in SubDirs)
            {
                
                parentFolderId = parent;
                folderName = subdir.Substring((subdir.LastIndexOf('\\') + 1));
                folderName = folderName.Replace("'", "''");
                sQry = "INSERT INTO Folder (FolderName,Description,DisplayOrder,ParentFolderId,CompanyId, OrganisationId) VALUES('" + folderName + "','" + folderName + "',1," + parentFolderId + ","+StoreId+","+OrganisationId+")";
                var folderId = InsertQry(sQry);
                txtStatus.Text += "Folder " + folderName + " is added." + Environment.NewLine;
                //AddedFolders.Add(new FilesFolders { FolderId = folderId, FolderName = folderName, ParentFolderId = parentFolderId });
                ParsePath(subdir, folderId);
            }
                
        }

        private long InsertQry(string qry)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                qry += " SELECT CAST(scope_identity() AS int)";
                SqlCommand command = new SqlCommand(qry, connection);
                command.CommandTimeout = 500;

                try
                {
                    if(connection.State != ConnectionState.Open)
                        connection.Open();

                    var result = command.ExecuteScalar();

                    //command.CommandText = "INSERT INTO [SystemUser] ([SystemUserId],[UserName],[OrganizationId],[FullName],[RoleId],[CostPerHour],[IsSystemUser],[Email])";
                    //command.CommandText += " values ('" + ID + "','" + username + "'," + siteOrganisationId + ",'" + ContactFullName + "','1',0,0,'" + Email + "')";


                    //result = command.ExecuteNonQuery();
                    command = null;

                    connection.Close();
                    
                    return Convert.ToInt64(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        private void UpdateQry(string qry)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
               
                SqlCommand command = new SqlCommand(qry, connection);
                command.CommandTimeout = 500;

                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    command.ExecuteScalar();

                    //command.CommandText = "INSERT INTO [SystemUser] ([SystemUserId],[UserName],[OrganizationId],[FullName],[RoleId],[CostPerHour],[IsSystemUser],[Email])";
                    //command.CommandText += " values ('" + ID + "','" + username + "'," + siteOrganisationId + ",'" + ContactFullName + "','1',0,0,'" + Email + "')";


                    //result = command.ExecuteNonQuery();


                    connection.Close();
               
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        private void btnImportDam_Click(object sender, EventArgs e)
        {
            if (txtRootPath.Text.Length > 0 && txtOrg.Text.Length > 0 && txtStore.Text.Length > 0)
            {
                sPath = txtRootPath.Text;
                txtStatus.Text = "";
                OrganisationId = Convert.ToInt64(txtOrg.Text);
                StoreId = Convert.ToInt64(txtStore.Text);
                ParsePath(txtRootPath.Text, 0);
            }
            else
            {
                MessageBox.Show("Please enter Root Path, Organisation Id and Store Id");
            }
            
        }

        private void btnGenerateThumbnails_Click(object sender, EventArgs e)
        {
            string[] entries = System.IO.Directory.GetFileSystemEntries(txtRootPath.Text, "*", System.IO.SearchOption.AllDirectories);
            foreach (var entry in entries)
            {
                bool isDirectory = System.IO.Path.GetExtension(entry) == string.Empty;
                if (!isDirectory)
                {
                    if (System.IO.File.Exists(entry))
                    {
                        if(IsImageExtension(System.IO.Path.GetExtension(entry)))
                            CreateThumbnail(300, entry, entry);
                    }
                }
                Application.DoEvents();
            }
            MessageBox.Show("Thumbnails under current folder are created.");
        }

        
        private void CreateThumbnail(int ThumbnailMax, string OriginalImagePath, string ThumbnailImagePath)
        {
            // Loads original image from file
            try
            {
                string sFileName = System.IO.Path.GetFileName(ThumbnailImagePath);
                string sFileNamewoExt = System.IO.Path.GetFileNameWithoutExtension(ThumbnailImagePath);
                string folderPath = ThumbnailImagePath.Substring(0, ThumbnailImagePath.Length - sFileName.Length);
                ThumbnailImagePath = folderPath + sFileNamewoExt + "_thumb.jpg";

                if (System.IO.File.Exists(ThumbnailImagePath) == false)
                {

                    Image imgOriginal = Image.FromFile(OriginalImagePath);
                    // Finds height and width of original image
                    float OriginalHeight = imgOriginal.Height;
                    float OriginalWidth = imgOriginal.Width;
                    // Finds height and width of resized image
                    int ThumbnailWidth;
                    int ThumbnailHeight;
                    if (OriginalHeight > OriginalWidth)
                    {
                        ThumbnailHeight = ThumbnailMax;
                        ThumbnailWidth = (int)((OriginalWidth / OriginalHeight) * (float)ThumbnailMax);
                    }
                    else
                    {
                        ThumbnailWidth = ThumbnailMax;
                        ThumbnailHeight = (int)((OriginalHeight / OriginalWidth) * (float)ThumbnailMax);
                    }
                    // Create new bitmap that will be used for thumbnail
                    Bitmap ThumbnailBitmap = new Bitmap(ThumbnailWidth, ThumbnailHeight);
                    Graphics ResizedImage = Graphics.FromImage(ThumbnailBitmap);
                    // Resized image will have best possible quality
                    ResizedImage.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    ResizedImage.CompositingQuality = CompositingQuality.HighQuality;
                    ResizedImage.SmoothingMode = SmoothingMode.HighQuality;
                    // Draw resized image
                    ResizedImage.DrawImage(imgOriginal, 0, 0, ThumbnailWidth, ThumbnailHeight);
                    // Save thumbnail to file
                    ThumbnailBitmap.Save(ThumbnailImagePath);
                    ThumbnailBitmap.Dispose();
                    ResizedImage.Dispose();
                    imgOriginal.Dispose();
                    txtStatus.Text += "Thumb Generated for " + OriginalImagePath + Environment.NewLine;
                }
                else
                {
                    txtStatus.Text += "Thumb skipped for " + OriginalImagePath + Environment.NewLine;
                }

            }
            catch (Exception e) 
            {

                txtStatus.Text += "Thumb gen crashed for " + OriginalImagePath + Environment.NewLine;
            }
            
        }

        private void cmdRenameFiles_Click(object sender, EventArgs e)
        {
            string[] entries = System.IO.Directory.GetFileSystemEntries(txtRootPath.Text, "*", System.IO.SearchOption.AllDirectories);
            foreach (var entry in entries)
            {
                bool isDirectory = System.IO.Path.GetExtension(entry) == string.Empty;
                if (!isDirectory)
                {
                    if (System.IO.File.Exists(entry))
                    {
                        string sFileName = System.IO.Path.GetFileName(entry);
                        string newFileName = sFileName.Replace(";", "_");
                        newFileName = newFileName.Replace(" ", "_");
                        string folderPath = entry.Substring(0, entry.Length - sFileName.Length);
                        System.IO.File.Move(entry, folderPath + newFileName);
                    }
                }
            }
            MessageBox.Show("Files under current folder are renamed.");
        }

        public string specialCharactersEncoder(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Replace("/", "");
                value = value.Replace(" ", "_");
                value = value.Replace(";", "_");
                value = value.Replace("&#34;", "");
                value = value.Replace("'", "");
                value = value.Replace("&", "");
                value = value.Replace("+", "");
                value = value.Replace("#", "");
                value = value.Replace("'", "''");
            }

            return value;
        }

        private void btnShortenFilenames_Click(object sender, EventArgs e)
        {

            int count = 0;
            int countTotal = 0;
            string qry = "";
            using (SqlConnection connection2 = new SqlConnection(connectionString))
            {

                connection2.Open();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create the Command and Parameter objects.
                    qry += "SELECT AssetId,ImagePath from assets";
                    SqlCommand command = new SqlCommand(qry, connection);
                    command.CommandTimeout = 500;

                    SqlCommand command2 = new SqlCommand(qry, connection2);
                    command.CommandTimeout = 500;

                    SqlDataReader rdr = null;

                    try
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        rdr = command.ExecuteReader();

                        while (rdr.Read())
                        {
                            long assetId = (long)rdr["AssetId"];
                            string ImagePath = (string)rdr["ImagePath"];
                            string OrigImagePath = (string)rdr["ImagePath"];
                            if (ImagePath.Length > 100)
                            {
                                ImagePath = @"D:\wwwRoot\australia.myprintcloud.com\mis" + ImagePath.Replace('/', System.IO.Path.DirectorySeparatorChar);
                                //txtStatus.Text +=ImagePath +  " XXXXXXX " + Path.GetFileName(ImagePath) +   Environment.NewLine; 

                                if (Delimon.Win32.IO.File.Exists(ImagePath))
                                {
                                    count++;
                                    countTotal++;


                                   // txtStatus.Text = count.ToString();


                                    string newPathname = OrigImagePath.Replace(System.IO.Path.GetFileName(ImagePath), "img" + assetId.ToString() + System.IO.Path.GetExtension(ImagePath));
                                    txtStatus.Text += assetId +  newPathname + Environment.NewLine;


                                    Delimon.Win32.IO.File.Move(ImagePath, ImagePath.Replace(System.IO.Path.GetFileName(ImagePath), "img" + assetId.ToString() + System.IO.Path.GetExtension(ImagePath)));

                                    string sourcethumb = ImagePath.Replace(System.IO.Path.GetFileName(ImagePath), System.IO.Path.GetFileNameWithoutExtension(ImagePath) + "_thumb.jpg" );
                                    //txtStatus.Text += sourcethumb + Environment.NewLine;

                                    string targetthumb = ImagePath.Replace(System.IO.Path.GetFileName(ImagePath), "img" + assetId.ToString() + "_thumb.jpg" );
                                    
                                    //txtStatus.Text += targetthumb + Environment.NewLine;

                                    if (Delimon.Win32.IO.File.Exists(sourcethumb))
                                        Delimon.Win32.IO.File.Move(sourcethumb, targetthumb);

                                    command2.CommandText = "update Assets set ImagePath='" + newPathname + "' where assetid =" + assetId.ToString();
                                    command2.ExecuteNonQuery();

                                }
                                else
                                {
                                    txtStatus.Text += "File Could not be found " + ImagePath + Environment.NewLine;
                                    //MessageBox.Show("File Could not be found " + ImagePath);
                                }


                                
                            }



                            Application.DoEvents();

                        }


                      

                        MessageBox.Show(count.ToString() + "-------" + countTotal.ToString());

                        
                        //command.CommandText += " values ('" + ID + "','" + username + "'," + siteOrganisationId + ",'" + ContactFullName + "','1',0,0,'" + Email + "')";


                        //result = command.ExecuteNonQuery();




                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        rdr.Close();
                        connection.Close();
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {



            
            int count = 0;
            int countTotal = 0;
            string qry = "";
            using (SqlConnection connection2 = new SqlConnection(connectionString))
            {

                connection2.Open();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create the Command and Parameter objects.
                    qry += "SELECT AssetId,ImagePath from assets where CompanyId = 33474";
                    SqlCommand command = new SqlCommand(qry, connection);
                    command.CommandTimeout = 500;

                    SqlCommand command2 = new SqlCommand(qry, connection2);
                    command.CommandTimeout = 500;

                    SqlDataReader rdr = null;

                    try
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        rdr = command.ExecuteReader();

                        while (rdr.Read())
                        {
                            long assetId = (long)rdr["AssetId"];
                            string ImagePath = (string)rdr["ImagePath"];
                            string OrigImagePath = (string)rdr["ImagePath"];
                           
                                ImagePath = @"D:\wwwRoot\australia.myprintcloud.com\mis" + ImagePath.Replace('/', System.IO.Path.DirectorySeparatorChar);
                                //txtStatus.Text +=ImagePath +  " XXXXXXX " + Path.GetFileName(ImagePath) +   Environment.NewLine; 

                                if (System.IO.File.Exists(ImagePath))
                                {
                                   
                                    var fileinfo = new CFileInfo(ImagePath);
                                    //txtStatus.Text += assetId + fileinfo.FileTags + Environment.NewLine;
                                    command2.CommandText = "update Assets set Keywords='" +  fileinfo.FileTags.Replace("'","''") + "' where assetid =" + assetId.ToString();
                                    command2.ExecuteNonQuery();


                                }
                                else
                                {
                                    txtStatus.Text += "File Could not be found " + ImagePath + Environment.NewLine;
                                    //MessageBox.Show("File Could not be found " + ImagePath);
                                }



                            count++;

                           

                            Application.DoEvents();

                        }


                        txtStatus.Text = "complete";

                        MessageBox.Show(count.ToString() + "-------" + countTotal.ToString());


                        //command.CommandText += " values ('" + ID + "','" + username + "'," + siteOrganisationId + ",'" + ContactFullName + "','1',0,0,'" + Email + "')";


                        //result = command.ExecuteNonQuery();




                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        rdr.Close();
                        connection.Close();
                    }

                }
            }
        }
    }

    
}
