namespace AuthMicroservice.Initializers.Security
{
    using System;

    public class AudienceModel
    {
        public string Secret { get; set; }

        public string Iss { get; set; }

        public string Aud { get; set; }
    }
}
