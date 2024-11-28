using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Vila_WebAPI.Utility
{
    public class SwaggerVilaDocument : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;


        public SwaggerVilaDocument(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {

            foreach (var item in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(item.GroupName,
      new OpenApiInfo()
      {
          Title = $"Vila Api Version {item.ApiVersion}",
          Version = item.ApiVersion.ToString()
      });
            }







            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "comment.xml"));

        }
    }
}
