using System.Collections.Generic;

namespace MinMaNet.Domain.Models
{
    public class Project
	{
		public Project(string title, List<Class> classes = null)
		{
			this.Title = title;
			this.Classes = classes ?? new List<Class>();
		}

		public string Title { get; set; }
		public List<Class> Classes { get; set; }
	}

	public class Class
	{
		public Class(string title, List<Property> properties)
		{
			this.Title = title;
			this.Properties = properties ?? new List<Property>();
		}

		public string Title { get; set; }
		public List<Property> Properties { get; set; }
	}

	public class Property
	{
		public Property(string title, string type)
		{
			this.Title = title;
			this.Type = type;
		}

		public string Title { get; set; }
		public string Type { get; set; }
	}
}
