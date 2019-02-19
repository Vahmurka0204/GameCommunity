using System.Collections.Generic;

namespace Library
{
    public class Community: ISubject
    {
        public List<Game> CommunityEvents;
        public List<IObserver> ListForNotifications;
        public void CreateEvent(string name,string date, int numberQuestion)
        {
            Game newGame = new Game(name, date, numberQuestion);
            CommunityEvents.Add( newGame);
            NotifyObservers();

        }

        public void NotifyObservers()
        {
            Game lastEvent = CommunityEvents[CommunityEvents.Count - 1];
            foreach(IObserver t in ListForNotifications)
            {
                t.Update(lastEvent);
            }
        }

        public void RegisterObserver(IObserver observer)
        {
            ListForNotifications.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            ListForNotifications.Remove(observer);
        }
    }

}
