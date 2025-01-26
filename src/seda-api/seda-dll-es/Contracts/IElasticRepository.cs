namespace seda_dll_es.Contracts;

public interface IElasticRepository<T>
{
    Task<bool> IndexDocumentAsync(T document);
    Task<IEnumerable<T>> QueryDocuments();  
}