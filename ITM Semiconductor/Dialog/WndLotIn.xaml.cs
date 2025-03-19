using ITM_Semiconductor.Properties;
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
using DTO;
using static ITM_Semiconductor.MesSettings;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for WndLotIn.xaml
    /// </summary>
    public partial class WndLotIn : Window
    {
        private static MyLogger logger = new MyLogger("WndLotInput");

        private LotInData settings;
        public WndLotIn()
        {
            InitializeComponent();
            this.btCancel.Click += BtCancel_Click;
            this.btOk.Click += BtOk_Click;
            
        }

       
        private void BtOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserManager.createUserLog(UserActions.LOTIN_BUTTON_OK);
                WndConfirm confirmYesNo = new WndConfirm();
                if (confirmYesNo.DoComfirmYesNo("Do You Want Save Setting?"))
                {
                    settings.workGroup = txtWorkGroup.Text;
                    settings.LotId = txtLotId.Text;
                    settings.Config = txtDeviceId.Text;
                    settings.lotQty = int.Parse(txtLotQty.Text);
                }
                else
                {
                    this.settings = null;
                }
                this.Close();

            }
            catch (Exception ex)
            {
                logger.Create("BtOk_Click error:" + ex.Message,LogLevel.Error);
            }
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.LOTIN_BUTTON_CANCEL);

            this.settings = null;
            this.Close();
        }
        public LotInData DoSettings(Window owner, LotInData oldSettings)
        {
            UserManager.createUserLog(UserActions.LOTIN_SHOW);
            this.Owner = owner;
            settings = oldSettings.Clone();

            txtWorkGroup.Text = settings.workGroup;
            txtDeviceId.Text = settings.Config;
            txtLotId.Text = settings.LotId;
            if (settings.lotQty > 0)
            {
                txtLotQty.Text = settings.lotQty.ToString();
            }

            this.ShowDialog();
            return this.settings;
        }
    }
}
