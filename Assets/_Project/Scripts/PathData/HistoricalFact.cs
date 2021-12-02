namespace PolSl.UrbanHealthPath.PathData
{
    public class HistoricalFact
    {
        private string _name;
        private string _description;

        public string HistoricalFactId { get; }

        public HistoricalFact(string historicalFactId, string name, string description)
        {
            HistoricalFactId = historicalFactId;
            _name = name;
            _description = description;
        }
    }
}