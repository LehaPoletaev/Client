using DesktopClient.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentFTP;
using System.IO;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;

namespace DesktopClient.ViewModels
{
    class MainViewModels : BaseViewModel
    {
        
        #region Authorization
        
        private string _Login { get; set; }
        public string Login
        {
            get => _Login;
            set
            {
                _Login = value;
                OnPropertyChanged("Login");
            }
        }

        private string _Password { get; set; }
        public string Password
        {
            get => _Password;
            set
            {
                _Password = value;
                OnPropertyChanged("Password");
            }
        }

        private DelegateCommand getToken;
        public ICommand Authorization
        {
            get
            {
                if (getToken == null)
                {
                    getToken = new DelegateCommand(Autorization);
                }
                return getToken;
            }
        }

        private void Autorization() {
            lg = Login;pw = Password;
            FtpClient ftp = new FtpClient("192.168.0.171", Login,Password);
            try
            {
                ftp.Port = 21;
                ftp.Connect();
               
                ftp.Disconnect();
            }
            catch(Exception e) { MessageBox.Show("Acces is denied"); }
        }
        #endregion

        #region Model
        private static string name,lg,pw;
        
        private string _folderName;

        public string FolderName
        {
            get { return _folderName; }
            set
            {
                _folderName = value;
                OnPropertyChanged("FolderName");
            }
        }

        private string _renameFile;
        
        public string renameFile
        {
            get { return _renameFile; }
            set
            {
               _renameFile = value;
                OnPropertyChanged("renameFile");
            }
        }

        private string _folder;

        public string Folder
        {
            get { return _folder; }
            set
            {
                _folder = value;
                OnPropertyChanged("Folder");
            }
        }

        private List<string> _remotePath;

        public List<string> remotePath
        {
            get => _remotePath;
            set 
            {
                _remotePath = value;
                OnPropertyChanged("remotePath");
            }
        }

        private List<string> _sourcePath;

        public List<string> sourcePath
        {
            get => _sourcePath;
            set
            {
                _sourcePath = value;
                OnPropertyChanged("sourcePath");
            }
        }

        private string _selectedPath;
        public string selectedPath
        {
            get => _selectedPath;
            set
            {
                _selectedPath = value;
                OnPropertyChanged("selectedPath");
            }
        }

        #endregion

        #region Command
        private DelegateCommand setftp;
        public ICommand Setftp
        {
            get
            {
                if (setftp == null)
                {
                    setftp = new DelegateCommand(FtpUpload);
                }
                return setftp;
            }
        }

        private DelegateCommand getftp;
        public ICommand Getftp
        {
            get
            {
                if (getftp == null)
                {
                    getftp = new DelegateCommand(FtpDownload);
                }
                return getftp;
            }
        }

        private DelegateCommand _rename;
        public ICommand Rename
        {
            get
            {
                if (_rename == null)
                {
                    _rename = new DelegateCommand(FtpRename);
                }
                return _rename;
            }
        }

        private DelegateCommand _delete;
        public ICommand Delete
        {
            get
            {
                if (_delete == null)
                {
                    _delete = new DelegateCommand(FtpDelete);
                }
                return _delete;
            }
        }

        private DelegateCommand opfld;
        public ICommand GetFile
        {
            get
            {
                if (opfld== null)
                {
                    opfld = new DelegateCommand(OpenFolderFile);
                }
                return opfld;
            }
        }

        private DelegateCommand of;
        public ICommand GetFolder
        {
            get
            {
                if (of == null)
                {
                    of = new DelegateCommand(OpenFolder);
                }
                return of;
            }
        }

        private DelegateCommand rp;
        public ICommand GetRemote
        {
            get
            {
                if (rp == null)
                {
                    rp = new DelegateCommand(GetRemotePath);
                }
                return rp;
            }
        }


        #endregion

        #region Method

        private void FtpUpload()
        {
            try
            {
                FtpClient ftp = new FtpClient("192.168.0.171", lg, pw);
                ftp.Port = 21;
                ftp.Connect();
               
                var str = selectedPath + "/" + name;
                ftp.UploadFile(FolderName, str);
                
                ftp.Disconnect();
                GetRemotePath();
                selectedPath = "";FolderName = "";
                MessageBox.Show("File uploaded successfully");
            }
            catch(Exception e) { MessageBox.Show("Access is denied"); }
        }
        private void FtpDownload()
        {
            try
            {
                FtpClient ftp = new FtpClient("192.168.0.171", lg, pw);
                ftp.Port = 21;
                ftp.Connect();
                
                ftp.DownloadFile(Folder + "/" + Path.GetFileName(selectedPath), selectedPath);
               
                ftp.Disconnect();
                GetRemotePath();
                selectedPath = "";Folder = "";
                MessageBox.Show("File downloaded successfully");
            }
            catch(Exception e) { MessageBox.Show("Access is denied"); }
        }
        private void FtpRename()
        {
            try
            {
                FtpClient ftp = new FtpClient("192.168.0.171",lg,pw);
                ftp.Port = 21;
                ftp.Connect();

                var res = Path.GetDirectoryName(selectedPath);
                ftp.Rename(selectedPath,res+"/"+renameFile);
                renameFile = "";
                
                ftp.Disconnect();
                GetRemotePath();
                MessageBox.Show("File renamed successfully");
            }
            catch(Exception e) { MessageBox.Show("Access is denied"); }
        }
        private void FtpDelete()
        {
            try
            {
                FtpClient ftp = new FtpClient("192.168.0.171", lg, pw);
                ftp.Port = 21;
                ftp.Connect();

                ftp.DeleteFile(selectedPath);

                ftp.Disconnect();
                GetRemotePath();
                MessageBox.Show("File deleted successfully");
            }
            catch (Exception e) { MessageBox.Show("Access is denied"); }
        }

        private void GetRemotePath()
        {
            FtpClient ftp = new FtpClient("192.168.0.171",lg,pw);
            ftp.Port = 21;
            ftp.Connect();
            List<string> tmp = new List<string>();
            List<string> all = new List<string>();
            foreach (var s in ftp.GetNameListing())
            {
                tmp.Add(s);
            }
            foreach(var s in tmp)
            {
                foreach (var k in ftp.GetNameListing(s))
                {
                    all.Add(k);
                } 
            }
            
            ftp.Disconnect();
            remotePath = tmp;
            sourcePath = all;
        }

        private void OpenFolderFile() 
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
                FolderName = openFileDialog.FileName;
            name = Path.GetFileName(FolderName);
                Console.WriteLine(name);
        }
        private void OpenFolder()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            Folder = dialog.FileName; 
            
        }

        #endregion 
    }
}
