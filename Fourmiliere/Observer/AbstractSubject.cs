using System.Collections.Generic;

namespace Fourmiliere.Observer
{
    public abstract class AbstractSubject
    {
        private readonly List<AbstractObserver> observerList = new List<AbstractObserver>();

        public void Attach(AbstractObserver observer)
        {
            observerList.Add(observer);
        }

        public void Detach(AbstractObserver observer)
        {
            observerList.Remove(observer);
        }

        public void Notify()
        {
            foreach (AbstractObserver abstractObserver in observerList)
            {
                abstractObserver.Update();
            }
        }
    }
}