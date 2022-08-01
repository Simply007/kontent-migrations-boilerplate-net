using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kontent.Ai.Management.Configuration;
using Newtonsoft.Json.Linq;

namespace Kontent.Ai.Management.Sample.Boilerplate
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello from migration tool!");


            var environments = ReadEnvironments();

            var client = new ManagementClient(new ManagementOptions()
            {
                ProjectId = environments.First.First.Value<String>("projectId"),
                ApiKey = environments.First.First.Value<String>("apiKey")
            });

            var migrations = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IMigrationModule).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => (IMigrationModule)Activator.CreateInstance(x))
                .OrderBy(x => x.Order);
            // TODO add some waiting to fulfil the rate limitations
            foreach (var migration in migrations) {
                await migration.RunAsync(client);
            }
        }

        static JObject ReadEnvironments()
        {
            using (StreamReader file = new StreamReader("./.environments.json"))
            {
                string json = file.ReadToEnd();
                return JObject.Parse(json);
            }
        }
    }
}
