using System.Threading.Tasks;
using Kentico.Kontent.Management.Models.Items;
using Kentico.Kontent.Management.Sample.Boilerplate;
using Kentico.Kontent.Management.Models.Shared;
using Kentico.Kontent.Management.Models.LanguageVariants;
using Kentico.Kontent.Management.Models.LanguageVariants.Elements;
using System.Linq;
using Kentico.Kontent.Management.Models.Types.Elements;
using Kentico.Kontent.Management.Models.Types;
using Kentico.Kontent.Boilerplate.Sample.Boilerplate.Migrations;

namespace Kentico.Kontent.Management.Sample.Boilerplate.Migrations
{
    public class SampleCreateAuthorType : IMigrationModule
    {
        public int Order => 3;

        public async Task RunAsync(ManagementClient client)
        {
            var response = await client.CreateContentTypeAsync(new ContentTypeCreateModel
            {
                Name = "Author",
                Codename = Constants.AUTHOR_TYPE_CODENAME,
                Elements = new[] {
                    new TextElementMetadataModel
                    {
                        Name = "Name",
                        Codename = "name",
                        
                    },
                    new TextElementMetadataModel
                    {
                        Name = "Twitter handle",
                        Codename = "twitter_handle",
                    }
                }
            });

            System.Console.Out.WriteLine($"Contenty type with ${response.Id} was created");

        }
    }
}
