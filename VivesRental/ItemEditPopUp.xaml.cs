using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace VivesRental
{
    /// <summary>
    /// Interaction logic for ItemEditPopUp.xaml
    /// </summary>
    public partial class ItemEditPopUp : Window
    {
        ItemService itemService = new ItemService();
        int id;
        public ItemEditPopUp(Item item)
        {
            id = item.Id;
            InitializeComponent();
            FillDataGrid(item);
        }

        private void FillDataGrid(Item item)
        {
            Name.Text = item.Name;
            Description.Text = item.Description;
            Manufacturer.Text = item.Manufacturer;
            Publisher.Text = item.Publisher;
            Exparation.Text = item.RentalExpiresAfterDays.ToString();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
            item.Id = id;
            item.Manufacturer = Manufacturer.Text;
            item.Description = Description.Text;
            item.Name = Name.Text;
            item.Publisher = Publisher.Text;
            item.RentalExpiresAfterDays = Convert.ToInt32(Exparation.Text);
            Name.Clear();
            Description.Clear();
            Manufacturer.Clear();
            Publisher.Clear();
            Exparation.Clear();
            itemService.Edit(item);
            this.DialogResult = true;
            this.Close();

        }
    }
}

