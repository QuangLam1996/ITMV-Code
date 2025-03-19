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

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for WndMessenger.xaml
    /// </summary>
    public partial class WndMessenger : Window
    {
        public WndMessenger()
        {
            InitializeComponent();
            this.btnYes.Click += BtnYes_Click;
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        public void MessengerShow(string message, Window owner = null)
        {
            this.Owner = owner;
            this.lblMessage.Content = message;

            this.ShowDialog();
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    
                    this.Close();
                    e.Handled = true;
                    break;
              
            }
            return;
            
        }
        private void Image_Mouseup(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
