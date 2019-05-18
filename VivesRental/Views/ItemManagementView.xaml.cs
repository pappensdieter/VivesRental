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
    /// Interaction logic for ItemManagementView.xaml
    /// </summary>
    public partial class ItemManagementView : Window
    {
        private ItemService itemService;

        public ItemManagementView()
        {
            itemService = new ItemService();

            InitializeComponent();
            FillItemTable();
        }

        // add a new item
        private void AddItem(object sender, RoutedEventArgs e)
        {
            Window window = new EditAddItemView(null);
            window.Title = "Add Item";
            window.ShowDialog();

            if (window.DialogResult == true)
            {
                FillItemTable();
            }
        }

        // edit he selected item
        private void EditItem(object sender, RoutedEventArgs e)
        {
            Item item = (Item)ItemTable.SelectedItem;
            Window window = new EditAddItemView(item);
            window.Title = "Edit Item with id: " + item.Id;
            window.ShowDialog();
            
            if (window.DialogResult == true)
            {
                FillItemTable();
            }
        }

        // show rental items of the selected item
        private void ShowRentalItems(object sender, RoutedEventArgs e)
        {
            Item item = (Item)ItemTable.SelectedItem;
            Window window = new RentalItemsView(item);
            window.Title = "Rental Items of: " + item.Name;
            window.ShowDialog();
        }

        // cancel/go back to prev screen
        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // fills item table
        private void FillItemTable()
        {
            var list = itemService.All();
            ItemTable.ItemsSource = list;
        }
    }
}
