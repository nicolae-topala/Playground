namespace RabbitMQ.BLL.Interfaces
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
