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

        // fills dropdown with userid's
        public void FillDrpUserId()
        {
            drpUserId.ItemsSource = "leeg";

            var listUsers = userService.All();
            var listUserIds = new List<String>();
            
            foreach (var user in listUsers)
            {
                listUserIds.Add(user.Id.ToString());
            }
            drpUserId.ItemsSource = listUserIds;
        }

        // select userid from dropdown
        private void SelectUser(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                User user = userService.Get(Convert.ToInt16(drpUserId.SelectedItem));
                if (user != null)
                {
                    SetUserToForm(user);
                    FormEditMode(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                string mesg = "You did not select a user!";
                MessageBox.Show(mesg);
            }
        }

        // add user to database
        private void AddUser(object sender, RoutedEventArgs e)
        {
            User user = GetUserFromForm();
            if (user != null)
            {
                ClearForm();
                userService.Create(user);
                FillDrpUserId();
            }
        }

        // edit the selected user
        private void EditUser(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = GetUserFromForm();
                if (user != null)
                {
                    user.Id = Convert.ToInt16(drpUserId.SelectedItem);
                    ClearForm();
                    FormEditMode(false);
                    userService.Edit(user);
                    FillDrpUserId();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                string mesg = "You did not select a user!";
                MessageBox.Show(mesg);
            }
        }

        // delete the selected user
        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            try
            {
                userService.Remove(Convert.ToInt16(drpUserId.SelectedItem));
                ClearForm();
                FormEditMode(false);
                FillDrpUserId();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                string mesg = "You did not select a user!";
                MessageBox.Show(mesg);
            }
        }

        // cancel the form or go back to prev screen
        private void Cancel(object sender, RoutedEventArgs e)
        {
            if (cbEditMode.IsChecked == true) // als editMode aan staan
            {
                ClearForm();
                FormEditMode(false);
                FillDrpUserId();
            }
            else // als editMode uit staan
            {
                this.Close();
            }
        }

        // get user from form without id and check for input control
        private User GetUserFromForm()
        {
            User user = new User();
            string mesg = "One or more fields are not filled in!";

            if (!string.IsNullOrWhiteSpace(FirstName.Text))
            {
                user.FirstName = FirstName.Text;

                if (!string.IsNullOrWhiteSpace(Name.Text))
                {
                    user.Name = Name.Text;

                    if (!string.IsNullOrWhiteSpace(PhoneNumber.Text))
                    {
                        user.PhoneNumber = PhoneNumber.Text;

                        if (!string.IsNullOrWhiteSpace(Email.Text))
                        {
                            user.Email = Email.Text;

                            return user;
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
