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

        private void EditItem(object sender, RoutedEventArgs e)
        {
            Item item = (Item)ItemTable.SelectedItem;
            Window window = new EditAddItemView(item);
            window.Title = "Edit Item";
            window.ShowDialog();
            
            if (window.DialogResult == true)
            {
                FillItemTable();
            }
        }

        private void NewRentalItem(object sender, RoutedEventArgs e)
        {
        }

        // fills item table
        private void FillItemTable()
        {
            var list = itemService.All();
            ItemTable.ItemsSource = list;
        }
    }
}
