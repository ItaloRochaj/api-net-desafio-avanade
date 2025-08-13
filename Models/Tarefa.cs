using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrilhaApiDesafio.Models
{
    /// <summary>
    /// Representa uma tarefa no sistema
    /// </summary>
    public class Tarefa
    {
        /// <summary>
        /// Identificador único da tarefa
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Título da tarefa
        /// </summary>
        public string Titulo { get; set; }
        
        /// <summary>
        /// Descrição detalhada da tarefa
        /// </summary>
        public string Descricao { get; set; }
        
        /// <summary>
        /// Data da tarefa
        /// </summary>
        public DateTime Data { get; set; }
        
        /// <summary>
        /// Status atual da tarefa
        /// </summary>
        public EnumStatusTarefa Status { get; set; }
    }
}