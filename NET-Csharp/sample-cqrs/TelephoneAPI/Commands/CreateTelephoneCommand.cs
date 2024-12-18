using MediatR;
namespace TelephoneAPI.Commands;

public class CreateTelephoneCommand: IRequest {
    public string Name {get; set;} = string.Empty;
    public string PhoneNumber{get; set;} = string.Empty;


}