using System;
using System.Collections;
public interface IObservable
{
	void AddObserver(IObserver observer);
	void RemoveObserver(IObserver observer);

	void NotifyObservers(object arg);
}

public interface IObserver
{
	void Update(IObservable observable, object arg);
}

public class Observable : IObservable
{
	protected ArrayList observers = new ArrayList();

	public void AddObserver(IObserver observer)
	{
		observers.Add(observer);
	}

	public void RemoveObserver(IObserver observer)
	{
		observers.Remove(observer);
	}

	public void NotifyObservers()
	{
		NotifyObservers(null);
	}

	public void NotifyObservers(object arg)
	{
		foreach (IObserver observer in observers)
			observer.Update(this, arg);
	}
}