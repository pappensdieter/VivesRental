using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VivesRental.Model;
using VivesRental.Services;

namespace VivesRental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IList<User> list;

        UserService service = new UserService();
        RentalOrderService OrderService = new RentalOrderService();
        RentalOrderLineService OrderLineService = new RentalOrderLineService();
        public MainWindow()
        {
            InitializeComponent();
            FillDrop();
        }

        private void Btn_ItemManger(object sender, RoutedEventArgs e)
        {
            Window window = new ItemManagment();
            window.Show();
        }
        public void FillDrop()
        {
            IList idList = new List<String>();
            list = service.All();
            idList.Add("-----------");
            foreach (var item in list)
            {
                idList.Add(item.Id.ToString());
            }
            drop.ItemsSource = idList;
        }

        private void Button_SubmitUser(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.FirstName = FirstName.Text;
            user.Name = Name.Text;
            user.PhoneNumber = Phone.Text;
            user.Email = Email.Text;
            FirstName.Clear();
            Name.Clear();
            Phone.Clear();
            Email.Clear();
            service.Create(user);
            FillDrop();
        }

        private void Button_ClickEdit(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Id = Convert.ToInt32(drop.SelectedItem);
            user.FirstName = FirstName.Text;
            user.Name = Name.Text;
            user.PhoneNumber = Phone.Text;
            user.Email = Email.Text;
            FirstName.Clear();
            Name.Clear();
            Phone.Clear();
            Email.Clear();
            service.Edit(user);
            FillDrop();
            Submit.IsEnabled = true;
            Edit.IsEnabled = false;
        }

        private void Button_ClickCancel(object sender, RoutedEventArgs e)
        {
            FirstName.Clear();
            Name.Clear();
            Phone.Clear();
            Email.Clear();
            Submit.IsEnabled = true;
            Edit.IsEnabled = false;
        }

        private void drop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User user = new User();
            user = service.Get(Convert.ToInt32(drop.SelectedItem));
            FirstName.Text = user.FirstName;
            Name.Text = user.Name;
            Phone.Text = user.PhoneNumber;
            Email.Text = user.Email;
            CbEdit.IsChecked = false;
            Submit.IsEnabled = false;
            Edit.IsEnabled = true;
            Delete.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = new newRental();
            window.Show();
        }

        private void Button_ClickDeleteUsr(object sender, RoutedEventArgs e)
        {
            FirstName.Clear();
            Name.Clear();
            Phone.Clear();
            Email.Clear();
            FillDrop();
            service.Remove(Convert.ToInt32(drop.SelectedItem));
            Submit.IsEnabled = true;
            Edit.IsEnabled = false;
            Delete.IsEnabled = false;
        }

        private void Button_ClickRentalOrders(object sender, RoutedEventArgs e)
        {
            Window RentalOrdersWindow = new RentalOrders();
            RentalOrdersWindow.ShowDialog();
        }

        private void Button_ClickAcceptReturn(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(RentalId.Text);
            var RentalOrderLines = OrderLineService.FindByRentalOrderId(id);
            foreach (var OrderLine in RentalOrderLines)
            {
                OrderLineService.Return(OrderLine.Id, DateTime.Now);
            }

            RentalId.Text = "";
        }
    }
}
