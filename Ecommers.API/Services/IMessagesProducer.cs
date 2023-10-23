using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommers.API.Services
{
    public interface IMessagesProducer
    {
        public void SendingMessages<T>(T message);
    }
}