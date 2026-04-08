public class CreateUserRequestModel
{
    public string? userName { get; set; }
    public string? password { get; set; }
}

public class GetUserResponseModel  
{ 
    public string? userId { get; set; }
    public string? userName { get; set; }
    public List<GetBookModel>? books { get; set; }
}
