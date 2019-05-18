using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Configuration;

namespace VivesRental
{
    /// <summary>
    /// Interaction logic for ItemManagment.xaml
    /// </summary>
    public partial class ItemManagment : Window
    {
        ItemService ItService = new ItemService();
        public ItemManagment()
        {
            InitializeComponent();
            FillDataGrid();
        }


        private void Button_ClickAddItem(object sender, RoutedEventArgs e)
        {
            Window Popup = new ItemAddPopUp();
            Popup.ShowDialog();
            if (Popup.DialogResult == true)
            {
                FillDataGrid();
            }
        }

        private void Item_Details(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
            item = (Item)ItemBox.SelectedItem;
            int Id = item.Id;
            Window PopUp = new ItemDetailsPopUp(Id);
            PopUp.ShowDialog();
        }

        private void Item_Edit(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
            item = (Item)ItemBox.SelectedItem;
            ItemEditPopUp PopUp = new ItemEditPopUp(item);
            PopUp.ShowDialog();
            if (PopUp.DialogResult == true)
            {
                FillDataGrid();
            }
        }

        private void Item_newRentalItem(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
            item = (Item)ItemBox.SelectedItem;
            Window PopUp = new RentalItems(item);
            PopUp.ShowDialog();
        }

        private void FillDataGrid()
        {
            var items = ItService.All();
            ItemBox.ItemsSource = items;
        }
    }
}
