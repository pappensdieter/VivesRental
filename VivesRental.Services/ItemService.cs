using System.Collections.Generic;
using System.Linq;
using VivesRental.Model;
using VivesRental.Repository.Core;
using VivesRental.Repository.Includes;
using VivesRental.Services.Contracts;
using VivesRental.Services.Extensions;

namespace VivesRental.Services
{
    public class ItemService: IItemService
    {
        public Item Get(int id)
        {
	        return Get(id, null);
        }

	    public Item Get(int id, ItemIncludes includes)
	    {
		    using (var unitOfWork = new UnitOfWork())
		    {
			    return unitOfWork.Items.Get(id, includes);
		    }
	    }

		public IList<Item> All()
        {
           return All(null);
        }

	    public IList<Item> All(ItemIncludes includes)
	    {
		    using (var unitOfWork = new UnitOfWork())
		    {
				return unitOfWork.Items.All(includes).ToList();
		    }
	    }

		public Item Create(Item entity)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if (!entity.IsValid())
                {
                    return null;
                }

                unitOfWork.Items.Add(entity);
                var numberOfObjectsUpdated = unitOfWork.Complete();
                if( numberOfObjectsUpdated > 0)
                {
                    return entity;
                }
                return null;
            }
        }

        public Item Edit(Item entity)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if (!entity.IsValid())
                {
                    return null;
                }

                //Get Item from unitOfWork
                var item = unitOfWork.Items.Get(entity.Id);
                if (item == null)
                {
                    return null;
                }

                //Only update the properties we want to update
                item.Name = entity.Name;
                item.Description = entity.Description;
                item.Manufacturer = entity.Manufacturer;
                item.Publisher = entity.Publisher;
                item.RentalExpiresAfterDays = entity.RentalExpiresAfterDays;
                
                var numberOfObjectsUpdated = unitOfWork.Complete();
                if(numberOfObjectsUpdated > 0)
                {
                    return entity;
                }
                return null;
            }
        }

        public bool Remove(int id)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var item = unitOfWork.Items.Get(id, new ItemIncludes{RentalItemsRentalOrderLines = true});
                if (item == null)
                    return false;

				//Remove rentalItems
	            foreach (var rentalItem in item.RentalItems.ToList())
	            {
					//Remove rentalOrderLines
		            foreach (var rentalOrderLine in rentalItem.RentalOrderLines.ToList())
		            {
			            unitOfWork.RentalOrderLines.Remove(rentalOrderLine);
		            }
		            unitOfWork.RentalItems.Remove(rentalItem);
	            }
				//Remove item
                unitOfWork.Items.Remove(item);

                var numberOfObjectsUpdated = unitOfWork.Complete();
                return numberOfObjectsUpdated > 0;
            }
        }
    }
}
