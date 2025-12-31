namespace SistemaReservas.Application.DTOs
{
    public class OperationResultDto<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static OperationResultDto<T> Fail(params string[] errors)
            => new() { Success = false, Errors = errors.ToList() };

        public static OperationResultDto<T> Ok(T data)
            => new() { Success = true, Data = data };
    }
}
