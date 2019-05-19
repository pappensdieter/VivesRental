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
        public void AddItem(object sender, RoutedEventArgs e)
        {
            // same screen as EditItem => give null as var to know difference
            Window window = new EditAddItemView(null);
            window.Title = "Add Item";
            window.ShowDialog();

            if (window.DialogResult == true)
            {
                FillItemTable();
            }
        }

        // edit the selected item
        public void EditItem(object sender, RoutedEventArgs e)
        {
            try // check for valid selected item
            {
                Item item = (Item)ItemTable.SelectedItem;
                if(item != null) // check for not null
                {
                    // same screen as AddItem => give item as var to know difference
                    Window window = new EditAddItemView(item);
                    window.Title = "Edit Item with id: " + item.Id;
                    window.ShowDialog();

                    if (window.DialogResult == true)
                    {
                        FillItemTable();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                string mesg = "You did not select an item!";
                MessageBox.Show(mesg);
            }
        }

        // show rental items of the selected item
        public void ShowRentalItems(object sender, RoutedEventArgs e)
        {
            try // check for valid selected item
            {
                Item item = (Item)ItemTable.SelectedItem;
                if (item != null) // check for not null
                {
                    Window window = new RentalItemsView(item);
                    window.Title = "Rental Items of: " + item.Name;
                    window.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                string mesg = "You did not select an item!";
                MessageBox.Show(mesg);
            }
        }

        // cancel/go back to prev screen
        public void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // fills item table
        public void FillItemTable()
        {
            var list = itemService.All();
            ItemTable.ItemsSource = list;
        }
    }
}
