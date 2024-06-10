namespace Questao2.External.Http
{
    public class Request
    {
        public Request(string uri,
                       HttpMethod httpMethod,
                       string? body = null,
                       Dictionary<string, string>? parameters = null,
                       Dictionary<string, string>? headers = null)
        {
            Uri = uri;
            HttpMethod = httpMethod;
            Body = body;
            Parameters = parameters;
            Headers = headers;
        }

        public string Uri { get; }
        public HttpMethod HttpMethod { get; }
        public string? Body { get; private set; }
        public Dictionary<string, string>? Parameters { get; private set; }
        public Dictionary<string, string>? Headers { get; private set; } =
               new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" }
                };

        public void SetRequestBody(string body) => Body = body;
        public void SetHeaders(Dictionary<string, string> headers) => Headers = headers;
        public void SetQueryParameters(Dictionary<string, string> parameters) => Parameters = parameters;
    }
}
