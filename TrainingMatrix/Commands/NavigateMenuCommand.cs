using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Treningelo.ViewModels;

namespace Treningelo.Commands
{
    class NavigateMenuCommand : CommandBase
    {
        private UserControl view;

        public NavigateMenuCommand(UserControl viewToNavigateTo)
        {
            this.view = viewToNavigateTo;
        }

        public override void Execute(object parameter)
        {
            var cc = parameter as ContentControl;
            cc.Content = view;
        }
    }
}
