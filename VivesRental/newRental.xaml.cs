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
    /// Interaction logic for newRental.xaml
    /// </summary>
    public partial class newRental : Window
    {
        NewRentalFormHandler formHandler = new NewRentalFormHandler();
        public newRental()
        {
            InitializeComponent();
            Available.ItemsSource = formHandler.Available;
        }

        private void Click_Select(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
            item = (Item)Available.SelectedItem;
            formHandler.SelectItems(item);
            Selected.ItemsSource = formHandler.SelectedSelection;
        }

        private void Click_Deselect(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
            item = (Item)Selected.SelectedItem;
            formHandler.deselect(item);
        }

        private void Click_Finish(object sender, RoutedEventArgs e)
        {
            Window FinishRental = new FinishRental(formHandler.SelectedSelection);
            FinishRental.ShowDialog();
            if (FinishRental.DialogResult == true)
            {
                formHandler.SelectedSelection.Clear();
            }
        }

        private void MouseDoubleClick_Available(object sender, MouseButtonEventArgs e)
        {
            Item item = new Item();
            item = (Item)Available.SelectedItem;
            formHandler.SelectItems(item);
            Selected.ItemsSource = formHandler.SelectedSelection;
        }

        private void MouseDoubleClick_Deselect(object sender, MouseButtonEventArgs e)
        {
            Item item = new Item();
            item = (Item)Selected.SelectedItem;
            formHandler.deselect(item);
        }
    }
}
