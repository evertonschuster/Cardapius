using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace BuildingBlock.Api.Modules
{
    /// <summary>
    /// Adds the route prefix to all actions 
    /// </summary>
    public class ModuleRoutingConvention : IApplicationModelConvention
    {
        private readonly IEnumerable<Module> _modules;

        public ModuleRoutingConvention(IEnumerable<Module> modules)
        {
            _modules = modules;
        }

        public void Apply(ApplicationModel application)
        {
            var modules = _modules.ToDictionary(e => e.Assembly, e => e);

            foreach (var controller in application.Controllers)
            {
                var module = modules[controller.ControllerType.Assembly];
                controller.RouteValues.Add("module", module?.RoutePrefix ?? string.Empty);
            }
        }
    }
}