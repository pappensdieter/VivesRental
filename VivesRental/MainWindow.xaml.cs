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
        public void ShowItemMangement(object sender, RoutedEventArgs e)
        {
            Window window = new ItemManagementView();
            window.ShowDialog();
        }

        // shows rental orders page
        public void ShowRentalOrders(object sender, RoutedEventArgs e)
        {
            Window window = new RentalOrdersView();
            window.ShowDialog();
        }

        // shows new rental page
        public void ShowNewRental(object sender, RoutedEventArgs e)
        {
            Window window = new NewRentalView();
            window.ShowDialog();
        }

        // shows user management page
        public void ShowUserManagement(object sender, RoutedEventArgs e)
        {
            Window window = new UserManagementView();
            window.ShowDialog();
        }

        // shows user management page
        public void Quit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // accept a renturn
        public void AcceptReturns(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(RentalId.Text))
            {
                int id = Convert.ToInt16(RentalId.Text);
                RentalOrder rentalOrder = rentalOrderService.Get(id);

                if (rentalOrder != null)
                {
                    Window window = new RentalOderDetailsView(rentalOrder);
                    window.Title = "Details of rental order: " + rentalOrder.Id;
                    window.ShowDialog();
                }
                else
                {
                    string mesg = "Rental id: " + id + " is invalid!";
                    MessageBox.Show(mesg);
                }
            }
            else
            {
                Window window = new RentalOrdersView();
                window.ShowDialog();
            }
        }
    }
}
