using System;
using System.Collections.Generic;
using System.Linq;
using VivesRental.Model;
using VivesRental.Repository.Core;
using VivesRental.Services.Contracts;
using VivesRental.Services.Extensions;

namespace VivesRental.Services
{
    public class RentalOrderService: IRentalOrderService
    {
        public RentalOrder Get(int id)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.RentalOrders.Get(id);
            }
        }

        public IList<RentalOrder> All()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.RentalOrders.GetAll().ToList();
            }
        }

        public RentalOrder Create(RentalOrder entity)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if (!entity.IsValid())
                {
                    return null;
                }

                unitOfWork.RentalOrders.Add(entity);
                var numberOfObjectsUpdated = unitOfWork.Complete();
                if( numberOfObjectsUpdated > 0)
                {
                    return entity;
                }
                return null;
            }
        }

        public bool Return(int rentalOrderId, DateTime returnedAt)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var rentalOrderLines = unitOfWork.RentalOrderLines.Find(rol => rol.RentalOrderId == rentalOrderId);
                foreach (var rentalOrderLine in rentalOrderLines)
                {
                    rentalOrderLine.ReturnedAt = returnedAt;
                }

                var numberOfObjectsUpdated = unitOfWork.Complete();
                return numberOfObjectsUpdated > 0;
            }
        }
    }
}
