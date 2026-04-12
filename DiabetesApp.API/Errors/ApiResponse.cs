
namespace Talabat.APIs.Errors
{
	public class ApiResponse
	{
        public int StatusCode { get; set; }
		public string? Message { get; set; }
        public ApiResponse(int code,string? message=null)
        {
            StatusCode = code;
            Message = message ?? GetDefulatMessageFromCode(code);
        }

		private string? GetDefulatMessageFromCode(int code)
		{
			return StatusCode switch
			{
				400 => "BadRequest",
				401=> "Unutherized",
				404=> "Resourse Not Found",
				500=>"Server Error",
				_=>null,
			};
		}
	}
}
