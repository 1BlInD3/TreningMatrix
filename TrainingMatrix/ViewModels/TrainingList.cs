using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treningelo.ViewModels
{
    class TrainingList : ObservableCollection<Employee>
    {
        public bool IsNotEmpty => this.Count != 0;

        public TrainingList()
        {
            base.CollectionChanged += delegate { OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsNotEmpty))); };
        }
    }
}
