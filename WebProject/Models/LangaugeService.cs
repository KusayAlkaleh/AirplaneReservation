using Microsoft.Extensions.Localization;
using System.Reflection;

namespace WebProject.Models
{
    public class LangaugeService
    {
        private readonly IStringLocalizer _stringLocalizer;

        public LangaugeService(IStringLocalizerFactory factory)
        {
            var type = typeof(ShareResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _stringLocalizer = factory.Create("ShareResource", assemblyName.Name);
        }
        public LocalizedString Getkey(string key) 
        {
            return _stringLocalizer[key];
        }
    }
}
