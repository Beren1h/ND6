using System.Collections.Concurrent;
using Microsoft.VisualBasic;

namespace nd6;

public class Analytics
{
    public ConcurrentDictionary<string, int> Results { get; set; } = [];
}
