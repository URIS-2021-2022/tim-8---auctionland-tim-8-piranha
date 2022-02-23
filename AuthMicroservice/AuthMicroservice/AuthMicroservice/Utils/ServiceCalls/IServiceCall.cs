namespace AuthMicroservice.Utils.ServiceCalls
{
    using System.Threading.Tasks;

    /// <summary>
    /// Service call interface.
    /// </summary>
    /// <typeparam name="T">Type of service.</typeparam>
    public interface IServiceCall<T>
    {
        /// <summary>
        /// Method used for sending an asynchronous request.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<T> SendGetRequestAsync(string url);
    }
}
