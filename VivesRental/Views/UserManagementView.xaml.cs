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
    /// Interaction logic for UserManagementView.xaml
    /// </summary>
    public partial class UserManagementView : Window
    {
        private UserService userService;

        public UserManagementView()
        {
            userService = new UserService();

            InitializeComponent();
            FillDrpUserId();
            FormEditMode(false);
        }

        public void FillDrpUserId()
        {
            var listUsers = userService.All();
            var listUserIds = new List<String>();

            listUserIds.Add("Please slect a userId");
            foreach (var user in listUsers)
            {
                listUserIds.Add(user.Id.ToString());
            }
            drpUserId.ItemsSource = listUserIds;
        }

        private void SelectUser(object sender, SelectionChangedEventArgs e)
        {
            User user = userService.Get(Convert.ToInt16(drpUserId.SelectedItem));
            if (user != null)
            {
                SetUserToForm(user);
                FormEditMode(true);
            }
        }

        // add user to database
        private void AddUser(object sender, RoutedEventArgs e)
        {
            User user = GetUserFromForm();
            ClearForm();
            userService.Create(user);
            FillDrpUserId();
        }

        // edit the selected user
        private void EditUser(object sender, RoutedEventArgs e)
        {
            User user = GetUserFromForm();
            user.Id = user.Id = Convert.ToInt16(drpUserId.SelectedItem);
            ClearForm();
            FormEditMode(false);
            userService.Edit(user);
        }

        // delete the selected user
        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            userService.Remove(Convert.ToInt16(drpUserId.SelectedItem));
            ClearForm();
            FormEditMode(false);
            FillDrpUserId();
        }

        // cancel the form
        private void Cancel(object sender, RoutedEventArgs e)
        {
            ClearForm();
            FormEditMode(false);
            this.Close();
        }

        // get user from form without id
        private User GetUserFromForm()
        {
            User user = new User();
            user.FirstName = FirstName.Text;
            user.Name = Name.Text;
            user.PhoneNumber = PhoneNumber.Text;
            user.Email = Email.Text;

            return user;
        }

        // set user to form
        private void SetUserToForm(User user)
        {
            FirstName.Text = user.FirstName;
            Name.Text = user.Name;
            PhoneNumber.Text = user.PhoneNumber;
            Email.Text = user.Email;
        }

        // clears form
        private void ClearForm()
        {
            FirstName.Clear();
            Name.Clear();
            PhoneNumber.Clear();
            Email.Clear();
        }

        // sets form to editmode
        private void FormEditMode(Boolean b)
        {
            btnAdd.IsEnabled = !b;
            cbEditMode.IsChecked = b;
            btnEdit.IsEnabled = b;
            btnDelete.IsEnabled = b;
        }
    }
}
