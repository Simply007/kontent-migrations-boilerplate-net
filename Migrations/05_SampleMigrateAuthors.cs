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
using Kentico.Kontent.Management.Modules.ModelBuilders;

namespace Kentico.Kontent.Management.Sample.Boilerplate.Migrations
{
    class SampleMigrateAuthors : IMigrationModule
    {
        public int Order => 5;

        public async Task RunAsync(ManagementClient client)
        {
            var blogType = await client.GetContentTypeAsync(Reference.ByCodename(Constants.BLOG_TYPE_CODENAME));
            var authorType = await client.GetContentTypeAsync(Reference.ByCodename(Constants.AUTHOR_TYPE_CODENAME));

            var blogAuthorElementId = blogType.Elements.First(x => x.Codename == "author").Id;

            var blogItemVariants = await client.ListLanguageVariantsByTypeAsync(Reference.ById(blogType.Id));

            var existingAuthors = new Dictionary<string, Guid>();

            foreach (var blogItemVariant in blogItemVariants)
            {
                // Find the value of the old author element
                string author = blogItemVariant.Elements.First(x => Guid.Parse(x.element.id) == blogAuthorElementId).value;

                // If the Author item doesn't exist -> create
                if (author != null && !existingAuthors.ContainsKey(author))
                {
                    var contentItem = await client.CreateContentItemAsync(new ContentItemCreateModel()
                    {
                        Name = author,
                        Type = Reference.ByCodename(Constants.AUTHOR_TYPE_CODENAME),
                    });

                    await client.UpsertLanguageVariantAsync(
                        new LanguageVariantIdentifier(Reference.ById(contentItem.Id), Reference.ByCodename(Constants.LANGUAGE_CODENAME)),
                        new LanguageVariantModel
                        {
                            Elements = ElementBuilder.GetElementsAsDynamic(new BaseElement[]
                            {
                                new TextElement
                                {
                                    // TODO reference identifier (to use codename?)
                                    Element = Reference.ById(authorType.Elements.First(x => x.Codename == Constants.AUTHOR_NAME_ELEMENT_CODENAME).Id),
                                    Value = author
                                }
                            })
                        }
                    );

                    existingAuthors.Add(author, contentItem.Id);
                }

                // Update blog item variant
                await client.UpsertLanguageVariantAsync(
                    new LanguageVariantIdentifier(Reference.ById(blogItemVariant.Item.Id.Value), Reference.ByCodename(Constants.LANGUAGE_CODENAME)),
                    new LanguageVariantModel
                    {
                        Elements = ElementBuilder.GetElementsAsDynamic(new BaseElement[]
                        {
                                new LinkedItemsElement
                                {
                                    Element = Reference.ById(blogType.Elements.First(x => x.Codename == Constants.BLOG_LINKED_AUTHOR_ELEMENT_CODENAME).Id),
                                    Value = new[] { Reference.ById(existingAuthors[author]) }
                                }
                        })
                    }
                );

            }

            System.Console.Out.WriteLine($"Migrated all authors from text fields to linked items element.");
        }
    }
}