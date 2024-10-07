using Orleans.Concurrency;
using Orleans.Runtime;
using Orleans.Serialization.Invocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld;

[MayInterleave(nameof(ArgHasInterleaveAttribute))]
public class SimpleGrain : Grain, ISimpleGrain
{

    public static bool ArgHasInterleaveAttribute(IInvokable req)
    {
        Console.WriteLine("May interleave call.");
        return true;
    }

    public Task Tell()
    {
        Console.WriteLine("Grain method call.");
        return Task.CompletedTask;
    }
}
