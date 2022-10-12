namespace desafio.feiras.infrastructure.mongodb.Repository
{
    public class MongoDBSettings
    {
        public const string CONFIG_KEY = "MongoDBSettings";

        public string DatabaseName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
