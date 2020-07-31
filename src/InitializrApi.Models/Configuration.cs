// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Steeltoe.InitializrApi.Models
{
    /// <summary>
    /// A model of project configuration metadata used by Initializr UIs and clients.
    /// </summary>
    public sealed class Configuration
    {
        /// <summary>
        /// Gets or sets "About" the server from whence this configuration.
        /// </summary>
        public About About { get; set; }

        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        public Text Name { get; set; }

        /// <summary>
        /// Gets or sets the project description.
        /// </summary>
        public Text Description { get; set; }

        /// <summary>
        /// Gets or sets the Steeltoe releases.
        /// </summary>
        public SingleSelectList SteeltoeRelease { get; set; }

        /// <summary>
        /// Gets or sets the DotNet target frameworks.
        /// </summary>
        public SingleSelectList DotnetFrameworkTarget { get; set; }

        /// <summary>
        /// Gets or sets the DotNet templates.
        /// </summary>
        public SingleSelectList DotnetTemplate { get; set; }

        /// <summary>
        /// Gets or sets the DotNet languages.
        /// </summary>
        public SingleSelectList DotnetLanguage { get; set; }

        /// <summary>
        /// Gets or sets the project dependencies.
        /// </summary>
        public GroupList Dependencies { get; set; }

        /// <summary>
        /// HTML form text data.
        /// </summary>
        public class Text
        {
            /// <summary>
            /// Gets or sets the widget type.
            /// </summary>
            public string Type { get; set; } = "text";

            /// <summary>
            /// Gets or sets the default value.
            /// </summary>
            public string Default { get; set; }
        }

        /// <summary>
        /// Abstraction of an item in a list.
        /// </summary>
        public abstract class Item
        {
            private string _name;

            /// <summary>
            /// Gets or sets the item ID.
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets the item name.
            /// </summary>
            public string Name
            {
                get => _name ?? Id;
                set => _name = value;
            }
        }

        /// <summary>
        /// Abstraction of a list of items.
        /// </summary>
        public abstract class ItemList
        {
            /// <summary>
            /// Gets or sets the widget type.
            /// </summary>
            public string Type { get; set; }
        }

        /// <summary>
        /// Abstraction of an item in a single select list.
        /// </summary>
        public class SelectItem : Item
        {
        }

        /// <summary>
        /// Abstraction of a list of items that has 1 and only 1 selected item.
        /// </summary>
        public class SingleSelectList : ItemList
        {
            /// <summary>
            /// Gets or sets the default selection.
            /// </summary>
            public string Default { get; set; }

            /// <summary>
            /// Gets or sets the list items.
            /// </summary>
            public SelectItem[] Values { get; set; }
        }

        /// <summary>
        /// Abstraction of an item in a group list.
        /// </summary>
        public class GroupItem : Item
        {
            /// <summary>
            /// Gets or sets the description.
            /// </summary>
            public string Description { get; set; }
        }

        /// <summary>
        /// A grouping of items in a group item list.
        /// </summary>
        public class Group
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the items.
            /// </summary>
            public GroupItem[] Values { get; set; }
        }

        /// <summary>
        /// Abstraction of a list of items that can have several selections.  The items are logical grouped.
        /// </summary>
        public class GroupList : ItemList
        {
            /// <summary>
            /// Gets or sets the groups.
            /// </summary>
            public Group[] Values { get; set; }
        }
    }
}
