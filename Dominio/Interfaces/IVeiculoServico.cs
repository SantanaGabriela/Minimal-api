using MinimalApi.Dominio.Entidades;
using System.Collections.Generic;

namespace MinimalApi.Dominio.Interfaces
{
    public interface IVeiculoServico
    {
        List<Veiculos> Todos(int pagina, string? nome = null, string? marca = null);
        Veiculos? BuscarPorId(int id);
        void Incluir(Veiculos veiculo);
        void Atualizar(Veiculos veiculo);
        void Apagar(Veiculos veiculo);
    }
}

