using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Steeltoe.Initializr.WebApi.Models.Project;
using Steeltoe.Initializr.WebApi.Services;

namespace Steeltoe.Initializr.WebApi.Controllers
{
	/// <summary>
	/// Project generation endpoint.
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class ProjectController : ControllerBase
	{
		private readonly IProjectGenerator _projectGenerator;

		/// <summary>
		/// Create a new ProjectGeneratorController.
		/// </summary>
		/// <param name="projectGenerator">project generator</param>
		public ProjectController(IProjectGenerator projectGenerator)
		{
			_projectGenerator = projectGenerator;
		}

		/// <summary>
		/// Implements <code>GET</code> by returning a project per provided configuration.
		/// Project is bundled as a byte array.
		/// </summary>
		/// <returns>Bundled generated project</returns>
		[HttpGet]
		public async Task<ActionResult> Get()
		{
			var config = new Configuration();
			var stream = await _projectGenerator.GenerateProject(config);
			var buf = new MemoryStream();
			await stream.CopyToAsync(buf);
			var bytes = buf.ToArray();
			stream.Close();
			buf.Close();
			return Ok(bytes);
		}
	}
}
