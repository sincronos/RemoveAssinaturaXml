using RemoveAssinaturaXml.Infraestrutura;
using RemoveAssinaturaXml.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;

namespace RemoveAssinaturaXml.ViewModel
{
    public class MainWindowViewModel : MBase
    {

        public RelayCommand LocalPathCommand { get; set; }
        public RelayCommand ProcessCommand { get; set; }

        public ObservableCollection<StatusModel> ListaStatus { get; set; } = new ObservableCollection<StatusModel>();


        private DateTime dtInicial;

        public DateTime DtInicial
        {
            get { return dtInicial; }
            set { dtInicial = value; OnPropertyChanged("DtInicial"); validData(); }
        }

        private DateTime dtFinal;

        public DateTime DtFinal
        {
            get { return dtFinal; }
            set { dtFinal = value; OnPropertyChanged("DtFinal"); validData(); }
        }


        private string pathXmls;

        public string PathXmls
        {
            get { return pathXmls; }
            set { pathXmls = value; OnPropertyChanged("PathXmls"); }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged("Status"); }
        }

        private ConfigFieldsAjust camposConfig;

        public ConfigFieldsAjust CamposConfig
        {
            get { return camposConfig; }
            set { camposConfig = value; OnPropertyChanged("CamposConfig"); }
        }



        /// <summary>
        /// Construtor
        /// </summary>
        public MainWindowViewModel()
        {
            LocalPathCommand = new RelayCommand(ExecuteSelectFolder);
            ProcessCommand = new RelayCommand(ExecuteProcessar, CanProcessar);
            //pathXmls = @"F:\Gerencial\Sistema\nfce_bak\Contingencia";
            DtInicial = new DateTime(2016, 4, 1);
            DtFinal = new DateTime(2016, 4, 10);
            Status = "Status";
            CamposConfig = new ConfigFieldsAjust();
        }
        private Boolean validData()
        {
            if (DtInicial <= dtFinal)
            {
                Status = "Status";
                return true;
            }
            else
            {
                Status = "********** DATA INICIAL NÃO PODE SER MAIOR DO QUE A DATA FINAL **********";
                return false;
            }
        }


        #region Commands

        private bool CanProcessar(object obj)
        {
            return !string.IsNullOrWhiteSpace(PathXmls) && (validData());
        }

        private void ExecuteProcessar(object obj)
        {            
                ListaStatus.Clear();

                DirectoryInfo path = new DirectoryInfo(pathXmls);
                FileInfo[] files = path.GetFiles("*.xml");


                foreach (FileInfo arq in files)
                {
                    var status = UtilXml.RemoveAssinatura(CamposConfig ,arq ,DtInicial, DtFinal);
                    ListaStatus.Add(status);
                }
             
        }
        
        private void ExecuteSelectFolder(object obj)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            PathXmls = fbd.SelectedPath.ToString();            
        }

        #endregion
    }
}
