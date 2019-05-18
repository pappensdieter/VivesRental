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

namespace VivesRental
{
    /// <summary>
    /// Interaction logic for RentalOrders.xaml
    /// </summary>
    public partial class RentalOrders : Window
    {
        RentalOrderService rentalOrderService = new RentalOrderService();
        RentalOrderLineService rentalOrderLineService = new RentalOrderLineService();
        public RentalOrders()
        {
            InitializeComponent();
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            var items = rentalOrderService.All();
            OrderBox.ItemsSource = items;
        }

        private void Item_Details(object sender, RoutedEventArgs e)
        {
            RentalOrder Rental = new RentalOrder();
            Rental = (RentalOrder)OrderBox.SelectedItem;
            int id = Rental.Id;
            Window PopUp = new RentalOrderDetailsPopUp(id);
            PopUp.ShowDialog();
        }

        private void Item_Return(object sender, RoutedEventArgs e)
        {
            RentalOrder Rental = new RentalOrder();
            Rental = (RentalOrder)OrderBox.SelectedItem;
            int id = Rental.Id;
            var RentalOrderLines = rentalOrderLineService.FindByRentalOrderId(id);
            foreach (var OrderLine in RentalOrderLines)
            {
                rentalOrderLineService.Return(OrderLine.Id, DateTime.Now);
            }
        }
    }
}
