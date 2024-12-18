using TelephoneAPI.Models;
using MediatR;

namespace TelephoneAPI.Queries;

public class GetTelephonesQuery: IRequest<Telephone> {
    public GetTelephonesQuery(){
        
    }
}