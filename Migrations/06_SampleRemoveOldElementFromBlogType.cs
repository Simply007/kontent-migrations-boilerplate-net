using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kentico.Kontent.Boilerplate.Sample.Boilerplate.Migrations;
using Kentico.Kontent.Management.Models.Items;
using Kentico.Kontent.Management.Models.LanguageVariants;
using Kentico.Kontent.Management.Models.LanguageVariants.Elements;
using Kentico.Kontent.Management.Models.Shared;
using Kentico.Kontent.Management.Models.Types.Patch;

namespace Kentico.Kontent.Management.Sample.Boilerplate.Migrations
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