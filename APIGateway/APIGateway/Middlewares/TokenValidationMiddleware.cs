namespace APIGateway.Middlewares
{
    using APIGateway.Consts;
    using System;

    public class TokenValidationMiddleware
    {
        public static void ValidateToken(String token)
        {
            if (token != null)
            {
                // token exists and needs to be validated
                // TODO: add logic for calling the Auth Resource to validate the token
                // [...]
                // [...]
                // [...]
            }
            else
            {
                //throw new UnauthenticatedException("You must provide a token to access this route!", GeneralConsts.MICROSERVICE_NAME);
            }
        }
    }
}
