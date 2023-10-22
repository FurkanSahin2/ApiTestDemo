namespace ApiTestDemo.Api
{
    public class ResponseMessage
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class BaseResponse<T>
    {
        public T response { get; set; }
        public List<ResponseMessage> messages { get; set; }
    }


}
