using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    [Serializable]
    public sealed class ArtWorkAttatchment
    {
        private string _fileName;
        private string _folderPath;
        private string _fileTitle;
        private string _fileExtention;
        private UploadFileTypes _uploadFileType;

        public string FileName
        {
            get { return this._fileName; }
            set { this._fileName = value; }
        }

        public string FolderPath
        {
            get { return _folderPath; }
            set { _folderPath = value; }
        }

        public string FileTitle
        {
            get { return _fileTitle; }
            set { _fileTitle = value; }
        }

        public string FileExtention
        {
            get { return _fileExtention; }
            set { _fileExtention = value; }
        }

        public UploadFileTypes UploadFileType
        {
            get { return _uploadFileType; }
            set { _uploadFileType = value; }
        }

        public string ImageFileType
        {
            get;
            set;
        }
    }

}
