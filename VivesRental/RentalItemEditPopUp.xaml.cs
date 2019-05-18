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
    /// Interaction logic for RentalItemEditPopUp.xaml
    /// </summary>
    public partial class RentalItemEditPopUp : Window
    {
        RentalItem Rtitem = new RentalItem();
        RentalItemService rtService = new RentalItemService();
        public RentalItemEditPopUp(RentalItem Rtitem)
        {
            this.Rtitem = Rtitem;
            InitializeComponent();
            if (Rtitem.Status.Equals(RentalItemStatus.Normal))
                Statusbox.SelectedIndex = 0;
            else if (Rtitem.Status.Equals(RentalItemStatus.Broken))
                Statusbox.SelectedIndex = 1;
            else if (Rtitem.Status.Equals(RentalItemStatus.InRepair))
                Statusbox.SelectedIndex = 2;
            else if (Rtitem.Status.Equals(RentalItemStatus.Lost))
                Statusbox.SelectedIndex = 3;
            else
                Statusbox.SelectedIndex = 4;

        }

        private void Button_ClickCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_ClickSubmit(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Rtitem.Status = (RentalItemStatus)Enum.Parse(typeof(RentalItemStatus), Statusbox.Text);
            rtService.Edit(Rtitem);
        }
    }
}
