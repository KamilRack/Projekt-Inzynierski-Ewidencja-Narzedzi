namespace Narzedzia.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int NrKontrolny { get; set; }
        public string Wydzial { get; set; }
        public string Stanowisko { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public int Liczba_narzedzi { get; set; }
        public string Imie_Nazwisko
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
