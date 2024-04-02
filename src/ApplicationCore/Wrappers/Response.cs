namespace ApplicationCore.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Result = data;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }



        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Result { get; set; }
    }
}
