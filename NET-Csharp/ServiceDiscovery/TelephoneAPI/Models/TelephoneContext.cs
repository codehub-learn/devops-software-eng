	using Microsoft.EntityFrameworkCore;
	namespace TelephoneAPI.Models;
	public class TelephoneContext: DbContext {
	  public TelephoneContext(DbContextOptions<TelephoneContext> options): base(options){}
	  public DbSet<Telephone> Telephones {get; set;} = null!;
	}