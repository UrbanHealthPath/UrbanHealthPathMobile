namespace PolSl.UrbanHealthPath.PathData
{
    public class HistoricalFact
    {
        public string Name { get; }
        public string Description { get; }
        public string HistoricalFactId { get; }

        public HistoricalFact(string historicalFactId, string name, string description)
        {
            HistoricalFactId = historicalFactId;
            Name = name;
            Description = description;
        }
    }
}