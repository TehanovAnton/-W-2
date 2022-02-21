﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicsParService
{
    interface ISqlCrud
    {
        bool Delete(int id);

        void GetAll();

        int LastId();
    }
}
