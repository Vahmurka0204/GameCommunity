namespace Library
{
    public interface IOracle
    {
        int MakeChoice(int min, int max);
    }

    public interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }

    public interface IObserver
    {
        void Update(Game game);
    }
}