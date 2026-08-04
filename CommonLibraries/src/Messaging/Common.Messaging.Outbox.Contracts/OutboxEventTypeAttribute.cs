namespace Common.Messaging.Outbox.Contracts
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class OutboxEventTypeAttribute : Attribute
    {
        public string Name { get; }

        public int Version { get; }

        public string Identifier => $"{Name}.v{Version}";

        public OutboxEventTypeAttribute(string name, int version)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(version);

            Name = name;
            Version = version;
        }
    }
}
