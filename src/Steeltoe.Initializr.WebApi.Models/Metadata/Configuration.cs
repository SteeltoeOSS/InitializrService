// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Steeltoe.Initializr.WebApi.Models.Metadata
{
    /// <summary>
    /// A model of configuration metadata used by Initializr UIs and clients.
    /// </summary>
    public sealed class Configuration
    {
        public Product About { get; set; }

        /// <summary>
        /// Steeltoe releases
        /// </summary>
        public SingleSelectList Releases { get; set; }

        /// <summary>
        /// DotNet target frameworks
        /// </summary>
        public SingleSelectList Targets { get; set; }

        /// <summary>
        /// DotNet templates
        /// </summary>
        public SingleSelectList Templates { get; set; }

        /// <summary>
        /// DotNet languages
        /// </summary>
        public SingleSelectList Languages { get; set; }

        /// <summary>
        /// Project dependencies
        /// </summary>
        public GroupList Dependencies { get; set; }

        /// <summary>
        /// Product information, such as version.
        /// </summary>
        public class Product
        {
            /// <summary>
            /// Gets or sets the product name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the product vendor.
            /// </summary>
            public string Vendor { get; set; }

            /// <summary>
            /// Gets or sets the product URL.
            /// </summary>
            public string Url { get; set; }

            /// <summary>
            /// Gets or sets the product version.
            /// </summary>
            public string Version { get; set; }

            /// <summary>
            /// Gets or sets the product build source control commit ID.
            /// </summary>
            public string Commit { get; set; }
        }


        public abstract class Item
        {
            public string Id { get; set; }

            private string _name;

            public string Name
            {
                get => _name ?? Id;
                set => _name = value;
            }
        }

        public abstract class ItemList
        {
            public string Type { get; set; }
        }

        public class SelectItem : Item
        {
        }

        public class SingleSelectList : ItemList
        {
            public string Default { get; set; }

            public SelectItem[] Values { get; set; }
        }

        public class GroupItem : Item
        {
            public string Description { get; set; }
        }

        public class Group
        {
            public string Name { get; set; }

            public GroupItem[] Values { get; set; }
        }

        public class GroupList : ItemList
        {
            public Group[] Values { get; set; }
        }
    }
}
