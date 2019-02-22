using System.Collections.Generic;
using Taijitan.Models.Domain;

namespace TaijitanTests.Data {
    public class DummyApplicationDbContext {
        private readonly IList<Lid> _leden;

        public IEnumerable<Lid> leden => _leden;
        public Lid BruceLee { get; }

        public DummyApplicationDbContext() {
            Adres adresBruceLee = new Adres("Belgie", "1001", "Gent", "eenstraat", "12");
            BruceLee = new Lid("BruceLee", "Lee", "Bruce", System.DateTime.Today, "047384950", "bruce.lee@hotmail.com",
                adresBruceLee);
            _leden = new List<Lid>() {
                BruceLee
            };
        }
    }
}
