using System;

public static class Connections
{
    public const string CloudBlobStorageConnection = "DefaultEndpointsProtocol=https;AccountName=nightowl;AccountKey=yS4NdaGYbrlGaW14Q0QRWbk3vpO0uLJ4aAlUGUQVoV5ILhmLnHz9NM2d7QY4ILMmmiUVzbytW8hL4LRdbvGOlw==;EndpointSuffix=core.windows.net";
    public const string sqlConnection = @"Server = tcp:nightowl.database.windows.net,1433; Initial Catalog = nightOwl; Persist Security Info = False; User ID =aurimasko; Password = Topas123; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
}
