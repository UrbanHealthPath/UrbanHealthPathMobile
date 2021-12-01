namespace PolSl.UrbanHealthPath.PathData
{
    public class HistoricalFact
    {
        private string _historicalFactId;
        private string _name;
        private string _description;

        public HistoricalFact(string historicalFactId, string name, string description)
        {
            _historicalFactId = historicalFactId;
            _name = name;
            _description = description;
        }
    }
}