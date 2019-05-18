using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for FinishRental.xaml
    /// </summary>
    public partial class FinishRental : Window
    {
        UserService userService = new UserService();
        ItemService itemService = new ItemService();
        RentalItemService rentalItemService = new RentalItemService();
        RentalOrderService rentalOrderService = new RentalOrderService();
        RentalOrderLineService rentalOrderLineService = new RentalOrderLineService();

        ObservableCollection<Item> SelectedItems = new ObservableCollection<Item>();
        public FinishRental(ObservableCollection<Item> SelectedItems)
        {
            InitializeComponent();
            IList<User> Users = userService.All();
            this.SelectedItems = SelectedItems;
            UserSelector.ItemsSource = Users;
        }

        private void Button_ClickSubmit(object sender, RoutedEventArgs e)
        {
            int RentalId = 0;

            RentalOrder rentalOrder = new RentalOrder();
            User user = new User();
            user = (User)UserSelector.SelectedItem;
            rentalOrder.UserId = user.Id;
            rentalOrder.UserFirstName = user.FirstName;
            rentalOrder.UserName = user.Name;
            rentalOrder.UserEmail = user.Email;
            rentalOrder.CreatedAt = DateTime.Now;
            rentalOrderService.Create(rentalOrder);
            RentalId = rentalOrderService.All().Last().Id;

            RentalOrderLine rentalOrderLine = new RentalOrderLine();

            int Aantal = SelectedItems.Count();
            int Gedaan = 0;

            foreach (var item in SelectedItems)
            {
                var RentalItems = rentalItemService.All().Where(RentalItem => RentalItem.ItemId == item.Id).Where(Status => Status.Status == RentalItemStatus.Normal);
                foreach (var rentalItem in RentalItems)
                {
                    if (Gedaan < Aantal)
                    {
                        rentalOrderLineService.Rent(RentalId, rentalItem.Id);
                        Gedaan += 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            this.DialogResult = true;
            this.Close();
        }

        private void Button_ClickCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
