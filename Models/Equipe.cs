using System;
using System.Collections.Generic;
using System.IO;
using EPlayers_AspNetCore.Interfaces;

namespace EPlayers_AspNetCore.Models
{
    public class Equipe : EPlayers_base, IEquipe
    {
        public int IdEquipe { get; set; }
        public string NomeEquipe { get; set; }
        public string Imagem { get; set; }

        private const string Path = "Database/Equipe.csv";

        /// <summary>
        /// Método construtor para criar o arquivo .csv para armazenar os dados das equipes
        /// </summary>
        public Equipe()
        {
            CreateFolderAndFile(Path);
        }

        /// <summary>
        /// Método para alocar os dados das equipes dentro do arquivo, separando por parametros
        /// </summary>
        /// <param name="_equipe">Dados da equipe cadastrada</param>
        /// <returns>Linha com os parametros e os dados cadstrados</returns>
        private string PrepareLines(Equipe _equipe)
        {
            return $"{_equipe.IdEquipe};{_equipe.NomeEquipe};{_equipe.Imagem}";
        }

        /// <summary>
        /// Método para cadastrar uma equipe com seus dados pertinentes
        /// </summary>
        /// <param name="_equipe">Equipe cadastrada</param>
        public void Create(Equipe _equipe)
        {
            string[] _lineWithDatas = new string[]
            {
                PrepareLines(_equipe)
            };

            File.AppendAllLines(Path, _lineWithDatas);
        }

        /// <summary>
        /// Método para ler todos as linhas com dados no arquivo .csv
        /// </summary>
        /// <returns>Lista com todas as equipes encontrados no arquivo .csv</returns>
        public List<Equipe> ReadAll()
        {
            List<Equipe> _listEquipes = new List<Equipe>();

            string[] _linesInFile = File.ReadAllLines(Path);

            foreach (var _item in _linesInFile)
            {
                Equipe _equipe = new Equipe();

                string[] _lineWithData = _item.Split(';');

                _equipe.IdEquipe = Int32.Parse(_lineWithData[0]);
                _equipe.NomeEquipe = _lineWithData[1];
                _equipe.Imagem = _lineWithData[2];

                _listEquipes.Add(_equipe);
            }

            return _listEquipes;
        }

        /// <summary>
        /// Método para alterar os dados de uma equipe
        /// </summary>
        /// <param name="_equipeAlterada">A equipe a ser alterada com os novos dados inseridos</param>
        public void Update(Equipe _equipeAlterada)
        {
            List<string> _linesInFile = ReadAllLinesCsv(Path);

            _linesInFile.RemoveAll(upd => upd.Split(';')[0] == _equipeAlterada.IdEquipe.ToString());
            _linesInFile.Add(PrepareLines(_equipeAlterada));

            RewriteCsv(Path, _linesInFile);
        }

        /// <summary>
        /// Método para deletar uma equipe armazenada no arquivo
        /// </summary>
        /// <param name="_idEquipe">O identificador da equipe a ser deletada</param>
        public void Delete(int _idEquipe)
        {
            List<string> _linesInFile = ReadAllLinesCsv(Path);

            _linesInFile.RemoveAll(rmv => rmv.Split(';')[0] == _idEquipe.ToString());

            RewriteCsv(Path, _linesInFile);
        }
    }
}