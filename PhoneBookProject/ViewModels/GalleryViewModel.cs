using PBP.DataAccess.Models;

namespace PBP.ViewModels;

public class GalleryViewModel
{
    public GalleryViewModel(Contact contact)
    {
        ContactId = contact.Id;
        ImageData = contact.Image!.Data;
    }

    public int? ContactId { get; set; }

    public byte[]? ImageData { get; set; }
}