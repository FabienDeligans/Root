﻿using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.MES;

namespace _LogicLayer.Logics.MES;

public class OrdreFabricationLogic : BaseApiLogic<OrdreFabrication>
{
    public OrdreFabricationLogic(IApiServiceDatabase serviceDatabase)
        : base(serviceDatabase)
    {
    }
}