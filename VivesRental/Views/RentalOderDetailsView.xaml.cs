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
    /// Interaction logic for RentalOderDetailsView.xaml
    /// </summary>
    public partial class RentalOderDetailsView : Window
    {
        private RentalOrderLineService rentalOrderLineService;

        private RentalOrder rentalOrderDetail;

        public RentalOderDetailsView(RentalOrder rentalOrder)
        {
            rentalOrderLineService = new RentalOrderLineService();

            InitializeComponent();
            rentalOrderDetail = rentalOrder;
            FillOrderTable();
        }

        // returns the selected orderline
        public void ReturnOrderLine(object sender, RoutedEventArgs e)
        {
            try
            {
                RentalOrderLine rentalOrderLine = (RentalOrderLine)OrderLineTable.SelectedItem;
                rentalOrderLineService.Return(rentalOrderLine.Id, DateTime.Now);
                FillOrderTable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                string mesg = "You did not select an orderline!";
                MessageBox.Show(mesg);
            }
        }

        // cancel/go back to prev screen
        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // fills the orderline table
        public void FillOrderTable()
        {
            var list = rentalOrderLineService.FindByRentalOrderId(rentalOrderDetail.Id);
            OrderLineTable.ItemsSource = list;
        }
    }
}
