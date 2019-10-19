namespace MovieCollection.TVMaze
{
    public class UrlParameter
    {
        public UrlParameter(string key, string value)
            : base()
        {
            Key = key;
            Value = value;
        }

        public UrlParameter(string key, object value)
            : base()
        {
            if (value is null)
            {
                throw new System.ArgumentNullException(nameof(value));
            }

            Key = key;
            Value = value.ToString();
        }

        public string Key { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Key}={Value}";
        }
    }
}
