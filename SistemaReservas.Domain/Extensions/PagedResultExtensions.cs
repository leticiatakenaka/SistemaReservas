using SistemaReservas.Domain.Common; 

namespace SistemaReservas.Domain.Extensions 
{
    public static class PagedResultExtensions
    {
        public static PagedResult<TDestino> Map<TOrigem, TDestino>(
            this PagedResult<TOrigem> resultadoOriginal,
            Func<TOrigem, TDestino> conversor)
        {
            var itensConvertidos = resultadoOriginal.Items.Select(conversor).ToList();

            return new PagedResult<TDestino>(itensConvertidos)
            {
                TotalCount = resultadoOriginal.TotalCount,
                Page = resultadoOriginal.Page,
                PageSize = resultadoOriginal.PageSize
            };
        }
    }
}