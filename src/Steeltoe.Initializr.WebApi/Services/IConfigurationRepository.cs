using System.Threading.Tasks;
using Steeltoe.Initializr.WebApi.Models.Metadata;

namespace Steeltoe.Initializr.WebApi.Services
{
	/// <summary>
	/// Contract for configuration repository implementations.
	/// </summary>
	public interface IConfigurationRepository
	{
		/// <summary>
		/// Gets the project generation configuration.
		/// </summary>
		/// <returns>project generation configuration</returns>
		public Task<Configuration> GetConfiguration();
	}
}
