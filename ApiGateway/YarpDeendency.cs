using Microsoft.OpenApi.Models;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Yarp.ReverseProxy.Configuration;
using Microsoft.OpenApi.Readers;

namespace ApiGateway
{
    public static class YarpDeendency
    {
        public static void MapGetSwaggerForYarp(this IEndpointRouteBuilder endpoints, IConfiguration configuration)
        {
            var clusters = configuration.GetSection("Gateway:Clusters");
            var routes = configuration.GetSection("Gateway:Routes").Get<List<RouteConfig>>();

            if (clusters != null)
            {
                foreach (var child in clusters.GetChildren())
                {
                    if (child.GetSection("Swagger").Exists())
                    {
                        var cluster = child.Get<ClusterConfig>();
                        var swagger = child.GetSection("Swagger").Get<GatewaySwaggerSpec>();

                        // Map swagger endpoint if we find a cluster with swagger configuration
                        endpoints.MapSwaggerSpecs(routes, cluster, swagger);
                    }
                }
            }
        }

        public static void MapSwaggerSpecs(this IEndpointRouteBuilder endpoints, List<RouteConfig> config, ClusterConfig cluster, GatewaySwaggerSpec swagger)
        {
            endpoints.Map(swagger.Endpoint, async context =>
            {
                var client = new HttpClient();
                var stream = await client.GetStreamAsync(swagger.Spec);

                var document = new OpenApiStreamReader().Read(stream, out var diagnostic);
                var rewrite = new OpenApiPaths();

                // map existing path
                var routes = config.Where(p => p.ClusterId == cluster.ClusterId);
                var hasCatchAll = routes != null && routes.Any(p => p.Match.Path.Contains("**catch-all"));

                foreach (var path in document.Paths)
                {

                    var rewritedPath = path.Key.Replace(swagger.TargetPath, swagger.OriginPath);

                    // there is a catch all, all route are elligible 
                    // or there is a route that match without any operation methods filtering
                    if (hasCatchAll || routes.Any(p => p.Match.Path.Equals(rewritedPath) && p.Match.Methods == null))
                    {
                        rewrite.Add(rewritedPath, path.Value);
                    }
                    else
                    {
                        // there is a route that match
                        var routeThatMatchPath = routes.Any(p => p.Match.Path.Equals(rewritedPath));
                        if (routeThatMatchPath)
                        {
                            // filter operation based on yarp config
                            var operationToRemoves = new List<OperationType>();
                            foreach (var operation in path.Value.Operations)
                            {
                                // match route and method
                                var hasRoute = routes.Any(
                                    p => p.Match.Path.Equals(rewritedPath) && p.Match.Methods.Contains(operation.Key.ToString().ToUpperInvariant())
                                );

                                if (!hasRoute)
                                {
                                    operationToRemoves.Add(operation.Key);
                                }
                            }

                            // remove operation routes
                            foreach (var operationToRemove in operationToRemoves)
                            {
                                path.Value.Operations.Remove(operationToRemove);
                            }

                            // add path if there is any operation
                            if (path.Value.Operations.Any())
                            {
                                rewrite.Add(rewritedPath, path.Value);
                            }
                        }
                    }
                }

                document.Paths = rewrite;

                var result = document.Serialize(OpenApiSpecVersion.OpenApi3_0, OpenApiFormat.Json);
                await context.Response.WriteAsync(
                    result
                );
            });
        }
    }
}
