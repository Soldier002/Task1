using Domain.Persistence.TableStorage.Models.Dtos;

namespace Domain.Persistence.BlobStorage.Builders
{
    public interface IBlobNameBuilder
    {
        string Build(DateTime dateTime);

        string Build(Keys keys);
    }
}