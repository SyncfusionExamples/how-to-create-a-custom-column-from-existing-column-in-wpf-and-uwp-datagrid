using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfTestingSample
{
    class ViewModel:INotifyPropertyChanged
    {
        EmployeeDetails emp = new EmployeeDetails();

        #region Constructor

        public ViewModel()
        {
            this.GDCSource = emp;
        }

        #endregion

        private ObservableCollection<BusinessObjects> gdcsource;
        public ObservableCollection<BusinessObjects> GDCSource
        {
            get
            {
                return gdcsource;
            }
            set
            {
                gdcsource = value;
                OnPropertyChanged("GDCSource");
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

    #region GDCSource DataSource
    class EmployeeDetails : ObservableCollection<BusinessObjects>
    {
        Random rand = new Random();
        public EmployeeDetails()
        {
            PopulateCollection();
        }

        

        private void PopulateCollection()
        {
            this.Clear();

            for (int i = 0; i < 2; i++)
            {                                
                BusinessObjects b = new BusinessObjects() { EmployeeDate1 = DateTimeOffset.Now };
                this.Add(b);
                BusinessObjects b1 = new BusinessObjects() { EmployeeDate1= DateTimeOffset.Now };
                this.Add(b1);                       
            }

        }
    }

    #endregion
}
