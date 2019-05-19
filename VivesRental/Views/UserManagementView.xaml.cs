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
            drpUserId.ItemsSource = "";

            var listUsers = userService.All();
            var listUserIds = new List<String>();
            
            foreach (var user in listUsers)
            {
                listUserIds.Add(user.Id.ToString());
            }
            drpUserId.ItemsSource = listUserIds;
        }

        // select userid from dropdown
        public void SelectUser(object sender, SelectionChangedEventArgs e)
        {
            try // check for valid selected item
            {
                User user = userService.Get(Convert.ToInt16(drpUserId.SelectedItem));
                if (user != null) // check for not null
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
        public void AddUser(object sender, RoutedEventArgs e)
        {
            User user = GetUserFromForm();
            if (user != null) // check for not null
            {
                ClearForm();
                userService.Create(user);
                FillDrpUserId();
            }
        }

        // edit the selected user
        public void EditUser(object sender, RoutedEventArgs e)
        {
            try // check for valid selected item
            {
                User user = GetUserFromForm();
                if (user != null) // check for not null
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
        public void DeleteUser(object sender, RoutedEventArgs e)
        {
            try // check for valid selected item
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
        public void Cancel(object sender, RoutedEventArgs e)
        {
            if (cbEditMode.IsChecked == true) // editMode ON
            {
                ClearForm();
                FormEditMode(false);
                FillDrpUserId();
            }
            else // editMode uit OFF
            {
                this.Close();
            }
        }

        // get user from form without id
        // 1 or more fields not filled in => return null
        public User GetUserFromForm()
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
        public void SetUserToForm(User user)
        {
            FirstName.Text = user.FirstName;
            Name.Text = user.Name;
            PhoneNumber.Text = user.PhoneNumber;
            Email.Text = user.Email;
        }

        // clears form
        public void ClearForm()
        {
            FirstName.Clear();
            Name.Clear();
            PhoneNumber.Clear();
            Email.Clear();
        }

        // sets form to editmode
        public void FormEditMode(Boolean b)
        {
            btnAdd.IsEnabled = !b;
            cbEditMode.IsChecked = b;
            btnEdit.IsEnabled = b;
            btnDelete.IsEnabled = b;
        }
    }
}
