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
    class SamplePublish : IMigrationModule
    {
        public int Order => 7;

        public async Task RunAsync(ManagementClient client)
        {
            var blogType = await client.GetContentTypeAsync(Reference.ByCodename(Constants.BLOG_TYPE_CODENAME));
            var authorType = await client.GetContentTypeAsync(Reference.ByCodename(Constants.AUTHOR_TYPE_CODENAME));

            // TODO - missing listing by type endpoint https://docs.kontent.ai/reference/management-api-v2#operation/list-language-variants-by-type
            var items = await client.ListContentItemsAsync();


            var authorVariants = items.Where(x => x.Type.Id == authorType.Id).Select(async item =>
            {
                var languageVariants = await client.ListContentItemVariantsAsync(Reference.ById(item.Id));
                return languageVariants.FirstOrDefault();
            }).Select(x => x.Result);
            foreach (var authorVariant in authorVariants)
            {
                await client.PublishContentItemVariant(
                    new ContentItemVariantIdentifier(
                        Reference.ById(authorVariant.Item.Id),
                         Reference.ByCodename(Constants.LANGUAGE_CODENAME))
                    );
            }

            var blogVariants = items.Where(x => x.Type.Id == blogType.Id).Select(async item =>
            {
                var languageVariants = await client.ListContentItemVariantsAsync(Reference.ById(item.Id));
                return languageVariants.FirstOrDefault();
            }).Select(x => x.Result);
            foreach (var blogvariant in blogVariants)
            {
                await client.PublishContentItemVariant(
                    new ContentItemVariantIdentifier(
                        Reference.ById(blogvariant.Item.Id),
                         Reference.ByCodename(Constants.LANGUAGE_CODENAME))
                    );
            }

            System.Console.Out.WriteLine($"All authors and blogs wer successfully published");
        }
    }
}