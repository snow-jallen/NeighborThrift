using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NeighborThrift4.Services
{
	public interface IPhotoPickerService
	{
		Task<Stream> GetImageStreamAsync();
	}
}
