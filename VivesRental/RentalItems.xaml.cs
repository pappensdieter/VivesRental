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
    /// Interaction logic for RentalItems.xaml
    /// </summary>
    public partial class RentalItems : Window
    {
        Item item = new Item();
        RentalItemService rtService = new RentalItemService();
        public RentalItems(Item item)
        {
            this.item = item;
            InitializeComponent();
            FillDataGrid();
        }

        private void Button_ClickAdd1(object sender, RoutedEventArgs e)
        {
            RentalItem rtItem = new RentalItem();
            rtItem.ItemId = item.Id;
            rtItem.Status = RentalItemStatus.Normal;
            rtService.Create(rtItem);
            FillDataGrid();
        }

        private void RentalItemEdit(object sender, RoutedEventArgs e)
        {
            RentalItem Rtitem = new RentalItem();
            Rtitem = (RentalItem)ItemBox.SelectedItem;
            Window PopUp = new RentalItemEditPopUp(Rtitem);
            PopUp.ShowDialog();
            if (PopUp.DialogResult == true)
            {
                FillDataGrid();
            }
        }

        private void FillDataGrid()
        {
            var items = rtService.All().Where(rtItem => rtItem.ItemId == item.Id);
            ItemBox.ItemsSource = items;
        }
    }
}
