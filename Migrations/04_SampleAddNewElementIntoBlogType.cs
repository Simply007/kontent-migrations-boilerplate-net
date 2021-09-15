using System.Threading.Tasks;
using Kentico.Kontent.Boilerplate.Sample.Boilerplate.Migrations;
using Kentico.Kontent.Management.Models.Shared;
using Kentico.Kontent.Management.Models.Types;
using Kentico.Kontent.Management.Models.Types.Elements;
using Kentico.Kontent.Management.Models.Types.Patch;

namespace Kentico.Kontent.Management.Sample.Boilerplate.Migrations
{
    class SampleAddNewElementIntoBlogType : IMigrationModule
    {
        public int Order => 4;

        public async Task RunAsync(ManagementClient client)
        {
            var blogType = await client.GetContentTypeAsync(Reference.ByCodename(Constants.BLOG_TYPE_CODENAME));
            var authorType = await client.GetContentTypeAsync(Reference.ByCodename(Constants.AUTHOR_TYPE_CODENAME));

            var modifiedContentType = await client.ModifyContentTypeAsync(
                Reference.ById(blogType.Id),
                // TODO - strongly typed patch? - ElementMetadataBase[]
                new[]
                {
                    new ContentTypeAddIntoPatchModel()
                    {
                        Path = "/elements",
                        Value = new LinkedItemsElementMetadataModel
                        {
                            Name = "Linked author",
                            Codename = Constants.BLOG_LINKED_AUTHOR_ELEMENT_CODENAME,
                            ItemCountLimit = new LimitModel
                            {
                                Condition = Models.Types.LimitType.AtMost,
                                Value = 1
                            },
                            AllowedTypes = new[] { ObjectIdentifier.ById(authorType.Id) }
                        }
                    },
                    // Tried to split as it is in boilerplate but does not work
                    // new ContentTypeAddIntoPatchModel()
                    // {
                    //     Path = $"/elements/codename:{Constants.BLOG_LINKED_AUTHOR_ELEMENT_CODENAME}/allowed_content_types",
                    //     Value =  new LinkedItemsElementMetadataModel
                    //     {
                    //         AllowedTypes = new[] { ObjectIdentifier.ById(authorType.Id) }
                    //     }
                    // },
                    // TODO - add guidelines to old text element about deprecation
                });

            System.Console.Out.WriteLine($"Contenty type with ${modifiedContentType.Id} was updated");
        }
    }
}