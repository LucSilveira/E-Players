using System;
using System.IO;
using EPlayers_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPlayers_AspNetCore.Controllers
{
    public class NoticiaController : Controller
    {
        Noticia _noticiaModel = new Noticia();

        public IActionResult Index()
        {
            ViewBag.Noticia = _noticiaModel.ReadAll();

            return View();
        }

        public IActionResult Cadastrar(IFormCollection _formulario)
        {
            // Criando uma nova noticia via formulário
            Noticia _novaNoticia = new Noticia();

            // Passando os dados do formulário para a nova notícia
            _novaNoticia.IdNoticia = Int32.Parse(_formulario["IdNoticia"]);
            _novaNoticia.Titulo = _formulario["Titulo"];
            _novaNoticia.TextoNoticia = _formulario["TextoNoticia"];

            // Tratamento de foto
            var _filePicture = _formulario.Files[0]; //pegando as imagens passadas no formulário
            var _folderPicture = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Noticia"); //Pegando o diretório atual da aplicação e combinar com a foto informada

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

                _novaNoticia.Imagem = _filePicture.FileName;
            }
            else
            {
                _novaNoticia.Imagem = "padrao.png";
            }
            
            // Passando a notícia cadastrada pelo formulário para o arquivo .csv
            _noticiaModel.Create(_novaNoticia);

            ViewBag.Noticia = _noticiaModel.ReadAll();

            // Criando uma rota de acesso para as notícias
            return LocalRedirect("~/Noticia");
        }


        [Route("Noticia/{id}")]
        public IActionResult Excluir(int id)
        {
            _noticiaModel.Delete(id);

            return LocalRedirect("~/Noticia");
        }
    }
}