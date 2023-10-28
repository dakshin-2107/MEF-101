using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MEF101
{
	#region Interfaces

	public interface INameMetadata
	{
		string metaDataName { get; }
	}

	public interface IName
	{
		string name { get; }
	}

	[InheritedExport]
	public interface INameInherited
	{
		string nameInherited { get; }
	}

	#endregion

	internal class Program
	{

		private string path = "C:\\Users\\daksh\\OneDrive\\Documents\\Dakshin\\Code workspaces\\dotnet workspace\\Extensions2.0\\bin\\Debug";

		#region Imports

		[Import]
		string message;

		[Import]
		int val2;
				
		[Import]
		public Func<int, int, int> func;

		// Any class that implements IName and has "metaDataName" as a metadata property
		[ImportMany(typeof(IName))]
		List<Lazy<IName, INameMetadata>> nameList;

		[Import]
		INameInherited inheritedImport;

		#endregion

		#region Exports

		[Export(typeof(IName))]
		[ExportMetadata("metaDataName", "myClass")]
		public class myClass : IName
		{
			public string name => "This is myClass";
		}

		#endregion

		private void Compose()
		{
			AssemblyCatalog assemblyCatalog = new AssemblyCatalog(typeof(Program).Assembly);
			DirectoryCatalog directoryCatalog = new DirectoryCatalog(path);
			AggregateCatalog aggregateCatalog = new AggregateCatalog();

			aggregateCatalog.Catalogs.Add(assemblyCatalog);
			aggregateCatalog.Catalogs.Add(directoryCatalog);

			CompositionContainer container = new CompositionContainer(aggregateCatalog);
			container.ComposeParts(this);
		}

		static void Main(string[] args)
		{
			Program p = new Program();
			p.Compose();

			// Only when nameList is accessed, it gets loaded
			foreach(Lazy<IName, INameMetadata> lazy in p.nameList)
			{
				Console.WriteLine(lazy.Metadata.metaDataName);
				Console.WriteLine(lazy.Value.name);
				Console.WriteLine();
			}

			Console.WriteLine(p.inheritedImport.nameInherited);

			Console.ReadLine();
		}
	}
}
