using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Outbox.Abstractions
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
