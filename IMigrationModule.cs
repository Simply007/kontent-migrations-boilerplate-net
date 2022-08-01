using System.Threading.Tasks;

namespace Kontent.Ai.Management.Sample.Boilerplate
{
    public interface IMigrationModule
    {
        int Order { get; }

        Task RunAsync(ManagementClient client);
    }
}
