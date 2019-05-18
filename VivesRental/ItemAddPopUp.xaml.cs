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
    /// Interaction logic for ItemAddPopUp.xaml
    /// </summary>
    public partial class ItemAddPopUp : Window
    {
        ItemService Itservice = new ItemService();
        public ItemAddPopUp()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Name.Clear();
            Description.Clear();
            Manufacturer.Clear();
            Publisher.Clear();
            Exparation.Clear();
            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
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
            Itservice.Create(item);
            this.DialogResult = true;
            this.Close();

        }
    }
}
