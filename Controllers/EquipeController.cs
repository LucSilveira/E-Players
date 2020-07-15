using System;
using System.IO;
using EPlayers_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPlayers_AspNetCore.Controllers
{
    public class EquipeController : Controller
    {
        Equipe _equipeModel = new Equipe();

        public IActionResult Index()
        {
            ViewBag.Equipe = _equipeModel.ReadAll();

            return View();
        }


        public IActionResult Cadastrar(IFormCollection _formulario)
        {
            // Criando uma nova equipe via formulário
            Equipe _novaEquipe = new Equipe();

            // Passando os dados do formulário para a nova equipe
            _novaEquipe.IdEquipe = Int32.Parse(_formulario["IdEquipe"]);
            _novaEquipe.NomeEquipe = _formulario["NomeEquipe"];
        
            // Tratamento de foto
            var _filePicture = _formulario.Files[0]; //pegando as imagens passadas no formulário
            var _folderPicture = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipe"); //Pegando o diretório atual da aplicação e combinar com a foto informada

            if(_filePicture != null)
            {
                // Verificando se o diretório existe
                if(!Directory.Exists(_folderPicture))
                {
                    Directory.CreateDirectory(_folderPicture);
                }

                // Combinando o diretório com a foto inserida
                var _pathPicture = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", _folderPicture, _filePicture.FileName);
            
                // Copiando o arquivo presente no computador para o nosso diretório da aplicação
                using(var _stream = new FileStream(_pathPicture, FileMode.Create))
                {
                    _filePicture.CopyTo(_stream);
                }

                _novaEquipe.Imagem = _filePicture.FileName;
            }
            else
            {
                _novaEquipe.Imagem = "padrao.png";
            }

            // Passando a equipe cadastrada pelo formulário para o arquivo .csv
            _equipeModel.Create(_novaEquipe);

            ViewBag.Equipe = _equipeModel.ReadAll();

            // Criando uma rota de acesso para as equipes
            return LocalRedirect("~/Equipe");
        }


        [Route("Equipe/{id}")]
        public IActionResult Excluir(int id)
        {
            _equipeModel.Delete(id);

            return LocalRedirect("~/Equipe");
        }
    }
}