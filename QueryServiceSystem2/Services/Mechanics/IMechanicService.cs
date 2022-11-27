using QueryServiceSystem2.Services.Mechanics.Models;
using QueryServiceSystem2.Services.Queries.Models;

namespace QueryServiceSystem2.Services.Mechanics
{
    public interface IMechanicService
    {
        public bool IsMechanic(string userId);

        public int IdByUser(string userId);
        bool Edit(
            int id,
            string name,
            string code,
            string phoneNumebr);
        MechanicDetailsServiceModel Details(int mechanicId);
        CarQueryServiceModel AllMechanics(
            string name = "",
                string code = "",
                string phoneNumebr = "");
    }
}
