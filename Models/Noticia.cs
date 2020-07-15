using System;
using System.Collections.Generic;
using System.IO;
using EPlayers_AspNetCore.Interfaces;

namespace EPlayers_AspNetCore.Models
{
    public class Noticia : EPlayers_base, INoticia
    {
        public int IdNoticia { get; set; }

        public string Titulo { get; set; }

        public string TextoNoticia { get; set; }

        public string Imagem { get; set; }

        private const string Path = "Database/Noticia.csv";


        /// <summary>
        /// Método construtor para criar o arquivo de armazenamento .csv para salvar os dados das notícias
        /// </summary>
        public Noticia()
        {
            CreateFolderAndFile(Path);
        }

        /// <summary>
        /// Método para alocar os dados das notícias dentro do arquivo, separando por parâmetros
        /// </summary>
        /// <param name="_noticias">Dados da notícia cadastrada</param>
        /// <returns>Linha com os parâmetros e os dados cadstrados</returns>
        private string PrepareLines(Noticia _noticias)
        {
            return $"{_noticias.IdNoticia};{_noticias.Titulo};{_noticias.TextoNoticia};{_noticias.Imagem}";
        }

        /// <summary>
        /// Método para cadastrar uma notícia com seus dados pertinentes
        /// </summary>
        /// <param name="_noticias">Notícia cadastrada</param>
        public void Create(Noticia _noticias)
        {
            string[] _lineWithDatas = new string[]
            {
                PrepareLines(_noticias)
            };

            File.AppendAllLines(Path, _lineWithDatas);
        }

        /// <summary>
        /// Método para ler todos as linhas com dados no arquivo .csv
        /// </summary>
        /// <returns>Lista com todas as notícias encontradas no arquivo .csv</returns>
        public List<Noticia> ReadAll()
        {
            List<Noticia> _listNoticias = new List<Noticia>();

            string[] _lineInFile = File.ReadAllLines(Path);

            foreach (var _item in _lineInFile)
            {
                Noticia _noticia = new Noticia();

                string[] _lineWithData = _item.Split(';');

                _noticia.IdNoticia = Int32.Parse(_lineWithData[0]);
                _noticia.Titulo = _lineWithData[1];
                _noticia.TextoNoticia = _lineWithData[2];
                _noticia.Imagem = _lineWithData[3];

                _listNoticias.Add(_noticia);
            }

            return _listNoticias;
        }

        
        /// <summary>
        /// Método para alterar os dados de uma notícia
        /// </summary>
        /// <param name="_noticaAlterada">A notícia a ser alterada com os novos dados inseridos</param>
        public void Update(Noticia _noticaAlterada)
        {
            List<string> _lineInFile = ReadAllLinesCsv(Path);

            _lineInFile.RemoveAll(upd => upd.Split(';')[0] == _noticaAlterada.IdNoticia.ToString());
            _lineInFile.Add(PrepareLines(_noticaAlterada));

            RewriteCsv(Path, _lineInFile);
        }


        /// <summary>
        /// Método para deletar uma notícia armazenada no arquivo .csv
        /// </summary>
        /// <param name="_idNoticia">o Identificador da notícia a ser deletada</param>
        public void Delete(int _idNoticia)
        {
            List<string> _lineInFile = ReadAllLinesCsv(Path);

            _lineInFile.RemoveAll(rmv => rmv.Split(';')[0] == _idNoticia.ToString());

            RewriteCsv(Path, _lineInFile);
        }
    }
}