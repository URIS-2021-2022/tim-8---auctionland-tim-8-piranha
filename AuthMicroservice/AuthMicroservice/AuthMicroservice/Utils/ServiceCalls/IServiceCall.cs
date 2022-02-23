namespace AuthMicroservice.Utils.ServiceCalls
{
    using System.Threading.Tasks;

    public interface IServiceCall<T>
    {
        Task<T> SendGetRequestAsync(string url);
    }
}
