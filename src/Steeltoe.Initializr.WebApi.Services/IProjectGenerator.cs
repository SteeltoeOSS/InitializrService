using System.IO;
using System.Threading.Tasks;
using Steeltoe.Initializr.WebApi.Models.Project;

namespace Steeltoe.Initializr.WebApi.Services
{
    /// <summary>
    /// Contract for project generator implementations.
    /// </summary>
    public interface IProjectGenerator
    {
        /// <summary>
        /// Generates a project as a byte stream.
        /// </summary>
        /// <param name="specification">Project configuration</param>
        /// <returns>project bundle  byte stream</returns>
        public Task<Stream> GenerateProject(Specification specification);
    }
}
