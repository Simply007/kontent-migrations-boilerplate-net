using System.Threading.Tasks;
using Kentico.Kontent.Management.Models.Shared;
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
            var contentGroupExternalId = "personal_details";
            var response = await client.CreateContentTypeAsync(new ContentTypeCreateModel
            {
                Name = "Author",
                Codename = Constants.AUTHOR_TYPE_CODENAME,
                Elements = new[] {
                    new TextElementMetadataModel
                    {
                        Name = "Name",
                        Codename = "name",
                        ContentGroup = Reference.ByExternalId(contentGroupExternalId)
                        
                    },
                    new TextElementMetadataModel
                    {
                        Name = "Twitter handle",
                        Codename = "twitter_handle",
                        ContentGroup = Reference.ByExternalId(contentGroupExternalId)
                    }
                },
                ContentGroups = new[]
                {
                    new ContentGroupModel
                    {
                        Name = "Personal details",
                        ExternalId = contentGroupExternalId
                    }
                }
            });

            System.Console.Out.WriteLine($"Contenty type with {response.Id} was created");

        }
    }
}
