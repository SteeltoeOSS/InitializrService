namespace Steeltoe.InitializrApi.Models
{
    /// <summary>
    /// Application information, such as version.
    /// </summary>
    public class About
    {
        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the application vendor.
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// Gets or sets the application URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the application version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the application build source control commit ID.
        /// </summary>
        public string Commit { get; set; }
    }
}
