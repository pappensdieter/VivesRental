using System.Collections.Generic;
using System.Linq;
using VivesRental.Model;
using VivesRental.Repository.Core;
using VivesRental.Repository.Includes;
using VivesRental.Services.Contracts;
using VivesRental.Services.Extensions;

namespace VivesRental.Services
{
    public class RentalItemService : IRentalItemService
    {

        public RentalItem Get(int id)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.RentalItems.Get(id);
            }
        }

        public RentalItem Get(int id, RentalItemIncludes includes)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.RentalItems.Get(id, includes);
            }
        }

        public IList<RentalItem> All()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.RentalItems.GetAll().ToList();
            }
        }

        public IList<RentalItem> All(RentalItemIncludes includes)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.RentalItems.All(includes).ToList();
            }
        }

        public IList<RentalItem> GetAvailableRentalItems()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Normal &&
                                                         ri.RentalOrderLines.All(rol => rol.ReturnedAt.HasValue)).ToList();
            }
        }

        public IList<RentalItem> GetAvailableRentalItems(RentalItemIncludes includes)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Normal &&
                                                         ri.RentalOrderLines.All(rol => rol.ReturnedAt.HasValue), includes).ToList();
            }
        }

        public IList<RentalItem> GetRentedRentalItems()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Normal &&
                                                         ri.RentalOrderLines.Any(rol => !rol.ReturnedAt.HasValue)).ToList();
            }
        }

        public IList<RentalItem> GetRentedRentalItems(RentalItemIncludes includes)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Normal &&
                                                         ri.RentalOrderLines.Any(rol => !rol.ReturnedAt.HasValue), includes).ToList();
            }
        }

        public IList<RentalItem> GetRentedRentalItems(int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Normal &&
                                                         ri.RentalOrderLines.Any(rol => !rol.ReturnedAt.HasValue && rol.RentalOrder.UserId == userId)).ToList();
            }
        }

        public IList<RentalItem> GetRentedRentalItems(int userId, RentalItemIncludes includes)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Normal &&
                                                         ri.RentalOrderLines.Any(rol => !rol.ReturnedAt.HasValue && rol.RentalOrder.UserId == userId), includes).ToList();
            }
        }

        public RentalItem Create(RentalItem entity)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if (!entity.IsValid())
                {
                    return null;
                }

                unitOfWork.RentalItems.Add(entity);
                var numberOfObjectsUpdated = unitOfWork.Complete();
                if( numberOfObjectsUpdated > 0)
                {
                    return entity;
                }
                return null;
            }
        }

        public RentalItem Edit(RentalItem entity)
        {
            using (var unitOfWork = new UnitOfWork())
            {

                if (!entity.IsValid())
                {
                    return null;
                }

                //Get Item from unitOfWork
                var rentalItem = unitOfWork.RentalItems.Get(entity.Id);
                if (rentalItem == null)
                {
                    return null;
                }

                //Only update the properties we want to update
                rentalItem.ItemId = entity.ItemId;
                rentalItem.Status = entity.Status;

                var numberOfObjectsUpdated = unitOfWork.Complete();
                if( numberOfObjectsUpdated > 0)
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
                var rentalItem = unitOfWork.RentalItems.Get(id);
                if (rentalItem == null)
                    return false;

                unitOfWork.RentalItems.Remove(rentalItem);

                var numberOfObjectsUpdated = unitOfWork.Complete();
                return numberOfObjectsUpdated > 0;
            }
        }
    }
}
