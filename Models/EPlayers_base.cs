using System.Collections.Generic;
using System.IO;

namespace EPlayers_AspNetCore.Models
{
    public class EPlayers_base
    {
        /// <summary>
        /// Método para criação do arquivo .csv para armazenar os dados
        /// </summary>
        /// <param name="_path">Caminho para o qual o arquivo ficará armazenado</param>
        public void CreateFolderAndFile(string _path)
        {
            string _folder = _path.Split('/')[0];

            if(!Directory.Exists(_folder))
            {
                Directory.CreateDirectory(_folder);
            }

            if(!File.Exists(_path))
            {
                File.Create(_path).Close();
            }
        }

        /// <summary>
        /// Método para ler todos os dados salvos dentro do arquivo de armazenamento .csv
        /// </summary>
        /// <param name="_path">Caminho onde se encontra o arquivo</param>
        /// <returns>Uma lista com os dados encontrados no arquivo</returns>
        public List<string> ReadAllLinesCsv(string _path)
        {
            List<string> _lines = new List<string>();

            using(StreamReader _file = new StreamReader(_path))
            {
                string _lineWithDatas;

                while((_lineWithDatas = _file.ReadLine()) != null)
                {
                    _lines.Add(_lineWithDatas);
                }
            }

            return _lines;
        }

        /// <summary>
        /// Método para reescrever o arquivo .csv uma vez que há arguma alteração no arquivo
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_lines"></param>
        public void RewriteCsv(string _path, List<string> _lines)
        {
            using(StreamWriter _output = new StreamWriter(_path))
            {
                foreach (var _item in _lines)
                {
                    _output.Write(_item + "\n");
                }
            }
        }
    }
}