using Microsoft.AspNetCore.Mvc;
using System;

namespace QueryServiceSystem2.Infrastructure.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetControllerName(this Type controllerType)
            => controllerType.Name.Replace(nameof(Controller), string.Empty);
    }
}
