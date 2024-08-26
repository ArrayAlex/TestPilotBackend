namespace TestPilot.Helpers
{
    public class HelperMethods
    {
        public bool TryGetHttpMethod(string method, out HttpMethod httpMethod)
        {
            switch (method.ToUpper())
            {
                case "GET":
                    httpMethod = HttpMethod.Get;
                    return true;
                case "POST":
                    httpMethod = HttpMethod.Post;
                    return true;
                case "PUT":
                    httpMethod = HttpMethod.Put;
                    return true;
                case "DELETE":
                    httpMethod = HttpMethod.Delete;
                    return true;
                default:
                    httpMethod = null!;
                    return false;
            }
        }
    }
}