using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model;

namespace DataAccessLayer.Interface
{
    public interface ISell
    {
        int AlbumAvgQuantity(int album_id);
        int AlbumAvgSellQuantity(int album_id);
    }
}
