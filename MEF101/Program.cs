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
		#region Basics

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

		#endregion

		#region CreationPolicies

		#region Imports

		[Import(RequiredCreationPolicy = CreationPolicy.Shared)]
		SharedClass1 sharedClass1;

		[Import(RequiredCreationPolicy = CreationPolicy.Shared)]
		SharedClass1 sharedClass2;

		// Should throw error if uncommented
		//[Import(RequiredCreationPolicy = CreationPolicy.Shared)]
		[Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
		NonSharedClass nonSharedClass1;

		[Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
		NonSharedClass nonSharedClass2;

		[Import(RequiredCreationPolicy = CreationPolicy.Any)]
		AnyClass3 anyClass1;

		[Import(RequiredCreationPolicy = CreationPolicy.Any)]
		AnyClass3 anyClass2;

		#endregion

		#region Exports

		[Export, PartCreationPolicy(CreationPolicy.Shared)]
		public class SharedClass1
		{
			public string name { get; set; }
		}

		[Export, PartCreationPolicy(CreationPolicy.NonShared)]
		public class NonSharedClass
		{
			public string name { get; set; }
		}

		// Deafult is Shared for Any, unless something else is used. 
		[Export, PartCreationPolicy(CreationPolicy.Any)]
		public class AnyClass3
		{
			public string name { get; set; }
		}

		#endregion

		#endregion

		private void Compose()
		{
			string path = "C:\\Users\\daksh\\OneDrive\\Documents\\Dakshin\\Code workspaces\\MEF dotnet workspace\\Extensions2.0\\bin\\Debug";

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
			//Basics();
			
			CreationPolicies();
		}

		static void Basics()
		{
			Program p = new Program();
			p.Compose();

			// Only when nameList is accessed, it gets loaded
			foreach (Lazy<IName, INameMetadata> lazy in p.nameList)
			{
				Console.WriteLine(lazy.Metadata.metaDataName);
				Console.WriteLine(lazy.Value.name);
				Console.WriteLine();
			}

			Console.WriteLine(p.inheritedImport.nameInherited);

			Console.ReadLine();
		}

		static void CreationPolicies()
		{
			Program p = new Program();
			p.Compose();

			Console.WriteLine("Shared objects");
			p.sharedClass1.name = "I've changed it";
			Console.WriteLine(p.sharedClass1.name);
			Console.WriteLine(p.sharedClass2.name);
			Console.WriteLine("Shared objects after changing one of them");
			p.sharedClass1.name = "I've changed it again ";
			Console.WriteLine(p.sharedClass1.name);
			Console.WriteLine(p.sharedClass2.name);

			Console.WriteLine();

			Console.WriteLine("Non Shared objects");
			p.nonSharedClass1.name = "I've changed it non shared class";
			p.nonSharedClass2.name = "I've changed it non shared class";
			Console.WriteLine(p.nonSharedClass1.name);
			Console.WriteLine(p.nonSharedClass2.name);
			Console.WriteLine("NonShared objects after changing one of them");
			p.nonSharedClass1.name = "I've cahnged it in non shared class again";
			Console.WriteLine(p.nonSharedClass1.name);
			Console.WriteLine(p.nonSharedClass2.name);

			Console.WriteLine();

			Console.WriteLine("Any policy objects");
			p.anyClass1.name = "I've changed it";
			Console.WriteLine(p.anyClass1.name);
			Console.WriteLine(p.anyClass2.name);
			Console.WriteLine("Any policy objects after changing one of them");
			p.anyClass1.name = "I've changed it again ";
			Console.WriteLine(p.anyClass1.name);
			Console.WriteLine(p.anyClass2.name);

			Console.ReadLine();

		}
	}
}
