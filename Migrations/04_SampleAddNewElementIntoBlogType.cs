using System.Threading.Tasks;
using Kontent.Ai.Boilerplate.Sample.Boilerplate.Migrations;
using Kontent.Ai.Management.Models.Shared;
using Kontent.Ai.Management.Models.Types;
using Kontent.Ai.Management.Models.Types.Elements;
using Kontent.Ai.Management.Models.Types.Patch;

namespace Kontent.Ai.Management.Sample.Boilerplate.Migrations
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
                            AllowedTypes = new[] { Reference.ById(authorType.Id) }
                        }
                    },
                    // TODO - add guidelines to old text element about deprecation
                });

            System.Console.Out.WriteLine($"Contenty type with {modifiedContentType.Id} was updated");
        }
    }
}