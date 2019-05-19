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
    /// Interaction logic for RentalItemsView.xaml
    /// </summary>
    public partial class RentalItemsView : Window
    {
        private RentalItemService rentalItemService;
        private Item itemToRentalItem;

        public RentalItemsView(Item item)
        {
            rentalItemService = new RentalItemService();

            InitializeComponent();
            itemToRentalItem = item;
            FillItemTable();
        }

        // add rental item of the selected item in the prev screen
        private void AddRentalItem(object sender, RoutedEventArgs e)
        {
            RentalItem rentalItem = new RentalItem();
            rentalItem.ItemId = itemToRentalItem.Id;
            rentalItem.Status = RentalItemStatus.Normal;
            rentalItemService.Create(rentalItem);
            FillItemTable();
        }

        // cancel/go back to prev screen
        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // fills table
        private void FillItemTable()
        {
            var list = rentalItemService.All().Where(x => x.ItemId == itemToRentalItem.Id);
            ItemTable.ItemsSource = list;
        }
    }
}
