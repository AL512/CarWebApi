using AutoMapper;
using System.Reflection;

namespace CarWebApi.Mappings
{
    /// <summary>
    /// Профили маппинга сборки
    /// </summary>
    public class AssemblyMappingProfile : Profile
    {
        /// <summary>
        /// Профили маппинга сборки
        /// </summary>
        /// <param name="assembly"></param>
        public AssemblyMappingProfile(Assembly assembly) =>
            ApplyMappingsFromAssembly(assembly);

        /// <summary>
        /// Применение маппинга из сборки приложения
        /// </summary>
        /// <param name="assembly">Сборка</param>
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            //Ищем типы, которые реализуют IMappWith
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            //Маппаем найденные типы
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
