using System.Text.Json.Nodes;

namespace JsonElement
{
    public class Response : AbstractJsonElement
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public Data? Data { get; set; }

        public static Response? Create(JsonNode? json)
        {
            if (json is null)
                return null;

            var response = new Response
            {
                Status = (string?)json["status"],
                Message = (string?)json["message"]
            };

            JsonObject? data = (JsonObject?)json["data"];
            if (data is null)
                return null;
            response.Data = Data.Create(data);

            return response;
        }
    }
}
