using System.Collections.Generic;
using Taijitan.Models.Domain;

namespace TaijitanTests.Data {
    public class DummyApplicationDbContext {
        private readonly IList<Gebruiker> _leden;

        public IEnumerable<Gebruiker> leden => _leden;
        public Gebruiker BruceLee { get; }

        public DummyApplicationDbContext() {
            Adres adresBruceLee = new Adres("Belgie", "1001", "Gent", "eenstraat", "12");
            BruceLee = new Gebruiker("BruceLee", "Lee", "Bruce", System.DateTime.Today, "047384950", "bruce.lee@hotmail.com",
                adresBruceLee);
            _leden = new List<Gebruiker>() {
                BruceLee
            };
        }
    }
}
