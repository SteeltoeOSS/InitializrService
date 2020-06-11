namespace Steeltoe.Initializr.WebApi.Services
{
	/// <summary>
	/// MetadataRepository options.
	/// </summary>
	public class MetadataRepositoryOptions
	{
		/// <summary>
		/// appsettings.json key
		/// </summary>
		public const string MetatdataRepository = "MetadataRepository";

		/// <summary>
		/// Metatdata repository URI.
		/// </summary>
		public string Uri { get; set; }
	}
}
