namespace APIGateway.Middlewares
{
    /// <summary>
    /// Audience model.
    /// </summary>
    public class AudienceModel
    {
        /// <summary>
        /// Secret.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Issuer.
        /// </summary>
        public string Iss { get; set; }

        /// <summary>
        /// Audience.
        /// </summary>
        public string Aud { get; set; }
    }
}
