﻿using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Core;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.DatatablesNetBs4;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.FontAwesome;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Select2;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Toastr;
using Volo.Abp.Modularity;

namespace Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Bundling
{
    [DependsOn(
        typeof(CoreStyleContributor),
        typeof(BootstrapStyleContributor),
        typeof(FontAwesomeStyleContributor),
        typeof(ToastrStyleBundleContributor),
        typeof(Select2StyleContributor),
        typeof(DatatablesNetBs4StyleContributor)
    )]
    public class SharedThemeGlobalStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddRange(new[]
            {
                "/libs/abp/aspnetcore-mvc-ui-theme-shared/datatables/datatables-styles.css"
            });
        }
    }
}