using System.Threading.Tasks;

namespace Motocycle.Application.Commons.Extensions
{
    public static class DocumentExtensions
    {
        // public static async Task SetStorageFilesAsync(this DocumentRequest documentRequest, IStorageService storage)
        // {
        //     if (documentRequest.SelfImage is not null)
        //         documentRequest.SelfImage = System.Convert.ToBase64String(await storage.GetFileAsync(documentRequest.SelfImage));

        //     if (documentRequest.BackImage is not null)
        //         documentRequest.BackImage = System.Convert.ToBase64String(await storage.GetFileAsync(documentRequest.BackImage));

        //     if (documentRequest.FrontImage is not null)
        //         documentRequest.FrontImage = System.Convert.ToBase64String(await storage.GetFileAsync(documentRequest.FrontImage));

        //     if(documentRequest.SocialContract is not null)
        //         documentRequest.SocialContract = System.Convert.ToBase64String(await storage.GetFileAsync(documentRequest.SocialContract));

        //     if (documentRequest.PoA is not null)
        //         documentRequest.PoA = System.Convert.ToBase64String(await storage.GetFileAsync(documentRequest.PoA));

        //     if(documentRequest.ResidenceProof is not null)
        //         documentRequest.ResidenceProof = System.Convert.ToBase64String(await storage.GetFileAsync(documentRequest.ResidenceProof));
        // }
    }
}
