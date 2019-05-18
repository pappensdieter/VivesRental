using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesRental.Model;
using VivesRental.Services;

namespace VivesRental
{

    class NewRentalFormHandler
    {
        RentalItemService service = new RentalItemService();
        ItemService itemService = new ItemService();
        RentalOrderLineService OrderLineService = new RentalOrderLineService();
        RentalOrderService OrderService = new RentalOrderService();

        public ObservableCollection<Item> Available = new ObservableCollection<Item>();
        public ObservableCollection<Item> SelectedSelection = new ObservableCollection<Item>();
        public NewRentalFormHandler()
        {
            Item it = new Item();
            IList<RentalItem> AvailableRentalItems;
            AvailableRentalItems = service.GetAvailableRentalItems();
            foreach (var AvailableItem in AvailableRentalItems)
            {
                var items = itemService.All().Where(item => item.Id == AvailableItem.ItemId);
                foreach (var item in items)
                {
                    Available.Add(item);
                }
            }
        }
        public void SelectItems(Item item)
        {
            Available.Remove(item);
            SelectedSelection.Add(item);
        }

        public void deselect(Item item)
        {
            Available.Add(item);
            SelectedSelection.Remove(item);
        }
    }
}
