using System.Collections.Generic;
using EPlayers_AspNetCore.Models;

namespace EPlayers_AspNetCore.Interfaces
{
    public interface IEquipe
    {
        void Create(Equipe _equipe);

        List<Equipe> ReadAll();

        void Update(Equipe _equipeAlterada);

        void Delete(int _idEquipe);
    }
}