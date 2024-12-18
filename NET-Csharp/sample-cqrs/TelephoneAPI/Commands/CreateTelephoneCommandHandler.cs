using TelephoneAPI.Models;
using MediatR;

namespace TelephoneAPI.Commands;

public class CreateTelephoneCommandHandler: IRequestHandler<CreateTelephoneCommand> {
    private readonly TelephoneContext _context;

    public CreateTelephoneCommandHandler(TelephoneContext context){
        _context = context;
    }

    public async Task Handle(CreateTelephoneCommand request, CancellationToken cancellationToken){
        var telephone = new Telephone {
            Name = request.Name,
            PhoneNumber = request.PhoneNumber
        };
        _context.Telephones.Add(telephone);
        await _context.SaveChangesAsync(cancellationToken);
    }
}