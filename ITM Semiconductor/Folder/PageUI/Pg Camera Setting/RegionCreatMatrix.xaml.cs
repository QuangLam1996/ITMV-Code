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
    public partial class RegionCreatMatrix : Window
    {
        public MatrixData matrix = new MatrixData();
        public RegionCreatMatrix()
        {
            InitializeComponent();
            this.btnOK.Click += BtnOK_Click;
            this.btnCancle.Click += BtnCancle_Click;
        }

        private void BtnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {

            matrix.Row = (int)xctRow.Value;
            matrix.Colum = (int)xctColum.Value;
            matrix.RowPitch = (int)xctRowP.Value;
            matrix.ColumPitch = (int)xctColumP.Value;
            this.Close();
        }

        public MatrixData DoConfirmMatrix(Point p, Window owner = null)
        {
            //UserManager.createUserLog(UserActions.CONFIRM_SHOW_YESNO);
            this.Owner = owner;
            this.Left = p.X;
            this.Top = p.Y;
            this.ShowDialog();
            return matrix;
        }

        public class MatrixData
        {
            public int Row { get; set; }
            public int RowPitch { get; set; }
            public int Colum { get; set; }
            public int ColumPitch { get; set; }

            public MatrixData()
            {

            }
        }
    }
}
