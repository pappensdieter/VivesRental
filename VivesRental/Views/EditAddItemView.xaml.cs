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
        public void SubmitItem(object sender, RoutedEventArgs e)
        {
            if (itemToEdit == null) // itemToEdit is null so create a new item
            {
                itemService.Create(GetItemFromForm());
            }
            else // edit itemToEdit
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
        public void Cancel(object sender, RoutedEventArgs e)
        {
            ClearForm();
            this.DialogResult = false;
            this.Close();
        }

        // get item from form without id
        // 1 or more fields not filled in => return null
        public Item GetItemFromForm()
        {
            Item item = new Item();
            string mesg = "One or more fields are not filled in!";

            if (!string.IsNullOrWhiteSpace(Name.Text))
            {
                item.Name = Name.Text;

                if (!string.IsNullOrWhiteSpace(Description.Text))
                {
                    item.Description = Description.Text;

                    if (!string.IsNullOrWhiteSpace(Manufacturer.Text))
                    {
                        item.Manufacturer = Manufacturer.Text;

                        if (!string.IsNullOrWhiteSpace(Publisher.Text))
                        {
                            item.Publisher = Publisher.Text;

                            if (!string.IsNullOrWhiteSpace(RentalExpiresAfterDays.Text))
                            {
                                item.RentalExpiresAfterDays = Convert.ToInt16(RentalExpiresAfterDays.Text);

                                return item;
                            }
                            else
                            {
                                MessageBox.Show(mesg);
                                return null;
                            }
                        }
                        else
                        {
                            MessageBox.Show(mesg);
                            return null;
                        }
                    }
                    else
                    {
                        MessageBox.Show(mesg);
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show(mesg);
                    return null;
                }
            }
            else
            {
                MessageBox.Show(mesg);
                return null;
            }
        }

        // fills form
        public void FillForm(Item item)
        {
            Name.Text = item.Name;
            Description.Text = item.Description;
            Manufacturer.Text = item.Manufacturer;
            Publisher.Text = item.Publisher;
            RentalExpiresAfterDays.Text = item.RentalExpiresAfterDays.ToString();
        }

        // clears form
        public void ClearForm()
        {
            Name.Clear();
            Description.Clear();
            Manufacturer.Clear();
            Publisher.Clear();
            RentalExpiresAfterDays.Clear();
        }
    }
}
