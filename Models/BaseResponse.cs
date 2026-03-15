namespace NottsBAAPI.Models;

public class BaseResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = null!;
}
