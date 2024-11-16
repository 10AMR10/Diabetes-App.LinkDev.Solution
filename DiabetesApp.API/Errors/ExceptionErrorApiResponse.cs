namespace Talabat.APIs.Errors
{
	public class ExceptionErrorApiResponse:ApiResponse
	{
        public string? Detials { get; set; }
        public ExceptionErrorApiResponse(int statusCode,string? message=null,string? detailas=null):base(statusCode,message)
        {
			Detials = detailas;

		}
    }
}
