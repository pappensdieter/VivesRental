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
        public void ShowRentalOderDetails(object sender, RoutedEventArgs e)
        {
            try // check for valid selected item
            {
                RentalOrder rentalOrder = (RentalOrder)OrderTable.SelectedItem;
                if (rentalOrder != null) // check for not null
                {
                    Window window = new RentalOderDetailsView(rentalOrder);
                    window.Title = "Details of rental order: " + rentalOrder.Id;
                    window.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                string mesg = "You did not select a rental order!";
                MessageBox.Show(mesg);
            }
        }

        // return the selected order
        public void ReturnOrder(object sender, RoutedEventArgs e)
        {
            try // check for valid selected item
            {
                RentalOrder rentalOrder = (RentalOrder)OrderTable.SelectedItem;
                if (rentalOrder != null) // check for not null
                {
                    var list = rentalOrderLineService.FindByRentalOrderId(rentalOrder.Id);
                    var i = 0; // +1 if item is already returned

                    foreach (var orderLine in list)
                    {
                        if (orderLine.ReturnedAt == null) // check for already returned
                        {
                            rentalOrderLineService.Return(orderLine.Id, DateTime.Now);
                        }
                        else
                        {
                            i++;
                        }
                    }

                    if (i < list.Count()) // 1 or more items were not yet returned
                    {
                        string mesg = "Succesfully returned order: " + rentalOrder.Id + "!";
                        MessageBox.Show(mesg);
                    }
                    else // all items are already returned
                    {
                        string mesg = "Order: " + rentalOrder.Id + " is already completely returned!";
                        MessageBox.Show(mesg);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                string mesg = "You did not select a rental order!";
                MessageBox.Show(mesg);
            }
        }

        // shows new rental page
        public void ShowNewRental(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window window = new NewRentalView();
            window.ShowDialog();
        }

        // cancel/go back to prev screen
        public void Cancel(object sender, RoutedEventArgs e)
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
