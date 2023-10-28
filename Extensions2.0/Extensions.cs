using System.ComponentModel.Composition;
using MEF101;

namespace Extensions2._0
{
	public class Extensions
	{
		[Export]
		public int val => 100;

		[Export]
		public int Add(int a, int b) => a + b;

		[Export]
		public string message = "Hello from the other side";

		[Export(typeof(IName))]
		[ExportMetadata("metaDataName", "A")]
		public class A : IName
		{
			public string name => "Class A";
		}

		[Export(typeof(IName))]
		[ExportMetadata("name", "B")]
		public class B : IName
		{
			public string name => "Class B";
		}

		[Export(typeof(IName))]
		[ExportMetadata("metaDataName", "C")]
		public class C : IName
		{
			public string name => "Class C";
		}

		public class D : INameInherited
		{
			public string nameInherited => "Look ma no export attribute.";
		}
	}
}
