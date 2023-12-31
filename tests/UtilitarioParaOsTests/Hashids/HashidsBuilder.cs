﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitarioParaOsTests.Hashids;
public class HashidsBuilder
{
    private static HashidsBuilder _instance;
    private readonly HashidsNet.Hashids _encripter;

    private HashidsBuilder()
    {
        if (_encripter == null)
            _encripter = new HashidsNet.Hashids("qj328TtMDy", 3);
    }

    public static HashidsBuilder Instance()
    {
        _instance = new HashidsBuilder();
        return _instance;
    }

    public HashidsNet.Hashids Build()
    {
        return _encripter;
    }
}
