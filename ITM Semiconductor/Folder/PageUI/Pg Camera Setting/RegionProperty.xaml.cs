using ITM_Semiconductor;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VisionInspection;

namespace AutoLaserCuttingInput
{
    /// <summary>
    /// Interaction logic for RegionCreatMatrix.xaml
    /// </summary>
    public partial class RegionProperty : Window
    {
        public ROIProperty property = UiManager.appSettings.Property;

        public RegionProperty()
        {
            InitializeComponent();
            this.btnOK.Click += BtnOK_Click;
            this.btnCancle.Click += BtnCancle_Click;
            this.Loaded += RegionProperty_Loaded;
        }

        private void RegionProperty_Loaded(object sender, RoutedEventArgs e)
        {
            xctStrokeThickness.Value = UiManager.appSettings.Property.StrokeThickness;
            xctCursorsRectSize.Value = UiManager.appSettings.Property.rectSize.Width;
            xctlabelFontSize.Value = UiManager.appSettings.Property.labelFontSize;
        }

        private void BtnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {

            property.StrokeThickness = (int)xctStrokeThickness.Value;
            property.labelFontSize = (int)xctlabelFontSize.Value;
            property.rectSize = new OpenCvSharp.Size( (int)xctCursorsRectSize.Value, (int)xctCursorsRectSize.Value) ;
            UiManager.SaveAppSetting();
            this.Close();
        }

        public bool DoConfirmMatrix(Point p, Window owner = null)
        {
            //UserManager.createUserLog(UserActions.CONFIRM_SHOW_YESNO);
            this.Owner = owner;
            this.Left = p.X;
            this.Top = p.Y;
            this.ShowDialog();
            return true;
        }

       
    }
}
