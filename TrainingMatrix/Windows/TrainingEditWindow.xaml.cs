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
    /// Interaction logic for TrainingEditWindow.xaml
    /// </summary>
    public partial class TrainingEditWindow : Window
    {
        public TrainingEditWindow(Training training)
        {
            this.DataContext = training;
            this.Title = training.ID == 0 ? "Új tréning" : "Tréning szerkesztése";
            this.Icon = training.ID == 0 ? new BitmapImage(new Uri("pack://application:,,,/Resources/new_ico.png")) :
                                           new BitmapImage(new Uri("pack://application:,,,/Resources/edit_ico.png"));
            training.BeginEdit();
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
