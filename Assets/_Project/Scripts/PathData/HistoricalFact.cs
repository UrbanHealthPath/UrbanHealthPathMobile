namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Class that holds data of a historical fact.
    /// </summary>
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