namespace NETCoreAPIConectaBarrio.DTOs
{
    public class ResponseResult<T>
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
        public T Result { get; set; }

        public ResponseResult() { Success = false; }
        public ResponseResult(bool success, T result, string msg) {
            Success = success;
            Msg = msg;
            Result = result;
        }
    }
}
