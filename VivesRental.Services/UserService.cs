using System.Collections.Generic;
using System.Linq;
using VivesRental.Model;
using VivesRental.Repository.Core;
using VivesRental.Services.Contracts;
using VivesRental.Services.Extensions;

namespace VivesRental.Services
{
    public class UserService: IUserService
    {
        

        public User Get(int id)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Users.Get(id);
            }
        }

        public IList<User> All()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Users.GetAll().ToList();
            }
        }

        public User Create(User entity)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if (!entity.IsValid())
                {
                    return null;
                }

                unitOfWork.Users.Add(entity);
                var numberOfObjectsUpdated = unitOfWork.Complete();
                if (numberOfObjectsUpdated > 0)
                    return entity;
                return null;
            }
        }

        public User Edit(User entity)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if (!entity.IsValid())
                {
                    return null;
                }

                //Get Item from unitOfWork
                var user = unitOfWork.Users.Get(entity.Id);
                if (user == null)
                {
                    return null;
                }

                //Only update the properties we want to update
                user.FirstName = entity.FirstName;
                user.Name = entity.Name;
                user.Email = entity.Email;
                user.PhoneNumber = entity.PhoneNumber;
                
                var numberOfObjectsUpdated = unitOfWork.Complete();
                if (numberOfObjectsUpdated > 0)
                    return entity;
                return null;
            }
        }

        public bool Remove(int id)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.Users.Get(id);
                if (user == null)
                    return false;

                unitOfWork.Users.Remove(user);

                var numberOfObjectsUpdated = unitOfWork.Complete();
                return numberOfObjectsUpdated > 0;
            }
        }
    }
}
