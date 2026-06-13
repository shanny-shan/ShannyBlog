using blog_common.Enums;

namespace blog_server.Annotatin
{
    public class AutoFill
    {
        [AttributeUsage(AttributeTargets.Method)]
        public class AutoFillAttribute : Attribute
        {
            public OperationType Value { get; }

            public AutoFillAttribute(OperationType value)
            {
                Value = value;
            }
        }
    }
}
