using System.Threading.Tasks;
using Kentico.Kontent.Boilerplate.Sample.Boilerplate.Migrations;
using Kentico.Kontent.Management.Models.Types;
using Kentico.Kontent.Management.Models.Types.Elements;
using Kentico.Kontent.Management.Sample.Boilerplate;

namespace Kentico.Kontent.Management.Sample.Boilerplate.Migrations
{
    public class SampleInitCreateBlogType : IMigrationModule
    {
        public int Order => 1;

        public async Task RunAsync(ManagementClient client)
        {
            var response = await client.CreateContentTypeAsync(new ContentTypeCreateModel()
            {
                Name = "Blog",
                Codename = Constants.BLOG_TYPE_CODENAME,
                Elements = new[] {
                    new TextElementMetadataModel()
                    {
                        Name= "Title",
                        Codename = "title",
                    },
                    new TextElementMetadataModel()
                    {
                        Name= "Author",
                        Codename = "author",
                    },
                    new TextElementMetadataModel()
                    {
                        Name= "Text",
                        Codename = "text",
                    }
                }
            });

            System.Console.Out.WriteLine($"Contenty type with ${response.Id} was created");
        }
    }
}