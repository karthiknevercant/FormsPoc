using System;
using System.Collections.Generic;
using System.Text;

namespace FormsPoc
{
    public interface ISign
    {
        string Sign();
        bool IsFileExists(string path);
    }
}
