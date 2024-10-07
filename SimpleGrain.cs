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
    bool interleavedOnce = false;

    public static bool ArgHasInterleaveAttribute(IInvokable req)
    {
        if (RequestContext.Get("MayInterleave") is bool mayInterleave && mayInterleave)
        {
            return mayInterleave;
        }
        return false;
    }

    public async Task Tell()
    {
        if (!interleavedOnce) {
            Console.WriteLine("Called without interleaving.");
            RequestContext.Set("MayInterleave", true);
            interleavedOnce = true;
            await GrainFactory.GetGrain<ISimpleGrain>(this.GetPrimaryKeyString()).Tell();
        }
        Console.WriteLine("Called with interleaving.");
        return;
    }
}
