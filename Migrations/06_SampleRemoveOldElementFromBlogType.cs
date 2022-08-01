using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kontent.Ai.Boilerplate.Sample.Boilerplate.Migrations;
using Kontent.Ai.Management.Models.Items;
using Kontent.Ai.Management.Models.LanguageVariants;
using Kontent.Ai.Management.Models.LanguageVariants.Elements;
using Kontent.Ai.Management.Models.Shared;
using Kontent.Ai.Management.Models.Types.Patch;

namespace Kontent.Ai.Management.Sample.Boilerplate.Migrations
{
    class SampleRemoveOldElementFromBlogType : IMigrationModule
    {
        public int Order => 6;

        public async Task RunAsync(ManagementClient client)
        {
            await client.ModifyContentTypeAsync(
                Reference.ByCodename(Constants.BLOG_TYPE_CODENAME),
                new [] {
                    new ContentTypeRemovePatchModel
                    {
                        Path = "/elements/codename:author"
                    }
                }
            );

            System.Console.Out.WriteLine($"Removed deprecated author text element.");
        }
    }
}