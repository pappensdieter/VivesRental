using System;
using System.Collections.Generic;
using System.Configuration;
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
using VivesRental.Services;

namespace VivesRental
{
    /// <summary>
    /// Interaction logic for ItemDetailsPopUp.xaml
    /// </summary>
    public partial class ItemDetailsPopUp : Window
    {
        public ItemDetailsPopUp(int Id)
        {
            InitializeComponent();
            FillDataGrid(Id);
        }

        private void FillDataGrid(int Id)
        {
            ItemService itemService = new ItemService();
            var items = itemService.All().Where(item => item.Id == Id);
            ItemBox.ItemsSource = items;
        }
    }
}
