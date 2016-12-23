using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoveAssinaturaXml.Model
{
    public class ConfigFieldsAjust : MBase
    {             

        private bool cbNCM;

        public bool CbNCM
        {
            get { return cbNCM; }
            set { cbNCM = value; OnPropertyChanged("CbNCM"); }
        }
        private bool cbCEST;

        public bool CbCEST
        {
            get { return cbCEST; }
            set { cbCEST = value; OnPropertyChanged("CbCEST"); }
        }

        private bool rej384;

        public bool Rej384
        {
            get { return rej384; }
            set { rej384 = value; OnPropertyChanged("Rej394"); }
        }


    }
}
