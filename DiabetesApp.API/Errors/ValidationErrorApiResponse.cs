namespace Talabat.APIs.Errors
{
	public class ValidationErrorApiResponse:ApiResponse
	{
        public IEnumerable<string> errors { get; set; }
        public ValidationErrorApiResponse():base(400)
        {
            errors = new List<string>();
            
        }
    }
}
