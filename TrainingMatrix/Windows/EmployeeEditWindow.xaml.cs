using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Treningelo.ViewModels;

namespace Treningelo.Windows
{
    /// <summary>
    /// Interaction logic for EmployeeEditWindow.xaml
    /// </summary>
    public partial class EmployeeEditWindow : Window
    {
        public EmployeeEditWindow(Employee employee)
        {
            this.DataContext = employee;
            this.Title = employee.Nev == string.Empty ? "Új dolgozó" : employee.Nev + " szerkesztése";
            this.Icon = employee.Nev == string.Empty ? new BitmapImage(new Uri("pack://application:,,,/Resources/new_ico.png")) : 
                                                       new BitmapImage(new Uri("pack://application:,,,/Resources/edit_ico.png"));
            employee.BeginEdit();
            InitializeComponent();
            tszBox.Focus();
        }
    }
}
