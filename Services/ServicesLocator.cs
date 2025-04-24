

public static class ServicesLocator
{
    static Dictionary<Type, object> _services = new();

    public static void Register<T>(T service)
    {
        if (_services.ContainsKey(typeof(T)))
        {
            throw new InvalidOperationException($"Service of type {typeof(T)} already registered");
        }
        else if (service == null) {
            throw new ArgumentNullException(nameof(service));
        }
        else {
        
            _services.Add(typeof(T), service);
        }
    }

    public static T Get<T>()
    {
        if (!_services.ContainsKey(typeof(T)))
        {
            throw new InvalidOperationException($"Service of type {typeof(T)} is not registered");
        }
        return (T) _services[typeof(T)];
    }
}

