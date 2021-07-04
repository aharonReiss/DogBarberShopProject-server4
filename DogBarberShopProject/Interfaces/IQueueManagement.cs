using DogBarberShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogBarberShopProject.Interfaces
{
    public interface IQueueManagement
    {
        object getQueues();
        bool deleteQueue(int id, string token);
        bool updateQueue(UpdateQueueModel updateQueueModel,string token);
        bool addQueue(AddQueueModel addQueueModel);
    }
}
