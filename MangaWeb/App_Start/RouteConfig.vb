Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

Public Module RouteConfig
    Public Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

        routes.MapRoute(
            name:="Default",
            url:="{controller}/{action}/{id}",
            defaults:=New With {.controller = "Usuarios", .action = "Index", .id = UrlParameter.Optional}
        )

        ' Rota personalizada para a página Logado.vbhtml
        routes.MapRoute(
            name:="Logado",
            url:="{controller}/{action}",
            defaults:=New With {.controller = "Usuarios", .action = "Logado"}
        )

        ' Rota personalizada para a página Login.vbhtml
        routes.MapRoute(
            name:="Login",
            url:="{controller}/{action}",
            defaults:=New With {.controller = "Usuarios", .action = "Login"}
        )

        ' Rota personalizada para a página EditarSenha.vbhtml
        routes.MapRoute(
            name:="EditarSenha",
            url:="{controller}/{action}",
            defaults:=New With {.controller = "Usuarios", .action = "EditarSenha"}
        )

    End Sub
End Module