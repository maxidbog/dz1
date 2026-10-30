namespace OrderReporter.Domain.Exceptions;

public class ClientNotFoundException : Exception
{
    public ClientNotFoundException(long clientId)
        : base($"Клиент {clientId} не найден")
    {
    }
}
