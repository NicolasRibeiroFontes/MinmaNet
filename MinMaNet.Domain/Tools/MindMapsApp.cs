using MinMaNet.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MinMaNet.Domain.Tools
{
    public class MindMapsApp
	{
		public MindMapsApp(string title, MindMupRoot mindmap)
		{
			Title = title;
			MindMap = mindmap;
		}

		[Required]
		public string Title { get; set; }
		[Required]
		public MindMupRoot MindMap { get; set; }

		public Project Convert()
		{
			Project project = new(title: MindMap?.Root?.Text?.Caption, classes: new List<Class>());

			//Classes and Properties
			MindMap.Root.Children.ForEach(classes =>
			{
				List<Property> properties = new();
				classes.Children.ForEach(property =>
				{
					properties.Add(new Property(
						property?.Text?.Caption,
						property?.Children?.FirstOrDefault()?.Text?.Caption)
						);
				});

				project.Classes.Add(new Class(classes?.Text?.Caption, properties));
			});

			return project;
		}
	}

	public class MindMupRoot
	{
		[Required]
		public MindMupProperty Root { get; set; }
	}

	public class MindMupProperty
	{
		public Guid ID { get; set; }
		public List<MindMupProperty> Children { get; set; }
		[Required]
		public MindMupTitleProperty Text { get; set; }
	}

	public class MindMupTitleProperty
	{
		[Required]
		public string Caption { get; set; }
	}
}
