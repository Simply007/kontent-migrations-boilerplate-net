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

            var authorLanguageVariants = await client.ListLanguageVariantsByTypeAsync(Reference.ById(authorType.Id));
            foreach (var authorVariant in authorLanguageVariants)
            {
                await client.PublishLanguageVariant(
                    new LanguageVariantIdentifier(
                        Reference.ById(authorVariant.Item.Id.Value),
                         Reference.ByCodename(Constants.LANGUAGE_CODENAME))
                    );
            }

            var blogLanguageVariants = await client.ListLanguageVariantsByTypeAsync(Reference.ById(blogType.Id));
            foreach (var blogvariant in blogLanguageVariants)
            {
                await client.PublishLanguageVariant(
                    new LanguageVariantIdentifier(
                        Reference.ById(blogvariant.Item.Id.Value),
                         Reference.ByCodename(Constants.LANGUAGE_CODENAME))
                    );
            }

            System.Console.Out.WriteLine($"All authors and blogs wer successfully published");
        }
    }
}