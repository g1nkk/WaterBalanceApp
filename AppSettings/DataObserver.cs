using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterBalance
{
    internal class DataObserver
    {

        List<IDataSubscriber> subscribers;

        public DataObserver()
        {
            subscribers = new();
        }

        public void Subscribe(IDataSubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void Unsubscribe(IDataSubscriber subscriber)
        {
            subscribers.Remove(subscriber);
        }

        public void NotifySubscribers()
        {
            foreach (IDataSubscriber subscriber in subscribers)
                subscriber.Update();
        }
    }

    public interface IDataSubscriber
    {
        void Update();
    }
}

