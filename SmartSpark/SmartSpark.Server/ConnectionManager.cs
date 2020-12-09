using System;
using System.Collections.Generic;

namespace SmartSpark.Server
{
    public class ConnectionManager
    {
        private static readonly Dictionary<Guid, HashSet<String>> Connections = new Dictionary<Guid, HashSet<String>>();

        public void AddConnection(Guid userId, String connectionId)
        {
            lock (Connections)
            {
                if (!Connections.TryGetValue(userId, out var value))
                {
                    Connections[userId] = new HashSet<string>();
                }

                Connections[userId].Add(connectionId);
            }
        }

        public void RemoveConnection(Guid userId, String connectionId)
        {
            lock (Connections)
            {
                if (Connections.TryGetValue(userId, out HashSet<String> userConnections))
                {
                    if (userConnections.Count == 1)
                        Connections.Remove(userId);
                    else
                        userConnections.Remove(connectionId);
                }
            }
        }

        public HashSet<String> GetUserConnections(Guid userId)
        {
            lock (Connections)
            {
                if (Connections.TryGetValue(userId, out HashSet<String> connections))
                    return new HashSet<String>(connections);

                return new HashSet<String>();
            }
        }
    }
}