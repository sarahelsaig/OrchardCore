using Microsoft.Extensions.Localization;
using OrchardCore.Contents;
using OrchardCore.ContentTypes.Controllers;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;

namespace OrchardCore.ContentTypes;

public sealed class AdminMenu : AdminNavigationProvider
{
    private static readonly string _adminControllerName = typeof(AdminController).ControllerName();

    internal readonly IStringLocalizer S;

    public AdminMenu(IStringLocalizer<AdminMenu> stringLocalizer)
    {
        S = stringLocalizer;
    }

    protected override ValueTask BuildAsync(NavigationBuilder builder)
    {
        if (NavigationHelper.UseLegacyFormat())
        {
            builder
                .Add(S["Content"], content => content
                    .Add(S["Content Definition"], S["Content Definition"].PrefixPosition("9"), contentDefinition => contentDefinition
                        .Add(S["Content Types"], S["Content Types"].PrefixPosition("1"), contentTypes => contentTypes
                            .Action(nameof(AdminController.List), _adminControllerName, "OrchardCore.ContentTypes")
                            .Permission(ContentTypesPermissions.ViewContentTypes)
                            .LocalNav()
                        )
                        .Add(S["Content Parts"], S["Content Parts"].PrefixPosition("2"), contentParts => contentParts
                            .Action(nameof(AdminController.ListParts), _adminControllerName, "OrchardCore.ContentTypes")
                            .Permission(ContentTypesPermissions.ViewContentTypes)
                            .LocalNav()
                        )
                    )
                );
        }

        builder
            .Add(S["Design"], content => content
                .Add(S["Content Definition"], S["Content Definition"].PrefixPosition(), contentDefinition => contentDefinition
                    .Add(S["Content Types"], S["Content Types"].PrefixPosition("1"), contentTypes => contentTypes
                        .Action(nameof(AdminController.List), _adminControllerName, "OrchardCore.ContentTypes")
                        .Permission(ContentTypesPermissions.ViewContentTypes)
                        .LocalNav()
                    )
                    .Add(S["Content Parts"], S["Content Parts"].PrefixPosition("2"), contentParts => contentParts
                        .Action(nameof(AdminController.ListParts), _adminControllerName, "OrchardCore.ContentTypes")
                        .Permission(ContentTypesPermissions.ViewContentTypes)
                        .LocalNav()
                    )
                )
            );

        return ValueTask.CompletedTask;
    }
}
