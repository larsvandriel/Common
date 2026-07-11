namespace Common.Results
{
    public sealed class ValidationErrors
    {
        private readonly Dictionary<string, List<string>> _errors = [];

        public void Add(string key, string message)
        {
            if(!_errors.TryGetValue(key, out var list))
            {
                list = [];
                _errors[key] = list;
            }

            list.Add(message);
        }

        public bool Any => _errors.Count > 0;

        public Dictionary<string, string[]> ToDictionary() =>
            _errors.ToDictionary(x => x.Key, x => x.Value.ToArray());
    }
}
