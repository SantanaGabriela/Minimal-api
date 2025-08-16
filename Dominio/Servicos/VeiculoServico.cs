using System.Linq;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Infraestrutura.Db;
using MinimalApi.Dominio.Entidades;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Dominio.Servicos
{
    public class VeiculoServico : IVeiculoServico
    {
        private readonly DbContexto _contexto;

        public VeiculoServico(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public void Apagar(Veiculos veiculo)
        {
            _contexto.Veiculos.Remove(veiculo);
            _contexto.SaveChanges();
        }

        public void Atualizar(Veiculos veiculo)
        {
            _contexto.Veiculos.Update(veiculo);
            _contexto.SaveChanges();
        }

        public void Incluir(Veiculos veiculo)
        {
            _contexto.Veiculos.Add(veiculo);
            _contexto.SaveChanges();
        }

        public Veiculos? BuscarPorId(int id)
        {
            return _contexto.Veiculos.FirstOrDefault(v => v.Id == id);
        }

        public List<Veiculos> Todos(int pagina, string? nome = null, string? marca = null)
        {
            var query = _contexto.Veiculos.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome.ToLower()}%"));
            }

            if (!string.IsNullOrEmpty(marca))
            {
                query = query.Where(v => EF.Functions.Like(v.Marca.ToLower(), $"%{marca.ToLower()}%"));
            }

            int itensPorPagina = 10;

            query = query.Skip((pagina - 1) * itensPorPagina)
                         .Take(itensPorPagina);

            return query.ToList();
        }
    }
}
