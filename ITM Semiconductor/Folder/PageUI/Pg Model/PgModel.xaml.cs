using DTO;
using KeyPad;
using Mitsubishi;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
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
    /// Interaction logic for PgModel.xaml
    /// </summary>
    public partial class PgModel : Page
    {
       
        MyLogger logger = new MyLogger("PG_TeachingMenu");
        private int SelectModel = 1;
        public PgModel()
        {
            InitializeComponent();
            this.Loaded += PgModel_Loaded;
            this.btSelectModel1.Click += BtSelectModel1_Click;
            this.btSelectModel2.Click += BtSelectModel2_Click;
            this.btSelectModel3.Click += BtSelectModel3_Click;        
            this.BtnSaveData.Click += BtnSaveData_Click;
            this.BtnLoadModel.Click += BtnLoadModel_Click;
           
        }

        private void PgModel_Loaded(object sender, RoutedEventArgs e)
        {
            if (UiManager.appSettings.connection.model == "X2833")
            {
                SelectModel = 1;
                UiManager.appSettings.connection.modelName = UiManager.appSettings.connection.model1;
            }
            if (UiManager.appSettings.connection.model == "X2835")
            {
                SelectModel = 2;
                UiManager.appSettings.connection.modelName = UiManager.appSettings.connection.model2;
            }
            if (UiManager.appSettings.connection.model == "X2836")
            {
                SelectModel = 3;
                UiManager.appSettings.connection.modelName = UiManager.appSettings.connection.model3;

            }
            this.lblModelCurrent.Content = UiManager.appSettings.connection.modelName;
            this.lblModelNo.Content = SelectModel.ToString();
            this.lbSelectModel.Content = SelectModel.ToString();

            this.txbModel1.Text = UiManager.appSettings.connection.model1;
            this.txbModel2.Text = UiManager.appSettings.connection.model2;
            this.txbModel3.Text = UiManager.appSettings.connection.model3;

            UiManager.PLC.WriteWord(DeviceCode.D, 7500, SelectModel);
        }
     
        private void BtnLoadModel_Click(object sender, RoutedEventArgs e)
        {
            if (!new WndConfirm().DoComfirmYesNo("Confirm you want to change model ", System.Windows.Window.GetWindow(this)))
            {
                return;
            }

            int SelectModel = Convert.ToInt16(this.lbSelectModel.Content.ToString());
            UiManager.PLC.WriteWord(DeviceCode.D, 7500, SelectModel);
            UiManager.PLC.WriteBit(DeviceCode.M, 543, true);
            LoadedModel();
        }
      
        private void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {
            UiManager.appSettings.connection.model1 = this.txbModel1.Text.ToString();
            UiManager.appSettings.connection.model2 = this.txbModel2.Text.ToString();
            UiManager.appSettings.connection.model3 = this.txbModel3.Text.ToString();
            UiManager.SaveAppSetting();

            int SelectModel = Convert.ToInt16(this.lbSelectModel.Content.ToString());
            UiManager.PLC.WriteWord(DeviceCode.D, 7500, SelectModel);
            UiManager.PLC.WriteBit(DeviceCode.M, 542, true);
            this.SaveModel();
        }
        #region Button
      
        private void BtSelectModel3_Click(object sender, RoutedEventArgs e)
        {
            this.SelectModel = 3;
            this.lbSelectModel.Content = SelectModel.ToString();
        }
       
        private void BtSelectModel2_Click(object sender, RoutedEventArgs e)
        {
            this.SelectModel = 2;
            this.lbSelectModel.Content = SelectModel.ToString();
          
        }
       
        private void BtSelectModel1_Click(object sender, RoutedEventArgs e)
        {
            this.SelectModel = 1;
            this.lbSelectModel.Content = SelectModel.ToString();
          

        }
        #endregion
        private void SaveModel()
        {

            

        }
        private void LoadedModel()
        {
            if (this.SelectModel == 1)
            {
                UiManager.appSettings.connection.model = "X2833";
                UiManager.appSettings.connection.modelName = UiManager.appSettings.connection.model1;
            }
            if (this.SelectModel == 2)
            {
                UiManager.appSettings.connection.model = "X2835";
                UiManager.appSettings.connection.modelName = UiManager.appSettings.connection.model2;
            }
            if (this.SelectModel == 3)
            {
                UiManager.appSettings.connection.model = "X2836";
                UiManager.appSettings.connection.modelName = UiManager.appSettings.connection.model3;
            }
            this.lblModelCurrent.Content = UiManager.appSettings.connection.modelName;
            this.lblModelNo.Content = SelectModel.ToString();
            this.lbSelectModel.Content = SelectModel.ToString();

            this.txbModel1.Text = UiManager.appSettings.connection.model1;
            this.txbModel2.Text = UiManager.appSettings.connection.model2;
            this.txbModel3.Text = UiManager.appSettings.connection.model3;

            UiManager.SaveAppSetting();

            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.ReadWord(DeviceCode.D, 7500, out SelectModel);
            if (this.SelectModel == 1)
            {
                UiManager.appSettings.connection.model = "X2833";
                UiManager.appSettings.connection.modelName = UiManager.appSettings.connection.model1;
            }
            if (this.SelectModel == 2)
            {
                UiManager.appSettings.connection.model = "X2835";
                UiManager.appSettings.connection.modelName = UiManager.appSettings.connection.model2;
            }
            if (this.SelectModel == 3)
            {
                UiManager.appSettings.connection.model = "X2836";
                UiManager.appSettings.connection.modelName = UiManager.appSettings.connection.model3;
            }
            this.lblModelCurrent.Content = UiManager.appSettings.connection.modelName;
            this.lblModelNo.Content = SelectModel.ToString();
            this.lbSelectModel.Content = SelectModel.ToString();

            this.txbModel1.Text = UiManager.appSettings.connection.model1;
            this.txbModel2.Text = UiManager.appSettings.connection.model2;
            this.txbModel3.Text = UiManager.appSettings.connection.model3;

            UiManager.SaveAppSetting();
        }

    }
}
