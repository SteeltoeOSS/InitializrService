namespace Steeltoe.Initializr.WebApi.Models.Metadata
{
	/// <summary>
	/// A model of configuration metadata used by Initializr UIs and clients.
	/// </summary>
	public class Configuration
	{
		/// <summary>
		/// Steeltoe releases
		/// </summary>
		public ItemList Releases { get; set; }

		/// <summary>
		/// DotNet target frameworks
		/// </summary>
		public ItemList Targets { get; set; }

		/// <summary>
		/// DotNet templates
		/// </summary>
		public ItemList Templates { get; set; }

		/// <summary>
		/// DotNet languages
		/// </summary>
		public ItemList Languages { get; set; }

		/// <summary>
		/// Project dependencies
		/// </summary>
		public GroupItemList Dependencies { get; set; }

		/// <summary>
		/// Compares the specified Configuration to this object..
		/// </summary>
		/// <param name="that">other Configuration</param>
		/// <returns>whether Configurations are equal</returns>
		public bool Equals(Configuration that)
		{
			if (that == null)
			{
				return false;
			}

			if (!ItemList.Equal(Releases, that.Releases))
			{
				return false;
			}

			if (!ItemList.Equal(Targets, that.Targets))
			{
				return false;
			}

			if (!ItemList.Equal(Templates, that.Templates))
			{
				return false;
			}

			if (!ItemList.Equal(Languages, that.Languages))
			{
				return false;
			}

			if (!GroupItemList.Equal(Dependencies, that.Dependencies))
			{
				return false;
			}

			return true;
		}

		public class Item
		{
			public string Id { get; set; }

			private string _name;

			public string Name
			{
				get => _name ?? Id;
				set => _name = value;
			}

			public bool Equals(Item that)
			{
				if (that == null)
				{
					return false;
				}

				if (Id != null || that.Id != null)
				{
					if (Id == null)
					{
						return false;
					}

					if (!Id.Equals(that.Id))
					{
						return false;
					}
				}

				if (Name != null || that.Name != null)
				{
					if (Name == null)
					{
						return false;
					}

					if (!Name.Equals(that.Name))
					{
						return false;
					}
				}

				return true;
			}
		}

		public class ItemList
		{
			public string Default { get; set; }
			public string Type { get; set; } = "single-select";
			public Item[] Values { get; set; }

			public static bool Equal(ItemList a, ItemList b)
			{
				if (a != null || b != null)
				{
					if (a == null)
					{
						return false;
					}

					if (!a.Equals(b))
					{
						return false;
					}
				}

				return true;
			}

			public bool Equals(ItemList that)
			{
				if (that == null)
				{
					return false;
				}

				if (Default != null || that.Default != null)
				{
					if (Default == null)
					{
						return false;
					}

					if (!Default.Equals(that.Default))
					{
						return false;
					}
				}

				if (Type != null || that.Type != null)
				{
					if (Type == null)
					{
						return false;
					}

					if (!Type.Equals(that.Type))
					{
						return false;
					}
				}

				if (Values != null || that.Values != null)
				{
					if (Values == null || that.Values == null)
					{
						return false;
					}

					if (Values.Length != that.Values.Length)
					{
						return false;
					}

					for (int i = 0; i < Values.Length; ++i)
					{
						var thisValue = Values[i];
						var thatValue = that.Values[i];
						if (thisValue != null || thatValue != null)
						{
							if (thisValue == null || thatValue == null)
							{
								return false;
							}

							if (!thisValue.Equals(thatValue))
							{
								return false;
							}
						}
					}
				}

				return true;
			}
		}

		public class GroupItem : Item
		{
			public string Description { get; set; }

			public bool Equals(GroupItem that)
			{
				if (!base.Equals(that))
				{
					return false;
				}

				if (Description != null || that.Description != null)
				{
					if (Description == null)
					{
						return false;
					}

					if (!Description.Equals(that.Description))
					{
						return false;
					}
				}

				return true;
			}
		}

		public class Group
		{
			public string Name { get; set; }

			public GroupItem[] Values { get; set; }

			public bool Equals(Group that)
			{
				if (that == null)
				{
					return false;
				}

				if (Name != null || that.Name != null)
				{
					if (Name == null)
					{
						return false;
					}

					if (!Name.Equals(that.Name))
					{
						return false;
					}
				}

				if (Values != null || that.Values != null)
				{
					if (Values == null || that.Values == null)
					{
						return false;
					}

					if (Values.Length != that.Values.Length)
					{
						return false;
					}

					for (int i = 0; i < Values.Length; ++i)
					{
						var thisValue = Values[i];
						var thatValue = that.Values[i];
						if (thisValue != null || thatValue != null)
						{
							if (thisValue == null)
							{
								return false;
							}

							if (!thisValue.Equals(thatValue))
							{
								return false;
							}
						}
					}
				}

				return true;
			}
		}

		public class GroupItemList
		{
			public string Type { get; set; }

			public Group[] Values { get; set; }

			public static bool Equal(GroupItemList a, GroupItemList b)
			{
				if (a != null || b != null)
				{
					if (a == null)
					{
						return false;
					}

					if (!a.Equals(b))
					{
						return false;
					}
				}

				return true;
			}

			public bool Equals(GroupItemList that)
			{
				if (that == null)
				{
					return false;
				}

				if (Type != null || that.Type != null)
				{
					if (Type == null)
					{
						return false;
					}

					if (!Type.Equals(that.Type))
					{
						return false;
					}
				}

				if (Values != null || that.Values != null)
				{
					if (Values == null || that.Values == null)
					{
						return false;
					}

					if (Values.Length != that.Values.Length)
					{
						return false;
					}

					for (int i = 0; i < Values.Length; ++i)
					{
						var thisValue = Values[i];
						var thatValue = that.Values[i];
						if (thisValue != null || thatValue != null)
						{
							if (thisValue == null || thatValue == null)
							{
								return false;
							}

							if (!thisValue.Equals(thatValue))
							{
								return false;
							}
						}
					}
				}

				return true;
			}
		}
	}
}
