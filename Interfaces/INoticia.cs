using System.Collections.Generic;
using EPlayers_AspNetCore.Models;

namespace EPlayers_AspNetCore.Interfaces
{
    public interface INoticia
    {
        void Create(Noticia _noticias);

        List<Noticia> ReadAll();

        void Update(Noticia _noticiaAlterada);

        void Delete(int _idNoticia);
    }
} 