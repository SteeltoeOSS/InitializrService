using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Steeltoe.Initializr.WebApi.Server.Models.Metadata;

namespace Steeltoe.Initializr.WebApi.Server.Data
{
	/// <summary>
	/// A configuration repository that loads configuration from a local JSON file.
	/// </summary>
	public class LocalConfigurationRepository : IConfigurationRepository
	{
		/// <summary>
		/// Gets the prohect configuration defined by the local JSON file.
		/// </summary>
		/// <returns>project generation configuration</returns>
		public Task<Configuration> GetConfiguration()
		{
			var config = new Configuration();
			var result = new TaskCompletionSource<Configuration>();
			result.SetResult(config);
			return result.Task;
		}
	}
}
