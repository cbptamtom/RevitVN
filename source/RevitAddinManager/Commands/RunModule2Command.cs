﻿using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;

namespace RevitAddinManager.Commands;
/// <summary>
///     External command entry point invoked from the Revit interface
/// </summary>
[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public class RunModule2Command : ExternalCommand
{
    public override void Execute()
    {
        Host.GetService<Module2.Commands.ShowWindowComponent>().Execute();
    }
}