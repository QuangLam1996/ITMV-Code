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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgAssignMenu.xaml
    /// </summary>
    public partial class PgAssignMenu : Page
    {
        public PgAssignMenu()
        {
            InitializeComponent();
            this.Loaded += PgAssignMenu_Loaded;
            this.BtnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm comfirmYesNo = new WndConfirm();
            if (!comfirmYesNo.DoComfirmYesNo(" Do you want to save ?")) return;
            UiManager.appSettings.SettingModel.NameMachine1 = this.txbNameMachine.Text;
            UiManager.SaveAppSetting();

        }

        private void PgAssignMenu_Loaded(object sender, RoutedEventArgs e)
        {
            this.txbNameMachine.Text = UiManager.appSettings.SettingModel.NameMachine1;
            
        }
    }
}
