using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace WebTruyen.UI.Admin.Service.ImageService
{
    public interface IImageService
    {
        public bool ImageIsValid(string contentType);
        public Task<byte[]> ImageToByte(IBrowserFile img);
        public Task<string> ImageToString(IBrowserFile img);
        public IAsyncEnumerable<byte[]> ImagesToByte(IReadOnlyList<IBrowserFile> imgs);

        public IAsyncEnumerable<((byte[] data, string fileName) image, string stringValue)> ImagesToString(
            IReadOnlyList<IBrowserFile> imgs);

    }
}
