namespace blog_common.Context
{
    public static class BaseContext
    {
        private static readonly AsyncLocal<string?> _asyncLocal = new();

        public static void SetCurrentId(string? id)
        {
            _asyncLocal.Value = id;
        }

        public static string? GetCurrentId()
        {
            return _asyncLocal.Value;
        }
        public static void RemoveCurrentId()
        {
            _asyncLocal.Value = null;
        }
    }
}
