namespace ApplicationCore.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
            Succeeded = false; // Por defecto, no se ha tenido éxito
        }

        public Response(T data, string message = null, bool succeeded = true)
        {
            Data = data;
            Message = message;
            Succeeded = succeeded;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public bool Succeeded { get; set; } // Indica si la operación fue exitosa
        public string Message { get; set; } // Mensaje opcional de respuesta
        public T Data { get; set; }         // Datos devueltos
    }
}
