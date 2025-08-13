using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    /// <summary>
    /// Controlador para gerenciamento de tarefas
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        /// <summary>
        /// Construtor do controlador de tarefas
        /// </summary>
        /// <param name="context">Contexto do banco de dados</param>
        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém uma tarefa pelo seu Id
        /// </summary>
        /// <param name="id">Id da tarefa</param>
        /// <returns>Dados da tarefa</returns>
        /// <response code="200">Retorna a tarefa encontrada</response>
        /// <response code="404">Se a tarefa não for encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ObterPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            
            if (tarefa == null)
                return NotFound();
                
            return Ok(tarefa);
        }

        /// <summary>
        /// Obtém todas as tarefas cadastradas
        /// </summary>
        /// <returns>Lista de tarefas</returns>
        /// <response code="200">Retorna a lista de todas as tarefas</response>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObterTodos()
        {
            var tarefas = _context.Tarefas.ToList();
            return Ok(tarefas);
        }

        /// <summary>
        /// Obtém tarefas que contenham o título especificado
        /// </summary>
        /// <param name="titulo">Texto a ser buscado no título</param>
        /// <returns>Lista de tarefas filtradas</returns>
        /// <response code="200">Retorna as tarefas encontradas</response>
        [HttpGet("ObterPorTitulo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            return Ok(tarefas);
        }

        /// <summary>
        /// Obtém tarefas pela data
        /// </summary>
        /// <param name="data">Data das tarefas</param>
        /// <returns>Lista de tarefas na data especificada</returns>
        /// <response code="200">Retorna as tarefas encontradas</response>
        [HttpGet("ObterPorData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        /// <summary>
        /// Obtém tarefas pelo status
        /// </summary>
        /// <param name="status">Status das tarefas (Pendente ou Finalizado)</param>
        /// <returns>Lista de tarefas com o status especificado</returns>
        /// <response code="200">Retorna as tarefas encontradas</response>
        [HttpGet("ObterPorStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa);
        }

        /// <summary>
        /// Cria uma nova tarefa
        /// </summary>
        /// <param name="tarefa">Dados da tarefa a ser criada</param>
        /// <returns>Tarefa recém-criada</returns>
        /// <response code="201">Retorna a tarefa recém-criada</response>
        /// <response code="400">Se os dados da tarefa estiverem inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        /// <summary>
        /// Atualiza uma tarefa existente
        /// </summary>
        /// <param name="id">Id da tarefa a ser atualizada</param>
        /// <param name="tarefa">Novos dados da tarefa</param>
        /// <returns>Tarefa atualizada</returns>
        /// <response code="200">Retorna a tarefa atualizada</response>
        /// <response code="400">Se os dados da tarefa estiverem inválidos</response>
        /// <response code="404">Se a tarefa não for encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;
            
            _context.Update(tarefaBanco);
            _context.SaveChanges();
            return Ok(tarefaBanco);
        }

        /// <summary>
        /// Remove uma tarefa
        /// </summary>
        /// <param name="id">Id da tarefa a ser removida</param>
        /// <returns>Nenhum conteúdo</returns>
        /// <response code="204">Se a tarefa foi removida com sucesso</response>
        /// <response code="404">Se a tarefa não for encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
