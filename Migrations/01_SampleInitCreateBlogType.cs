using System.Collections.Generic;
using System.Threading.Tasks;
using Kontent.Ai.Boilerplate.Sample.Boilerplate.Migrations;
using Kontent.Ai.Management.Models.Types;
using Kontent.Ai.Management.Models.Types.Elements;
using Kontent.Ai.Management.Sample.Boilerplate;

namespace Kontent.Ai.Management.Sample.Boilerplate.Migrations
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
                Elements = new List<ElementMetadataBase> {
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
                    },
                    new MultipleChoiceElementMetadataModel
                    {
                        Name = "Is featured",
                        Codename = "is_featured",
                        IsRequired = false,
                        Mode = MultipleChoiceMode.Single,
                        Options = new[] {
                            new MultipleChoiceOptionModel
                            {
                                Name = "Yes",
                                Codename = "yes"
                            }
                        }
                    }
                }
            });

            System.Console.Out.WriteLine($"Contenty type with {response.Id} was created");
        }
    }
}