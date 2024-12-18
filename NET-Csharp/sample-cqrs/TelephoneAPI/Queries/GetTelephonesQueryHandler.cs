using TelephoneAPI.Models;
using MediatR;

namespace TelephoneAPI.Queries;

public class GetTelephonesQueryHandler: IRequestHandler<GetTelephonesQuery, Telephone>
{
    private readonly TelephoneContext _context;

    public GetTelephonesQueryHandler(TelephoneContext context){
        _context = context;
    }

    public async Task<Telephone> Handle(GetTelephonesQuery request, CancellationToken cancellationToken){
        return await _context.Telephones.FindAsync(1) ?? new Telephone{};
    }
}