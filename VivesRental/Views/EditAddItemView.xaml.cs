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
    /// Interaction logic for EditAddItemView.xaml
    /// </summary>
    public partial class EditAddItemView : Window
    {
        private ItemService itemService;
        private Item itemToEdit;

        public EditAddItemView(Item item)
        {
            itemService = new ItemService();

            InitializeComponent();
            if (item != null)
            {
                itemToEdit = item;
                FillForm(item);
            }
        }

        // add or update item
        private void SubmitItem(object sender, RoutedEventArgs e)
        {
            if (itemToEdit == null)
            {
                itemService.Create(GetItemFromForm());
            }
            else
            {
                // needs to remember id cuz it become 0 otherwise
                int id = itemToEdit.Id;
                itemToEdit = GetItemFromForm();
                itemToEdit.Id = id;
                itemService.Edit(itemToEdit);
            }

            ClearForm();
            this.DialogResult = true;
            this.Close();
        }

        // cancel/go back to prev screen
        private void Cancel(object sender, RoutedEventArgs e)
        {
            ClearForm();
            this.DialogResult = false;
            this.Close();
        }

        // get item from form without id
        private Item GetItemFromForm()
        {
            Item item = new Item();
            item.Name = Name.Text;
            item.Description = Description.Text;
            item.Manufacturer = Manufacturer.Text;
            item.Publisher = Publisher.Text;
            item.RentalExpiresAfterDays = Convert.ToInt16(RentalExpiresAfterDays.Text);

            return item;
        }

        // fills form
        private void FillForm(Item item)
        {
            Name.Text = item.Name;
            Description.Text = item.Description;
            Manufacturer.Text = item.Manufacturer;
            Publisher.Text = item.Publisher;
            RentalExpiresAfterDays.Text = item.RentalExpiresAfterDays.ToString();
        }

        // clears form
        private void ClearForm()
        {
            Name.Clear();
            Description.Clear();
            Manufacturer.Clear();
            Publisher.Clear();
            RentalExpiresAfterDays.Clear();
        }
    }
}
