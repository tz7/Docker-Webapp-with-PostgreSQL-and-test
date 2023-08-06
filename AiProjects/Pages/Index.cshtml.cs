using AiProjects.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AiProjects.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public string ConnectionStatus { get; private set; }
        public List<string> Tables { get; set; } = new List<string>();

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            if (_context.Database.CanConnect())
            {
                ConnectionStatus = "Connected to the database!";
                try
                {
                    var connection = _context.Database.GetDbConnection();
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT id, test FROM public.test";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var id = reader.GetInt32(0); // Assuming id is an integer
                                var test = reader.GetString(1); // Assuming test is a string
                                Tables.Add($"ID: {id}, Test: {test}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ConnectionStatus += " But an exception occurred while fetching tables: " + ex.Message;
                }
            }
            else
            {
                ConnectionStatus = "Failed to connect to the database!";
            }
        }
    }
}