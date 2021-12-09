using System.Threading.Tasks;
using Kentico.Kontent.Management.Models.Items;
using Kentico.Kontent.Management.Models.Shared;
using Kentico.Kontent.Management.Models.LanguageVariants;
using Kentico.Kontent.Management.Models.LanguageVariants.Elements;
using System.Linq;
using Kentico.Kontent.Boilerplate.Sample.Boilerplate.Migrations;
using Kentico.Kontent.Management.Models.Types.Elements;

namespace Kentico.Kontent.Management.Sample.Boilerplate.Migrations
{
    public class SampleInitCreateBlogItems : IMigrationModule
    {
        public int Order => 2;

        public async Task RunAsync(ManagementClient client)
        {
            var blogType = await client.GetContentTypeAsync(Reference.ByCodename(Constants.BLOG_TYPE_CODENAME));

            for (int i = 0; i < 9; i++)
            {

                var contentItem = await client.CreateContentItemAsync(new ContentItemCreateModel()
                {
                    Type = Reference.ByCodename(Constants.BLOG_TYPE_CODENAME),
                    Name = $"About coffee no {i}",
                });
                System.Console.Out.WriteLine($"Contenty item with ${contentItem.Id} was created");


                var contentItemVariant = await client.UpsertLanguageVariantAsync(
                new LanguageVariantIdentifier(Reference.ById(contentItem.Id), Reference.ByCodename(Constants.LANGUAGE_CODENAME)),
                new LanguageVariantModel
                {
                    Elements = new BaseElement[]
                    {
                        new TextElement
                        {
                            // TODO reference
                            Element = Reference.ById(blogType.Elements.First(x => x.Codename == "title").Id),
                            Value = $"Coffee no. {i}"
                        },
                        new TextElement
                        {
                            Element = Reference.ById(blogType.Elements.First(x => x.Codename == "author").Id),
                            Value = i%2 == 0 ? "John Doe" : "Jane Doe"
                        },
                        new TextElement
                        {
                            Element = Reference.ById(blogType.Elements.First(x => x.Codename == "text").Id),
                            Value = $"Coffee is a brewed drink prepared from roasted coffee beans, the seeds of berries from certain Coffee species. no. {i}"
                        },
                        new MultipleChoiceElement
                        {
                            Element = Reference.ById(blogType.Elements.First(x => x.Codename == "is_featured").Id),
                            Value =
                            (i%2 == 0)
                            ? new[]
                            {
                                Reference.ById(
                                    blogType.Elements
                                        .OfType<MultipleChoiceElementMetadataModel>()
                                        .First(x => x.Codename  == "is_featured")
                                        .Options
                                        .First(options => options.Codename == "yes")
                                        .Id
                                    )
                            }
                            : new Reference[]{}
                        }
                    }
                });
                System.Console.Out.WriteLine($"Contenty item variant with ${contentItemVariant.Item.Id} was created");
            }

        }
    }
}