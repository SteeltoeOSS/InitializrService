using System.IO;
using System.Text;
using System.Threading.Tasks;
using Steeltoe.Initializr.WebApi.Models.Project;

namespace Steeltoe.Initializr.WebApi.Services
{
	/// <summary>
	/// A project generator that uses the Mustache templating framework.
	/// </summary>
	public class DummyProjectGenerator : IProjectGenerator
	{
		public Task<Stream> GenerateProject(Specification specification)
		{
			var bytes = new UnicodeEncoding().GetBytes("DummyProject");
			var stream = new MemoryStream(bytes.Length);
			stream.Write(bytes, 0, bytes.Length);
			stream.Seek(0, SeekOrigin.Begin);
			var result = new TaskCompletionSource<Stream>();
			result.SetResult(stream);
			return result.Task;
		}
	}
}
