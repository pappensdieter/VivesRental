using System;
using System.Windows;
using VivesRental.Model;
using VivesRental.Services;

namespace VivesRental
{
    /// <summary>
    /// Interaction logic for RentalOrderDetailsPopUp.xaml
    /// </summary>
    public partial class RentalOrderDetailsPopUp : Window
    {
        RentalOrderLineService rentalOrderLineService = new RentalOrderLineService();
        int id;
        public RentalOrderDetailsPopUp(int id)
        {
            this.id = id;
            InitializeComponent();
            FillDataGrid(id);
        }

        private void FillDataGrid(int id)
        {
            var items = rentalOrderLineService.FindByRentalOrderId(id);
            OrderBox.ItemsSource = items;
        }

        private void Item_Return(object sender, RoutedEventArgs e)
        {
            RentalOrderLine OrderLine = new RentalOrderLine();
            OrderLine = (RentalOrderLine)OrderBox.SelectedItem;
            rentalOrderLineService.Return(OrderLine.Id, DateTime.Now);
            FillDataGrid(id);
        }
    }
}
