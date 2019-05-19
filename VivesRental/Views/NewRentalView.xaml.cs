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

namespace VivesRental.Views
{
    /// <summary>
    /// Interaction logic for NewRentalView.xaml
    /// </summary>
    public partial class NewRentalView : Window
    {
        private UserService userService;
        private ItemService itemService;
        private RentalItemService rentalItemService;
        private RentalOrderLineService rentalOrderLineService;
        private RentalOrderService rentalOrderService;

        private ObservableCollection<RentalItem> AvailableItemList = new ObservableCollection<RentalItem>();
        private ObservableCollection<RentalItem> OrderItemList = new ObservableCollection<RentalItem>();

        public NewRentalView()
        {
            userService = new UserService();
            itemService = new ItemService();
            rentalItemService = new RentalItemService();
            rentalOrderLineService = new RentalOrderLineService();
            rentalOrderService = new RentalOrderService();

            InitializeComponent();

            // fills lists
            FillAvailableItemsList();
            FillDrpUser();
            AvailableList.ItemsSource = AvailableItemList;
            OrderList.ItemsSource = OrderItemList;
        }

        // adds item from available to your order
        private void AddItemToOrder(object sender, MouseButtonEventArgs e)
        {
            try
            {
                RentalItem rentalItem = (RentalItem)AvailableList.SelectedItem;
                if (rentalItem != null)
                {
                    AvailableItemList.Remove(rentalItem);
                    OrderItemList.Add(rentalItem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                string mesg = "You did not select a rental item!";
                MessageBox.Show(mesg);
            }
        }

        // removes item from your order
        private void RemoveItemFromOrder(object sender, MouseButtonEventArgs e)
        {
            try
            {
                RentalItem rentalItem = (RentalItem)OrderList.SelectedItem;
                if (rentalItem != null)
                {
                    AvailableItemList.Add(rentalItem);
                    OrderItemList.Remove(rentalItem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                string mesg = "You did not select a rental item!";
                MessageBox.Show(mesg);
            }
        }

        // rent your items
        private void Order(object sender, RoutedEventArgs e)
        {
            try
            {
                // get user
                User user = (User)cbUser.SelectedItem;

                if (user != null)
                {
                    if (OrderItemList.Count() != 0)
                    {
                        // create rental order object
                        RentalOrder rentalOrder = new RentalOrder();
                        rentalOrder.UserId = user.Id;
                        rentalOrder.UserFirstName = user.FirstName;
                        rentalOrder.UserName = user.Name;
                        rentalOrder.UserEmail = user.Email;
                        rentalOrder.CreatedAt = DateTime.Now;
                        rentalOrderService.Create(rentalOrder);

                        // get rental order id
                        var rentalOrderId = rentalOrderService.All().Last().Id;

                        // rent all the items form order table
                        foreach (var rentalItem in OrderItemList)
                        {
                            rentalOrderLineService.Rent(rentalOrderId, rentalItem.Id);
                        }
                        this.Close();
                    }
                    else
                    {
                        string mesg = "You did not select a rental item!";
                        MessageBox.Show(mesg);
                    }
                }
                else
                {
                    string mesg = "You did not select a user!";
                    MessageBox.Show(mesg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                string mesg = "You did not select a user!";
                MessageBox.Show(mesg);
            }
        }

        // cancel/go back to prev screen
        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // fills the user dropdown
        public void FillDrpUser()
        {
            var listUsers = userService.All();
            cbUser.ItemsSource = listUsers;
        }

        // fills the available items list
        private void FillAvailableItemsList()
        {
            var list = rentalItemService.GetAvailableRentalItems();
            foreach (var rentalItem in list)
            {
                Item item = itemService.Get(rentalItem.ItemId);
                rentalItem.Item = item;
                AvailableItemList.Add(rentalItem);
            }
        }
    }
}
