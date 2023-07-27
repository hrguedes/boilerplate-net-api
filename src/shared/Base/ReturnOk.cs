namespace Base;

public class ReturnOk<T>
{
    public int StatusCode { get; set; }
    public string[] Messages { get; set; }
    public bool Ok { get; set; }
    public T Data { get; set; }

    public ReturnOk(T data, string[] messages, bool ok = true, int code = 200)
    {
        this.Messages = messages;
        this.Ok = ok;
        this.Data = data;
        this.StatusCode = code;
    }
}