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
using VivesRental.Model;
using VivesRental.Services;

namespace VivesRental.Views
{
    /// <summary>
    /// Interaction logic for RentalOrdersView.xaml
    /// </summary>
    public partial class RentalOrdersView : Window
    {
        private RentalOrderService rentalOrderService;
        private RentalOrderLineService rentalOrderLineService;

        public RentalOrdersView()
        {
            rentalOrderService = new RentalOrderService();
            rentalOrderLineService = new RentalOrderLineService();

            InitializeComponent();
            FillOrderTable();
        }

        // show all the orderlines in the selected order
        private void ShowRentalOderDetails(object sender, RoutedEventArgs e)
        {
            RentalOrder rentalOrder = (RentalOrder)OrderTable.SelectedItem;
            Window window = new RentalOderDetailsView(rentalOrder);
            window.Title = "Details of rental order: " + rentalOrder.Id;
            window.ShowDialog();
        }

        // return the selected order
        private void ReturnOrder(object sender, RoutedEventArgs e)
        {
            RentalOrder rentalOrder = (RentalOrder)OrderTable.SelectedItem;
            var list = rentalOrderLineService.FindByRentalOrderId(rentalOrder.Id);

            foreach (var orderLine in list)
            {
                rentalOrderLineService.Return(orderLine.Id, DateTime.Now);
            }
        }

        // shows new rental page
        private void ShowNewRental(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window window = new NewRentalView();
            window.ShowDialog();
        }

        // cancel/go back to prev screen
        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // fills the order table
        public void FillOrderTable()
        {
            var list = rentalOrderService.All();
            OrderTable.ItemsSource = list;
        }
    }
}
