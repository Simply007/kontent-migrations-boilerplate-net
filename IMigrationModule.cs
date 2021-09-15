using System.Threading.Tasks;

namespace Kentico.Kontent.Management.Sample.Boilerplate
{
    public interface IMigrationModule
    {
        int Order { get; }

        Task RunAsync(ManagementClient client);
    }
}
