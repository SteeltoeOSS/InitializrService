namespace Steeltoe.Initializr.WebApi.Models.Metadata
{
	/// <summary>
	/// A model of configuration metadata used by Initializr UIs and clients.
	/// </summary>
	public class Configuration
	{
		/// <summary>
		/// Compares the specified object to this object.
		/// </summary>
		/// <param name="obj">other instance</param>
		/// <returns>whether objects are equal</returns>
		public override bool Equals(object obj)
		{
			return true;
		}

		/// <summary>
		/// Returns the hash code for this object.
		/// </summary>
		/// <returns>object hash code</returns>
		public override int GetHashCode()
		{
			return 0;
		}
	}
}
