namespace MultiFilter.Data
{
    public class DataModel
    {
        public string Shortcut { get; set; }

        public string Title { get; set; }

        public string FilterValue { get; set; }

        public DataModel(string shortcut, string title, string filtervalue)
        {
            Shortcut = shortcut;
            Title = title;
            FilterValue = filtervalue;
        }

        public DataModel()
        {

        }
    }
}
