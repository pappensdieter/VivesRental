using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VivesRental.Model;
using VivesRental.Services;
using VivesRental.Views;

namespace VivesRental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RentalOrderService rentalOrderService;
        private RentalOrderLineService rentalOrderLineService;

        public MainWindow()
        {
            rentalOrderService = new RentalOrderService();
            rentalOrderLineService = new RentalOrderLineService();

            InitializeComponent();
        }

        // shows item management page
        private void ShowItemMangement(object sender, RoutedEventArgs e)
        {
            Window window = new ItemManagementView();
            window.ShowDialog();
        }

        // shows rental orders page
        private void ShowRentalOrders(object sender, RoutedEventArgs e)
        {
            Window window = new RentalOrdersView();
            window.ShowDialog();
        }

        // shows new rental page
        private void ShowNewRental(object sender, RoutedEventArgs e)
        {
            Window window = new NewRentalView();
            window.ShowDialog();
        }

        // shows user management page
        private void ShowUserManagement(object sender, RoutedEventArgs e)
        {
            Window window = new UserManagementView();
            window.ShowDialog();
        }

        // shows user management page
        private void Quit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        // accept a renturn
        private void AcceptReturn(object sender, RoutedEventArgs e)
        {
            if (RentalId.Text != null)
            {
                int id = Convert.ToInt16(RentalId.Text);
                
            }
        }

    }
}
