//using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfTestingSample
{
    class BusinessObjects : INotifyPropertyChanged
    {      
        private DateTimeOffset _edatetime1;
        public DateTimeOffset EmployeeDate1
        {
            get
            {
                return _edatetime1;
            }
            set
            {
                _edatetime1 = value;
                OnPropertyChanged("EmployeeDate1");
            }
        }      

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
