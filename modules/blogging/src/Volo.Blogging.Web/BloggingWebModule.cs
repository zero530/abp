﻿using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AutoMapper;
using Volo.Abp.Localization;
using Volo.Abp.Localization.Resources.AbpValidation;
using Volo.Abp.Modularity;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Blogging.Localization;

namespace Volo.Blogging
{
    [DependsOn(
        typeof(BloggingHttpApiModule),
        typeof(AbpAspNetCoreMvcUiBootstrapModule),
        typeof(AbpAspNetCoreMvcUiBundlingModule),
        typeof(AbpAutoMapperModule)
    )]
    public class BloggingWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(BloggingResource), typeof(BloggingWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NavigationOptions>(options =>
            {
                options.MenuContributors.Add(new BloggingMenuContributor());
            });

            Configure<VirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BloggingWebModule>("Volo.Blogging");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Resources
                    .Get<BloggingResource>()
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddBaseTypes(typeof(AbpUiResource))
                    .AddVirtualJson("/Localization/Resources/Blogging/Web");
            });

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpBloggingWebAutoMapperProfile>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                var urlOptions = context.Services
                    .GetRequiredServiceLazy<IOptions<BloggingUrlOptions>>()
                    .Value.Value;

                var routePrefix = urlOptions.RoutePrefix;

                options.Conventions.AddPageRoute("/Blog/Posts/Index", routePrefix + "{blogShortName}");
                options.Conventions.AddPageRoute("/Blog/Posts/Detail", routePrefix + "{blogShortName}/{postUrl}");
                options.Conventions.AddPageRoute("/Blog/Posts/Edit", routePrefix + "{blogShortName}/posts/{postId}/edit");
                options.Conventions.AddPageRoute("/Blog/Posts/New", routePrefix + "{blogShortName}/posts/new");
            });
        }
    }
}
