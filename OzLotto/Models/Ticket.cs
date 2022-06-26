using System;
namespace OzLotto.Models
{
	public class Ticket
	{
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public string? ProductName { get; set; }

        public long DrawNumber { get; set; }

        public int Selection1 { get; set; }

        public int Selection2 { get; set; }

        public int Selection3 { get; set; }

        public int Selection4 { get; set; }

        public int Selection5 { get; set; }

        public int Selection6 { get; set; }

        public int Selection7 { get; set; }
    }
}

