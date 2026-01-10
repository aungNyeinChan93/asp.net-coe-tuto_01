namespace asp_tuto_01.Classes.Quotes
{
    public class Quote
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Author { get; set; } = null!;

        public Quote() { }

        public Quote(int id, string name, string author)
        {
            this.Id = id;
            this.Name = name;
            this.Author = author;
        }


    }

    static class QuoteRepository
    {
        private static List<Quote> _quotes = new List<Quote>()
        {
            new Quote(1,"Quote One","Chan"),
            new Quote(2,"Quote Two","Chan"),
            new Quote(3,"Quote Three","Susu"),
        };

        public static List<Quote>? GetAllQuotes() => _quotes.Count > 1 ? _quotes: null;

        public static void AddQuote(Quote? quote) => _quotes.Add(quote!); 

        public static bool UpdateQuote(int? id,Quote? quote)
        {
            if(id is null && quote is null) return false;

            var oldQuote = _quotes.FirstOrDefault(q => q.Id == id);

            if(oldQuote == null) return false;

            //oldQuote.Id = quote!.Id;
            oldQuote.Name = quote!.Name;
            oldQuote.Author = quote.Author;

            return true;
        }

        public static bool DeleteQuote(int? id)
        {
            if (id is null) return false;

            var oldQuote = _quotes.FirstOrDefault(q => q.Id == id);

            if (oldQuote == null) return false;

            _quotes.Remove(oldQuote);

            return true;
        }
    }
}
