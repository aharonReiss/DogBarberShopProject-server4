using DogBarberShopProject.Interfaces;
using DogBarberShopProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogBarberShopProject.Services
{
    public class QueueManagementService : IQueueManagement
    {   
        private DataContext context;
        private IAuthentication _authentication;

        public QueueManagementService(DataContext dc,IAuthentication ia)
        {
            context = dc;
            _authentication = ia;
        }

        public bool addQueue(AddQueueModel addQueueModel)
        {
            var user = context.users.Where(u => u.Id == addQueueModel.UserId);
            DateTime d = DateTime.Now;
            context.queues.Add(new Queues
            {
                CreationQueueTime = d,
                QueueTime = DateTime.Parse(addQueueModel.QueueTime),
                UserId = addQueueModel.UserId
            });
            context.SaveChanges();
            return true;
        }

        public bool deleteQueue(int id, string token)
        {
            var q = context.queues.Include(u => u.User).Where(q => q.User.UserName == _authentication.DecodeToken(token)).SingleOrDefault(q => q.Id == id);
            if (q == null)
                return false;
            context.queues.Remove(q);
            context.SaveChanges();
            return true;
        }

        public object getQueues()
        {
            var Queues = context.queues.Include(u => u.User)
                .Select(q => new
                {
                    q.CreationQueueTime,
                    q.QueueTime,
                    q.User.Name,
                    q.Id,
                    q.UserId
                });
            return Queues;
        }

        public bool updateQueue(UpdateQueueModel updateQueueModel,string token)
        {
            var queue = context.queues.Include(u => u.User).Where(q => q.User.UserName == _authentication.DecodeToken(token)).SingleOrDefault(q => q.Id == updateQueueModel.QueueId);
            if (queue == null)
                return false;
            queue.QueueTime = DateTime.Parse(updateQueueModel.NewQueueTime);
           // var que = new Queues() { Id = updateQueueModel.QueueId, QueueTime = DateTime.Parse(updateQueueModel.NewQueueTime) };
            //context.queues.Attach(que);
            //var q = context.Entry(que).Property(q => q.QueueTime).IsModified = true;
            context.SaveChanges();
            return true;
        }
    }
}
